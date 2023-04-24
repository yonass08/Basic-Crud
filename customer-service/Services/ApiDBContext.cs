using Microsoft.EntityFrameworkCore;
using Customer_Service.Models;

namespace Customer_Service.Services;

public class ApiDbContext: DbContext{

    public virtual DbSet<Customer> Customers {get; set;} = null!;
    public virtual DbSet<Order> Orders {get; set;}  = null!;
    public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options){
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        base.OnModelCreating(modelBuilder);
    }
}