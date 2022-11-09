using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibClass.Tests
{
    [TestClass()]
    public class EntradaTests
    {
        private Usuario dueño;
        private Entrada entrada;

        [TestInitialize]
        public void Initialize()
        {
            dueño = new Usuario(0, "Nombre1", "Apellido1", "dueño@ubu.es", "p@sword", true);
            entrada = new Entrada(0, dueño, "email@gmail.com" , "P@ssword", "Descripción de la entrada");
        }


        [TestMethod()]
        public void EntradaTest()
        {
            //IMPORTANTE: Se ha decidido abstraer la comprobación de correctitud de emails

            /*
             * CP11: Probar la creación de una entrada completa y correctamente.
             * Precondiciones: Entrada a probar creado
             * Datos de entrada: La Entrada
             * Proceso: 1 Comprobar que el objeto se ha creado 
             *          2 Comprobar la correctitud de todas las propiedades.
             * Resultado esperado: Todas las comprobaciones se verifican con los datos en cuestión.
             */
            Assert.IsNotNull(entrada);

            Assert.AreEqual(entrada.Id, 0);
            Assert.AreEqual(entrada.Dueño, dueño);
            Assert.AreEqual(entrada.Email, "email@gmail.com");
            Assert.AreEqual(entrada.Password, "P@ssword");
            Assert.AreEqual(entrada.Descripción, "Descripción de la entrada");

            /*
             * CP12: Probar la modificación de las propiedades de entrada (internamente setters y getters).
             * Precondiciones: Usuario a probar creado
             * Datos de entrada: Dueño, Email, Password, Descripción nuevos.
             * Proceso: 1 Establecer las propiedades a los valores nuevos.
             *          2 Obtener las propiedades y verificar coincidencia con los datos de entrada.
             * Resultado esperado: Todos las propiedades se establecen y obtienen correctamente.
             */
            Usuario nuevoDueño = new Usuario(1, "Nombre2", "Apellido2", "cambio@ubu.es", "p@sword", false);
            entrada.Dueño = nuevoDueño;
            entrada.Email = nuevoDueño.Email;
            entrada.Password = "nuevoP@ssword";
            entrada.Descripción = "Nueva descripción de la entrada";

            Assert.AreEqual(entrada.Id, 0);
            Assert.AreEqual(entrada.Dueño, nuevoDueño);
            Assert.AreEqual(entrada.Email, "cambio@ubu.es");
            Assert.AreEqual(entrada.Password, "nuevoP@ssword");
            Assert.AreEqual(entrada.Descripción, "Nueva descripción de la entrada");
        }

        [TestMethod]
        public void AccesoDueño()
        {
            /*
             * CP13: Probar si el dueño tiene acceso a su entrada.
             * Precondiciones: Entrada creada con propieda de ese dueño.
             * Datos de entrada: Usuario (supuesto dueño)
             * Proceso: 1 Comprobar si el usuario es el dueño de la entrada.
             *          2 Comprobar que no se le puede dar acceso (es redundante)
             *          3 Comprobar que la revocación del acceso a un dueño no tiene efecto:
             *              3.1 Quitar acceso
             *              3.2 Comprobar si está autorizado
             * Resultado esperado: El dueño tiene acceso en cualquier caso.
             */
            Assert.IsTrue(entrada.EstaAutorizado(dueño));
            Assert.IsFalse(entrada.Autorizar(dueño));
            //3
            Assert.IsFalse(entrada.Desautorizar(dueño));
            Assert.IsTrue(entrada.EstaAutorizado(dueño));
        }

        [TestMethod]
        public void AccesoUsuario()
        {
            var usuario = new Usuario(1, "Nombre2", "Apellido2", "usuario@alu.ubu.es", "p@sword", false);

            /*
             * CP14: Probar que un usuario no tiene acceso al no estar incluido como usuario autorizado.
             * Precondiciones: Entrada creada.
             * Datos de entrada: Usuario
             * Proceso: 1 Comprobar si el usuario está entre el conjunto de usuario autorizado
             *          2 Comprobar que la desautorización no tiene efecto
             * Resultado esperado: El usuario no tiene acceso.
             */
            Assert.IsFalse(entrada.EstaAutorizado(usuario));
            Assert.IsFalse(entrada.Desautorizar(usuario));

            /*
             * CP15: Probar que es posible autorizar a un usuario.
             * Precondiciones: Entrada creada.
             * Datos de entrada: Usuario no autorizado
             * Proceso: 1 Autorizar usuario
             *          2 Comprbar que está autorizado
             *          3 Comprobar que la autorización de nuevo no tendría efecto
             * Resultado esperado: El usuario tiene acceso después de la autorización.
             */
            Assert.IsTrue(entrada.Autorizar(usuario));
            Assert.IsTrue(entrada.EstaAutorizado(usuario));
            Assert.IsFalse(entrada.Autorizar(usuario));


            /*
             * CP16: Probar que es posible desautorizar a un usuario.
             * Precondiciones: Entrada creada.
             * Datos de entrada: Usuario autorizado
             * Proceso: 1 Desautorizar usuario
             *          2 Comprbar que no está autorizado
             *          3 Comprobar que la desautorización de nuevo no tendría efecto
             * Resultado esperado: El usuario no tiene acceso después de la desautorización.
             */
            Assert.IsTrue(entrada.Desautorizar(usuario));
            Assert.IsFalse(entrada.EstaAutorizado(usuario));
            Assert.IsFalse(entrada.Desautorizar(usuario));
        }

        [TestMethod]
        public void FormatoTest()
        {
            /*
             * CP17: Probar la representación textual de una Entrada respecto al formato requerido.
             * Precondiciones: Entrada creada
             * Datos de entrada: -
             * Proceso: 1 Obtener la representación textual
             *          2 Comparar con una cadena creada correcta
             * Resultado esperado: La cadena generada coincide con la creada particularmente.
             */
            Assert.AreEqual(entrada.ToString(), $"Entrada | Dueño: {dueño} | Descripción: Descripción de la entrada | Email: email@gmail.com | Contraseña: P@ssword");
        }
    }
}