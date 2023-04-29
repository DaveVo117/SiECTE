using SiECTE.Entity;

namespace SiECTE.AplicacionWeb.Models.ViewModels
{
    public class VMTipoDocumentoIngreso
    {

        //public TipoDocumentoVenta()
        //{
        //    Venta = new HashSet<Venta>();
        //}

        public int IdCtrlDocumentoIngresoResidente { get; set; }
        public int IdResidente { get; set; }
        public int IdDocumento { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public int? UsuarioActualiza { get; set; }
        public int? SnActivo { get; set; }


        //public virtual CteCatDocumentoIngreso IdDocumentoNavigation { get; set; } = null!;
        //public virtual CteFichaIdentificacionResidente IdResidenteNavigation { get; set; } = null!;
        //public virtual CteCatUsuario? UsuarioActualizaNavigation { get; set; }

    }
}
