using Microsoft.AspNetCore.Mvc;

namespace SiECTE.AplicacionWeb.Models.ViewModels
{
    public class curpModel
    {
        public string? curp { get; set; }
        public string? nombres { get; set; }
        public string? apellido1 { get; set; }
        public string? apellido2 { get; set; }
        public string? tipoError { get; set; }
        public string? codigoError { get; set; }
        public string? sessionID { get; set; }
        public string? mensaje { get; set; }
        public string? sexo { get; set; }
        public DateTime? fechNac { get; set; }
        public string? nacionalidad { get; set; }
        public string? cveEntidadEmisora { get; set; }
        public string? entidadFederativa { get; set; }
        public string? statusCURP { get; set; }
        public string? statusOper { get; set; }
        public int? docProbatorio { get; set; }
        public string anioReg { get; set; }
        public string? foja { get; set; }
        public string? tomo { get; set; }
        public string? libro { get; set; }
        public string? numActa { get; set; }
        public string? crip { get; set; }
        public string? entidadRegistro { get; set; } 
        public string? municiioRegistro { get; set; }
        public string? numRegExtranjeros { get; set; }
        public string? folioCarta { get; set; }


    }
}
