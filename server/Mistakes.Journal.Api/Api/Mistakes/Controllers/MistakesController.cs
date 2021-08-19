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

            if (newMistake.Tips != null)
                mistake.Tips.AddRange(newMistake.Tips.Where(t => !string.IsNullOrWhiteSpace(t)).Select(t => new Tip(t)));

            if (newMistake.Labels != null)
            {
                foreach (var labelName in newMistake.Labels)
                {
                    var mistakeLabel = new MistakeLabel();
                    mistake.MistakeLabels.Add(mistakeLabel);
                    var label = await _dataContext.Set<Label>().SingleOrDefaultAsync(l => l.Name == labelName);
                    label.MistakeLabels.Add(mistakeLabel);
                    _dataContext.Entry(label).State = EntityState.Modified;
                }
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
                .SingleOrDefaultAsync(m => m.Id == mistakeId);

            if (mistake is null)
                return NotFound();

            var newMistake = mistake.ToNewMistakeWebModel();

            return await AddMistake(newMistake);
        }

        [HttpPost("{mistakeId:guid}/add-repetiton")]
        public async Task<ActionResult<MistakeWebModel>> AddRepetitionResetCounter(Guid mistakeId, [MJIncorrectMistakeDate(1)] DateTime? dateTime)
        {
            // TODO test czy to z tym atrybutem w argumencie wgl działa xD
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mistake = await _dataContext.Set<Mistake>()
                .SingleOrDefaultAsync(m => m.Id == mistakeId);

            if (mistake is null)
                return NotFound();

            mistake.Counter++;
            mistake.LastProgressInDays = (DateTime.Now - mistake.LastRepetitionDateTime).Days;
            mistake.LastRepetitionDateTime = dateTime ?? DateTime.Now;
            mistake.LastRepetitionRegistrationDateTime = DateTime.Now;

            _dataContext.Entry(mistake).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();

            return Ok(mistake.ToWebModel());
        }


        [HttpPost("search")]
        public async Task<ActionResult<MistakeWebModel>> GetMistakes(MistakeSearchWebModel searchModel)
        {
            var mistakes = await _dataContext.Set<Mistake>()
                .WhereIf(!string.IsNullOrEmpty(searchModel.Name), m => m.Name.ToLower().Contains(searchModel.Name.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(searchModel.Goal), m => m.Goal.ToLower().Contains(searchModel.Goal.ToLower()))
                .WhereIf(searchModel.Priorities != null && searchModel.Priorities.Any(), m => searchModel.Priorities.Contains(m.Priority))
                .WhereIf(
                    searchModel.Labels != null && searchModel.Labels.Any(),
                    m => searchModel.Labels.Any(l => m.MistakeLabels.Select(ml => ml.Label.Name).Contains(l)))
                .Skip(searchModel.StartAt)
                .Take(searchModel.MaxResults)
                .ToListAsync();

            return Ok(mistakes.Select(m => m.ToWebModel()));
        }

        #endregion

        #region GET

        [HttpGet]
        public async Task<ActionResult<MistakeWebModel>> GetMistakes([FromQuery] PagingParameters pagingParameters)
        {
            var mistakes = await _dataContext.Set<Mistake>()
                .Include(m => m.Tips)
                .Skip(pagingParameters.StartAt)
                .Take(pagingParameters.MaxResults)
                .ToListAsync();

            return Ok(mistakes.Select(m => m.ToWebModel()));
        }

        [HttpGet("{mistakeId:guid}")]
        public async Task<ActionResult<ICollection<MistakeWebModel>>> GetMistake(Guid mistakeId)
        {
            var mistake = await _dataContext.Set<Mistake>()
                .Include(m => m.Tips)
                .SingleOrDefaultAsync(m => m.Id == mistakeId);

            if (mistake is null)
                return NotFound();

            return Ok(mistake.ToWebModel());
        }

        [HttpGet("{mistakeId:guid}/can-undo-repetition")]
        public async Task<ActionResult<bool>> GetPermissionToWithdrawLastRepetition(Guid mistakeId)
        {
            var mistake = await _dataContext.Set<Mistake>()
                .FirstOrDefaultAsync(m => m.Id == mistakeId);

            if (mistake is null)
                return NotFound();

            if (!mistake.LastRepetitionRegistrationDateTime.HasValue || !mistake.LastProgressInDays.HasValue)
                return Ok(false);

            return Ok(mistake.LastRepetitionRegistrationDateTime >= DateTime.Now - TimeSpan.FromDays(1));
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

        [HttpDelete("{mistakeId:guid}/last-repetition")]
        public async Task<ActionResult> RemoveLastRepetition(Guid mistakeId)
        {
            if (GetPermissionToWithdrawLastRepetition(mistakeId).Result.Value)
                return BadRequest(ErrorMessageType.LastRepetitionIsTooOldToWithdraw);

            var mistake = await _dataContext.Set<Mistake>()
                .FirstOrDefaultAsync(m => m.Id == mistakeId);

            if (mistake is null)
                return NotFound();

            mistake.LastRepetitionDateTime = mistake.LastRepetitionRegistrationDateTime.GetValueOrDefault() -
                                             TimeSpan.FromDays(mistake.LastProgressInDays.GetValueOrDefault());
            mistake.LastRepetitionRegistrationDateTime = null;
            mistake.LastProgressInDays = null;

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