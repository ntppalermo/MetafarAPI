namespace Metafar_API.Data.Entities
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public int NumeroCuenta { get; set; }
        public virtual ICollection<Tarjeta> Tarjetas { get; set; } = new List<Tarjeta>();
    }
}
