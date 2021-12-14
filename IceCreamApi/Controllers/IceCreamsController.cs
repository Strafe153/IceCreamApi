using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using IceCreamApi.Dtos;
using IceCreamApi.Models;
using IceCreamApi.Repositories;
using AutoMapper;

namespace IceCreamApi.Controllers
{
    [ApiController]
    [Route("api/icecream")]
    public class IceCreamsController : ControllerBase
    {
        private readonly IControllerRepository _repository;
        private readonly IMapper _mapper;

        public IceCreamsController(IControllerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/icecream
        [HttpGet]
        public async Task<IEnumerable<IceCreamReadDto>> GetIceCreamsAsync()
        {
            var iceCreams = await _repository.GetAllIceCreamsAsync();
            var readDtos = _mapper.Map<IEnumerable<IceCreamReadDto>>(iceCreams);

            return readDtos;
        }

        // GET api/icecream/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IceCreamReadDto>> GetIceCreamByIdAsync(Guid id)
        {
            IceCream iceCream = await _repository.GetIceCreamByIdAsync(id);

            if (iceCream is null)
            {
                return NotFound();
            }

            IceCreamReadDto readDto = _mapper.Map<IceCreamReadDto>(iceCream);

            return Ok(readDto);
        }

        // POST api/icecream
        [HttpPost]
        public async Task<ActionResult<IceCream>> CreateIceCreamAsync(IceCreamCreateUpdateDto createDto)
        {
            IceCream iceCream = _mapper.Map<IceCream>(createDto);

            _repository.AddIceCream(iceCream);
            await _repository.SaveChangesAsync();

            IceCreamReadDto readDto = _mapper.Map<IceCreamReadDto>(iceCream);

            return CreatedAtAction(nameof(GetIceCreamByIdAsync), new { Id = readDto.Id }, readDto);
        }

        // PUT api/icecream/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateIceCreamAsync(Guid id, IceCreamCreateUpdateDto updateDto)
        {
            IceCream iceCream = await _repository.GetIceCreamByIdAsync(id);

            if (iceCream is null)
            {
                return NotFound();
            }

            _mapper.Map(updateDto, iceCream);
            _repository.UpdateIceCream(iceCream);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        // PATCH api/icecream/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateIceCreamAsync(Guid id, 
            JsonPatchDocument<IceCreamCreateUpdateDto> patchDoc)
        {
            IceCream iceCream = await _repository.GetIceCreamByIdAsync(id);

            if (iceCream is null)
            {
                return NotFound();
            }

            var updateDto = _mapper.Map<IceCreamCreateUpdateDto>(iceCream);
            patchDoc.ApplyTo(updateDto, ModelState);

            if (!TryValidateModel(updateDto))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(updateDto, iceCream);
            _repository.UpdateIceCream(iceCream);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/icecream/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIceCreamAsync(Guid id)
        {
            IceCream iceCream = await _repository.GetIceCreamByIdAsync(id);

            if (iceCream is null)
            {
                return NotFound();
            }

            _repository.DeleteIceCream(iceCream);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
