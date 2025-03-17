using Dot.DataAccess.Data;
using Dot.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DotMastery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QrController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public QrController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("scan")]
        public IActionResult Scan(int id)
        {
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(new { Success = true, Category = category });
        }
    }
}