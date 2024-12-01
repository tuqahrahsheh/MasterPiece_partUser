using MasterPeace.DTOs;
using MasterPeace.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace MasterPeace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalSuppliesController : ControllerBase
    {
        private readonly MyDbContext _db;

        public MedicalSuppliesController(MyDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult AddMedicalSupplies([FromForm] MedicalSuppliesRequestDTO DTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string filePath = null;

            if (DTO.PrescriptionFile != null && DTO.PrescriptionFile.Length > 0)
            {
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(DTO.PrescriptionFile.FileName)}";
                filePath = Path.Combine(uploadsDir, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    DTO.PrescriptionFile.CopyTo(fileStream);
                }
            }

            var addMedicalSupplies = new MedicalSuppliesRequest
            {
                UserId = DTO.UserId,
                UserName = DTO.UserName,
                Address = DTO.Address,
                DeliveryTime = DTO.DeliveryTime,
                MedicationName = DTO.MedicationName,
                PrescriptionFilePath = filePath
            };

            try
            {
                _db.MedicalSuppliesRequests.Add(addMedicalSupplies);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while saving the data.", error = ex.Message });
            }

            return CreatedAtAction(nameof(GetMedicalById), new { id = addMedicalSupplies.RequestId }, addMedicalSupplies);
        }

        [HttpGet("getalldata")]
        public IActionResult GetAllData([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var data = _db.MedicalSuppliesRequests.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return Ok(data);
        }

        [HttpGet("getRequestById/{id}")]
        public IActionResult GetMedicalById(int id)
        {
            var medicalById = _db.MedicalSuppliesRequests.FirstOrDefault(x => x.RequestId == id);
            if (medicalById == null)
            {
                return NotFound(new { message = "Request not found." });
            }
            return Ok(medicalById);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRequest(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var req = _db.MedicalSuppliesRequests.FirstOrDefault(y => y.RequestId == id);
            if (req == null)
            {
                return NotFound();
            }
            _db.MedicalSuppliesRequests.Remove(req);
            _db.SaveChanges();
            return Ok(new { message = "Request deleted successfully." });
        }
    }
}