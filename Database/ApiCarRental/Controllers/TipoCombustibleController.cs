using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiCarRental.Controllers
{
    public class TipoCombustibleController : ApiController
    {
        


        public RespuestaApi Get()
        {
            RespuestaApi resultado = new RespuestaApi();
            List<TipoCombustible> dataTipoCombustible = new List<TipoCombustible>();
            try
            {
                db.Conectar();
                if (db.EstaLaConexionAbierta())
                {
                    dataTipoCombustible = db.DameListaTPConProcedimientoAlmacenado();
                    resultado.error = ""; //  TE SALE UN NULL EN CASO DE QUE NO HAYA DATOS
                }
            }
            catch (Exception ex)
            {
                resultado.error = "Error";
            }

            db.Desconectar();

            resultado.totalElementos = dataTipoCombustible.Count;
            resultado.dataTipoCombustible = dataTipoCombustible;
            return resultado;

        }


        
        //public RespuestaApi Get(long id)
        //{

        //    RespuestaApi resultado = new RespuestaApi();
        //    List<Marca> dataMarca = new List<Marca>();
        //    try
        //    {
        //        db.Conectar();
        //        if (db.EstaLaConexionAbierta())
        //        {
        //            dataMarca = db.DameListaMarcasConProcedimientoAlmacenadoPorId(id);
        //            resultado.error = "";
        //        }
        //        db.Desconectar();
        //    }
        //    catch (Exception ex)
        //    {
        //        resultado.error = "Error";
        //    }
        //    resultado.totalElementos = dataMarca.Count;
        //    resultado.dataMarca = dataMarca;
        //    return resultado;

        //}

        // POST: api/Marca
        [HttpPost]
        public IHttpActionResult Post([FromBody] TipoCombustible tipoCombustible)
        {
            RespuestaApi respuesta = new RespuestaApi();
            respuesta.error = "";
            int filasAfectadas = 0;
            try
            {
                db.Conectar();

                if (db.EstaLaConexionAbierta())
                {
                    filasAfectadas = db.AgregarTipoCombustible(tipoCombustible);

                }

                respuesta.totalElementos = filasAfectadas;

                db.Desconectar();
            }
            catch
            {

                respuesta.totalElementos = 0;
                respuesta.error = "Error al agregar el tipo de combustible";
            }

            return Ok(respuesta);
        }

        //PUT: api/TipoCombustible/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TipoCombustible/5
        public void Delete(int id)
        {
        }
    }
}
