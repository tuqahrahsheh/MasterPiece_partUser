using MasterPeace.DTOs;
using MasterPeace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace MasterPeace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeDoctorsController : ControllerBase
    { private readonly MyDbContext _db;

        public HomeDoctorsController (MyDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult GetDoctors()
        {
            var doctors = _db.HomeDoctors.ToList();
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public IActionResult GetDoctor(int id)
        {
            var doctor = _db.HomeDoctors.Find(id);
            if (doctor == null) return NotFound();
            return Ok(doctor);
        }

        //[HttpPost]
        //public IActionResult AddDoctor(HomeDoctor doctor)
        //{
        //    _db.HomeDoctors.Add(doctor);
        //    _db.SaveChanges();
        //    return CreatedAtAction(nameof(GetDoctor), new { id = doctor.DoctorId }, doctor);
        //}
        [HttpPost]
        public IActionResult adddoctor([FromForm] HomeDoctorDTORequest DTO)
        {
            var doctor = new HomeDoctor
            {
                FullName = DTO.FullName,
                Specialty = DTO.Specialty,
                ExperienceYears = DTO.ExperienceYears,
                PhoneNumber = DTO.PhoneNumber,
                Rating = DTO.Rating,
                Availability = DTO.Availability,
            };

            _db.HomeDoctors.Add(doctor);
            _db.SaveChanges();

            return Ok(doctor);
        }
    }
}