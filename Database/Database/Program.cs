using System;
using System.Collections.Generic;

namespace Database
{
    public class Program
    {
        static void Main(string[] args)
        {

            /*Console.WriteLine("Conectando a la base de datos");
            //db database = new db();
            //database.Conectar();
            db.Conectar();
           


            if (db.EstaLaConexionAbierta())
            {
                Usuario nuevoUsuario = new Usuario()
                {
                    //hiddenId = 0,
                    //id = "",
                    firstName = "MANOLO",
                    lastName = "EL DEL BOMBO",
                    email = "kk8@kk.com",
                    password = "123456",
                    photoUrl = "",
                    searchPreferences = "",
                    status = false,
                    deleted = false,
                    isAdmin = false,
                };
                db.InsertarUsuario(nuevoUsuario);
                Console.WriteLine("Usuario insertado, pulsa una tecla para continuar...");
                Console.ReadKey();

                nuevoUsuario.firstName += " modificado!!";
                db.ActualizarUsuario(nuevoUsuario);
                Console.WriteLine("Usuario actualizado, pulsa una tecla para continuar...");
                Console.ReadKey();

                db.EliminarUsuario("kk3@kk.com");
                Console.WriteLine("Usuario eliminado, pulsa una tecla para continuar...");

                List<Usuario> listaUsuarios = db.DameLosUsuarios();
                listaUsuarios.ForEach(usuario =>
                {
                    Console.WriteLine(
                       "hiddenId: " + usuario.hiddenId
                       +
                       " Id: " + usuario.id
                       +
                       " Email: " + usuario.email
                       +
                       " Password: " + usuario.password
                       +
                       " Nombre: " + usuario.firstName
                       +
                      " Apellidos: " + usuario.lastName
                       +
                       "PhotoUrl: " + usuario.photoUrl
                       +
                       " SearchPreferencies: " + usuario.searchPreferences
                       +
                       " Status: " + usuario.status
                       +
                       " Deleted: " + usuario.deleted
                       +
                       " IsAdmin: " + usuario.isAdmin
                        
                    );
                });
            }

            db.Desconectar();
            //database = null;
            Console.ReadKey();*/

            //Console.WriteLine("Conectando a la base de datos");
            //db.Conectar();

            //if (db.EstaLaConexionAbierta())
            //{
            //    List<Coche> listaCoches = db.DameListaCochesConProcedimientoAlmacenado();
            //    listaCoches.ForEach(coche =>
            //       {
            //           Console.WriteLine(
            //               @"Matricula: " + coche.matricula +
            //               "Marca: " + coche.marca.denominacion +
            //               "Tipo de combustible: " + coche.tipoCombustible.denominacion);
            //       });
            //}

            //db.Desconectar();
            //Console.ReadKey();

            //Console.WriteLine("Conectando a la base de datos");
            //db.Conectar();

            //if (db.EstaLaConexionAbierta())
            //{
            //    List<Coche> listaCoches = db.DameListaCochesConProcedimientoAlmacenadoDos();
            //    listaCoches.ForEach(coche =>
            //    {
            //        Console.WriteLine(
            //          @"Marca: " + coche.marca.denominacion +
            //           " Matricula: " + coche.matricula +
            //           " Número de plazas: " + coche.nPlazas   );
            //    });
            //}

            //db.Desconectar();
            //Console.ReadKey();


            Console.WriteLine("Conectando a la base de datos");
            db.Conectar();

            if (db.EstaLaConexionAbierta())
            {
                List<Coche> listaCoches = db.GET_COCHE_POR_MARCA_MATRICULA_PLAZAS_2("toyota", 4);
                listaCoches.ForEach(coche =>
                {
                    Console.WriteLine(
                      @"Marca: " + coche.marca.denominacion +
                       " Número de plazas: " + coche.nPlazas);
                });
            }

            db.Desconectar();
            Console.ReadKey();
        }


    }

}

