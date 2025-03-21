using HotelGestionale.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public DbSet<Cliente> Clienti { get; set; }
    public DbSet<Rooms> Camere { get; set; }
    public DbSet<Prenotazione> Prenotazioni { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
