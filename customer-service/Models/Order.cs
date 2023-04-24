using Microsoft.EntityFrameworkCore;

namespace Customer_Service.Models;

public class Order
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Count { get; set; }

    public float Price { get; set; }

    public int CustomerId {get; set;}

}
