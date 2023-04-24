using Microsoft.AspNetCore.Mvc;
using Customer_Service.Models;
using Microsoft.EntityFrameworkCore;
using Customer_Service.Services;

namespace Customer_Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : Controller
{

    private ApiDbContext dbContext;

    public CustomerController(ApiDbContext context)
    {
        dbContext = context;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var Customers = await dbContext.Customers.ToListAsync();
        return Ok(Customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var Customer = await dbContext.Customers.FindAsync(id);
        return Ok(Customer);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Customer customer)
    {
        await dbContext.Customers.AddAsync(customer);
        await dbContext.SaveChangesAsync();

        return Ok("created");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
    {
        var Customer = await dbContext.Customers.FindAsync(id);
        if(Customer == null){
            return NotFound();
        }
        Customer.Name = customer.Name;
        Customer.Address = customer.Address;
        Customer.Orders = customer.Orders;
        await dbContext.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var Customer = await dbContext.Customers.FindAsync(id);
        if(Customer == null){
            return NotFound();
        }
        dbContext.Customers.Remove(Customer);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }

}