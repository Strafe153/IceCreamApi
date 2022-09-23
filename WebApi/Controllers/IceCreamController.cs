using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

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
    public async Task<ActionResult<IEnumerable<IceCreamReadDto>>> GetAsync()
    {
        var iceCreams = await _service.GetAllAsync();
        var readDtos = _mapper.Map<IEnumerable<IceCreamReadDto>>(iceCreams);

        return Ok(readDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IceCreamReadDto>> GetAsync([FromRoute] string id)
    {
        var iceCream = await _service.GetByIdAsync(id);
        var readDto = _mapper.Map<IceCreamReadDto>(iceCream);

        return Ok(readDto);
    }

    [HttpPost]
    public async Task<ActionResult<IceCreamReadDto>> CreateAsync([FromBody] IceCreamCreateUpdateDto createDto)
    {
        var iceCream = _mapper.Map<IceCream>(createDto);
        await _service.UpdateAsync(iceCream);

        var readDto = _mapper.Map<IceCreamReadDto>(iceCream);

        return CreatedAtAction(nameof(GetAsync), new { id = readDto.Id }, readDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync([FromRoute] string id, [FromBody] IceCreamCreateUpdateDto updateDto)
    {
        var iceCream = await _service.GetByIdAsync(id);

        _mapper.Map(updateDto, iceCream);
        await _service.UpdateAsync(iceCream);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateAsync(
        [FromRoute] string id,
        [FromBody] JsonPatchDocument<IceCreamCreateUpdateDto> patchDocument)
    {
        var iceCream = await _service.GetByIdAsync(id);
        var updateDto = _mapper.Map<IceCreamCreateUpdateDto>(iceCream);

        patchDocument.ApplyTo(updateDto, ModelState);

        if (!TryValidateModel(updateDto))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(updateDto, iceCream);
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
