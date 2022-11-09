using Microsoft.VisualStudio.TestTools.UnitTesting;
using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibClass;
using System.Security.Cryptography;

namespace Datos.Tests
{
    [TestClass()]
    public class DBPruebasTests
    {
        private DBPruebas db;

        [TestInitialize]
        public void Initialize()
        {
            db = new DBPruebas();
        }

        [TestMethod()]
        public void AñadirUsuarioTest()
        {
            int actual = db.NumeroUsuarios();
            Assert.AreEqual(3, actual);

            /*
             * CP27: Probar la introducción de un usuario con email incorrecto.
             * Precondiciones: Base de datos instanciada
             * Datos de entrada: Nombre, Apellidos, Email, Flag de gestor y Password
             * Proceso: 1 Intentar crear un usuario
             *          2 Comprobar que el número de usuario de la base de datos no ha cambiado
             * Resultado esperado: No se ha podido añadir (y por tanto crear) el usuario.
             */
            Assert.IsNull(db.AñadirUsuario("Nombre", "Apellido", "na1004alu.ubu.es", false, "12345678"));
            Assert.AreEqual(actual, db.NumeroUsuarios());

            /*
             * CP28: Probar la introducción de un usuario con password incorrecto.
             * Precondiciones: Base de datos instanciada
             * Datos de entrada: Nombre, Apellidos, Email, Flag de gestor y Password
             * Proceso: 1 Intentar crear un usuario
             *          2 Comprobar que el número de usuario de la base de datos no ha cambiado
             * Resultado esperado: No se ha podido añadir (y por tanto crear) el usuario.
             */
            Assert.IsNull(db.AñadirUsuario("Nombre", "Apellido", "email@email.com", false, "0123456789ABCDEFG"));
            Assert.AreEqual(actual, db.NumeroUsuarios());

            /*
             * CP29: Probar la introducción de un usuario ya existente.
             * Precondiciones: Base de datos instanciada
             * Datos de entrada: Nombre, Apellidos, Email, Flag de gestor y Password
             * Proceso: 1 Intentar crear un usuario
             *          2 Comprobar que el número de usuario de la base de datos no ha cambiado
             * Resultado esperado: No se ha podido añadir (y por tanto crear) el usuario.
             */
            Assert.IsNull(db.AñadirUsuario("Nombre", "Apellido", "pperez@ubu.es", false, "0123456789"));
            Assert.AreEqual(actual, db.NumeroUsuarios());

            /*
             * CP30: Probar la introducción de un usuario correcta.
             * Precondiciones: Base de datos instanciada
             * Datos de entrada: Nombre, Apellidos, Email, Flag de gestor y Password
             * Proceso: 1 Intentar crear un usuario
             *          2 Comprobar que el número de usuario de la base de datos ha aumentado en uno
             * Resultado esperado: No se ha podido añadir (y por tanto crear) el usuario.
             */
            Assert.IsNotNull(db.AñadirUsuario("Nombre", "Apellido", "email@email.com", false, "0123456789"));
            Assert.AreEqual(actual, db.NumeroUsuarios() - 1);

            /*
             * CP31: Probar la correctitud de un usuario agregado mediante su email.
             * Precondiciones: Base de datos instanciada y usuario añadido
             * Datos de entrada: Email del usuario
             * Proceso: 1 Obtener el usuario con el email en cuestión
             *          2 Comprobar que las propiedades del usuario obtenido coinciden con las de su creación
             * Resultado esperado: Propiedades correctas.
             */
            Usuario u = db.ObtenerUsuario("email@email.com");
            Assert.IsNotNull(u);
            //Al mismo tiempo se comprueba el usuario añadido y los métodos de ObtenerUsuario
            Assert.AreEqual(u.Nombre, "Nombre");
            Assert.AreEqual(u.Apellidos, "Apellido");
            Assert.AreEqual(u.Email, "email@email.com");
            Assert.AreEqual(u.EsGestor, false);
            Assert.IsTrue(u.ComprobarContraseña("0123456789"));

            /*
             * CP32: Probar la correctitud de un usuario agregado mediante su Id.
             * Precondiciones: Base de datos instanciada y usuario añadido
             * Datos de entrada: Id del usuario
             * Proceso: 1 Obtener el usuario con el Id en cuestión
             *          2 Comprobar que las propiedades del usuario obtenido coinciden con las de su creación
             * Resultado esperado: Propiedades correctas.
             */
            u = db.ObtenerUsuario(u.Id);
            Assert.IsNotNull(u);
            //Al mismo tiempo se comprueba el usuario añadido y los métodos de ObtenerUsuario
            Assert.AreEqual(u.Nombre, "Nombre");
            Assert.AreEqual(u.Apellidos, "Apellido");
            Assert.AreEqual(u.Email, "email@email.com");
            Assert.AreEqual(u.EsGestor, false);
            Assert.IsTrue(u.ComprobarContraseña("0123456789"));

            /*
             * CP33: Probar obtener un usuario no existente mediante email.
             * Precondiciones: Base de datos instanciada
             * Datos de entrada: Email del usuario
             * Proceso: 1 Obtener el usuario con el email en cuestión
             *          2 Comprobar que no se obtiene ninguna referencia a un usuario
             * Resultado esperado: Usuario no encontrado, ninguna referencia.
             */
            Assert.IsNull(db.ObtenerUsuario("noexiste@email.com"));

            /*
             * CP34: Probar obtener un usuario no existente mediante Id.
             * Precondiciones: Base de datos instanciada
             * Datos de entrada: Id del usuario
             * Proceso: 1 Obtener el usuario con el Id en cuestión
             *          2 Comprobar que no se obtiene ninguna referencia a un usuario
             * Resultado esperado: Usuario no encontrado, ninguna referencia.
             */
            Assert.IsNull(db.ObtenerUsuario(100));

            /*
             * CP35: Probar obtener todos los usuarios.
             * Precondiciones: Base de datos instanciada con usuarios.
             * Datos de entrada: Email de todos los usuario esperados que ya están en la base de datos
             * Proceso: 1 Comprobar que la obtención de todos los usuarios de verdad contiene a todos
             * Resultado esperado: Todos los usuarios se encuentran en la base de datos.
             */
            Assert.IsTrue(db.ObtenerTodosUsuarios().Contains(db.ObtenerUsuario("pperez@ubu.es")));
            Assert.IsTrue(db.ObtenerTodosUsuarios().Contains(db.ObtenerUsuario("ffx1004@alu.ubu.es")));
            Assert.IsTrue(db.ObtenerTodosUsuarios().Contains(db.ObtenerUsuario("mmx1003@alu.ubu.es")));
            Assert.IsTrue(db.ObtenerTodosUsuarios().Contains(db.ObtenerUsuario("email@email.com")));
        }

