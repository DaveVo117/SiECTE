using System;
using System.Collections.Generic;

namespace SiECTE.Entity
{
    public partial class Organismo
    {
        public Organismo()
        {
            CteCatAreas = new HashSet<CteCatArea>();
            CteCatDocumentoIngresos = new HashSet<CteCatDocumentoIngreso>();
            CteCatIdentificacionResidentes = new HashSet<CteCatIdentificacionResidente>();
            CteCatTipoNota = new HashSet<CteCatTipoNota>();
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
        public bool? SnActivo { get; set; }

        public virtual ICollection<CteCatArea> CteCatAreas { get; set; }
        public virtual ICollection<CteCatDocumentoIngreso> CteCatDocumentoIngresos { get; set; }
        public virtual ICollection<CteCatIdentificacionResidente> CteCatIdentificacionResidentes { get; set; }
        public virtual ICollection<CteCatTipoNota> CteCatTipoNota { get; set; }
        public virtual ICollection<CteCatUsuario> CteCatUsuarios { get; set; }
        public virtual ICollection<CteFichaIdentificacionResidente> CteFichaIdentificacionResidentes { get; set; }
    }
}
