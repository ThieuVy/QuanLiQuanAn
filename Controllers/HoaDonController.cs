using Microsoft.AspNetCore.Mvc;
using QuanLiQuanAn.Data;
using QuanLiQuanAn.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiQuanAn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HoaDonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/HoaDon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CHoaDon>>> GetHoaDons()
        {
            return await _context.HoaDons
                .Include(hd => hd.TenTK)
                .Include(hd => hd.MaMA)
                .ToListAsync();
        }

        // GET: api/HoaDon/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CHoaDon>> GetHoaDon(int id)
        {
            var hoaDon = await _context.HoaDons
                .Include(hd => hd.TenTK)
                .Include(hd => hd.MaMA)
                .FirstOrDefaultAsync(hd => hd.SoHD == id);

            if (hoaDon == null)
            {
                return NotFound();
            }

            return hoaDon;
        }

        // PUT: api/HoaDon/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHoaDon(int id, CHoaDon hoaDon)
        {
            if (id != hoaDon.SoHD)
            {
                return BadRequest();
            }

            _context.Entry(hoaDon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoaDonExists(id))
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

        // POST: api/HoaDon
        [HttpPost]
        public async Task<ActionResult<CHoaDon>> PostHoaDon(CHoaDon hoaDon)
        {
            _context.HoaDons.Add(hoaDon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHoaDon", new { id = hoaDon.SoHD }, hoaDon);
        }

        // DELETE: api/HoaDon/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoaDon(int id)
        {
            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            _context.HoaDons.Remove(hoaDon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HoaDonExists(int id)
        {
            return _context.HoaDons.Any(e => e.SoHD == id);
        }
    }
}
