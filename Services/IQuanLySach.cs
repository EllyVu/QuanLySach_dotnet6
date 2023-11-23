using Microsoft.EntityFrameworkCore;
using QuanLySach.Models;
using QuanLySach.Services;


namespace QuanLySach.Services
{
    public interface IQuanLySach
    {
        Task<List<Sach>> GetAllSachAsync(string searchString, string maSach);
        Task<Sach> GetSachByIdAsync(int? id);
        Task CreateSachAsync(Sach sach);
        Task UpdateSachAsync(Sach sach);
        Task DeleteSachAsync(int masach);
    }

    public class QuanLySachService : IQuanLySach
    {
        private readonly QlsContext _context;

        public QuanLySachService(QlsContext QlsContext)
        {
            _context = QlsContext;
        }

        public async Task<List<Sach>> GetAllSachAsync(string searchString, string maSach)
        {
            var saches = from s in _context.Saches
                         .Include(m => m.NhaXuatBan)
                         .Include(m => m.LoaiSach)
                         select s;
            return await saches.ToListAsync();
        }

        public async Task<Sach> GetSachByIdAsync(int? id)
        {
            var sach = await _context.Saches
                 .Include(s => s.NhaXuatBan)
                 .Include(s => s.LoaiSach)
                 .FirstOrDefaultAsync(m => m.Masach == id);
                return sach;
        }
        public async Task CreateSachAsync(Sach sach)
        {
            await _context.Saches.AddAsync(sach);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSachAsync(Sach sach)
        {
            _context.Entry(sach).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        public async Task DeleteSachAsync(int masach)
        {
            var sach = await _context.Saches.FindAsync(masach);
            _context.Saches.Remove(sach);
            await _context.SaveChangesAsync();          
        }
    }

}
