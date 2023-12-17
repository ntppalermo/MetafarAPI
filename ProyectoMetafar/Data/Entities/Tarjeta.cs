namespace Metafar_API.Data.Entities;

public class Tarjeta
{
    public int NumeroTarjeta { get; set; }
    public int? UsuarioID { get; set; }
    public int Pin { get; set; }
    public decimal? Saldo { get; set; }
    public int? IntentosFallidos { get; set; }
    public DateTime? FechaUltimaExtraccion { get; set; }
    public bool Bloqueada { get; set; }
    public Usuario Usuario { get; set; }
}
