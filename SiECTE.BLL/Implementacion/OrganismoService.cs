using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SiECTE.BLL.Interfaces;
using SiECTE.DAL.Interfaces;
using SiECTE.Entity;

namespace SiECTE.BLL.Implementacion
{
    public class OrganismoService : IOrganismoService
    {
    /*ATRIBUTOS*/
        private readonly IGenericRepository<Organismo> _repositorio;
        private readonly IFireBaseService _firebaseService;
    /*CONSTRUCTOR*/
        public OrganismoService(IGenericRepository<Organismo> repositorio, IFireBaseService firebaseService)
        {
            _repositorio= repositorio;
            _firebaseService= firebaseService;
        }



    /*METODOS*/
        public async Task<Organismo> Obtener()
        {
            try
            {

                Organismo Organismo_encontrado = await _repositorio.Obtener(n => n.IdOrganismo == 1);
                return Organismo_encontrado;

            }
            catch
            {

                throw;
            }
        }




        public async Task<List<Organismo>> ListaOrganismos()
        {
            try
            {

                IQueryable < Organismo > ListaOrganismos = await _repositorio.Consultar();
                return ListaOrganismos.ToList();

            }
            catch
            {

                throw;
            }
        }





        public async Task<Organismo> GuardarCambios(Organismo entidad, Stream Logo = null, string NombreLogo = "")
        {
            try
            {

                Organismo Organismo_encontrado = await _repositorio.Obtener(n=>n.IdOrganismo==1);//EN este sistema siempre se va a trabajar con el Organismo con ID = 1

                Organismo_encontrado.TxtSiglas = entidad.TxtSiglas;
                Organismo_encontrado.TxtNombre= entidad.TxtNombre;
                Organismo_encontrado.TxtCorreo= entidad.TxtCorreo;
                Organismo_encontrado.TxtDireccion= entidad.TxtDireccion;
                Organismo_encontrado.TxtTelefono= entidad.TxtTelefono;
                Organismo_encontrado.TxtTitular = entidad.TxtTitular;
                Organismo_encontrado.TxtCargoTitular = entidad.TxtCargoTitular;

                Organismo_encontrado.TxtNombreLogo = Organismo_encontrado.TxtNombreLogo == ""  || Organismo_encontrado.TxtNombreLogo == null  ? NombreLogo : Organismo_encontrado.TxtNombreLogo;

                if (Logo != null)
                {
                    string urlFoto = await _firebaseService.SubirStorage(Logo, "carpeta_logo", Organismo_encontrado.TxtNombreLogo);
                    Organismo_encontrado.TxtUrlLogo= urlFoto;
                }

                await _repositorio.Editar(Organismo_encontrado);
                return Organismo_encontrado;

            }
            catch 
            {

                throw;
            }
        }


    }
}
