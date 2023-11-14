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
    public class LoaiSachesController : Controller
    {
        private readonly QlsContext _context;

        public LoaiSachesController(QlsContext context)
        {
            _context = context;
        }

        // GET: LoaiSaches
        public async Task<IActionResult> Index()
        {
              return _context.LoaiSaches != null ? 
                          View(await _context.LoaiSaches.ToListAsync()) :
                          Problem("Entity set 'QlsContext.LoaiSaches'  is null.");
        }

        // GET: LoaiSaches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LoaiSaches == null)
            {
                return NotFound();
            }

            var loaiSach = await _context.LoaiSaches
                .FirstOrDefaultAsync(m => m.MaLoai == id);
            if (loaiSach == null)
            {
                return NotFound();
            }

            return View(loaiSach);
        }

        // GET: LoaiSaches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiSaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoai,TenLoai")] LoaiSach loaiSach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiSach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiSach);
        }

        // GET: LoaiSaches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LoaiSaches == null)
            {
                return NotFound();
            }

            var loaiSach = await _context.LoaiSaches.FindAsync(id);
            if (loaiSach == null)
            {
                return NotFound();
            }
            return View(loaiSach);
        }

        // POST: LoaiSaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLoai,TenLoai")] LoaiSach loaiSach)
        {
            if (id != loaiSach.MaLoai)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiSach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiSachExists(loaiSach.MaLoai))
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
            return View(loaiSach);
        }

        // GET: LoaiSaches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LoaiSaches == null)
            {
                return NotFound();
            }

            var loaiSach = await _context.LoaiSaches
                .FirstOrDefaultAsync(m => m.MaLoai == id);
            if (loaiSach == null)
            {
                return NotFound();
            }

            return View(loaiSach);
        }

        // POST: LoaiSaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LoaiSaches == null)
            {
                return Problem("Entity set 'QlsContext.LoaiSaches'  is null.");
            }
            var loaiSach = await _context.LoaiSaches.FindAsync(id);
            if (loaiSach != null)
            {
                _context.LoaiSaches.Remove(loaiSach);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiSachExists(int id)
        {
          return (_context.LoaiSaches?.Any(e => e.MaLoai == id)).GetValueOrDefault();
        }
    }
}
