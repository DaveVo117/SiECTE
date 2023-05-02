using System;
using System.Collections.Generic;

namespace SiECTE.Entity
{
    public partial class CteCatArea
    {
        public int IdArea { get; set; }
        public int? IdOrganismo { get; set; }
        public string? TxtArea { get; set; }
        public int? Nivel { get; set; }
        public int? IdAreaDependenciaDirecta { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public int? UsuarioActualiza { get; set; }

        public virtual Organismo? IdOrganismoNavigation { get; set; }
        public virtual CteCatUsuario? UsuarioActualizaNavigation { get; set; }
    }
}
