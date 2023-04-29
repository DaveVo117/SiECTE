using System;
using System.Collections.Generic;

namespace SiECTE.Entity
{
    public partial class CteCatRol
    {
        public CteCatRol()
        {
            CteCatUsuarios = new HashSet<CteCatUsuario>();
            RolMenus = new HashSet<RolMenu>();
        }

        public int IdRol { get; set; }
        public string? TxtRol { get; set; }
        public bool? SnActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<CteCatUsuario> CteCatUsuarios { get; set; }
        public virtual ICollection<RolMenu> RolMenus { get; set; }
    }
}
