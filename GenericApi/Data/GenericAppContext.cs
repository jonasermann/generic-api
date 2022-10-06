using Microsoft.EntityFrameworkCore;
using GenericApi.Models;

namespace GenericApi.Data;

public class GenericAppContext : DbContext
{
    public GenericAppContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Person>? Persons { get; set; }

    public DbSet<Job>? Jobs { get; set; }
}
