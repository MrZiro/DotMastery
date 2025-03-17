using Dot.DataAccess.Data;
using Dot.Models.Models;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing.Imaging;
namespace DotMastery.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();

                // Generate QR Code
                try
                {
                    string qrCodeData = $"https://localhost:7008/api/qr/scan?id={obj.id}";
                    using (var qrGenerator = new QRCodeGenerator())
                    {
                        var qrCodeDataObject = qrGenerator.CreateQrCode(qrCodeData, QRCodeGenerator.ECCLevel.Q);
                        using (var qrCode = new QRCode(qrCodeDataObject))
                        {
                            using (var bitmap = qrCode.GetGraphic(20))
                            {
                                var randomString = Guid.NewGuid().ToString("N").Substring(0, 8);
                                var qrCodeFilePath = Path.Combine(
                                    Directory.GetCurrentDirectory(),
                                    "wwwroot", "images",
                                    $"category_{obj.id}_{randomString}.png"
                                );
                                bitmap.Save(qrCodeFilePath, ImageFormat.Png);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, "Unable to generate QR code.");
                    return View(obj);
                }

                return RedirectToAction("Index", "Category");
            }
            return View();
        }



        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
    }
}