using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuanLySach.Models;

namespace QuanLySach.Controllers
{
    public class SachesController : Controller
    {
        private readonly QlsContext _context;   

        public SachesController(QlsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, string maSach, int tenLoai, string TenLoai)
        {
            IQueryable<string> tenloaiQuery = from t in _context.LoaiSaches
                                            orderby t.TenLoai
                                            select t.TenLoai;

            var saches = from s in _context.Saches
                         .Include(m => m.NhaXuatBan)
                         .Include(m => m.LoaiSach)
                         select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                saches = saches.Where(s => s.Tensach.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(maSach))
            {
                int maSachInt;
                if (int.TryParse(maSach, out maSachInt))
                {
                    saches = saches.Where(s => s.Masach == maSachInt);
                }

            }

            if (!string.IsNullOrEmpty(TenLoai))
            {
                saches = saches.Where(s => s.LoaiSach.TenLoai == TenLoai);
            }

            return View(await saches.ToListAsync());
        }



        // GET: Saches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Saches == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches
                .FirstOrDefaultAsync(m => m.Masach == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // GET: Saches/Create
        public IActionResult Create()
        {
            ViewData["Maxb"] = new SelectList(_context.NhaXuatBans, "MaXb", "Tenxb");
            ViewData["Maloai"] = new SelectList(_context.LoaiSaches, "MaLoai", "TenLoai");
            return View();
        }

        // POST: Saches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Masach,Maxb,Maloai,Tensach,Tacgia")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Maxb"] = new SelectList(_context.LoaiSaches, "MaXb", "Tenxb", sach.Maxb);
            ViewData["Maloai"] = new SelectList(_context.NhaXuatBans, "MaLoai", "TenLoai", sach.Maloai);
            return View(sach);
        }


        // GET: Saches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Saches == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches.FindAsync(id);
            if (sach == null)
            {
                return NotFound();
            }
            return View(sach);
        }

        // POST: Saches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Masach,Maxb,Maloai,Tensach,Tacgia")] Sach sach)
        {
            if (id != sach.Masach)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SachExists(sach.Masach))
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
            return View(sach);
        }

        // GET: Saches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Saches == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches
                .FirstOrDefaultAsync(m => m.Masach == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // POST: Saches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Saches == null)
            {
                return Problem("Entity set 'QlsContext.Saches'  is null.");
            }
            var sach = await _context.Saches.FindAsync(id);
            if (sach != null)
            {
                _context.Saches.Remove(sach);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SachExists(int id)
        {
            return (_context.Saches?.Any(e => e.Masach == id)).GetValueOrDefault();
        }
    }
}
