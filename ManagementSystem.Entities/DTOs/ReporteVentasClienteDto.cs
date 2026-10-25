namespace ManagementSystem.Entities.DTOs
{
    /// <summary>
    /// DTO para el reporte LINQ de total de ventas agrupadas por cliente.
    /// </summary>
    public class ReporteVentasClienteDto
    {
        public int NumeroFila { get; set; }
        public int ClienteId { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public int CantidadCompras { get; set; }
        public decimal PromedioCompra { get; set; }
        public decimal TotalVentas { get; set; }
        public decimal CompraMaxima { get; set; }
        public decimal CompraMinima { get; set; }
    }
}
