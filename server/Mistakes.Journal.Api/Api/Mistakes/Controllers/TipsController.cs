using System;
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
    [Route("/api/tips")]
    public class TipsController : ControllerBase
    {
        private readonly MistakesJournalContext _dataContext;

        public TipsController(
            MistakesJournalContext dataContext
        )
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        public async Task<ActionResult<TipWebModel>> AddTip(NewTipWebModel newTip)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tip = newTip.ToEntity();

            await _dataContext.Set<Tip>().AddAsync(tip);
            await _dataContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTip), new { tipId = tip.Id }, tip.ToWebModel());
        }

        [HttpGet]
        public async Task<ActionResult<TipWebModel>> GetTips([FromQuery] PagingParameters pagingParameters)
        {
            var tips = await _dataContext.Set<Tip>()
                .Skip(pagingParameters.StartAt)
                .Take(pagingParameters.MaxResults)
                .ToListAsync();

            return Ok(tips.Select(t => t.ToWebModel()));
        }

        [HttpGet("{tipId:guid}")]
        public async Task<ActionResult<TipWebModel>> GetTip(Guid tipId)
        {
            var tip = await _dataContext.Set<Tip>()
                .SingleOrDefaultAsync(m => m.Id == tipId);

            if (tip is null)
                return NotFound();

            return Ok(tip.ToWebModel());
        }
    }
}
