﻿using System.Linq;
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

        [HttpGet]
        public async Task<ActionResult<MistakeWebModel>> GetTips(PagingParameters pagingParameters)
        {
            var tips = await _dataContext.Set<Tip>()
                .Skip(pagingParameters.StartAt)
                .Take(pagingParameters.MaxResults)
                .ToListAsync();

            return Ok(tips.Select(t => t.ToWebModel()));
        }
    }
}