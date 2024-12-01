using MasterPeace.DTOs;
using MasterPeace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterPeace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly MyDbContext _db;

        public AdminController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetAllAdmin")]
        public IActionResult GetAllAdmin()
        {
            var data = _db.Admins.ToList();

            return Ok(data);
        }



        [HttpPost("AddAdmin")]
        public IActionResult AddAdmin([FromForm] AdminReqDTO newAdmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var admin = new Admin
            {
                Email = newAdmin.Email,
                Name = newAdmin.Name,
            };

            _db.Admins.Add(admin);  
            _db.SaveChanges();      

            return Ok(new { message = "Admin added successfully!" });
        }
    }
}