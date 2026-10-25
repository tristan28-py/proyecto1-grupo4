using System;
using System.Collections.Generic;
using System.Linq;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.Entities;
using SistemaGestion.Logica.Servicios;
using Xunit;

namespace SistemaGestion.Pruebas
{
    /// <summary>
    /// Pruebas unitarias sobre la capa de lógica de negocio (SistemaGestion.Logica)
    /// para verificar las reglas obligatorias de la rúbrica de evaluación.
    /// </summary>
    public class PruebasReglasNegocio
    {
        // =========================================================================
        // 1. Pruebas de Stock Disponible (No permitir vender más que el stock)
        // =========================================================================

        [Fact]
        public void ValidarStockDisponible_CuandoStockEsSuficiente_NoLanzaExcepcion()
        {
            // Preparar
            var repositorioProductosFalso = new RepositorioProductosEnMemoria();
            var repositorioVentasFalso = new RepositorioVentasEnMemoria();
            var servicioHistorial = new ServicioHistorial();

            var producto = new Producto
            {
                ProductoId = 1,
                CategoriaId = 1,
                Nombre = "Teclado Mecánico",
                PrecioCompra = 20000m,
                PrecioVenta = 35000m,
                StockActual = 10,
                StockMinimo = 2,
                Activo = true
            };
            repositorioProductosFalso.Insertar(producto);

            var servicioVentas = new ServicioVentas(repositorioVentasFalso, repositorioProductosFalso, servicioHistorial);

            // Actuar y Afirmar (no debe lanzar excepción al solicitar 5 unidades teniendo 10 en stock)
            var excepcion = Record.Exception(() => servicioVentas.ValidarStockDisponible(producto, 5));
            Assert.Null(excepcion);
        }

        [Fact]
        public void ValidarStockDisponible_CuandoCantidadSuperaStock_LanzaExcepcionDeNegocio()
        {
            // Preparar
            var repositorioProductosFalso = new RepositorioProductosEnMemoria();
            var repositorioVentasFalso = new RepositorioVentasEnMemoria();
            var servicioHistorial = new ServicioHistorial();

            var producto = new Producto
            {
                ProductoId = 1,
                CategoriaId = 1,
                Nombre = "Monitor 24 Pulgadas",
                PrecioCompra = 60000m,
                PrecioVenta = 95000m,
                StockActual = 3,
                StockMinimo = 1,
                Activo = true
            };
            repositorioProductosFalso.Insertar(producto);

            var servicioVentas = new ServicioVentas(repositorioVentasFalso, repositorioProductosFalso, servicioHistorial);

            // Actuar y Afirmar (solicitar 5 unidades cuando solo hay 3 disponibles debe fallar)
            var excepcion = Assert.Throws<InvalidOperationException>(() =>
                servicioVentas.ValidarStockDisponible(producto, 5));

            Assert.Contains("superior al stock disponible", excepcion.Message);
        }

        // =========================================================================
        // 2. Pruebas de Cálculo de Subtotal
        // =========================================================================

        [Fact]
        public void CalcularSubTotalLinea_ConCantidadYPrecioValidos_RetornaMultiplicacionExacta()
        {
            // Preparar
            var servicioVentas = CrearServicioVentasConRepositoriosEnMemoria();

            // Actuar
            decimal subTotal = servicioVentas.CalcularSubTotalLinea(4, 12500.50m);

            // Afirmar
            Assert.Equal(50002.00m, subTotal);
        }

        [Theory]
        [InlineData(0, 1000)]
        [InlineData(-3, 1000)]
        [InlineData(2, 0)]
        [InlineData(2, -500)]
        public void CalcularSubTotalLinea_ConCantidadOPrecioInvalidos_LanzaArgumentException(int cantidad, decimal precioUnitario)
        {
            // Preparar
            var servicioVentas = CrearServicioVentasConRepositoriosEnMemoria();

            // Actuar y Afirmar
            Assert.Throws<ArgumentException>(() =>
                servicioVentas.CalcularSubTotalLinea(cantidad, precioUnitario));
        }

