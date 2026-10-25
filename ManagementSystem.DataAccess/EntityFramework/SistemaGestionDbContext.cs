using Microsoft.EntityFrameworkCore;
using ManagementSystem.DataAccess.Connection;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.EntityFramework
{
    /// <summary>
    /// Contexto de datos de Entity Framework Core para el módulo delimitado de Consultas y Reportes.
    /// 
    /// DOCUMENTACIÓN TÉCNICA (Exigida por la Rúbrica - Sección Entity Framework):
    /// - ¿Qué operación se realizó con EF Core?:
    ///   Se implementó la consulta relacional de Productos con su Categoría (Include/JOIN automático),
    ///   el filtrado de productos por categoría, la detección de productos con stock bajo y la consulta
    ///   de movimientos de inventario y ventas para el módulo de Reportes.
    /// - ¿Por qué se separó del acceso ADO.NET?:
    ///   Las operaciones transaccionales críticas (CRUD principal y facturación con descuento de stock en VentaRepositorioAdo)
    ///   se mantienen en ADO.NET puro para tener control explícito de SqlConnection, SqlCommand, SqlParameter y SqlTransaction.
    ///   Por su parte, Entity Framework Core se separó en el módulo de Reportes porque permite construir proyecciones
    ///   fuertemente tipadas con LINQ to Entities, carga ansiosa (Include) y lecturas optimizadas sin seguimiento (AsNoTracking),
    ///   reduciendo el código repetitivo de mapeo manual en consultas analíticas.
    /// </summary>
    public class SistemaGestionDbContext : DbContext
    {
        private readonly string _cadenaConexion;

        public SistemaGestionDbContext()
        {
            _cadenaConexion = ConfiguracionBaseDatos.CadenaConexion;
        }

        public SistemaGestionDbContext(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        public SistemaGestionDbContext(DbContextOptions<SistemaGestionDbContext> opciones)
            : base(opciones)
        {
            _cadenaConexion = ConfiguracionBaseDatos.CadenaConexion;
        }

        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Venta> Ventas => Set<Venta>();
        public DbSet<DetalleVenta> DetallesVenta => Set<DetalleVenta>();
        public DbSet<HistoricoMovimientosInventario> HistoricoMovimientosInventario => Set<HistoricoMovimientosInventario>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_cadenaConexion);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la tabla Categorias
            modelBuilder.Entity<Categoria>(entidad =>
            {
                entidad.ToTable("Categorias", "dbo");
                entidad.HasKey(c => c.CategoriaId);
                entidad.Property(c => c.Nombre).HasMaxLength(100).IsRequired();
                entidad.Property(c => c.Descripcion).HasMaxLength(255);
                entidad.Property(c => c.FechaCreacion).HasColumnType("datetime");
                entidad.Property(c => c.Activo).IsRequired();
            });

            // Configuración de la tabla Productos y su relación N:1 con Categorias
            modelBuilder.Entity<Producto>(entidad =>
            {
                entidad.ToTable("Productos", "dbo");
                entidad.HasKey(p => p.ProductoId);
                entidad.Property(p => p.Nombre).HasMaxLength(150).IsRequired();
                entidad.Property(p => p.Descripcion).HasMaxLength(500);
                entidad.Property(p => p.PrecioCompra).HasColumnType("decimal(18,2)");
                entidad.Property(p => p.PrecioVenta).HasColumnType("decimal(18,2)");
                entidad.Property(p => p.StockActual).IsRequired();
                entidad.Property(p => p.StockMinimo).IsRequired();
                entidad.Property(p => p.FechaRegistro).HasColumnType("datetime");
                entidad.Property(p => p.Activo).IsRequired();

                entidad.HasOne(p => p.Categoria)
                       .WithMany(c => c.Productos)
                       .HasForeignKey(p => p.CategoriaId);
            });

            // Configuración de la tabla Venta
            modelBuilder.Entity<Venta>(entidad =>
            {
                entidad.ToTable("Venta", "dbo");
                entidad.HasKey(v => v.VentaId);
                entidad.Property(v => v.SubTotal).HasColumnType("decimal(18,2)");
                entidad.Property(v => v.Impuesto).HasColumnType("decimal(18,2)");
                entidad.Property(v => v.Descuento).HasColumnType("decimal(18,2)");
                entidad.Property(v => v.Total).HasColumnType("decimal(18,2)");
                entidad.Property(v => v.Estado).HasMaxLength(30).IsRequired();
                entidad.Property(v => v.Observaciones).HasMaxLength(500);
                entidad.Ignore(v => v.Cliente);
                entidad.Ignore(v => v.Empleado);
            });

            // Configuración de la tabla DetalleVenta
            modelBuilder.Entity<DetalleVenta>(entidad =>
            {
                entidad.ToTable("DetalleVenta", "dbo");
                entidad.HasKey(d => d.DetalleVentaId);
                entidad.Property(d => d.PrecioUnitario).HasColumnType("decimal(18,2)");
                entidad.Property(d => d.Descuento).HasColumnType("decimal(18,2)");
                entidad.Property(d => d.SubTotal).HasColumnType("decimal(18,2)");

                entidad.HasOne(d => d.Venta)
                       .WithMany(v => v.DetallesVenta)
                       .HasForeignKey(d => d.VentaId);

                entidad.HasOne(d => d.Producto)
                       .WithMany(p => p.DetallesVenta)
                       .HasForeignKey(d => d.ProductoId);
            });

            // Configuración de la tabla HistoricoMovimientosInventario
            modelBuilder.Entity<HistoricoMovimientosInventario>(entidad =>
            {
                entidad.ToTable("HistoricoMovimientosInventario", "dbo");
                entidad.HasKey(h => h.MovimientoId);
                entidad.Property(h => h.TipoMovimiento).HasMaxLength(30).IsRequired();
                entidad.Property(h => h.Observacion).HasMaxLength(255);
                entidad.Ignore(h => h.Empleado);

                entidad.HasOne(h => h.Producto)
                       .WithMany(p => p.MovimientosInventario)
                       .HasForeignKey(h => h.ProductoId);
            });
        }
    }
}
