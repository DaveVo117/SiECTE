using SiECTE.Entity;

namespace SiECTE.AplicacionWeb.Models.ViewModels
{
    public class VMResidente
    {

        public int IdResidente { get; set; }
        public int? IdBeneficiario { get; set; }
        public string? TxtFotografia { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string? DependenciaEmite { get; set; }
        public decimal? UltimaCuota { get; set; }
        public string? Religion { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public int? UsuarioActualiza { get; set; }
        public int? IdEstatus { get; set; }

        public virtual ICollection<VMHistorialIngreso> historialIngreso { get; set; }
   public virtual ICollection<CteCtrlDocumentoIngresoResidente> CteCtrlDocumentoIngresoResidentes { get; set; }


        //public DateTime? FechaRegistro { get; set; }

        //public virtual Categoria? IdCategoriaNavigation { get; set; }

    }
}
