using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities;
using Entities.Events;
using StudentPlanner.Core.DTO;

namespace StudentPlanner.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicEventsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AcademicEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AcademicEvents
        [HttpGet]
        public async Task<ActionResult<List<AcademicEventResponse>>> GetAcademicEvents()
        {
            return (await _context.AcademicEvents.ToListAsync()).Select(e=>e.ToAcademicEventResponse()).ToList();
        }

        // GET: api/AcademicEvents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AcademicEventResponse>> GetAcademicEvent(Guid id)
        {
            var academicEvent = await _context.AcademicEvents.FindAsync(id);

            if (academicEvent == null)
            {
                return NotFound();
            }

            return academicEvent.ToAcademicEventResponse();
        }

        // PUT: api/AcademicEvents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAcademicEvent(Guid id, AcademicEvent academicEvent)
        {
            if (id != academicEvent.EventId)
            {
                return BadRequest();
            }

            _context.Entry(academicEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcademicEventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AcademicEvents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AcademicEventResponse>> PostAcademicEvent(AcademicEvent academicEvent)
        {
            _context.AcademicEvents.Add(academicEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAcademicEvent", new { id = academicEvent.EventId }, academicEvent);
        }

        // DELETE: api/AcademicEvents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAcademicEvent(Guid id)
        {
            var academicEvent = await _context.AcademicEvents.FindAsync(id);
            if (academicEvent == null)
            {
                return NotFound();
            }

            _context.AcademicEvents.Remove(academicEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AcademicEventExists(Guid id)
        {
            return _context.AcademicEvents.Any(e => e.EventId == id);
        }
    }
}
