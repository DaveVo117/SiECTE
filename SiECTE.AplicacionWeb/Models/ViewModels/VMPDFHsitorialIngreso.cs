using Ronature.AplicacionWeb.Models.ViewModels;

namespace SiECTE.AplicacionWeb.Models.ViewModels
{
    public class VMPDFHsitorialIngreso
    {

        public VMOrganismo? organismo { get; set; }
        public VMHistorialIngreso? historial { get; set; }
        public VMResidente? residente { get; set;}
        public VMTipoDocumentoIngreso? documento { get; set; }

    }
}
