using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuanLySach.Models;
using QuanLySach.Services;

namespace QuanLySach.Controllers
{
    public class SachesController : Controller
    {
        private readonly IQuanLySach _sach;
        private readonly QlsContext _context;

        public SachesController(QlsContext context, IQuanLySach quanLySach)
        {
            _sach = quanLySach;
            _context = context;
        }

        //Get : Sach/Index
        public async Task<IActionResult> Index(string searchString, string maSach)
        {

            var books = await _sach.GetAllSachAsync(searchString, maSach);
            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Tensach.Contains(searchString)).ToList();
            }
            int parsedMaSach;
            if (int.TryParse(maSach, out parsedMaSach))
            {
                books = books.Where(s => s.Masach == parsedMaSach).ToList();
            }
            return View(books);
        }

        // GET: Saches/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            var sachDetails = await _sach.GetSachByIdAsync(id);
            return View(sachDetails);
        }

        // GET: Saches/Create
        public IActionResult Create()
        {
            ViewData["Maxb"] = new SelectList(_context.NhaXuatBans, "MaXb", "Tenxb");
            ViewData["Maloai"] = new SelectList(_context.LoaiSaches, "MaLoai", "TenLoai");
            return View();
        }

        // POST: Saches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Masach,Maxb,Maloai,Tensach,Tacgia")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sach);
                await _sach.CreateSachAsync(sach);
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
            ViewData["Maxb"] = new SelectList(_context.NhaXuatBans, "MaXb", "Tenxb");
            ViewData["Maloai"] = new SelectList(_context.LoaiSaches, "MaLoai", "TenLoai");
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
                    await _sach.UpdateSachAsync(sach);  
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
            ViewData["Maxb"] = new SelectList(_context.NhaXuatBans, "MaXb", "Tenxb", sach.Maxb);
            ViewData["Maloai"] = new SelectList(_context.LoaiSaches, "MaLoai", "TenLoai", sach.Maloai);
            return View(sach);
        }

        // GET: Saches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context == null || _context.Saches == null)
            {
                return NotFound();
            }

            var sach = await _sach.GetSachByIdAsync(Convert.ToInt32(id));
      
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
                await _sach.DeleteSachAsync(id);
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