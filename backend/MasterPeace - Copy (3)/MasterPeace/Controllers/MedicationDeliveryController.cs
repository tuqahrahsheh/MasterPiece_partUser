using MasterPeace.DTOs;
using MasterPeace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterPeace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationDeliveryController : ControllerBase

    {
        private readonly MyDbContext _context;

        public MedicationDeliveryController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDeliveries()
        {
            var deliveries = _context.MedicationDeliveries.ToList();
            return Ok(deliveries);
        }

        //[HttpGet]
        //public IActionResult Get() { 

        //var delevaries = _context.MedicationDeliveries.ToList();
        //}


        [HttpGet("{id}")]
        public IActionResult GetDelivery(int id)
        {
            var delivery = _context.MedicationDeliveries.Find(id);
            if (delivery == null) return NotFound();
            return Ok(delivery);
        }

        //[HttpPost]
        //public IActionResult RequestDelivery(MedicationDelivery delivery)
        //{
        //    _context.MedicationDeliveries.Add(delivery);
        //    _context.SaveChanges();
        //    return CreatedAtAction(nameof(GetDelivery), new { id = delivery.DeliveryId }, delivery);
        //}
        [HttpPost]
        public IActionResult RequestDelivery([FromForm] MedicationDelevaryDTO dTO)
        {

            var RequestDelevary = new MedicationDelivery
            {
                UserId = dTO.UserId,
                Address = dTO.Address,
                DeliveryDate = dTO.DeliveryDate,
                DeliveryFee = dTO.DeliveryFee,
                TotalAmount = dTO.TotalAmount,
                Status = dTO.Status,
            };

            _context.MedicationDeliveries.Add(RequestDelevary);
            _context.SaveChanges();
            return Ok(RequestDelevary);

        }

    }
}