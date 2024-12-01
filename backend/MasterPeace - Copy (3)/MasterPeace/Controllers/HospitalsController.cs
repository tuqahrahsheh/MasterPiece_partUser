using MasterPeace.DTOs;
using MasterPeace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MasterPeace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    { 
        private readonly MyDbContext _db;
        
        public HospitalsController (MyDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult GetHospitals()
        {
            var hospitals = _db.Hospitals.ToList();
            return Ok(hospitals);
        }

        [HttpGet("{id}")]
        public IActionResult GetHospital(int id)
        {
            var hospital = _db.Hospitals.Find(id);
            if (hospital == null) return NotFound();
            return Ok(hospital);
        }

        //[HttpPost]
        //public IActionResult AddHospital(Hospital hospital)
        //{
        //    _db.Hospitals.Add(hospital);
        //    _db.SaveChanges();
        //    return CreatedAtAction(nameof(GetHospital), new { id = hospital.HospitalId }, hospital);
        //}

        [HttpPost]
        public IActionResult addHospital([FromForm] HospitalsDTO dTO)
        {
            var addHospital = new Hospital
            {
                HospitalName = dTO.HospitalName,
                Address = dTO.Address,
                Latitude = dTO.Latitude,
                Longitude = dTO.Longitude,
                Services = dTO.Services,
                Rating = dTO.Rating,
                PhoneNumber = dTO.PhoneNumber,
                WorkingHours = dTO.WorkingHours
            };
            _db.Hospitals.Add(addHospital);
            _db.SaveChanges();
            return Ok();
        }
    }
}