        // =========================================================================
        // 3. Pruebas de Cálculo del Total de una Venta
        // =========================================================================

        [Fact]
        public void CalcularTotalVenta_ConVariosDetalles_RetornaSumaExactaDeSubtotales()
        {
            // Preparar
            var servicioVentas = CrearServicioVentasConRepositoriosEnMemoria();
            var detalles = new List<DetalleVenta>
            {
                new DetalleVenta { ProductoId = 1, Cantidad = 2, PrecioUnitario = 15000m }, // 30,000
                new DetalleVenta { ProductoId = 2, Cantidad = 3, PrecioUnitario = 5000m },  // 15,000
                new DetalleVenta { ProductoId = 3, Cantidad = 1, PrecioUnitario = 8500.75m } // 8,500.75
            };

            // Actuar
            decimal totalCalculado = servicioVentas.CalcularTotalVenta(detalles);

            // Afirmar
            Assert.Equal(53500.75m, totalCalculado);
        }

        [Fact]
        public void CalcularTotalVenta_SinDetalles_LanzaInvalidOperationException()
        {
            // Preparar
            var servicioVentas = CrearServicioVentasConRepositoriosEnMemoria();
            var detallesVacios = new List<DetalleVenta>();

            // Actuar y Afirmar (no se permite registrar ni calcular una venta sin detalles)
            Assert.Throws<InvalidOperationException>(() =>
                servicioVentas.CalcularTotalVenta(detallesVacios));
        }

        // =========================================================================
        // 4. Pruebas de Rechazo de Precios y Stock Inválidos en Productos
        // =========================================================================

        [Theory]
        [InlineData(0)]
        [InlineData(-1500)]
        public void AgregarProducto_ConPrecioVentaMenorOIgualACero_LanzaArgumentException(decimal precioVentaInvalido)
        {
            // Preparar
            var repositorioProductos = new RepositorioProductosEnMemoria();
            var servicioHistorial = new ServicioHistorial();
            var servicioProductos = new ServicioProductos(repositorioProductos, servicioHistorial);

            var productoInvalido = new Producto
            {
                CategoriaId = 1,
                Nombre = "Producto Precio Inválido",
                PrecioCompra = 1000m,
                PrecioVenta = precioVentaInvalido,
                StockActual = 10
            };

            // Actuar y Afirmar
            Assert.Throws<ArgumentException>(() => servicioProductos.AgregarProducto(productoInvalido));
        }

        [Fact]
        public void AgregarProducto_ConStockNegativo_LanzaArgumentException()
        {
            // Preparar
            var repositorioProductos = new RepositorioProductosEnMemoria();
            var servicioHistorial = new ServicioHistorial();
            var servicioProductos = new ServicioProductos(repositorioProductos, servicioHistorial);

            var productoStockNegativo = new Producto
            {
                CategoriaId = 1,
                Nombre = "Producto Stock Negativo",
                PrecioCompra = 1000m,
                PrecioVenta = 2000m,
                StockActual = -5
            };

            // Actuar y Afirmar
            Assert.Throws<ArgumentException>(() => servicioProductos.AgregarProducto(productoStockNegativo));
        }

        // =========================================================================
        // 5. Pruebas de Cola (Queue<Pedido>) en Orden FIFO (Primero en Entrar, Primero en Salir)
        // =========================================================================

