using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ApiCarRental.Controllers
{
    public class CochesController : ApiController
    {
        // GET: api/Coches
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //public IEnumerable<Coche> Get()
        public RespuestaApi Get()
        {
            RespuestaApi resultado = new RespuestaApi();
            List<Coche> dataCoche = new List<Coche>();
            try
            {
                db.Conectar();
                if (db.EstaLaConexionAbierta())
                {
                    dataCoche = db.DameListaCochesConProcedimientoAlmacenado();
                    resultado.error = "";
                }
            }
            catch (Exception ex)
            {
                resultado.error = "Error";
            }

            db.Desconectar();

            resultado.totalElementos = dataCoche.Count;
            resultado.dataCoche = dataCoche;
            return resultado;

        }

        // GET: api/Coches/5
        public RespuestaApi Get(long id)
        {
           
            RespuestaApi resultado = new RespuestaApi();
            List<Coche> dataCoche = new List<Coche>();
            try
            {
                db.Conectar();
                if (db.EstaLaConexionAbierta())
                {
                    dataCoche = db.DameListaCochesConProcedimientoAlmacenadoPorId(id);
                    resultado.error = "";
                }
                db.Desconectar();
            }
            catch (Exception ex)
            {
                resultado.error = "Error";
            }
            resultado.totalElementos = dataCoche.Count;
            resultado.dataCoche = dataCoche;
            return resultado;

        }

        // POST: api/Coches
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Coches/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Coches/5
        public void Delete(int id)
        {
        }
    }
}

