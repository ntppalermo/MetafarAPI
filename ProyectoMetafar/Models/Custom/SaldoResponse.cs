namespace Metafar_API.Models.Custom
{
    public class SaldoResponse
    {
        public string NombreUsuario { get; set; }
        public int NumeroCuenta { get; set; }
        public decimal? SaldoActual { get; set; }
        public DateTime? FechaUltimaExtraccion { get; set; }
    }   
}
