using MasterPeace.DTOs;
using MasterPeace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MasterPeace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly MyDbContext _db;

        public AppointmentsController(MyDbContext db)
        {
            _db = db;
        }
        [HttpPost("Order")]
        public IActionResult Order([FromForm] AppointmentRequestDTO DTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _db.Users.Find(DTO.UserID);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var service = _db.Services.Find(DTO.ServiceID);
            if (service == null)
            {
                return NotFound(new { message = "Service not found." });
            }

            var appointment = new Appointment
            {
                FirstName = DTO.FirstName,
                LastName = DTO.LastName,
                Phone = DTO.Phone,
                Message = DTO.Message,
                UserId = DTO.UserID, 
                ServiceId = DTO.ServiceID
            };

            _db.Appointments.Add(appointment);
            _db.SaveChanges();

            return Ok("Order booked successfully!");
        }


        [HttpGet("GetAllOrdersAdmin")]
        
            public IActionResult GetAllOrdersAdmin()
            {
                var data = _db.Appointments
                    .Include(a => a.User)
                    .Select(x => new AdminOrderHistiryOrdersDTO
                    {
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        UserId = x.User.UserId,
                        ServiceId = x.ServiceId,
                        Status = x.Status,
                        Message = x.Message,
                        Phone = x.Phone
                    })
                    .ToList();
                return Ok(data);

            }
        [HttpGet("{UserId}")]
        public IActionResult GetAllOrdersByUserId(int UserId)
        {
            var orders = _db.Appointments
                            .Where(order => order.UserId == UserId)
                            .ToList();

            if (orders == null || !orders.Any())
            {
                return NotFound("No orders found for this user.");
            }

            return Ok(orders);
        }


        [ HttpPut("{id}")]
        public IActionResult UpdateOrder (int id, [FromForm ] AppointmentUpdateDTO dTO)



        { 
            
            if (!ModelState.IsValid)
            {

            return BadRequest(ModelState); 
            }    
            var data = _db.Appointments.Find( id);
            if (data == null)
            {
                return NotFound(new { message = "Appointment not found." });
            }


            //data.Date = DateOnly.FromDateTime(dTO.Date);
            //data.Time = TimeOnly.FromTimeSpan(dTO.Time);
            data.Status = dTO.Status;
            data.Message = dTO.Message;

            _db.Appointments.Update(data);
            _db.SaveChanges();


            return Ok("Order updated successfully.");
        
        }




        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            var appointment = _db.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            _db.Appointments.Remove(appointment);
            _db.SaveChanges();

            return Ok(new { message = "Order deleted successfully." });
        }
    }
}
