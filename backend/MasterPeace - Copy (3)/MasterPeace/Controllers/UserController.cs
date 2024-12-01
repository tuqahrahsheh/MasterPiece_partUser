using MasterPeace.DTOs;
using MasterPeace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MasterPeace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _db;

        public UsersController(MyDbContext db)
        {
            _db = db;
        }

        [HttpPost("register")]
        public IActionResult Register([FromForm] UserRequestDTO model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("Password and Confirm Password do not match.");
            }

            var existingUser = _db.Users.FirstOrDefault(u => u.Email == model.Email);
            if (existingUser != null)
            {
                return Conflict("Email already exists.");
            }

            User newUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Phone = model.Phone,
                Role = model.Role
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();
            

            return Ok(new { message = "User registered successfully!" });
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _db.Users.FirstOrDefault(u => u.Email == model.Email);

            if (user == null || user.Password != model.Password)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            return Ok(user);
        }




        [HttpGet("profile/{email}")]
        public IActionResult GetUserProfile(string email)
        {
            var user = _db.Users
                .Include(u => u.Appointments)
                    .ThenInclude(a => a.Service)
                
                    .FirstOrDefault(u => u.Email == email);

            if (user == null )
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
                    //Date = a.Date,
                    //Time = a.Time,
                    Status = a.Status,  
                    Message = a.Message     
                }).ToList(),
                
            };

            return Ok(userProfile);
        }


        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var data = _db.Users.ToList();
            return Ok(data);
        }



        [HttpGet("ShowUserByID/{id:int}")]
        public IActionResult GetUserById(int id)
        {
            var data = _db.Users.Find(id);
            return Ok(data);
        }

        [HttpGet("GetUserHistory/{userId}")]
        public IActionResult GetUserHistory(int userId)
        {
            var userHistory = _db.Users
                .Where(u => u.UserId == userId)
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    u.Email,
                    u.Phone,
                    Appointments = u.Appointments.ToList(),
                    HomeVisits = u.HomeVisits.ToList(),
                    MedicalSuppliesRequests = u.MedicalSuppliesRequests.Where(u => u.UserId == userId).ToList(),
                    MedicationDeliveries = u.MedicationDeliveries.ToList(),
                    Subscriptions = u.Subscriptions.ToList()
                })
                .FirstOrDefault();

            if (userHistory == null)
            {
                return NotFound("User history not found.");
            }

            return Ok(userHistory);
        }

    }
}
