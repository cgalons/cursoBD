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
    public class db
    {
        private SqlConnection conexion = null;

        public void Conectar()
        {


            try
            {
                //Preparo la cadena de conexión a la BBDD. La cadena de conexión es de tipo texto

                string cadenaConexion = @"Server=.\sqlexpress;
                                        Database=testdb;
                                        User Id=testuser;
                                        Password=!Curso@2017; ";

                //Creo la conexión. Crear objeto conexión.
                conexion = new SqlConnection();
                conexion.ConnectionString = cadenaConexion;

                //Trato de abrir la conexión
                conexion.Open();

                /*Pregunto por el estado de la conexión
                if (conexion.State == ConnectionState.Open)
                {
                    Console.WriteLine("Conexión abierta con éxito");
                    //Cierro la conexión
                    conexion.Close();

                }*/




            }
            catch (Exception)
            {

                Console.WriteLine("Lo siento mijo, pero la base de dato está escacharrada");
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

            finally
            {
                /*Destruyo la conexión
                if (conexion != null)
                {

                    if (conexion.State != ConnectionState.Closed)
                    {
                        conexion.Close();
                    }

                }
                conexion.Dispose();
                conexion = null;*/
            }



        }

        public bool EstaLaConexionAbierta()
        {

            return conexion.State == ConnectionState.Open;

        }



        public void Desconectar()
        {

            if (this.conexion != null)
            {
                if (this.conexion.State != ConnectionState.Closed)
                {
                    this.conexion.Close();

                }
            }
            
        }

        public List<Usuario> DameLosUsuarios()
        {
            List<Usuario> usuarios = null;
            //Preparo la consulta SQL para obtener los usuarios
            string consultaSQL = "SELECT * FROM Users;";

            //Preparo un comando para ejecutar a la BBDD
            SqlCommand comando = new SqlCommand(consultaSQL, conexion);

            //Recojo los datos
            SqlDataReader reader = comando.ExecuteReader();

            usuarios = new List<Usuario>();

            //int numeroDeUsuarios = 0;
            while(reader.Read())
            {
                usuarios.Add(new Usuario()
                {
                    hiddenId = int.Parse (reader["hiddenId"].ToString()),
                    id = reader["id"].ToString (),
                    email = reader["email"].ToString(),
                    password = reader["password"].ToString(),
                    firstName = reader["firstName"].ToString(),
                    lastName = reader["lastName"].ToString(),
                    photoUrl = reader["photoUrl"].ToString(),
                    searchPreferences = reader["searchPreferences"].ToString(),
                    status = bool.Parse (reader["status"].ToString()),
                    deleted = (bool)reader["deleted"],
                    isAdmin = Convert.ToBoolean(reader["isADmin"])

                });
                
                /*usuarios[numeroDeUsuarios] = new Usuario();
                {
                    firstName = reader["firstName"].ToString(),
                    lastName = reader["lastName"].ToString()
                });

                numeroDeUsuarios++;*/
                //Console.WriteLine("Nombre: " + reader["firstName"]);
            }
            //Devuelvo los datos

            return usuarios;
        }

        public void InsertarUsuario(Usuario usuario)
        {

            //Preparo la consulta SQL para insertar los usuarios
            string consultaSQL = @"INSERT INTO Users (
                                                email
                                               ,password
                                               ,firstName
                                               ,lastName
                                               ,photoUrl
                                               ,searchPreferences
                                               ,status
                                               ,deleted
                                               ,isAdmin
                                               )
                                         VALUES (";
            consultaSQL += "'" + usuario.email + "'";
            consultaSQL += ",'" + usuario.password + "'";
            consultaSQL += ",'" + usuario.firstName + "'";
            consultaSQL += ",'" + usuario.lastName + "'";
            consultaSQL += ",'" + usuario.photoUrl + "'";
            consultaSQL += ",'" + usuario.searchPreferences + "'";

            /*if(usuario.status)// Se sobreentiende que compara con true (!usuario.status = false)
            {
                consultaSQL += ",1";
            }
            else
            {
                consultaSQL += ",0";
            }*/

            consultaSQL += "," + (usuario.status ? "1" : "0"); // Operador ternario, tres partes
            consultaSQL += "," + (usuario.deleted ? "1" : "0");
            consultaSQL += "," + (usuario.isAdmin ? "1" : "0");
            consultaSQL += ");";

            //Preparo un comando para ejecutar a la BBDD
            SqlCommand comando = new SqlCommand(consultaSQL, conexion);

            //Envío los datos
            comando.ExecuteNonQuery();

            
        }

        public void EliminarUsuario (string email)
        {
            //Preparo la consulta SQL para eliminar los usuarios
            string consultaSQL = @"DELETE FROM Users WHERE email = '" + email + "';"; 

            //Preparo un comando para eliminar a la BBDD
            SqlCommand comando = new SqlCommand(consultaSQL, conexion);

            //Envío los datos
            comando.ExecuteNonQuery();

        }

        public void ActualizarUsuario (Usuario usuario)
        {
            //Preparo la consulta SQL para actualizar los usuarios

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


            /*if(usuario.status)// Se sobreentiende que compara con true (!usuario.status = false)
            {
                consultaSQL += ",1";
            }
            else
            {
                consultaSQL += ",0";
            }*/

            //Preparo un comando para ejecutar a la BBDD
            SqlCommand comando = new SqlCommand(consultaSQL, conexion);

            //Envío los datos
            comando.ExecuteNonQuery();
        }

    }
    
}
