using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mistakes.Journal.Api.Api.Mistakes.Mappers;
using Mistakes.Journal.Api.Api.Mistakes.WebModels;
using Mistakes.Journal.Api.Api.Shared;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.Controllers
{
    [ApiController]
    [Route("/api/mistakes")]
    public class MistakesController : ControllerBase
    {
        private readonly MistakesJournalContext _dataContext;

        public MistakesController(
            MistakesJournalContext dataContext
        )
        {
            _dataContext = dataContext;
        }

        #region POST

        [HttpPost]
        public async Task<ActionResult<MistakeWebModel>> AddMistake(NewMistakeWebModel newMistake)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var mistake = new Mistake(newMistake.Name, newMistake.Goal, newMistake.Priority.GetValueOrDefault());
            mistake.Tips.AddRange(newMistake.Tips.Where(t => !string.IsNullOrWhiteSpace(t)).Select(t => new Tip(t)));

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

            var newMistake = new NewMistakeWebModel
            {
                Name = mistake.Name,
                Goal = mistake.Goal,
                Priority = mistake.Priority,
                Tips = mistake.Tips.Select(t => t.Content).ToList(),
            };

            return await AddMistake(newMistake);
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

        [HttpPost("search")]
        public async Task<ActionResult<MistakeWebModel>> GetMistakes(MistakeSearchWebModel searchModel)
        {
            var mistakes = await _dataContext.Set<Mistake>()
                .WhereIf(!string.IsNullOrEmpty(searchModel.Name), m => m.Name.ToLower().Contains(searchModel.Name.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(searchModel.Goal), m => m.Goal.ToLower().Contains(searchModel.Goal.ToLower()))
                .WhereIf(searchModel.Priorities != null, m => searchModel.Priorities.Contains(m.Priority))
                .Skip(searchModel.StartAt)
                .Take(searchModel.MaxResults)
                .ToListAsync();

            return Ok(mistakes.Select(m => m.ToWebModel()));
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
        public async Task<ActionResult<ICollection<MistakeWebModel>>> UpdateMistake(Guid mistakeId, EditedMistakeWebModel mistake)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingMistake = await _dataContext.Set<Mistake>()
                .Include(m => m.Tips)
                .FirstOrDefaultAsync(m => m.Id == mistakeId);

            existingMistake.Name = mistake.Name;
            existingMistake.Goal = mistake.Goal;
            existingMistake.Priority = mistake.Priority.GetValueOrDefault();

            var deletedTips = existingMistake.Tips.Where(t => !mistake.Tips.Contains(t.Content));
            _dataContext.Set<Tip>().RemoveRange(deletedTips);

            var newTips = mistake.Tips.Where(t => !existingMistake.Tips.Select(et => et.Content).Contains(t));
            existingMistake.Tips.AddRange(newTips.Where(t => !string.IsNullOrWhiteSpace(t)).Select(t => new Tip(t)));

            _dataContext.Entry(existingMistake).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();

            return Ok(mistake);
        }

        #endregion
    }
}