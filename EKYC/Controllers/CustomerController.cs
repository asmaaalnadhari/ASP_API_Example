using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//------------------------------------------------
namespace EKYC.Controllers
{
  [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
      //------------------------------------------------------  
     private readonly CustomerContext _context;
      public CustomerController(CustomerContext context)
        {
            _context = context;
        }
       //----------------------------------------------------
       //GET:
      [HttpGet]
        public async Task<ActionResult<List<CustomerModel>>> Get()
        {
            return Ok(await _context.CustomersList.ToListAsync());
        }
      //GET(ID)
      [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModel>> Get(int id)
        {
            var customer = await _context.CustomersList.FindAsync(id);
            if (customer == null)
                return BadRequest("Cusomer  not found.");
            return Ok(customer);
        }
      //POST  
      [HttpPost]
          public async Task<ActionResult<List<CustomerModel>>> AddCustomer(CustomerModel customer)
    {
       var existingCustomer = await _context.CustomersList.FirstOrDefaultAsync(c => c.PhoneNo == customer.PhoneNo);

        if (existingCustomer != null)
        {
           return BadRequest("This customer already exists.");
         }
         _context.CustomersList.Add(customer);
         await _context.SaveChangesAsync();

        return Ok(await _context.CustomersList.ToListAsync());
        }
      //PUT
      [HttpPut]
        public async Task<ActionResult<List<CustomerModel>>> UpdateCustomer(CustomerModel request)
        {
            var dbCustomeer= await _context.CustomersList.FindAsync(request.Id);
            if (dbCustomeer == null)
                return BadRequest("Customer not found.");

            dbCustomeer.FullName = request.FullName;
            dbCustomeer.BirthDay = request.BirthDay;
            dbCustomeer.ImageSelfy = request.ImageSelfy; 
            await _context.SaveChangesAsync();
            return Ok(await _context.CustomersList.ToListAsync());
        }

      [HttpDelete("{id}")]
        public async Task<ActionResult<List<CustomerModel>>> Delete(int id)
        {
            var dbCustomer= await _context.CustomersList.FindAsync(id);
            if (dbCustomer == null)
                return BadRequest("Customer not found.");

            _context.CustomersList.Remove(dbCustomer);
            await _context.SaveChangesAsync();

            return Ok(await _context.CustomersList.ToListAsync());
        }

}
}