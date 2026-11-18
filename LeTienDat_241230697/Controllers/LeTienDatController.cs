using Microsoft.AspNetCore.Mvc;
using LeTienDat_241230697.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace LeTienDat_241230697.Controllers
{
    public class LeTienDatController : Controller
    {
        private readonly QLNhaSachContext _db;
        public LeTienDatController(QLNhaSachContext db)
        {
            _db = db;
        }
        private async Task NapTheLoai()
        {
            ViewBag.TheLoais = await _db.TheLoais
                .OrderBy(t => t.MaTl)
                .ToListAsync();
        }
        public async Task<IActionResult> Index()
        {
            await NapTheLoai();
            var sach = await _db.Saches
                .Where(s => s.Gia.HasValue && s.Gia.Value >= 100)
                .Include(s => s.MaTlNavigation)
                .OrderBy(s => s.MaSach)
                .ToListAsync();
            return View(sach);
        }
        [HttpGet]
        public async Task<IActionResult> LocTheoTheLoai(int id)
        {
            var sach = await _db.Saches
                .Where(s => s.MaTl == id && s.Gia.HasValue && s.Gia.Value >= 100)
                .Include(s => s.MaTlNavigation)
                .OrderBy(s => s.MaSach)
                .ToListAsync();
            return PartialView("_DanhSachSach", sach);
        }
        [HttpGet]
        public async Task<IActionResult> ThemSach()
        {
            await NapTheLoai();
            await NapDanhSachTheLoai();
            return View(new Sach());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemSach(Sach sach)
        {
            if (!ModelState.IsValid)
            {
                await NapTheLoai();
                await NapDanhSachTheLoai();
                return View(sach);
            }

            _db.Saches.Add(sach);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private async Task NapDanhSachTheLoai()
        {
            ViewBag.DanhSachTheLoai = new SelectList(
                await _db.TheLoais
                .OrderBy(t => t.TenTl)
                .ToListAsync(),
                "MaTl",
                "TenTl"
                );
        }
    }
}