        [TestMethod()]
        public void BorrarUsuarioTest()
        {


            /*
             * CP36: Probar borrar usuario inexistente.
             * Precondiciones: Base de datos instanciada (con o sin usuarios).
             * Datos de entrada: Id de un usuario no existente.
             * Proceso: 1 Almacenar número de usuarios actual 
             *          2 Intentar eliminar con resultado falso
             *          3 Comprobar que el número de usuarios no ha cambiado
             * Resultado esperado: El borrado no tiene efecto y la base de datos no se ve modificado.
             */
            int actual = db.NumeroUsuarios();
            Assert.IsFalse(db.BorraUsuario(100));
            Assert.AreEqual(actual, db.NumeroUsuarios());

            /*
             * CP37: Probar borrar usuario inexistente.
             * Precondiciones: Base de datos instanciada (con o sin usuarios).
             * Datos de entrada: Email de un usuario no existente.
             * Proceso: 1 Intentar eliminar con resultado falso
             *          2 Comprobar que el número de usuarios no ha cambiado
             * Resultado esperado: El borrado no tiene efecto y la base de datos no se ve modificado.
             */
            Assert.IsFalse(db.BorraUsuario("noexiste@email.com"));
            Assert.AreEqual(actual, db.NumeroUsuarios());

            /*
             * CP38: Probar borrar usuario existente.
             * Precondiciones: Base de datos instanciada con usuarios.
             * Datos de entrada: Email de un usuario existente.
             * Proceso: 1 Almacenar número de usuarios actual  
             *          2 Intentar eliminar usuario
             *          3 Comprobar que el número de usuarios no ha cambiado
             *          4 Comprobar que al buscar el usuario no se obtiene referencia
             * Resultado esperado: El borrado es correcto y el usuario ya no se encuentra en la base de datos.
             */
            actual = db.NumeroUsuarios();
            db.BorraUsuario("pperez@ubu.es");
            Assert.AreEqual(actual-1, db.NumeroUsuarios());
            Assert.IsNull(db.ObtenerUsuario("pperez@ubu.es"));

            /*
             * CP39: Probar borrar usuario existente.
             * Precondiciones: Base de datos instanciada (con usuarios).
             * Datos de entrada: Id de un usuario existente.
             * Proceso: 1 Almacenar número de usuarios actual 
             *          2 Intentar eliminar usuario
             *          3 Comprobar que el número de usuarios no ha cambiado
             *          4 Comprobar que al buscar el usuario no se obtiene referencia
             * Resultado esperado: El borrado es correcto y el usuario ya no se encuentra en la base de datos.
             */
            actual = db.NumeroUsuarios();
            short id = 1; //Usuario ffx1004@alu.ubu.es
            db.BorraUsuario(id);
            Assert.AreEqual(actual-1, db.NumeroUsuarios());
            Assert.IsNull(db.ObtenerUsuario(id));
        }

