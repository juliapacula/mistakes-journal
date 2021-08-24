using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mistakes.Journal.Api.Api.Mistakes.Mappers;
using Mistakes.Journal.Api.Api.Mistakes.WebModels;
using Mistakes.Journal.Api.Api.Shared;
using Mistakes.Journal.Api.Api.Shared.Validators;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.Controllers
{
    [ApiController]
    [Route("/api/mistakes")]
    public class MistakesController : ControllerBase
    {
        #region Fields & Constructors

        private readonly MistakesJournalContext _dataContext;

        public MistakesController(
            MistakesJournalContext dataContext
        )
        {
            _dataContext = dataContext;
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<ActionResult<MistakeWebModel>> AddMistake(NewMistakeWebModel newMistake)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mistake = newMistake.ToEntity();

            mistake.Repetitions.Add(new Repetition(newMistake.AddDateTime ?? DateTime.Now));

            if (newMistake.Tips != null)
                mistake.Tips.AddRange(newMistake.Tips.Where(t => !string.IsNullOrWhiteSpace(t)).Select(t => new Tip(t)));

            foreach (var labelName in newMistake.Labels.EmptyIfNull())
            {
                var mistakeLabel = new MistakeLabel();
                mistake.MistakeLabels.Add(mistakeLabel);
                var label = await _dataContext.Set<Label>().SingleOrDefaultAsync(l => l.Id == labelName);

                if (label is null)
                    return BadRequest(ErrorMessageType.LabelDoesNotExist);

                label.MistakeLabels.Add(mistakeLabel);
                _dataContext.Entry(label).State = EntityState.Modified;
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

        [HttpPost("{mistakeId:guid}/add-repetiton")]
        public async Task<ActionResult<MistakeWebModel>> AddRepetitionResetCounter(Guid mistakeId, [FromBody, MJIncorrectMistakeDate(1)] DateTime? dateTime)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mistake = await _dataContext.Set<Mistake>()
                .Include(m => m.Repetitions)
                .SingleOrDefaultAsync(m => m.Id == mistakeId);

            if (mistake is null)
                return NotFound();

            mistake.Repetitions.Add(new Repetition(dateTime ?? DateTime.Now));

            _dataContext.Entry(mistake).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();

            return Ok(mistake.ToWebModel());
        }


        [HttpPost("search")]
        public async Task<ActionResult<MistakeWebModel>> GetMistakes(MistakeSearchWebModel searchModel)
        {
            var mistakes = await _dataContext.Set<Mistake>()
                .Include(m => m.Tips)
                .Include(m => m.Repetitions)
                .Include(m => m.MistakeLabels)
                .ThenInclude(m => m.Label)
                .WhereIf(!string.IsNullOrEmpty(searchModel.Name), m => m.Name.ToLower().Contains(searchModel.Name.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(searchModel.Goal), m => m.Goal != null && m.Goal.ToLower().Contains(searchModel.Goal.ToLower()))
                .WhereIf(!searchModel.Priorities.IsNullOrEmpty(), m => searchModel.Priorities.Contains(m.Priority))
                .ToListAsync();

            mistakes = mistakes
                    // this is too hard for .NET Core
                .WhereIf(!searchModel.Labels.IsNullOrEmpty(), m => m.MistakeLabels.Select(ml => ml.LabelId).Intersect(searchModel.Labels).Any())
                .Skip(searchModel.StartAt)
                .Take(searchModel.MaxResults)
                .OrderByDescending(m => m.Repetitions.First().DateTime)
                .ToList();

            return Ok(mistakes.Select(m => m.ToWebModel()));
        }

        #endregion

        #region GET

        [HttpGet]
        public async Task<ActionResult<MistakeWebModel>> GetMistakes([FromQuery] PagingParameters pagingParameters)
        {
            var mistakes = await _dataContext.Set<Mistake>()
                .Skip(pagingParameters.StartAt)
                .Take(pagingParameters.MaxResults)
                .Include(m => m.Tips)
                .Include(m => m.MistakeLabels)
                .ThenInclude(m => m.Label)
                .Include(m => m.Repetitions)
                .OrderByDescending(m => m.Repetitions.First().DateTime)
                .ToListAsync();

            return Ok(mistakes.Select(m => m.ToWebModel()));
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
                .FirstOrDefaultAsync(m => m.Id == mistakeId);

            if (mistake is null)
                return NotFound();

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
                .FirstOrDefaultAsync(m => m.Id == mistakeId);

            if (existingMistake is null)
                return NotFound();

            existingMistake.Name = mistake.Name ?? existingMistake.Name;
            existingMistake.Goal = mistake.Goal ?? existingMistake.Goal;
            existingMistake.Priority = mistake.Priority ?? existingMistake.Priority;

            if (mistake.Tips != null)
            {
                var deletedTips = existingMistake.Tips.Where(t => !mistake.Tips.Contains(t.Content));
                _dataContext.Set<Tip>().RemoveRange(deletedTips);

                var newTips = mistake.Tips.Where(t => !existingMistake.Tips.Select(et => et.Content).Contains(t));
                existingMistake.Tips.AddRange(newTips.Where(t => !string.IsNullOrWhiteSpace(t))
                    .Select(t => new Tip(t)));
            }

            _dataContext.Entry(existingMistake).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();

            return Ok(existingMistake.ToWebModel());
        }

        #endregion
    }
}