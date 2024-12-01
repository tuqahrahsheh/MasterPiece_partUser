using MasterPeace.DTOs;
using MasterPeace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterPeace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase

    {
        private readonly MyDbContext _db;

        public FeedbackController(MyDbContext db)
        {
            _db = db;
        }


        //////////////////////////////////////////////////////


        [HttpPost("AddTestimonial/{id}")]
        public IActionResult Addtestimonial(int id, [FromBody] AddtestimonialDTO addtestimonialDTO)
        {
            if (id == null || id == 0)
            {
                return BadRequest("The id is null or 0 here");
            }

            var user = _db.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var addtestimonial = new Testimonial
            {
                UserId = id,
                Firstname = user.FirstName,
                Lastname = user.LastName,
                Email = user.Email,
                TheTestimonials = addtestimonialDTO.TheTestimonials
            };
            _db.Testimonials.Add(addtestimonial);
            _db.SaveChanges();
            return Ok();
        }
        [HttpGet("GetAllAcceptedTestimonial")]
        public IActionResult GetAllAcceptedTestimonial()
        {
            var testmonials = _db.Testimonials.Where(a => a.Isaccepted == true).ToList();
            var lesttestimonial = new List<GetAcceptTestimonialDTO>();
            foreach (var item in testmonials)
            {
                var accepttestimonial = new GetAcceptTestimonialDTO
                {
                    username = item.Firstname + " " + item.Lastname,

                    TheTestimonials = item.TheTestimonials,
                    Email = item.Email,
                };
                lesttestimonial.Add(accepttestimonial);
            }

            return Ok(lesttestimonial);

        }



        ////////////////////////////////testimonial///////////
        [HttpGet("GetAllTestimonials")]
        public IActionResult GetAllTestimonials()
        {
            var testimonials = _db.Testimonials.OrderBy(m => m.Isaccepted).ToList();
            var lestTestimonials = new List<GetAllTestimonialsDTO>();

            foreach (var item in testimonials)
            {
                var testimonial = new GetAllTestimonialsDTO
                {
                    Id = item.Id,
                    username = item.Firstname + " " + item.Lastname,
                    Email = item.Email,
                    TheTestimonials = item.TheTestimonials,
                    Isaccepted = item.Isaccepted
                };
                lestTestimonials.Add(testimonial);
            }
            return Ok(lestTestimonials);
        }

        [HttpDelete("DeleteTestimonial/{id}")]
        public IActionResult DeleteTestimonial(int id)
        {
            if (id <= 0)
            {
                return BadRequest("You can not use 0 or negative value for id");
            }

            var testmonial = _db.Testimonials.FirstOrDefault(u => u.Id == id);
            if (testmonial == null)
            {
                return NotFound();
            }
            _db.Testimonials.Remove(testmonial);
            _db.SaveChanges();
            return Ok();
        }
        [HttpPut("AcceptTestimonial/{id}")]
        public IActionResult AcceptTestimonial(int id)
        {
            if (id <= 0)
            {
                return BadRequest("You can not use 0 or negative value for id");
            }
            var testimonial = _db.Testimonials.FirstOrDefault(u => u.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }
            testimonial.Isaccepted = true;
            _db.Testimonials.Update(testimonial);
            _db.SaveChanges();
            return Ok();
        }
    }
}