namespace SistemaGestion.Entidades.DTOs
{
    /// <summary>
    /// DTO para cálculos estadísticos con LINQ (Count, Sum, Average, Max, Min).
    /// </summary>
    public class EstadisticasReporteDto
    {
        public int CantidadRegistros { get; set; }
        public decimal SumatoriaTotal { get; set; }
        public decimal Promedio { get; set; }
        public decimal ValorMaximo { get; set; }
        public decimal ValorMinimo { get; set; }
    }
}
