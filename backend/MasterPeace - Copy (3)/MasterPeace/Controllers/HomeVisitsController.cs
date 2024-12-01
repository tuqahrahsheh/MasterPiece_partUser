using MasterPeace.DTOs;
using MasterPeace.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MasterPeace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeVisitsController : ControllerBase
    {
        private readonly MyDbContext _db;

        public HomeVisitsController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetVisits()
        {
            var visits = _db.HomeVisits.ToList();
            return Ok(visits);
        }

        [HttpGet("{id}")]
        public IActionResult GetVisit(int id)
        {
            var visit = _db.HomeVisits.Find(id);
            if (visit == null)
                return NotFound();

            return Ok(visit);
        }

        [HttpPost]
        public IActionResult BookVisit([FromForm] HomeVisitDTO dto)
        {
            var visit = new HomeVisit
            {
                UserId = dto.UserId,
                DoctorId = dto.DoctorId,
                VisitDate = dto.VisitDate,
                VisitTime = dto.VisitTime
                //ServiceFee = dto.ServiceFee,
                //DiscountAmount = dto.DiscountAmount,
                //Status = dto.Status
            };

            _db.HomeVisits.Add(visit);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetVisit), new { id = visit.VisitId }, visit);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVisit(int id, [FromForm] HomeVisitDTO dto)
        {
            var visit = _db.HomeVisits.Find(id);
            if (visit == null)
                return NotFound();

            visit.UserId = dto.UserId;
            visit.DoctorId = dto.DoctorId;
            visit.VisitDate = dto.VisitDate;
            visit.VisitTime = dto.VisitTime;
            visit.ServiceFee = dto.ServiceFee;
            visit.DiscountAmount = dto.DiscountAmount;
            visit.Status = dto.Status;

            _db.SaveChanges();
            return Ok(visit);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVisit(int id)
        {
            var visit = _db.HomeVisits.Find(id);
            if (visit == null)
                return NotFound();

            _db.HomeVisits.Remove(visit);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
