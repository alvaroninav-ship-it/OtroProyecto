using Microsoft.EntityFrameworkCore;
using OtroProyecto.Models;

namespace OtroProyecto.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Person> Persons { get; set; }


    }
}
