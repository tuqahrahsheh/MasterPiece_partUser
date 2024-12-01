using MasterPeace.DTOs;
using MasterPeace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MasterPeace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly MyDbContext _db;

        public UserProfileController(MyDbContext db)
        {
            _db = db;
        }

        // Get user profile by ID
        [HttpGet("{id}")]
        public IActionResult GetUserProfile(int id)
        {
            var user = _db.Users
                .Include(u => u.Appointments)
                    .ThenInclude(a => a.Service)
                .FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var userProfile = new UserProfileDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role,
                Appointments = user.Appointments.Select(a => new AppointmentDTO
                {
                    AppointmentID = a.AppointmentId,
                    ServiceName = a.Service.ServiceName,
                    Status = a.Status,
                    Message = a.Message
                }).ToList()
            };

            return Ok(userProfile);
        }

        // Get user history of medical supply requests
        [HttpGet("requests/{userId}")]
        public IActionResult GetUserRequestHistory(int userId)
        {
            var requests = _db.MedicalSuppliesRequests
                .Where(r => r.UserId == userId)
                .Select(r => new MedicalSuppliesRequestDTO
                {
                    UserName = r.UserName,
                    Address = r.Address,
                    DeliveryTime = r.DeliveryTime,
                    MedicationName = r.MedicationName
                })
                .ToList();

            if (!requests.Any())
            {
                return NotFound(new { message = "No medical supply requests found for this user." });
            }

            return Ok(requests);
        }

        // Update user profile
        [HttpPut("UpdateUserProfile/{id}")]
        public async Task<IActionResult> UpdateUserProfile(int id, [FromForm] UserProfileDTO updatedUserDto)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            user.FirstName = updatedUserDto.FirstName;
            user.LastName = updatedUserDto.LastName;
            user.Email = updatedUserDto.Email;
            user.Phone = updatedUserDto.Phone;

            // Handle image update
            //if (updatedUserDto.Image != null)
            //{
            //    var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            //    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            //    var imageFilePath = Path.Combine(folder, updatedUserDto.Image.FileName);
            //    using (var stream = new FileStream(imageFilePath, FileMode.Create))
            //    {
            //        await updatedUserDto.Image.CopyToAsync(stream);
            //    }
            //    user.Image = updatedUserDto.Image.FileName;
            //}

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound(new { message = "User not found." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Helper method to check if a user exists
        private bool UserExists(int id)
        {
            return _db.Users.Any(u => u.UserId == id);
        }
    }
}
