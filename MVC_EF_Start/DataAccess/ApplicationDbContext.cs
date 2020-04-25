using Microsoft.EntityFrameworkCore;
using MVC_EF_Start.Models;


namespace MVC_EF_Start.DataAccess
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Sentry> SentryEntries { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Fireball> FireballEntries { get; set; }
  }
}