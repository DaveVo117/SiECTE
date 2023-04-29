using SiECTE.Entity;

namespace SiECTE.AplicacionWeb.Models.ViewModels
{
    public class VMHistorialIngreso
    {

        //public Venta()
        //{
        //    DetalleVenta = new HashSet<DetalleVenta>();
        //}

        public int IdHistorialIngreso { get; set; }
        public int IdResidente { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string? ObservacionesSalida { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public int? UsuarioActualiza { get; set; }

        public virtual CteFichaIdentificacionResidente IdResidenteNavigation { get; set; } = null!;
        public virtual CteCatUsuario? UsuarioActualizaNavigation { get; set; }
        public string? TxtUsuario { get; set; }

    }
}