        [TestMethod()]
        public void EntradaTest()
        {
            //Añadir entrada con usuario inexistente
            /*
             * CP40: Probar crear entrada con usuario inexistente.
             * Precondiciones: Base de datos instanciada.
             * Datos de entrada: Descripción, Id del usuario, Email, Password
             * Proceso: 1 Intentar añadir entrada
             *          2 Comprobar que no se ha creado (referencia nula)
             * Resultado esperado: La entrada no se ha podido crear (y por tanto no se añade).
             */
            Assert.IsNull(db.CrearEntrada("Descripcion", 10, "noexiste@email.com", "P@assword"));

            /*
             * CP41: Probar crear entrada con email erróneo.
             * Precondiciones: Base de datos instanciada.
             * Datos de entrada: Descripción, Id del usuario, Email, Password
             * Proceso: 1 Intentar añadir entrada
             *          2 Comprobar que no se ha creado (referencia nula)
             * Resultado esperado: La entrada no se ha podido crear (y por tanto no se añade).
             */
            Assert.IsNull(db.CrearEntrada("Descripcion", 0, "noexiste&email.com", "P@assword"));

            //Añadir entrada correcta
            /*
             * CP42: Probar crear entrada correcta y obtención de entradas.
             * Precondiciones: Base de datos instanciada.
             * Datos de entrada: Descripción, Id del usuario, Email, Password
             * Proceso: 1 Intentar añadir entrada
             *          2 Comprobar que sí se crea
             *          3 Comprobar que está en la base de datos (obtención de entradas)
             * Resultado esperado: La entrada se ha podido crear, se ha añadido y al obtenerla coincide con la creación de la misma.
             */
            Entrada en = db.CrearEntrada("Descripción", 0, "existe@gmail.com" , "P@ssword");
            Assert.IsNotNull(en);
            Assert.AreEqual(db.ObtenerEntrada(en.Id), en);

            /*
             * CP43: Probar se obtienen todas las entradas.
             * Precondiciones: Base de datos instanciada con entradas.
             * Datos de entrada: -
             * Proceso: 1 Obtener todas las entradas
             *          2 Comprobar que realmente son todas las que contiene
             * Resultado esperado: Contiene todas las entradas.
             */
            Assert.IsTrue(db.ObtenerTodasEntradas().Contains(db.ObtenerEntrada(0)));
            Assert.IsTrue(db.ObtenerTodasEntradas().Contains(db.ObtenerEntrada(1)));
            Assert.IsTrue(db.ObtenerTodasEntradas().Contains(db.ObtenerEntrada(2)));
            Assert.IsTrue(db.ObtenerTodasEntradas().Contains(en));

            /*
             * CP44: Probar a borrar una entrada.
             * Precondiciones: Base de datos instanciada con entradas.
             * Datos de entrada: -
             * Proceso: 1 Intentar borrar una entrada
             *          2 Comprobar que no es posible obtener la entrada recién eliminada
             * Resultado esperado: La entrada eliminada ya no está en la base de datos.
             */
            Assert.IsTrue(db.BorrarEntrada(en)); //Se prueba implícitamente también mediante su Id.
            Assert.IsNull(db.ObtenerEntrada(en.Id));

        }


