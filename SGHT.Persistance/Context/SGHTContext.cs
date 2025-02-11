using System.Net.Security;
using Microsoft.EntityFrameworkCore;
using SGHT.Domain.Entities;

namespace SGHT.Persistance.Context
{
    public class SGHTContext : DbContext
    {
        public SGHTContext(DbContextOptions<SGHTContext> options) : base(options)
        {

        }

        public DbSet<Catetgoria> catetgorias { get; set; }
        public DbSet<Cliente> clientes { get; set; }
        public DbSet<EstadoHabitacion> estadoHabitaciones { get; set; }
        public DbSet<Habitacion> habitaciones { get; set; }
        public DbSet<Piso> piso { get; set; }
        public DbSet<Recepcion> recepcion { get;set; }
        public DbSet<RolUsuario> rolUsuarios { get; set; }
        public DbSet<Servicios> servicios { get; set; }
        public DbSet<Tarifas> tarifas { get; set; }
        public DbSet<Usuarios> usuarios { get; set; }
    }
}
