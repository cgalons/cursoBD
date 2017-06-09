using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiCarRental.Controllers
{
    public class MarcasController : ApiController
    {
        // GET: api/Marca
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        public RespuestaApi Get()
        {
            RespuestaApi resultado = new RespuestaApi();
            List<Marca> dataMarca = new List<Marca>();
            try
            {
                db.Conectar();
                if (db.EstaLaConexionAbierta())
                {
                    dataMarca = db.DameListaMarcasConProcedimientoAlmacenado();
                    resultado.error = ""; //  TE SALE UN NULL EN CASO DE QUE NO HAYA DATOS
                }
            }
            catch (Exception ex)
            {
                resultado.error = "Error";
            }

            db.Desconectar();

            resultado.totalElementos = dataMarca.Count;
            resultado.dataMarca = dataMarca;
            return resultado;

        }


        //GET: api/Marca/5
        //public string Get(int id)
        //{
        //    return "value";
        //}


        public RespuestaApi Get(long id)
        {

            RespuestaApi resultado = new RespuestaApi();
            List<Marca> dataMarca = new List<Marca>();
            try
            {
                db.Conectar();
                if (db.EstaLaConexionAbierta())
                {
                    dataMarca = db.DameListaMarcasConProcedimientoAlmacenadoPorId(id);
                    resultado.error = "";
                }
                db.Desconectar();
            }
            catch (Exception ex)
            {
                resultado.error = "Error";
            }
            resultado.totalElementos = dataMarca.Count;
            resultado.dataMarca = dataMarca;
            return resultado;

        }

        // POST: api/Marca
        [HttpPost]
        public IHttpActionResult Post([FromBody] Marca marca)
        {
            RespuestaApi respuesta = new RespuestaApi();
            respuesta.error = "";
            int filasAfectadas = 0;
            try
            {
                db.Conectar();

                if (db.EstaLaConexionAbierta())
                {
                    filasAfectadas = db.AgregarMarca(marca);

                }

                respuesta.totalElementos = filasAfectadas;

                db.Desconectar();
            }
            catch 
            {

                respuesta.totalElementos = 0;
                respuesta.error = "Error al agregar la marca";
            }

            return Ok(respuesta);
        }


        [HttpPut]
        // PUT: api/Marca/5
        public IHttpActionResult Put(int id, [FromBody] Marca marca)
        {
            //return Ok(marca);
            RespuestaApi respuesta = new RespuestaApi();
            respuesta.error = "";
            int filasAfectadas = 0;
            try
            {
                db.Conectar();

                if (db.EstaLaConexionAbierta())
                {
                    filasAfectadas = db.ActualizarMarca(id, marca);

                }

                respuesta.totalElementos = filasAfectadas;

                db.Desconectar();
            }
            catch
            {

                respuesta.totalElementos = 0;
                respuesta.error = "Error al actualizar la marca";
            }

            return Ok(respuesta);

        }

        [HttpDelete]
        // DELETE: api/Marca/5
        public IHttpActionResult Delete(int id)
        {
            //return Ok();
            RespuestaApi respuesta = new RespuestaApi();
            respuesta.error = "";
            int filasAfectadas = 0;
            try
            {
                db.Conectar();

                if (db.EstaLaConexionAbierta())
                {
                    filasAfectadas = db.ActualizarMarca(id);

                }

                respuesta.totalElementos = filasAfectadas;

                db.Desconectar();
            }
            catch
            {

                respuesta.totalElementos = 0;
                respuesta.error = "Error al eliminar la marca";
            }

            return Ok(respuesta);


        }
    }
}