        [Fact]
        public void ServicioPedidos_EncolarYAtenderPedidos_RespetaOrdenFifo()
        {
            // Preparar
            var servicioPedidos = new ServicioPedidos();
            servicioPedidos.LimpiarCola();

            // Actuar: Encolar dos pedidos en orden (Enqueue)
            Pedido primerPedido = servicioPedidos.EncolarPedido("Cliente Uno", "Mouse Inalámbrico", 1, "Entrega inmediata", 1, 1);
            Pedido segundoPedido = servicioPedidos.EncolarPedido("Cliente Dos", "Teclado USB", 2, "Recoge en tienda", 2, 2);

            // Afirmar: Cantidad en cola y próximo pedido (Peek)
            Assert.Equal(2, servicioPedidos.CantidadPendientes);
            Pedido? proximo = servicioPedidos.VerSiguientePedido();
            Assert.NotNull(proximo);
            Assert.Equal(primerPedido.PedidoId, proximo!.PedidoId);
            Assert.Equal("Cliente Uno", proximo.NombreCliente);

            // Atender el primer pedido (Dequeue) -> debe salir "Cliente Uno"
            Pedido primerAtendido = servicioPedidos.AtenderSiguientePedido();
            Assert.Equal("Cliente Uno", primerAtendido.NombreCliente);
            Assert.True(primerAtendido.Atendido);
            Assert.Equal(1, servicioPedidos.CantidadPendientes);

            // Atender el segundo pedido (Dequeue) -> debe salir "Cliente Dos"
            Pedido segundoAtendido = servicioPedidos.AtenderSiguientePedido();
            Assert.Equal("Cliente Dos", segundoAtendido.NombreCliente);
            Assert.True(segundoAtendido.Atendido);
            Assert.Equal(0, servicioPedidos.CantidadPendientes);
        }

        [Fact]
        public void ServicioPedidos_AtenderCuandoColaEstaVacia_LanzaInvalidOperationException()
        {
            // Preparar
            var servicioPedidos = new ServicioPedidos();
            servicioPedidos.LimpiarCola();

            // Actuar y Afirmar
            Assert.Throws<InvalidOperationException>(() => servicioPedidos.AtenderSiguientePedido());
        }

        // =========================================================================
        // 6. Pruebas de Pila (Stack<AccionHistorial>) en Orden LIFO (Último en Entrar, Primero en Salir)
        // =========================================================================

        [Fact]
        public void ServicioHistorial_RegistrarYDeshacerAcciones_RespetaOrdenLifo()
        {
            // Preparar
            var servicioHistorial = new ServicioHistorial();
            servicioHistorial.LimpiarHistorial();
            bool accion2Deshecha = false;

            // Actuar: Registrar dos acciones (Push)
            servicioHistorial.RegistrarAccion(
                modulo: "Productos",
                descripcion: "Primer producto creado",
                tipoAccion: "Creación");

            servicioHistorial.RegistrarAccion(
                modulo: "Ventas",
                descripcion: "Última venta registrada",
                tipoAccion: "Venta",
                entidadId: 10,
                accionDeshacer: () => accion2Deshecha = true);

            // Afirmar: Cantidad en pila y última acción en el tope (Peek)
            Assert.Equal(2, servicioHistorial.CantidadAcciones);
            AccionHistorial? tope = servicioHistorial.VerUltimaAccion();
            Assert.NotNull(tope);
            Assert.Equal("Última venta registrada", tope!.Descripcion);

            // Deshacer última acción (Pop) -> debe recuperar "Última venta registrada" primero
            AccionHistorial primeraDeshecha = servicioHistorial.DeshacerUltimaAccion();
            Assert.Equal("Última venta registrada", primeraDeshecha.Descripcion);
            Assert.True(accion2Deshecha);
            Assert.Equal(1, servicioHistorial.CantidadAcciones);

            // Deshacer siguiente acción (Pop) -> debe recuperar "Primer producto creado"
            AccionHistorial segundaDeshecha = servicioHistorial.DeshacerUltimaAccion();
            Assert.Equal("Primer producto creado", segundaDeshecha.Descripcion);
            Assert.Equal(0, servicioHistorial.CantidadAcciones);
        }

        [Fact]
        public void ServicioHistorial_DeshacerCuandoPilaEstaVacia_LanzaInvalidOperationException()
        {
            // Preparar
            var servicioHistorial = new ServicioHistorial();
            servicioHistorial.LimpiarHistorial();

            // Actuar y Afirmar
            Assert.Throws<InvalidOperationException>(() => servicioHistorial.DeshacerUltimaAccion());
        }

