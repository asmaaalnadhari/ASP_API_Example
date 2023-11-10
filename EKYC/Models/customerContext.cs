using Customer.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer.Models;


public class CustomerContext : DbContext
{
    public CustomerContext(DbContextOptions<CustomerContext> options)
        : base(options)
    {
    }

    public DbSet<CustomerModel> CustomersList { get; set; } = null!;
}