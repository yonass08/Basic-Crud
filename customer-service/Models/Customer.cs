namespace Customer_Service.Models;

public class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public List<int> Orders { get; set; } = null!;

}