        // =========================================================================
        // 7. Pruebas de Consultas LINQ sobre List<T> y Polimorfismo POO
        // =========================================================================

        [Fact]
        public void ServicioProductos_ConsultasLinqSobreLista_FiltraYOrdenaCorrectamente()
        {
            // Preparar
            var repositorioProductos = new RepositorioProductosEnMemoria();
            var servicioHistorial = new ServicioHistorial();
            var servicioProductos = new ServicioProductos(repositorioProductos, servicioHistorial);

            repositorioProductos.Insertar(new Producto
            {
                ProductoId = 1,
                CategoriaId = 1,
                Nombre = "Laptop Gamer",
                PrecioCompra = 400000m,
                PrecioVenta = 650000m,
                StockActual = 8,
                StockMinimo = 3,
                Activo = true
            });
            repositorioProductos.Insertar(new Producto
            {
                ProductoId = 2,
                CategoriaId = 1,
                Nombre = "Cable HDMI",
                PrecioCompra = 1500m,
                PrecioVenta = 3500m,
                StockActual = 2,
                StockMinimo = 5,
                Activo = true
            });

            // Actuar: Filtrar stock bajo con LINQ (Where + OrderBy) y ordenar por precio y nombre
            List<Producto> stockBajo = servicioProductos.FiltrarProductosStockBajo();
            List<Producto> ordenadosPorPrecio = servicioProductos.OrdenarPorPrecioYNombre();

            // Afirmar
            Assert.Single(stockBajo);
            Assert.Equal("Cable HDMI", stockBajo[0].Nombre);
            Assert.Equal("Cable HDMI", ordenadosPorPrecio.First().Nombre);
            Assert.Equal("Laptop Gamer", ordenadosPorPrecio.Last().Nombre);
        }

        [Fact]
        public void JerarquiaPersona_PolimorfismoEInterfaces_RetornaComportamientoSobrescrito()
        {
            // Preparar: Lista polimórfica de la clase abstracta Persona
            List<Persona> personas = new List<Persona>
            {
                new Cliente
                {
                    PersonaId = 1,
                    ClienteId = 1,
                    Identificacion = "101110111",
                    NombreCompleto = "Ana Solís",
                    Telefono = "8888-1111",
                    Email = "ana@correo.com",
                    Direccion = "San José",
                    LimiteCredito = 500000m
                },
                new Empleado
                {
                    PersonaId = 2,
                    EmpleadoId = 1,
                    Identificacion = "202220222",
                    NombreCompleto = "Carlos Mora",
                    Telefono = "8888-2222",
                    Email = "carlos@empresa.com",
                    Direccion = "Heredia",
                    Puesto = "Analista",
                    Salario = 850000m
                },
                new Usuario
                {
                    PersonaId = 3,
                    EmpleadoId = 2,
                    UsuarioId = 1,
                    Identificacion = "303330333",
                    NombreCompleto = "Laura Vega",
                    Telefono = "8888-3333",
                    Email = "laura@empresa.com",
                    Direccion = "Cartago",
                    Puesto = "Administradora",
                    Salario = 950000m,
                    NombreUsuario = "lvega",
                    RolId = 1,
                    Rol = new Rol { RolId = 1, NombreRol = "Administrador" }
                }
            };

            // Actuar y Afirmar: Cada subclase sobrescribe ObtenerTipoPersona() y ObtenerResumenPerfil()
            Assert.Equal("Cliente", personas[0].ObtenerTipoPersona());
            Assert.Equal("Empleado", personas[1].ObtenerTipoPersona());
            Assert.Equal("Usuario", personas[2].ObtenerTipoPersona());
            Assert.All(personas, p => Assert.False(string.IsNullOrWhiteSpace(p.ObtenerResumenPerfil())));
        }

