using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCarRental
{
    public static class db
    {
        private static SqlConnection conexion = null;

        public static void Conectar()
        {


            try
            {
                //CADENA DE CONEXIÓN A LA BD
                string cadenaConexion = @"Server=.\sqlexpress;
                                          Database=carrental;
                                          User Id=testuser;
                                          Password=!Curso@2017;";

                // CREO LA CONEXIÓN
                conexion = new SqlConnection();
                conexion.ConnectionString = cadenaConexion;

                // TRATO DE ABRIR LA CONEXION
                conexion.Open();

                //// PREGUNTO POR EL ESTADO DE LA CONEXIÓN
                //if (conexion.State == ConnectionState.Open)
                //{
                //    Console.WriteLine("Conexión abierta con éxito");
                //    // CIERRO LA CONEXIÓN
                //    conexion.Close();
                //}
            }
            catch (Exception)
            {
                if (conexion != null)
                {
                    if (conexion.State != ConnectionState.Closed)
                    {
                        conexion.Close();
                    }
                    conexion.Dispose();
                    conexion = null;
                }
            }
            finally
            {
                // DESTRUYO LA CONEXIÓN
                //if (conexion != null)
                //{
                //    if (conexion.State != ConnectionState.Closed)
                //    {
                //        conexion.Close();
                //        Console.WriteLine("Conexión cerrada con éxito");
                //    }
                //    conexion.Dispose();
                //    conexion = null;
                //}
            }
        }

        public static bool EstaLaConexionAbierta()
        {
            return conexion.State == ConnectionState.Open;
        }

        public static void Desconectar()
        {
            if (conexion != null)
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }

        public static List<Usuario> DameLosUsuarios()
        {
            //Usuario[]     usuarios = null;
            List<Usuario> usuarios = null;
            // PREPARO LA CONSULTA SQL PARA OBTENER LOS USUARIOS
            string consultaSQL = "SELECT * FROM Users;";
            // PREPARO UN COMANDO PARA EJECUTAR A LA BASE DE DATOS
            SqlCommand comando = new SqlCommand(consultaSQL, conexion);
            // RECOJO LOS DATOS
            SqlDataReader reader = comando.ExecuteReader();
            usuarios = new List<Usuario>();

            while (reader.Read())
            {
                usuarios.Add(new Usuario()
                {
                    hiddenId = int.Parse(reader["hiddenId"].ToString()),
                    id = reader["id"].ToString(),
                    email = reader["email"].ToString(),
                    password = reader["password"].ToString(),
                    firstName = reader["firstName"].ToString(),
                    lastName = reader["lastName"].ToString(),
                    photoUrl = reader["photoUrl"].ToString(),
                    searchPreferences = reader["searchPreferences"].ToString(),
                    status = bool.Parse(reader["status"].ToString()),
                    deleted = (bool)reader["deleted"],
                    isAdmin = Convert.ToBoolean(reader["isAdmin"])
                });
            }

            // DEVUELVO LOS DATOS
            return usuarios;
        }

        //public static List<MarcasNCoches> DameListaMarcasNCoches()
        //{
        //    List<MarcasNCoches> resultados = new List<MarcasNCoches>();
        //    // PREPARO LA CONSULTA SQL PARA OBTENER LOS USUARIOS
        //    string consultaSQL = "SELECT * FROM V_N_COCHES_POR_MARCA;";
        //    // PREPARO UN COMANDO PARA EJECUTAR A LA BASE DE DATOS
        //    SqlCommand comando = new SqlCommand(consultaSQL, conexion);
        //    // RECOJO LOS DATOS
        //    SqlDataReader reader = comando.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        resultados.Add(new MarcasNCoches()
        //        {
        //            marca = reader["Marca"].ToString(),
        //            nCoches = (int)reader["nCoches"]
        //        });
        //    }

        //    // DEVUELVO LOS DATOS
        //    return resultados;
        //}


        public static List<Marca> DameListaMarcasConProcedimientoAlmacenado()
        {
            // CREO EL OBJETO EN EL QUE SE DEVOLVERÁN LOS RESULTADOS
            List<Marca> resultados = new List<Marca>();

            // PREPARO LA LLAMADA AL PROCEDIMIENTO ALMACENADO
            string procedimientoAEjecutar = "dbo.GET_MARCA";

            // PREPARAMOS EL COMANDO PARA EJECUTAR EL PROCEDIMIENTO ALMACENADO
            SqlCommand comando = new SqlCommand(procedimientoAEjecutar, conexion);
            comando.CommandType = CommandType.StoredProcedure;

            // EJECUTO EL COMANDO Y RECOJO RESULTADOS CON EL READER
            SqlDataReader reader = comando.ExecuteReader();

            // RECORRO EL RESULTADO Y LO PASO A LA VARIABLE A DEVOLVER
            while (reader.Read()) // EL READ LEE SI HAY UNA PRIMERA FILA, LA VUELCA Y VUELVE PARA SEGUIR CON LA SEGUNDA, ETC
            {
                // CREO LA MARCA
                Marca marca = new Marca();
                marca.id = (long)reader["id"];
                marca.denominacion = reader["denominacion"].ToString();
                
                // AÑADO LA MARCA A LA LISTA DE RESULTADOS
                resultados.Add(marca);

            }

            return resultados;
        }



        public static List<Coche> DameListaCochesConProcedimientoAlmacenado()
        {
            // CREO EL OBJETO EN EL QUE SE DEVOLVERÁN LOS RESULTADOS
            List<Coche> resultados = new List<Coche>();

            // PREPARO LA LLAMADA AL PROCEDIMIENTO ALMACENADO
            string procedimientoAEjecutar = "dbo.GET_COCHE_POR_MARCA";

            // PREPARAMOS EL COMANDO PARA EJECUTAR EL PROCEDIMIENTO ALMACENADO
            SqlCommand comando = new SqlCommand(procedimientoAEjecutar, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            // EJECUTO EL COMANDO
            SqlDataReader reader = comando.ExecuteReader();
            // RECORRO EL RESULTADO Y LO PASO A LA VARIABLE A DEVOLVER
            while (reader.Read())
            {
                // CREO EL COCHE
                Coche coche = new Coche();
                coche.id = (long)reader["id"];
                coche.matricula = reader["matricula"].ToString();
                coche.color = reader["color"].ToString();
                coche.cilindrada = (decimal)reader["cilindrada"];
                coche.nPlazas = (short)reader["nPlazas"];
                coche.fechaMatriculacion = (DateTime)reader["fechaMatriculacion"];
                coche.marca = new Marca();
                coche.marca.id = (long)reader["idMarca"];
                coche.marca.denominacion = reader["denominacionMarca"].ToString();
                coche.tipoCombustible = new TipoCombustible();
                coche.tipoCombustible.id = (long)reader["idTipoCombustible"];
                coche.tipoCombustible.denominacion = reader["denominacionTipoCombustible"].ToString();
                // AÑADO EL COCHE A LA LISTA DE RESULTADOS
                resultados.Add(coche);

            }

            return resultados;
        }


        public static List<Coche> DameListaCochesConProcedimientoAlmacenadoPorId(long id)
        {
            // CREO EL OBJETO EN EL QUE SE DEVOLVERÁN LOS RESULTADOS
            List<Coche> resultados = new List<Coche>();

            // PREPARO LA LLAMADA AL PROCEDIMIENTO ALMACENADO
            string procedimientoAEjecutar = "dbo.GET_COCHE_POR_MARCA_ID";

            // PREPARAMOS EL COMANDO PARA EJECUTAR EL PROCEDIMIENTO ALMACENADO
            SqlCommand comando = new SqlCommand(procedimientoAEjecutar, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            SqlParameter parametroId = new SqlParameter();
            parametroId.ParameterName = "id";
            parametroId.SqlDbType = SqlDbType.BigInt;
            parametroId.SqlValue = id;
            comando.Parameters.Add(parametroId);
            // EJECUTO EL COMANDO
            SqlDataReader reader = comando.ExecuteReader();
            // RECORRO EL RESULTADO Y LO PASO A LA VARIABLE A DEVOLVER
            while (reader.Read())
            {
                // LO QUE SALE POR JSON, LISTADO COMPLETO DE LAS CARACTERÍSTICAS DE COCHES SEAN 5.000 O UNO O NINGUNO
                Coche coche = new Coche();
                coche.id = (long)reader["id"];
                coche.matricula = reader["matricula"].ToString();
                coche.color = reader["color"].ToString();
                coche.cilindrada = (decimal)reader["cilindrada"];
                coche.nPlazas = (short)reader["nPlazas"];
                coche.fechaMatriculacion = (DateTime)reader["fechaMatriculacion"];
                coche.marca = new Marca();
                coche.marca.id = (long)reader["idMarca"];
                coche.marca.denominacion = reader["denominacionMarca"].ToString();
                coche.tipoCombustible = new TipoCombustible();
                coche.tipoCombustible.id = (long)reader["idTipoCombustible"];
                coche.tipoCombustible.denominacion = reader["denominacionTipoCombustible"].ToString();
                // AÑADO EL COCHE A LA LISTA DE RESULTADOS
                resultados.Add(coche);

            }

            return resultados;
        }

        public static List<Marca> DameListaMarcasConProcedimientoAlmacenadoPorId(long id)
        {
            // CREO EL OBJETO EN EL QUE SE DEVOLVERÁN LOS RESULTADOS
            List<Marca> resultados = new List<Marca>();

            // PREPARO LA LLAMADA AL PROCEDIMIENTO ALMACENADO
            string procedimientoAEjecutar = "dbo.GET_MARCA_POR_ID";

            // PREPARAMOS EL COMANDO PARA EJECUTAR EL PROCEDIMIENTO ALMACENADO
            SqlCommand comando = new SqlCommand(procedimientoAEjecutar, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            SqlParameter parametroId = new SqlParameter();
            parametroId.ParameterName = "id";
            parametroId.SqlDbType = SqlDbType.BigInt;
            parametroId.SqlValue = id;
            comando.Parameters.Add(parametroId);
            // EJECUTO EL COMANDO
            SqlDataReader reader = comando.ExecuteReader();
            // RECORRO EL RESULTADO Y LO PASO A LA VARIABLE A DEVOLVER
            while (reader.Read())
            {
                // LO QUE SALE POR JSON, LISTADO COMPLETO DE LAS CARACTERÍSTICAS DE COCHES SEAN 5.000 O UNO O NINGUNO
                Marca marca = new Marca();
                marca.id = (long)reader["id"];
                marca.denominacion = reader["denominacion"].ToString();

                // AÑADO EL COCHE A LA LISTA DE RESULTADOS
                resultados.Add(marca);

            }

            return resultados;
        }

        public static List<Coche> GET_COCHE_POR_MARCA_MATRICULA_PLAZAS()
        {
            // CREO EL OBJETO EN EL QUE SE DEVOLVERÁN LOS RESULTADOS
            List<Coche> resultados = new List<Coche>();

            // PREPARO LA LLAMADA AL PROCEDIMIENTO ALMACENADO
            string procedimientoAEjecutar = "dbo.GET_COCHE_POR_MARCA_MATRICULA_PLAZAS";

            // PREPARAMOS EL COMANDO PARA EJECUTAR EL PROCEDIMIENTO ALMACENADO
            SqlCommand comando = new SqlCommand(procedimientoAEjecutar, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            // EJECUTO EL COMANDO
            SqlDataReader reader = comando.ExecuteReader();
            // RECORRO EL RESULTADO Y LO PASO A LA VARIABLE A DEVOLVER
            while (reader.Read())
            {
                // CREO EL COCHE
                Coche coche = new Coche();
                coche.matricula = reader["matricula"].ToString();
                coche.nPlazas = (short)reader["nPlazas"];
                coche.marca = new Marca();
                coche.marca.denominacion = reader["Marca"].ToString();
                // AÑADO EL COCHE A LA LISTA DE RESULTADOS
                resultados.Add(coche);
            }

            return resultados;
        }

        public static List<Coche> GET_COCHE_POR_MARCA_MATRICULA_PLAZAS_2(string marca, short nPlazas)
        {
            // CREO EL OBJETO EN EL QUE SE DEVOLVERÁN LOS RESULTADOS
            List<Coche> resultados = new List<Coche>();

            // PREPARO LA LLAMADA AL PROCEDIMIENTO ALMACENADO
            string procedimientoAEjecutar = "dbo.GET_COCHE_POR_MARCA_MATRICULA_PLAZAS_2";

            // PREPARAMOS EL COMANDO PARA EJECUTAR EL PROCEDIMIENTO ALMACENADO
            SqlCommand comando = new SqlCommand(procedimientoAEjecutar, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            // PREPARO LOS PARAMETROS Y LES PASO LOS VALORES
            SqlParameter parametroMarca = new SqlParameter();
            parametroMarca.ParameterName = "marca";
            parametroMarca.SqlDbType = SqlDbType.NVarChar;
            parametroMarca.SqlValue = marca;
            comando.Parameters.Add(parametroMarca);

            SqlParameter parametroNPlazas = new SqlParameter();
            parametroNPlazas.ParameterName = "nPlazas";
            parametroNPlazas.SqlDbType = SqlDbType.SmallInt;
            parametroNPlazas.SqlValue = nPlazas;
            comando.Parameters.Add(parametroNPlazas);

            // EJECUTO EL COMANDO
            SqlDataReader reader = comando.ExecuteReader();
            // RECORRO EL RESULTADO Y LO PASO A LA VARIABLE A DEVOLVER
            while (reader.Read())
            {
                // CREO EL COCHE
                Coche coche = new Coche();
                coche.matricula = reader["matricula"].ToString();
                coche.nPlazas = (short)reader["nPlazas"];
                coche.marca = new Marca();
                coche.marca.denominacion = reader["Marca"].ToString();
                // AÑADO EL COCHE A LA LISTA DE RESULTADOS
                resultados.Add(coche);
            }

            return resultados;
        }

        public static void InsertarUsuario(Usuario usuario)
        {
            // PREPARO LA CONSULTA SQL PARA INSERTAR AL NUEVO USUARIO
            string consultaSQL = @"INSERT INTO Users (
                    email,password,firstName,lastName,photoUrl
                    ,searchPreferences,status,deleted,isAdmin
		                                       )
                                         VALUES (";
            consultaSQL += "'" + usuario.email + "'";
            consultaSQL += ",'" + usuario.password + "'";
            consultaSQL += ",'" + usuario.firstName + "'";
            consultaSQL += ",'" + usuario.lastName + "'";
            consultaSQL += ",'" + usuario.photoUrl + "'";
            consultaSQL += ",'" + usuario.searchPreferences + "'";
            consultaSQL += "," + (usuario.status ? "1" : "0");
            consultaSQL += "," + (usuario.deleted ? "1" : "0");
            consultaSQL += "," + (usuario.isAdmin ? "1" : "0");
            consultaSQL += ");";

            // PREPARO UN COMANDO PARA EJECUTAR A LA BASE DE DATOS
            SqlCommand comando = new SqlCommand(consultaSQL, conexion);
            // RECOJO LOS DATOS
            comando.ExecuteNonQuery();
        }

        public static void EliminarUsuario(string email)
        {
            // PREPARO LA CONSULTA SQL PARA INSERTAR AL NUEVO USUARIO
            string consultaSQL = @"DELETE FROM Users 
                                   WHERE email = '" + email + "';";

            // PREPARO UN COMANDO PARA EJECUTAR A LA BASE DE DATOS
            SqlCommand comando = new SqlCommand(consultaSQL, conexion);
            // EJECUTO EL COMANDO
            comando.ExecuteNonQuery();
        }

        public static void ActualizarUsuario(Usuario usuario)
        {
            // PREPARO LA CONSULTA SQL PARA INSERTAR AL NUEVO USUARIO
            string consultaSQL = @"UPDATE Users ";
            consultaSQL += "   SET password = '" + usuario.password + "'";
            consultaSQL += "      , firstName = '" + usuario.firstName + "'";
            consultaSQL += "      , lastName = '" + usuario.lastName + "'";
            consultaSQL += "      , photoUrl = '" + usuario.photoUrl + "'";
            consultaSQL += "      , searchPreferences = '" + usuario.searchPreferences + "'";
            consultaSQL += "      , status = " + (usuario.status ? "1" : "0");
            consultaSQL += "      , deleted = " + (usuario.deleted ? "1" : "0");
            consultaSQL += "      , isAdmin = " + (usuario.isAdmin ? "1" : "0");
            consultaSQL += " WHERE email = '" + usuario.email + "';";

            // PREPARO UN COMANDO PARA EJECUTAR A LA BASE DE DATOS
            SqlCommand comando = new SqlCommand(consultaSQL, conexion);
            // EJECUTO EL COMANDO
            comando.ExecuteNonQuery();
        }
    }
}
