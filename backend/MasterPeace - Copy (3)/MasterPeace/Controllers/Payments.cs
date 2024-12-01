using MasterPeace.DTOs;
using MasterPeace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MasterPeace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly MyDbContext _db;

        public PaymentController(MyDbContext db)
        {
            _db = db;
        }

        [HttpPost("submit")]
        public  IActionResult SubmitPayment([FromForm] PaymentRequestDTO paymentRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var payment = new PaymentRequest
            {
                FullName = paymentRequest.FullName,
                Email = paymentRequest.Email,
                Address = paymentRequest.Address,
                City = paymentRequest.City,
                ZipCode = paymentRequest.ZipCode,
                CardholderName = paymentRequest.CardholderName,
                CardNumber = paymentRequest.CardNumber,
                ExpiryMonth = paymentRequest.ExpiryMonth,
                ExpiryYear = paymentRequest.ExpiryYear,
                Cvv = paymentRequest.CVV,
                TotalAmount = paymentRequest.TotalAmount
            };

            _db.PaymentRequests.Add(payment);
             _db.SaveChangesAsync();




            return Ok(new { message = "Payment successfully submitted!" });
        }
        [HttpGet]
        public IActionResult GetPayments()
        {
            var payments = _db.PaymentRequests.ToList();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public IActionResult GetPayment(int id)
        {
            var payment = _db.PaymentRequests.Find(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }
    }
}
