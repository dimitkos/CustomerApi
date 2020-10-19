using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IReadOnlyList<Customer>> Get()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer == null)
                return NotFound();

            return customer;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Post(Customer customer)
        {
            await _repository.AddAsync(customer);

            return CreatedAtAction("Get", new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();
            
            await _repository.UpdateAsync(customer);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            await _repository.DeleteAsync(customer);

            return customer;
        }
    }
}