        [TestMethod()]
        public void AccesoTest()
        {
            Usuario usuario = db.AñadirUsuario("Nombre2", "Apellido2", "usuario@alu.ubu.es", false, "p@ssword");
            Entrada en = db.CrearEntrada("Descripción", 0, "acceso@gmail.com", "P@ssword");

            /*
             * CP45: Probar que un usuario no tiene acceso a una entrada en el que no está autorizado.
             * Precondiciones: Base de datos instanciada y Entrada creada.
             * Datos de entrada: Entrada y Usuario
             * Proceso: 1 Comprobar que no puede acceder.
             *          2 Comprobar que la revocación del acceso no tiene efecto
             * Resultado esperado: El usuario no tiene acceso.
             */
            Assert.IsFalse(db.PuedeAccederEntrada(en, usuario.Id));
            Assert.IsFalse(db.QuitarAcceso(en, usuario.Id));

            /*
             * CP46: Probar la concesión de acceso a un usuario en una entrada.
             * Precondiciones: Base de datos instanciada y Entrada creada.
             * Datos de entrada: Entrada y Usuario
             * Proceso: 1 Dar acceso al usuario.
             *          2 Comprobar que puede acceder ahora a la entrada
             *          3 Comprobar que la concesión de acceso no tiene efecto
             * Resultado esperado: El usuario tiene acceso ahora.
             */
            Assert.IsTrue(db.DarAcceso(en, usuario.Id));
            Assert.IsTrue(db.PuedeAccederEntrada(en, usuario.Id));
            Assert.IsFalse(db.DarAcceso(en, usuario.Id));

            /*
             * CP47: Probar la revocación de acceso de una entrada.
             * Precondiciones: Base de datos instanciada y Entrada creada.
             * Datos de entrada: Entrada y Usuario
             * Proceso: 1 Quitar acceso al usuario.
             *          2 Comprobar que no puede acceder a la entrada
             *          3 Comprobar que la revocación ya no tiene efecto
             * Resultado esperado: El usuario no tiene acceso desde ese momento.
             */
            Assert.IsTrue(db.QuitarAcceso(en, usuario.Id));
            Assert.IsFalse(db.PuedeAccederEntrada(en, usuario.Id));
            Assert.IsFalse(db.QuitarAcceso(en, usuario.Id));
        }

        [TestMethod()]
        public void ObtenerEntradasLogTest()
        {
            Usuario usuario = db.AñadirUsuario("Nombre2", "Apellido2", "usuario@alu.ubu.es", false, "p@ssword");
            Entrada en = db.CrearEntrada("Descripción", 0, "paralog@email.com", "P@ssword");

            /*
             * CP48: Probar la obtención correcta de las entradas del log al realizar operaciones de inserción.
             * Precondiciones: Base de datos instanciada.
             * Datos de entrada: Entrada (credenciales) y Usuario
             * Proceso: 1 Almacenar número de entradas log actuales
             *          2 Añadir Entrada de Log de inicio de sesión
             *          3 Añadir Entrada de Log de acceso a una entrada de credenciales
             *          3 Comprobar que el número de entradas log ha aumentado en 2
             * Resultado esperado: Almacenamiento correcto de nuevas entradas al log.
             */
            int actual = db.ObtenerTodasEntradasLog().Count;
            db.CrearEntradaLog(usuario.Id, null);
            db.CrearEntradaLog(usuario.Id, en);
            Assert.AreEqual(db.ObtenerTodasEntradasLog().Count, actual + 2);
        }
    }
}