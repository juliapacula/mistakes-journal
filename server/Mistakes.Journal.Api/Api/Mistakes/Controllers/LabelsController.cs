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
using Mistakes.Journal.Api.Logic.Identity.Services;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/labels")]
    public class LabelsController : ControllerBase
    {
        #region Fields & Constructors

        private readonly MistakesJournalContext _dataContext;
        private readonly IUserProvider _userProvider;

        public LabelsController(
            MistakesJournalContext dataContext,
            IUserProvider userProvider)
        {
            _dataContext = dataContext;
            _userProvider = userProvider;
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<ActionResult<LabelWebModel>> AddLabel(NewLabelWebModel newLabel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nameLowerCase = newLabel.Name.ToLower();
            var existingLabel = await _dataContext.Set<Label>()
                .FirstOrDefaultAsync(l => l.Name.ToLower() == nameLowerCase);

            if (existingLabel != null)
                return BadRequest(ErrorMessageType.NotUnique);

            var label = newLabel.ToEntity(_userProvider.GetId());

            await _dataContext.Set<Label>().AddAsync(label);
            await _dataContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLabel), new { labelId = label.Id }, label.ToWebModel());
        }

        [HttpPost("search")]
        public async Task<ActionResult<LabelWebModel>> GetLabels(LabelSearchWebModel searchModel)
        {
            var labels = await _dataContext.Set<Label>()
                .Include(l => l.MistakeLabels)
                .WhereIf(searchModel.Name.IsPresent(),
                    l => l.Name.ToLower().Contains(searchModel.Name.ToLower()))
                .WhereIf(!searchModel.Colors.IsNullOrEmpty(), l => searchModel.Colors.Contains(l.Color))
                .ToListAsync();

            return Ok(labels.Select(l => l.ToWebModel()));
        }

        #endregion

        #region GET

        [HttpGet("{labelId:guid}")]
        public async Task<ActionResult<ICollection<LabelWebModel>>> GetLabel(Guid labelId)
        {
            var label = await _dataContext.Set<Label>()
                .Include(l => l.MistakeLabels)
                .SingleOrDefaultAsync(l => l.Id == labelId);

            if (label is null)
                return NotFound();

            return Ok(label.ToWebModel());
        }

        #endregion

        #region DELETE

        [HttpDelete("{labelId:guid}")]
        public async Task<ActionResult> RemoveLabel(Guid labelId)
        {
            var label = await _dataContext.Set<Label>()
                .FirstOrDefaultAsync(l => l.Id == labelId);

            if (label is null)
                return NotFound();

            _dataContext.Set<Label>().Remove(label);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        #endregion

        #region PUT

        [HttpPut("{labelId:guid}")]
        public async Task<ActionResult<LabelWebModel>> UpdateLabel(Guid labelId, UpdatedLabelWebModel label)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingLabel = await _dataContext.Set<Label>()
                .Include(l => l.MistakeLabels)
                .FirstOrDefaultAsync(l => l.Id == labelId);

            if (existingLabel is null)
                return NotFound();

            existingLabel.Name = label.Name ?? existingLabel.Name;
            existingLabel.Color = label.Color ?? existingLabel.Color;

            await _dataContext.SaveChangesAsync();

            return Ok(existingLabel.ToWebModel());
        }

        #endregion
    }
}
