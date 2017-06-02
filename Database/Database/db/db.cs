using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class db
    {
        private static SqlConnection conexion = null;

        public static void Conectar()
        {


            try
            {
                //Preparo la cadena de conexión a la BBDD. La cadena de conexión es de tipo texto

                string cadenaConexion = @"Server=.\sqlexpress;
                                        Database=carrental;
                                        User Id=testuser;
                                        Password=!Curso@2017; ";

                //Creo la conexión. Crear objeto conexión.
                conexion = new SqlConnection();
                conexion.ConnectionString = cadenaConexion;

                //Trato de abrir la conexión
                conexion.Open();

                


            }
            catch (Exception)
            {

                Console.WriteLine("Error connection");
                //Destruyo la conexión
                if (conexion != null)
                {

                    if (conexion.State != ConnectionState.Closed)
                    {
                        conexion.Close();
                    }

                }
                conexion.Dispose();
                conexion = null;

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

        ////public static List<Usuario> DameLosUsuarios()
        //{
        //    List<Usuario> usuarios = null;
        //    //Preparo la consulta SQL para obtener los usuarios
        //    string consultaSQL = "SELECT * FROM Users;";

        //    //Preparo un comando para ejecutar a la BBDD
        //    SqlCommand comando = new SqlCommand(consultaSQL, conexion);

        //    //Recojo los datos
        //    SqlDataReader reader = comando.ExecuteReader();

        //    usuarios = new List<Usuario>();

        //    //int numeroDeUsuarios = 0;
        //    while(reader.Read())
        //    {
        //        usuarios.Add(new Usuario()
        //        {
        //            hiddenId = int.Parse (reader["hiddenId"].ToString()),
        //            id = reader["id"].ToString (),
        //            email = reader["email"].ToString(),
        //            password = reader["password"].ToString(),
        //            firstName = reader["firstName"].ToString(),
        //            lastName = reader["lastName"].ToString(),
        //            photoUrl = reader["photoUrl"].ToString(),
        //            searchPreferences = reader["searchPreferences"].ToString(),
        //            status = bool.Parse (reader["status"].ToString()),
        //            deleted = (bool)reader["deleted"],
        //            isAdmin = Convert.ToBoolean(reader["isADmin"])

        //        });


        //    }
        //Devuelvo los datos

        //    return usuarios;
        //}



        ////public static List<MarcasNCoches> DameListaMarcasNCoches()
        //{
        //    //List<int> kk = new List<int>(); List<miClase_Objeto> resultados = new List <miClase_Objeto>(); 
        //    //Llamo a la clase con los métodos

        //    List<MarcasNCoches> resultados = new List<MarcasNCoches>();
        //    //Preparo la consulta SQL para obtener los usuarios
        //    string consultaSQL = "SELECT * FROM V_N_COCHES_POR_MARCA;";

        //    //Preparo un comando para ejecutar a la BBDD
        //    SqlCommand comando = new SqlCommand(consultaSQL, conexion);

        //    //Recojo los datos
        //    SqlDataReader reader = comando.ExecuteReader();


        //    //int numeroDeUsuarios = 0;
        //    while (reader.Read())
        //    {
        //        resultados.Add(new MarcasNCoches()
        //        {
        //            marca = reader["Marca"].ToString(),
        //            nCoches = (int)reader["nCoches"]

        //        });
        //    }
        //}

        //public static List<Coche> DameListaCochesConProcedimientoAlmacenado()
        //{
        //    // CREO EL OBJETO EN EL QUE SE DEVOLVERÁN LOS RESULTADOS
        //    List<Coche> resultados = new List<Coche>();

        //    //PREPARO LA LLAMADA AL PROCEDIMIENTO ALMACENADO
        //    string procedimientoAEjecutar = "dbo.GET_COCHES_POR_MARCAS";

        //    //PREPARAMOS EL COMANDO PARA EJECUTAR EL PROCEDIMIENTO. CAMBIAR TIPO DE COMANDO PARA EL PROCEDIMIENTO ALMACENADO
        //    SqlCommand comando = new SqlCommand(procedimientoAEjecutar, conexion);
        //    comando.CommandType = CommandType.StoredProcedure;

        //    //EJECUTO EL COMANDO
        //    SqlDataReader reader = comando.ExecuteReader();

        //    //RECORRO EL RESULTADO Y LO PASO A LA VARIABLE A DEVOLVER
        //    while (reader.Read())
        //    {
        //        //LO QUE VA ENTRE CORCHETES ES EL NOMBRE DE LA COLUMNA Y DEBE SER IGUAL A LOS NOMBRES DE LAS COLUMNAS EN BBDD
        //        Coche coche = new Coche();
        //        coche.id = (long)reader["id"]; // CONVERSIÓN DE DATOS 
        //        coche.matricula = reader["matricula"].ToString();
        //        coche.color = reader["color"].ToString();
        //        coche.cilindrada = (decimal)reader["cilindrada"];
        //        coche.nPlazas = (short)reader["nPlazas"];
        //        coche.fechaMatriculacion = (DateTime)reader["fechaMatriculacion"];

        //        coche.marca = new Marca();
        //        coche.marca.id = (long)reader["idMarca"];
        //        coche.marca.denominacion = reader["denominacionMarca"].ToString();


        //        coche.tipoCombustible = new TipoCombustible();
        //        coche.tipoCombustible.id = (long)reader["idTipoCombustible"];
        //        coche.tipoCombustible.denominacion = reader["denominacionTipos"].ToString();

        //        resultados.Add(coche);
        //    }

        //    return resultados;

        //}


        //public static void InsertarUsuario(Usuario usuario)
        // {

        //     //Preparo la consulta SQL para insertar los usuarios
        //     string consultaSQL = @"INSERT INTO Users (
        //                                         email
        //                                        ,password
        //                                        ,firstName
        //                                        ,lastName
        //                                        ,photoUrl
        //                                        ,searchPreferences
        //                                        ,status
        //                                        ,deleted
        //                                        ,isAdmin
        //                                        )
        //                                  VALUES (";
        //     consultaSQL += "'" + usuario.email + "'";
        //     consultaSQL += ",'" + usuario.password + "'";
        //     consultaSQL += ",'" + usuario.firstName + "'";
        //     consultaSQL += ",'" + usuario.lastName + "'";
        //     consultaSQL += ",'" + usuario.photoUrl + "'";
        //     consultaSQL += ",'" + usuario.searchPreferences + "'";



        //     consultaSQL += "," + (usuario.status ? "1" : "0"); // Operador ternario, tres partes
        //     consultaSQL += "," + (usuario.deleted ? "1" : "0");
        //     consultaSQL += "," + (usuario.isAdmin ? "1" : "0");
        //     consultaSQL += ");";

        //     //Preparo un comando para ejecutar a la BBDD
        //     SqlCommand comando = new SqlCommand(consultaSQL, conexion);

        //     //Envío los datos

        //     comando.ExecuteNonQuery();


        // }

        // public static void EliminarUsuario (string email)
        // {
        //     //Preparo la consulta SQL para eliminar los usuarios
        //     string consultaSQL = @"DELETE FROM Users WHERE email = '" + email + "';"; 

        //     //Preparo un comando para eliminar a la BBDD
        //     SqlCommand comando = new SqlCommand(consultaSQL, conexion);

        //     //Envío los datos
        //     comando.ExecuteNonQuery();

        // }

        // public static void ActualizarUsuario (Usuario usuario)
        // {
        //     //Preparo la consulta SQL para actualizar los usuarios

        //     string consultaSQL = @"UPDATE Users ";
        //     consultaSQL += "   SET password = '" + usuario.password + "'";
        //     consultaSQL += "      , firstName = '" + usuario.firstName + "'";
        //     consultaSQL += "      , lastName = '" + usuario.lastName + "'";
        //     consultaSQL += "      , photoUrl = '" + usuario.photoUrl + "'";
        //     consultaSQL += "      , searchPreferences = '" + usuario.searchPreferences + "'";
        //     consultaSQL += "      , status = " + (usuario.status ? "1" : "0");
        //     consultaSQL += "      , deleted = " + (usuario.deleted ? "1" : "0");
        //     consultaSQL += "      , isAdmin = " + (usuario.isAdmin ? "1" : "0");
        //     consultaSQL += " WHERE email = '" + usuario.email + "';";



        //     //Preparo un comando para ejecutar a la BBDD
        //     SqlCommand comando = new SqlCommand(consultaSQL, conexion);

        //     //Envío los datos
        //     comando.ExecuteNonQuery();
        // }

        public static List<Coche> DameListaCochesConProcedimientoAlmacenadoDos()
        {
            // CREO EL OBJETO EN EL QUE SE DEVOLVERÁN LOS RESULTADOS
            List<Coche> resultados = new List<Coche>();

            //PREPARO LA LLAMADA AL PROCEDIMIENTO ALMACENADO
            string procedimientoAEjecutar = "dbo.GET_COCHES_POR_MARCAS";

            //PREPARAMOS EL COMANDO PARA EJECUTAR EL PROCEDIMIENTO. CAMBIAR TIPO DE COMANDO PARA EL PROCEDIMIENTO ALMACENADO
            SqlCommand comando = new SqlCommand(procedimientoAEjecutar, conexion);
            comando.CommandType = CommandType.StoredProcedure;

            //EJECUTO EL COMANDO
            SqlDataReader reader = comando.ExecuteReader();

            //RECORRO EL RESULTADO Y LO PASO A LA VARIABLE A DEVOLVER
            while (reader.Read())
            {
                //LO QUE VA ENTRE CORCHETES ES EL NOMBRE DE LA COLUMNA Y DEBE SER IGUAL A LOS NOMBRES DE LAS COLUMNAS EN BBDD
                // SOLO INDICAR LOS DATOS EN CANTIDAD Y DENOMINACION QUE SE PIDEN EN EL PROCEDIMIENTO ALMACENADO
                Coche coche = new Coche();
               
                coche.matricula = reader["matricula"].ToString();
                coche.nPlazas = (short)reader["nPlazas"];
                coche.marca = new Marca();
                coche.marca.denominacion = reader["denominacionMarca"].ToString();

                resultados.Add(coche);
            }

            return resultados;

        }

    }
    
}
