using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mistakes.Journal.Api.Api.Mistakes.Mappers;
using Mistakes.Journal.Api.Api.Mistakes.WebModels;
using Mistakes.Journal.Api.Api.Shared.Validators;
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
        
        
        [HttpPost]
        public async Task<ActionResult<MistakeWebModel>> AddMistake(NewMistakeWebModel newMistake)
        {
            if (!newMistake.Name.IsPresent() || newMistake.Priority == null)
            {
                return BadRequest();
            }
            
            var mistake = new Mistake(newMistake.Name, newMistake.Goal, newMistake.Priority.Value);

            var tips = newMistake.Tips.Select(t => new Tip(t));

            foreach (var tip in tips)
            {
                mistake.Tips.Add(tip);
            }

            await _dataContext.Set<Mistake>().AddAsync(mistake);
            await _dataContext.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetMistake), new { mistakeId = mistake.Id }, mistake.ToWebModel());
        }
        
        [HttpGet]
        public async Task<ActionResult<MistakeWebModel>> GetMistakes()
        {
            var mistakes = await _dataContext.Set<Mistake>()
                .Include(m => m.Tips)
                .ToListAsync();

            return Ok(mistakes.Select(m => m.ToWebModel()));
        }
        
        [HttpGet("{mistakeId:guid}")]
        public async Task<ActionResult<ICollection<MistakeWebModel>>> GetMistake(Guid mistakeId)
        {
            var mistake = await _dataContext.Set<Mistake>()
                .Include(m => m.Tips)
                .FirstOrDefaultAsync(m => m.Id == mistakeId);

            if (mistake != null)
            {
                return Ok(mistake.ToWebModel());
            }

            return NotFound();
        }
    }
}