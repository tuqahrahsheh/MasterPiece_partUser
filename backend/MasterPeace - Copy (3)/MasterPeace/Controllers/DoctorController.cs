using MasterPeace.DTOs;
using MasterPeace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace MasterPeace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly MyDbContext _db;
        private readonly string _uploadFolderPath;

        public DoctorsController(MyDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _uploadFolderPath = Path.Combine(env.WebRootPath, "uploads");
        }

        // Get all doctors
        [HttpGet]
        public IActionResult GetDoctors()
        {
            var doctors = _db.HomeDoctors.Select(d => new
            {
                d.DoctorId,
                d.FullName,
                d.Specialty,
                d.Rating,
                d.Availability,
              //  DoctorImageUrl = Path.Combine("/uploads", d.im)
            }).ToList();

            return Ok(doctors);
        }

        // Add a new doctor
        [HttpPost]
        public IActionResult AddDoctor([FromForm] DoctorRequestDTO2 doctorRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (doctorRequest.DoctorImage == null || doctorRequest.DoctorImage.Length == 0)
            {
                return BadRequest("Doctor image is required.");
            }

            if (!Directory.Exists(_uploadFolderPath))
            {
                Directory.CreateDirectory(_uploadFolderPath);
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(doctorRequest.DoctorImage.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return BadRequest("Only image files (.jpg, .jpeg, .png, .gif) are allowed.");
            }

            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(doctorRequest.DoctorImage.FileName)}";
            var filePath = Path.Combine(_uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                doctorRequest.DoctorImage.CopyTo(stream);
            }

            var doctor = new DoctorRequest
            {
                RequestId = doctorRequest.RequestId,
                Name = doctorRequest.Name,
                Specialty = doctorRequest.Specialty,
                Description = doctorRequest.Description,
                ContactEmail = doctorRequest.ContactEmail,
                DoctorImage = fileName
            };
            
            _db.DoctorRequests.Add(doctor);
            _db.SaveChanges();

            return Ok(new { message = "Doctor added successfully!" });
        }

        // Update a doctor
        [HttpPut("{id}")]
        public IActionResult UpdateDoctor(int id, [FromForm] DoctorRequestDTO2 doctorRequest)
        {
            var doctor = _db.DoctorRequests.FirstOrDefault(d => d.RequestId == id);
            if (doctor == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(doctorRequest.Name)) doctor.Name = doctorRequest.Name;
            if (!string.IsNullOrEmpty(doctorRequest.Specialty)) doctor.Specialty = doctorRequest.Specialty;
            if (!string.IsNullOrEmpty(doctorRequest.Description)) doctor.Description = doctorRequest.Description;
            if (!string.IsNullOrEmpty(doctorRequest.ContactEmail)) doctor.ContactEmail = doctorRequest.ContactEmail;

            if (doctorRequest.DoctorImage != null && doctorRequest.DoctorImage.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(doctorRequest.DoctorImage.FileName)}";
                var filePath = Path.Combine(_uploadFolderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    doctorRequest.DoctorImage.CopyTo(stream);
                }

                doctor.DoctorImage = fileName;
            }

            _db.Entry(doctor).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok(new { message = "Doctor updated successfully!" });
        }
    }
}
