using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mistakes.Journal.Api.Api.Mistakes.Mappers;
using Mistakes.Journal.Api.Api.Mistakes.WebModels;
using Mistakes.Journal.Api.Api.Shared;
using Mistakes.Journal.Api.Api.Shared.RequestsParameters;
using Mistakes.Journal.Api.Api.Shared.Validators;
using Mistakes.Journal.Api.Logic.Identity.Services;
using Mistakes.Journal.Api.Logic.Mistakes.Extensions;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/mistakes")]
    public class MistakesController : ControllerBase
    {
        #region Fields & Constructors

        private readonly MistakesJournalContext _dataContext;
        private readonly IUserProvider _userProvider;

        public MistakesController(
            MistakesJournalContext dataContext,
            IUserProvider userProvider
        )
        {
            _dataContext = dataContext;
            _userProvider = userProvider;
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<ActionResult<MistakeWebModel>> AddMistake(NewMistakeWebModel newMistake)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var count = await _dataContext.Set<Mistake>().CountAsync();
            if (count >= Constants.MistakesLimit)
                return BadRequest(ErrorMessageType.TooMuchItemsExist);

            var mistake = newMistake.ToEntity(_userProvider.GetId());
            if (mistake.Tips.Count >= Constants.TipsLimit)
                return BadRequest(ErrorMessageType.TooMuchTipsExist);

            if (newMistake.Tips != null)
                mistake.Tips.AddRange(newMistake.Tips.Where(t => t.IsPresent()).Select(t => new Tip(mistake.UserId, t)));

            foreach (var labelId in newMistake.Labels.EmptyIfNull())
            {
                var label = await _dataContext.Set<Label>().SingleOrDefaultAsync(l => l.Id == labelId);

                if (label is null)
                    return BadRequest(ErrorMessageType.LabelDoesNotExist);

                var mistakeLabel = new MistakeLabel(label, mistake);
                _dataContext.Set<MistakeLabel>().Add(mistakeLabel);
            }

            await _dataContext.Set<Mistake>().AddAsync(mistake);
            await _dataContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMistake), new { mistakeId = mistake.Id }, mistake.ToWebModel());
        }

        [HttpPost("{mistakeId:guid}/clone")]
        public async Task<ActionResult<MistakeWebModel>> CloneMistake(Guid mistakeId)
        {
            var mistake = await _dataContext.Set<Mistake>()
                .Include(m => m.Tips)
                .Include(m => m.MistakeLabels)
                    .ThenInclude(m => m.Label)
                .Include(m => m.Repetitions)
                .SingleOrDefaultAsync(m => m.Id == mistakeId);

            if (mistake is null)
                return NotFound();

            var newMistake = mistake.ToNewMistakeWebModel();

            return await AddMistake(newMistake);
        }

        [HttpPost("{mistakeId:guid}/add-repetition")]
        public async Task<ActionResult<MistakeWebModel>> AddRepetitionResetCounter(Guid mistakeId, [FromBody, MJIncorrectRepetitionDate(1)] DateTime? occurredAt)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mistake = await _dataContext.Set<Mistake>()
                .Include(m => m.Repetitions)
                .Include(m => m.MistakeLabels)
                    .ThenInclude(m => m.Label)
                .Include(m => m.Tips)
                .SingleOrDefaultAsync(m => m.Id == mistakeId);

            if (mistake is null)
                return NotFound();

            if (mistake.IsSolved)
                return BadRequest(ErrorMessageType.MistakeIsAlreadySolved);

            if (mistake.Repetitions.Count >= Constants.RepetitionsLimit)
                return BadRequest(ErrorMessageType.TooMuchItemsExist);

            mistake.Repetitions.Add(new Repetition(occurredAt ?? DateTime.UtcNow));

            await _dataContext.SaveChangesAsync();

            return Ok(mistake.ToWebModel());
        }

        [HttpPost("search")]
        public async Task<ActionResult<MistakeWebModel>> SearchMistakes(
            MistakeSearchWebModel searchModel,
            [FromQuery] MistakesListFilters mistakesListFilters,
            [FromQuery] MistakesSortingParameters sortingParameters)
        {
            var mistakes = await _dataContext.Set<Mistake>()
                .Include(m => m.Tips)
                .Include(m => m.Repetitions)
                .Include(m => m.MistakeLabels)
                    .ThenInclude(m => m.Label)
                .WhereIf(searchModel.Name.IsPresent(), m => m.Name.ToLower().Contains(searchModel.Name.ToLower()))
                .Where(m => m.IsSolved && mistakesListFilters.IncludeSolved || !m.IsSolved && mistakesListFilters.IncludeUnsolved)
                .WhereIf(searchModel.Goal.IsPresent(), m => m.Goal != null && m.Goal.ToLower().Contains(searchModel.Goal.ToLower()))
                .WhereIf(!searchModel.Priorities.IsNullOrEmpty(), m => searchModel.Priorities.Contains(m.Priority))
                .WhereIf(!searchModel.Labels.IsNullOrEmpty(), m => m.MistakeLabels.Any(ml => searchModel.Labels.Contains(ml.LabelId)))
                .AsQueryable()
                .OrderByField(sortingParameters)
                .Skip(searchModel.StartAt)
                .Take(searchModel.MaxResults)
                .ToListAsync();

            return Ok(mistakes.Select(m => m.ToWebModel()));
        }

        [HttpPost("{mistakeId:guid}/mark-as-solved")]
        public async Task<ActionResult<MistakeWebModel>> MarkAsSolved(Guid mistakeId)
        {
            var mistake = await _dataContext.Set<Mistake>()
                .FirstOrDefaultAsync(m => m.Id == mistakeId);

            if (mistake is null)
                return NotFound();

            if (mistake.IsSolved)
                return BadRequest(ErrorMessageType.MistakeIsAlreadySolved);

            if (!mistake.CanBeSolved())
                return BadRequest(ErrorMessageType.CannotBeSolved);

            mistake.IsSolved = true;
            await _dataContext.SaveChangesAsync();

            return Ok(mistake.ToWebModel());
        }

        #endregion

        #region GET

        [HttpGet]
        public async Task<ActionResult<MistakeWebModel>> GetMistakes(
            [FromQuery] MistakesListFilters mistakesListFilters,
            [FromQuery] MistakesSortingParameters sortingParameters,
            [FromQuery] PagingParameters pagingParameters)
        {
            var mistakes = _dataContext.Set<Mistake>()
                .Include(m => m.Tips)
                .Include(m => m.MistakeLabels)
                    .ThenInclude(m => m.Label)
                .Include(m => m.Repetitions)
                .WhereIf(mistakesListFilters.LabelId.HasValue, m => m.MistakeLabels.Any(i => i.Label.Id == mistakesListFilters.LabelId.Value))
                .Where(m => m.IsSolved && mistakesListFilters.IncludeSolved || !m.IsSolved && mistakesListFilters.IncludeUnsolved)
                .Where(m => m.IsSolved && mistakesListFilters.IncludeSolved || !m.IsSolved && mistakesListFilters.IncludeUnsolved)
                .OrderByField(sortingParameters);

            var count = await mistakes.CountAsync();

            var paginatedMistakes = await mistakes
                .Skip(pagingParameters.StartAt)
                .Take(pagingParameters.MaxResults)
                .ToListAsync();

            return Ok(paginatedMistakes.Select(m => m.ToWebModel()).ToPaginatedResult(count));
        }

        [HttpGet("{mistakeId:guid}")]
        public async Task<ActionResult<ICollection<MistakeWebModel>>> GetMistake(Guid mistakeId)
        {
            var mistake = await _dataContext.Set<Mistake>()
                .Where(m => m.Id == mistakeId)
                .Include(m => m.Tips)
                .Include(m => m.MistakeLabels)
                    .ThenInclude(m => m.Label)
                .Include(m => m.Repetitions)
                .FirstOrDefaultAsync();

            if (mistake is null)
                return NotFound();

            return Ok(mistake.ToWebModel());
        }

        #endregion

        #region DELETE

        [HttpDelete("{mistakeId:guid}")]
        public async Task<ActionResult> RemoveMistake(Guid mistakeId)
        {
            var mistake = await _dataContext.Set<Mistake>()
                .Include(m => m.Tips)
                .FirstOrDefaultAsync(m => m.Id == mistakeId);

            if (mistake is null)
                return NotFound();

            foreach (var tip in mistake.Tips)
            {
                _dataContext.Set<Tip>().Remove(tip);
            }

            _dataContext.Set<Mistake>().Remove(mistake);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        #endregion

        #region PUT

        [HttpPut("{mistakeId:guid}")]
        public async Task<ActionResult<MistakeWebModel>> UpdateMistake(Guid mistakeId, UpdatedMistakeWebModel mistake)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingMistake = await _dataContext.Set<Mistake>()
                .Include(m => m.Tips)
                .Include(m => m.MistakeLabels)
                    .ThenInclude(ml => ml.Label)
                .Include(m => m.Repetitions)
                .Include(m => m.AdditonalQuestions)
                .FirstOrDefaultAsync(m => m.Id == mistakeId);

            if (existingMistake is null)
                return NotFound();

            existingMistake.Name = mistake.Name ?? existingMistake.Name;
            existingMistake.Goal = mistake.Goal ?? existingMistake.Goal;
            existingMistake.Priority = mistake.Priority ?? existingMistake.Priority;

            existingMistake.AdditonalQuestions ??= new Mistake.MistakeAdditionalQuestions();
            existingMistake.AdditonalQuestions.Consequences = mistake.Consequences;
            existingMistake.AdditonalQuestions.WhatCanIDoBetter = mistake.WhatCanIDoBetter;
            existingMistake.AdditonalQuestions.WhatDidILearn = mistake.WhatDidILearn;
            existingMistake.AdditonalQuestions.CanIFixIt = mistake.CanIFixIt;
            existingMistake.AdditonalQuestions.OnlyResponsible = mistake.OnlyResponsible;

            if (mistake.Tips != null)
            {
                if (mistake.Tips.Count >= Constants.TipsLimit)
                    return BadRequest(ErrorMessageType.TooMuchTipsExist);

                var deletedTips = existingMistake.Tips.Where(t => !mistake.Tips.Contains(t.Content)).ToList();

                var newTips = mistake.Tips
                    .Where(t => !existingMistake.Tips.Select(et => et.Content).Contains(t))
                    .Where(t => t.IsPresent())
                    .Select(t => new Tip(existingMistake.UserId, t))
                    .ToList();

                _dataContext.Set<Tip>().RemoveRange(deletedTips);
                existingMistake.Tips.AddRange(newTips);
            }

            if (mistake.Labels != null)
            {
                var deletedLabels = existingMistake.MistakeLabels.Where(ml => !mistake.Labels.Contains(ml.LabelId));
                _dataContext.Set<MistakeLabel>().RemoveRange(deletedLabels);

                var newLabels = mistake.Labels.Except(existingMistake.MistakeLabels.Select(ml => ml.LabelId));

                foreach (var labelId in newLabels)
                {
                    var label = await _dataContext.Set<Label>().SingleOrDefaultAsync(l => l.Id == labelId);

                    if (label is null)
                        return BadRequest(ErrorMessageType.LabelDoesNotExist);

                    var mistakeLabel = new MistakeLabel(label, existingMistake);
                    _dataContext.Set<MistakeLabel>().Add(mistakeLabel);
                }
            }

            await _dataContext.SaveChangesAsync();

            return Ok(existingMistake.ToWebModel());
        }

        #endregion
    }
}