        // =========================================================================
        // Repositorios Falsos en Memoria (Mocks ligeros) para aislar la Lógica
        // =========================================================================

        private static ServicioVentas CrearServicioVentasConRepositoriosEnMemoria()
        {
            return new ServicioVentas(
                new RepositorioVentasEnMemoria(),
                new RepositorioProductosEnMemoria(),
                new ServicioHistorial());
        }

        private class RepositorioProductosEnMemoria : IProductoRepositorio
        {
            private readonly List<Producto> _productos = new List<Producto>();

            public IEnumerable<Producto> ObtenerTodos() => _productos.ToList();

            public IEnumerable<Producto> ObtenerActivos() => _productos.Where(p => p.Activo).ToList();

            public Producto? ObtenerPorId(int id) => _productos.FirstOrDefault(p => p.ProductoId == id);

            public IEnumerable<Producto> BuscarProductos(string terminoBusqueda) =>
                _productos.Where(p => p.Nombre.Contains(terminoBusqueda, StringComparison.OrdinalIgnoreCase)).ToList();

            public IEnumerable<Producto> ObtenerPorCategoria(int categoriaId) =>
                _productos.Where(p => p.CategoriaId == categoriaId).ToList();

            public int Insertar(Producto entidad)
            {
                if (entidad.ProductoId == 0)
                    entidad.ProductoId = _productos.Count + 1;
                _productos.Add(entidad);
                return entidad.ProductoId;
            }

            public bool Actualizar(Producto entidad)
            {
                var existente = ObtenerPorId(entidad.ProductoId);
                if (existente == null) return false;
                _productos.Remove(existente);
                _productos.Add(entidad);
                return true;
            }

            public bool ActualizarStock(int productoId, int cambioCantidad)
            {
                var existente = ObtenerPorId(productoId);
                if (existente == null) return false;
                existente.StockActual += cambioCantidad;
                return true;
            }

            public bool Eliminar(int id)
            {
                var existente = ObtenerPorId(id);
                if (existente == null) return false;
                existente.Activo = false;
                return true;
            }
        }

        private class RepositorioVentasEnMemoria : IVentaRepositorio
        {
            private readonly List<Venta> _ventas = new List<Venta>();
            private readonly List<HistoricoMovimientosInventario> _movimientos = new List<HistoricoMovimientosInventario>();

            public IEnumerable<Venta> ObtenerTodos() => _ventas.ToList();

            public Venta? ObtenerPorId(int id) => _ventas.FirstOrDefault(v => v.VentaId == id);

            public IEnumerable<DetalleVenta> ObtenerDetallesPorVenta(int ventaId) =>
                ObtenerPorId(ventaId)?.DetallesVenta ?? new List<DetalleVenta>();

            public IEnumerable<HistoricoMovimientosInventario> ObtenerHistorialMovimientos() => _movimientos.ToList();

            public int Insertar(Venta entidad)
            {
                if (entidad.VentaId == 0)
                    entidad.VentaId = _ventas.Count + 1;
                _ventas.Add(entidad);
                return entidad.VentaId;
            }

            public int RegistrarVentaConTransaccion(Venta venta, IEnumerable<DetalleVenta> detalles, int empleadoId)
            {
                venta.DetallesVenta = detalles.ToList();
                return Insertar(venta);
            }

            public bool ConfirmarVentaConTransaccion(int ventaId, int empleadoId)
            {
                var existente = ObtenerPorId(ventaId);
                if (existente == null) return false;
                existente.Estado = Venta.EstadoProcesada;
                return true;
            }

            public bool Actualizar(Venta entidad)
            {
                var existente = ObtenerPorId(entidad.VentaId);
                if (existente == null) return false;
                existente.Estado = entidad.Estado;
                return true;
            }

            public bool Eliminar(int id)
            {
                var existente = ObtenerPorId(id);
                if (existente == null) return false;
                existente.Estado = Venta.EstadoAnulada;
                return true;
            }
        }
    }
}
