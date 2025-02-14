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
<<<<<<< HEAD
        public DbSet<Catetgoria> Catetgorias { get; set; }
=======

        public DbSet<Categoria> Categorias { get; set; }
>>>>>>> main
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<EstadoHabitacion> EstadoHabitaciones { get; set; }
        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<Piso> Piso { get; set; }
        public DbSet<Recepcion> Recepcion { get;set; }
        public DbSet<RolUsuario> RolUsuarios { get; set; }
        public DbSet<Servicios> Servicios { get; set; }
        public DbSet<Tarifas> Tarifas { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
