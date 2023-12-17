using Metafar_API.Data.Entities;

namespace Metafar_API.Models.Custom
{
    public class OperacionResponse
    {
        public string CodigoError { get; set; }
        public decimal? MontoRetirado { get; set; }
        public decimal? NuevoSaldo { get; set; }
        public string Resumen { get; set; }
        public bool OperacionExitosa { get; set; }
    }
}
