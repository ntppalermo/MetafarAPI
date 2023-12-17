namespace Metafar_API.Data.Entities
{
    public class Operacion
    {
        public int OperacionID { get; set; }
        public int NumeroTarjeta { get; set; }
        public int TipoOperacionID { get; set; }
        public decimal? MontoExtraido { get; set; }
        public DateTime FechaOperacion { get; set; }
        public TipoOperacion TipoOperacion { get; set; }
    }
}
