using AutoMapper;
using Core.Entities;
using Core.Interfaces.Services;
using Core.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/icecreams")]
    [ApiController]
    public class IceCreamController : ControllerBase
    {
        private readonly IIceCreamService _service;
        private readonly IMapper _mapper;

        public IceCreamController(
            IIceCreamService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IceCreamReadViewModel>>> GetAsync()
        {
            var iceCreams = await _service.GetAllAsync();
            var readModels = _mapper.Map<IEnumerable<IceCreamReadViewModel>>(iceCreams);

            return Ok(readModels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IceCreamReadViewModel>> GetAsync([FromRoute] string id)
        {
            var iceCream = await _service.GetByIdAsync(id);
            var readModel = _mapper.Map<IceCreamReadViewModel>(iceCream);

            return Ok(readModel);
        }

        [HttpPost]
        public async Task<ActionResult<IceCreamReadViewModel>> CreateAsync(
            [FromBody] IceCreamCreateUpdateViewModel createModel)
        {
            var iceCream = _mapper.Map<IceCream>(createModel);
            await _service.UpdateAsync(iceCream);

            var readModel = _mapper.Map<IceCreamReadViewModel>(iceCream);

            return CreatedAtAction(nameof(GetAsync), new { id = readModel.Id }, readModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(
            [FromRoute] string id, 
            [FromBody] IceCreamCreateUpdateViewModel updateModel)
        {
            var iceCream = await _service.GetByIdAsync(id);

            _mapper.Map(updateModel, iceCream);
            await _service.UpdateAsync(iceCream);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateAsync(
            [FromRoute] string id,
            [FromBody] JsonPatchDocument<IceCreamCreateUpdateViewModel> patchDocument)
        {
            var iceCream = await _service.GetByIdAsync(id);
            var updateModel = _mapper.Map<IceCreamCreateUpdateViewModel>(iceCream);

            patchDocument.ApplyTo(updateModel, ModelState);

            if (!TryValidateModel(updateModel))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(updateModel, iceCream);
            await _service.UpdateAsync(iceCream);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] string id)
        {
            var iceCream = await _service.GetByIdAsync(id);
            await _service.DeleteAsync(iceCream);

            return NoContent();
        }
    }
}
