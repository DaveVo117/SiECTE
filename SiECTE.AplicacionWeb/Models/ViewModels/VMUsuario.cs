using SiECTE.Entity;

namespace SiECTE.AplicacionWeb.Models.ViewModels
{
    public class VMUsuario
    {

        public int IdUsuario { get; set; }
        public string CveUsuario { get; set; } = null!;
        //public string PassUsuario { get; set; } = null!;
        public string? NombreCompleto { get; set; }
        public string? TxtCorreo { get; set; }
        public string? TxtTelefono { get; set; }
        public string Nombre { get; set; } = null!;
        public string PrimerApellido { get; set; } = null!;
        public string SegundoApellido { get; set; } = null!;
        public string? Cargo { get; set; }
        //public int IdArea { get; set; }
        public int? NoEmpleado { get; set; }
        //public string? TxtNombreFoto { get; set; }
        public DateTime FechaAlta { get; set; }
        //public DateTime? FechaBaja { get; set; }
        //public string? ObservacionBaja { get; set; }
        public int? IdRol { get; set; }
        public string? TxtRol { get; set; }
        public int? IdOrganismo { get; set; }
        public string? TxtOrganismo { get; set; }
        //public DateTime? FechaActualiza { get; set; }
        //public int? UsuarioActualiza { get; set; }
        public int? SnActivo { get; set; } //se convierte en entero en el view model
        public string? TxtUrlFoto { get; set; }

    }
}
