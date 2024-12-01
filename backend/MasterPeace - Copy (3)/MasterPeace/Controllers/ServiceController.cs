using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasterPeace.Models;
using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;
using MasterPeace.DTOs;

namespace MasterPeace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly MyDbContext _db;

        public  ServiceController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAllServices()
        {
            var services = _db.Services.ToList();
            return Ok(services);
        }



        [HttpGet("{id}")]
        public IActionResult GetServiceById(int id)
        {
            var service = _db.Services.AsNoTracking().FirstOrDefault(s => s.ServiceId == id);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateService(int id, [FromForm] Service service)
        {
            var serviceToUpdate = _db.Services.FirstOrDefault(s => s.ServiceId == id);
            if (serviceToUpdate == null)
            {
                return NotFound();
            }

            serviceToUpdate.ServiceName = service.ServiceName;
            serviceToUpdate.Description = service.Description;

            _db.SaveChanges();

            return Ok(serviceToUpdate);
        }

        [HttpPost]
        public IActionResult CreateService([FromForm] ServiceResponseDTO service)
        {
            

            var addservice = new Service
            {
                 ServiceName = service.ServiceName,
                 Description = service.Description,
            };
                
            _db.Services.Add(addservice);
           _db.SaveChanges();
            return Ok(service);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            var serviceToDelete = _db.Services.Find(id);
            if (serviceToDelete == null)
            {
                return NotFound();
            }

            _db.Services.Remove(serviceToDelete);
            _db.SaveChanges();
            return Ok();
        }
    }
}
