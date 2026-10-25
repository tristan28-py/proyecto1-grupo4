namespace ManagementSystem.Entities.DTOs
{
    /// <summary>
    /// DTO proyectado mediante Entity Framework Core para consultar productos por categoría y estado de stock.
    /// </summary>
    public class ProductoCategoriaEfDto
    {
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public bool StockBajo { get; set; }
        public decimal ValorInventario { get; set; }
        public bool Activo { get; set; }
    }
}
