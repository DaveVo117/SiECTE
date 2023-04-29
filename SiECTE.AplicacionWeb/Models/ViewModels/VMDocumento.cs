using SiECTE.Entity;

namespace SiECTE.AplicacionWeb.Models.ViewModels
{
    public class VMDocumento
    {

        //public Categoria()
        //{
        //    Productos = new HashSet<Producto>();
        //}

        public int IdCtrlDocumentoIngresoResidente { get; set; }
        public int IdResidente { get; set; }
        public int IdDocumento { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public int? UsuarioActualiza { get; set; }
        public int? SnActivo { get; set; }

        public virtual CteCatDocumentoIngreso IdDocumentoNavigation { get; set; } = null!;
        public virtual CteFichaIdentificacionResidente IdResidenteNavigation { get; set; } = null!;
        public virtual CteCatUsuario? UsuarioActualizaNavigation { get; set; }

        //public virtual ICollection<CteCtrlDocumentoIngresoResidente> CteCtrlDocumentoIngresoResidentes { get; set; }
        //public DateTime? FechaRegistro { get; set; }

        //public virtual ICollection<Producto> Productos { get; set; }

    }
}
