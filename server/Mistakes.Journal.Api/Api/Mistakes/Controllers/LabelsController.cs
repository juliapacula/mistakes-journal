﻿using System;
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
    [Route("/api/labels")]
    public class LabelsController : ControllerBase
    {
        #region Fields & Constructors

        private readonly MistakesJournalContext _dataContext;

        public LabelsController(MistakesJournalContext dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<ActionResult<LabelWebModel>> AddLabel(NewLabelWebModel newLabel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingLabel = await _dataContext.Set<Label>()
                .SingleOrDefaultAsync(m => m.Name == newLabel.Name);

            if (existingLabel != null)
                return BadRequest(ErrorMessageType.NotUnique);

            var label = newLabel.ToEntity();

            await _dataContext.Set<Label>().AddAsync(label);
            await _dataContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLabel), new {labelId = label.Id}, label.ToWebModel());
        }

        [HttpPost("search")]
        public async Task<ActionResult<LabelWebModel>> GetLabels(LabelSearchWebModel searchModel)
        {
            var labels = await _dataContext.Set<Label>()
                .WhereIf(!string.IsNullOrEmpty(searchModel.Name),
                    m => m.Name.ToLower().Contains(searchModel.Name.ToLower()))
                .WhereIf(!searchModel.Colors.IsNullOrEmpty(), m => searchModel.Colors.Contains(m.Color))
                .Skip(searchModel.StartAt)
                .Take(searchModel.MaxResults)
                .ToListAsync();

            return Ok(labels.Select(m => m.ToWebModel()));
        }

        #endregion

        #region GET

        [HttpGet]
        public async Task<ActionResult<LabelWebModel>> GetLabels([FromQuery] PagingParameters pagingParameters)
        {
            var labels = await _dataContext.Set<Label>()
                .Skip(pagingParameters.StartAt)
                .Take(pagingParameters.MaxResults)
                .ToListAsync();

            return Ok(labels.Select(t => t.ToWebModel()));
        }

        [HttpGet("{labelId:guid}")]
        public async Task<ActionResult<ICollection<LabelWebModel>>> GetLabel(Guid labelId)
        {
            var label = await _dataContext.Set<Label>()
                .Include(m => m.MistakeLabels)
                .SingleOrDefaultAsync(m => m.Id == labelId);

            if (label is null)
                return NotFound();

            return Ok(label.ToWebModel());
        }

        #endregion

        #region DELETE

        [HttpDelete("{labelId:guid}")]
        public async Task<ActionResult> RemoveMistake(Guid labelId)
        {
            var label = await _dataContext.Set<Label>()
                .FirstOrDefaultAsync(m => m.Id == labelId);

            if (label is null)
                return NotFound();

            _dataContext.Set<Label>().Remove(label);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        #endregion

        #region PUT

        [HttpPut("{labelId:guid}")]
        public async Task<ActionResult<MistakeWebModel>> UpdateLabel(Guid labelId, UpdatedLabelWebModel label)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingLabel = await _dataContext.Set<Label>()
                .FirstOrDefaultAsync(m => m.Id == labelId);

            if (existingLabel is null)
                return NotFound();

            existingLabel.Name = label.Name ?? existingLabel.Name;
            existingLabel.Color = label.Color ?? existingLabel.Color;

            _dataContext.Entry(existingLabel).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();

            return Ok(existingLabel.ToWebModel());
        }

        #endregion
    }
}