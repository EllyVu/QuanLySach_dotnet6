using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLySach.Models;

namespace QuanLySach.Controllers
{
    public class NhaXuatBansController : Controller
    {
        private readonly QlsContext _context;

        public NhaXuatBansController(QlsContext context)
        {
            _context = context;
        }

        // GET: NhaXuatBans
        public async Task<IActionResult> Index()
        {
              return _context.NhaXuatBans != null ? 
                          View(await _context.NhaXuatBans.ToListAsync()) :
                          Problem("Entity set 'QlsContext.NhaXuatBans'  is null.");
        }

        // GET: NhaXuatBans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NhaXuatBans == null)
            {
                return NotFound();
            }

            var nhaXuatBan = await _context.NhaXuatBans
                .FirstOrDefaultAsync(m => m.MaXb == id);
            if (nhaXuatBan == null)
            {
                return NotFound();
            }

            return View(nhaXuatBan);
        }

        // GET: NhaXuatBans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NhaXuatBans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaXb,Tenxb,DiaChi,GhiChu")] NhaXuatBan nhaXuatBan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhaXuatBan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhaXuatBan);
        }

        // GET: NhaXuatBans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NhaXuatBans == null)
            {
                return NotFound();
            }

            var nhaXuatBan = await _context.NhaXuatBans.FindAsync(id);
            if (nhaXuatBan == null)
            {
                return NotFound();
            }
            return View(nhaXuatBan);
        }

        // POST: NhaXuatBans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaXb,Tenxb,DiaChi,GhiChu")] NhaXuatBan nhaXuatBan)
        {
            if (id != nhaXuatBan.MaXb)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhaXuatBan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhaXuatBanExists(nhaXuatBan.MaXb))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nhaXuatBan);
        }

        // GET: NhaXuatBans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NhaXuatBans == null)
            {
                return NotFound();
            }

            var nhaXuatBan = await _context.NhaXuatBans
                .FirstOrDefaultAsync(m => m.MaXb == id);
            if (nhaXuatBan == null)
            {
                return NotFound();
            }

            return View(nhaXuatBan);
        }

        // POST: NhaXuatBans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NhaXuatBans == null)
            {
                return Problem("Entity set 'QlsContext.NhaXuatBans'  is null.");
            }
            var nhaXuatBan = await _context.NhaXuatBans.FindAsync(id);
            if (nhaXuatBan != null)
            {
                _context.NhaXuatBans.Remove(nhaXuatBan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhaXuatBanExists(int id)
        {
          return (_context.NhaXuatBans?.Any(e => e.MaXb == id)).GetValueOrDefault();
        }
    }
}
