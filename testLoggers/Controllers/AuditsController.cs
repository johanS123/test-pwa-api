using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testLoggers.Helpers;
using testLoggers.Models;

namespace testLoggers.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuditsController : ControllerBase
    {
        private readonly TestLoggerCTX _context;

        public AuditsController(TestLoggerCTX context)
        {
            _context = context;
        }

        // GET: api/Audits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Audits>>> GetAudits()
        {
            return await _context.Audits.ToListAsync();
        }

        // GET: api/Audits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Audits>> GetAudits(int id)
        {
            var audits = await _context.Audits.FindAsync(id);

            if (audits == null)
            {
                return NotFound();
            }

            return audits;
        }

        // GET: api/Audits/user/5
        [HttpGet("user/{idUser}")]
        public async Task<ActionResult<IEnumerable<Audits>>> GetAuditsXUser(int idUser)
        {
            var audits = await _context.Audits.Where(x => x.idUser == idUser).ToListAsync();

            if (audits == null)
            {
                return NotFound();
            }

            return audits;
        }

        // PUT: api/Audits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAudits(int id, Audits audits)
        {
            if (id != audits.idUser)
            {
                return BadRequest();
            }

            _context.Entry(audits).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditsExists(id))
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

        // POST: api/Audits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{idUser}")]
        public async Task<ActionResult<Audits>> PostAudits(int idUser)
        {

            var User = await _context.Users.AsNoTracking().Where(x => x.id == idUser).SingleOrDefaultAsync();
            
            if (User == null)
            {
                return NotFound(ErrorHelper.Response(404, "Usuario no encontrado."));
            }

            _context.Audits.Add(new Audits()
            {
                datelog = DateTime.Now,
                idUser = User.id
            });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtAction("GetAudits", new { id = User.id }, null);
        }

        // DELETE: api/Audits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAudits(int id)
        {
            var audits = await _context.Audits.FindAsync(id);
            if (audits == null)
            {
                return NotFound();
            }

            _context.Audits.Remove(audits);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuditsExists(int id)
        {
            return _context.Audits.Any(e => e.idUser == id);
        }
    }
}
