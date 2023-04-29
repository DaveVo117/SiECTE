using System;
using System.Collections.Generic;

namespace SiECTE.Entity
{
    public partial class Organismo
    {
        public Organismo()
        {
            CteCatUsuarios = new HashSet<CteCatUsuario>();
            CteFichaIdentificacionResidentes = new HashSet<CteFichaIdentificacionResidente>();
        }

        public int IdOrganismo { get; set; }
        public string? TxtUrlLogo { get; set; }
        public string? TxtNombreLogo { get; set; }
        public string? TxtSiglas { get; set; }
        public string? TxtNombre { get; set; }
        public string? TxtCorreo { get; set; }
        public string? TxtDireccion { get; set; }
        public string? TxtTelefono { get; set; }
        public string? TxtTitular { get; set; }
        public string? TxtCargoTitular { get; set; }

        public virtual ICollection<CteCatUsuario> CteCatUsuarios { get; set; }
        public virtual ICollection<CteFichaIdentificacionResidente> CteFichaIdentificacionResidentes { get; set; }
    }
}
