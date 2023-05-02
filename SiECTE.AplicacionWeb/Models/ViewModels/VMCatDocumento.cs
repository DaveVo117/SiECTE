
using SiECTE.Entity;

namespace SiECTE.AplicacionWeb.Models.ViewModels
{
    public class VMCatDocumento
    {

        public int IdDocumentoIngreso { get; set; }
        public int? IdOrganismo { get; set; }
        public string? TxtDocumentoIngreso { get; set; }
        public string? TxtDescripcion { get; set; }
        public bool? SnActivo { get; set; }

        //public virtual Organismo? IdOrganismoNavigation { get; set; }
        //public virtual CteCatUsuario? UsuarioActualizaNavigation { get; set; }
        //public virtual ICollection<CteCtrlDocumentoIngresoResidente> CteCtrlDocumentoIngresoResidentes { get; set; }

    }
}
