using Microsoft.AspNetCore.Mvc;
using Customer_Service.Models;
using Microsoft.EntityFrameworkCore;
using Customer_Service.Services;

namespace Customer_Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : Controller
{

    private ApiDbContext dbContext;

    public OrderController(ApiDbContext context)
    {
        dbContext = context;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var Orders = await dbContext.Orders.ToListAsync();
        return Ok(Orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var Order = await dbContext.Orders.FindAsync(id);
        return Ok(Order);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Order order)
    {
        var Customer = await dbContext.Customers.FindAsync(order.CustomerId);
        if(Customer == null){
            return NotFound();
        }
        await dbContext.Orders.AddAsync(order);
        await dbContext.SaveChangesAsync();
        Customer.Orders.Add(order.Id);
        await dbContext.SaveChangesAsync();

        return Ok("created");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Order order)
    {
        var Order = await dbContext.Orders.FindAsync(id);
        if(Order == null){
            return NotFound();
        }
        Order.Name = order.Name;
        Order.Count = order.Count;
        Order.Price = order.Price;
        await dbContext.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var Order = await dbContext.Orders.FindAsync(id);
        if(Order == null){
            return NotFound();
        }
        var Customer = await dbContext.Customers.FindAsync(Order.CustomerId);
        if(Customer == null){
            return NotFound();  
        }
        Customer.Orders.Remove(Order.Id);
        dbContext.Orders.Remove(Order);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }

}