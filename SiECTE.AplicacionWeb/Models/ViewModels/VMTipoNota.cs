
using SiECTE.Entity;

namespace SiECTE.AplicacionWeb.Models.ViewModels
{
    public class VMTipoNota
    {

        public int IdTipoNota { get; set; }
        public int? IdOrganismo { get; set; } 
        public string? TxtTipoNota { get; set; }
        public string? TxtDescripcion { get; set; }

        //public virtual Organismo? IdOrganismoNavigation { get; set; }
        //public virtual ICollection<CteCtrlNotaResidente> CteCtrlNotaResidentes { get; set; }

    }
}
