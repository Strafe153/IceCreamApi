using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using IceCreamApi.Models;

namespace IceCreamApi.Controllers
{
    [ApiController]
    [Route("api/icecream")]
    public class IceCreamsController : ControllerBase
    {
        private readonly IceCreamContext _context;

        public IceCreamsController(IceCreamContext context)
        {
            _context = context;
        }

        // GET api/icecream
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IceCream>>> GetIceCreamsAsync()
        {
            return await _context.IceCreams.ToListAsync();
        }

        // GET api/icecream/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IceCream>> GetIceCreamByIdAsync(Guid id)
        {
            IceCream iceCream = await _context.IceCreams.FirstOrDefaultAsync(i => i.Id == id);

            if (iceCream is null)
            {
                return NotFound();
            }

            return Ok(iceCream);
        }

        // POST api/icecream
        [HttpPost]
        public async Task<ActionResult<IceCream>> CreateIceCreamAsync(IceCream iceCream)
        {
            _context.Add(iceCream);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIceCreamByIdAsync), new { Id = iceCream.Id }, iceCream);
        }

        // PUT api/icecream/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateIceCreamAsync(Guid id, IceCream newIceCream)
        {
            IceCream iceCream = await _context.IceCreams.FirstOrDefaultAsync(i => i.Id == id);

            if (iceCream is null)
            {
                return NotFound();
            }

            iceCream.Flavour = newIceCream.Flavour;
            iceCream.Color = newIceCream.Color;
            iceCream.Price = newIceCream.Price;
            iceCream.WeightInGrams = newIceCream.WeightInGrams;

            _context.Update(iceCream);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH api/icecream/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateIceCreamAsync(Guid id, 
            JsonPatchDocument<IceCream> patchDoc)
        {
            IceCream iceCream = await _context.IceCreams.FirstOrDefaultAsync(i => i.Id == id);

            if (iceCream is null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(iceCream, ModelState);

            if (!TryValidateModel(iceCream))
            {
                return ValidationProblem(ModelState);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/icecream/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIceCreamAsync(Guid id)
        {
            IceCream iceCream = await _context.IceCreams.FirstOrDefaultAsync(i => i.Id == id);

            if (iceCream is null)
            {
                return NotFound();
            }

            _context.Remove(iceCream);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
