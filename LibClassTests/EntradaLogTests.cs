using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LibClass.Tests
{
    [TestClass()]
    public class EntradaLogTests
    {
        private DateTime fecha;
        private Usuario u;
        private Entrada e;
        private EntradaLog elEntrada;
        private EntradaLog elLogin;

        [TestInitialize]
        public void Initialize()
        {
            fecha = DateTime.Now;
            u = new Usuario(0, "Guillermo", "Borlán", "gbs2222@alu.ubu.es", "P@ssword", false);
            e = new Entrada(0, u, "netflix@gmail.com", "hola", "Netflix");
            elLogin = new EntradaLog(0, fecha, u);
            elEntrada = new EntradaLog(1, fecha, u, e);
        }

        [TestMethod()]
        public void GetTest()
        {

            /*
             * CP18: Probar la creación de una entrada log completa y correctamente de tipo inicio de sesión.
             * Precondiciones: Entrada Log creado.
             * Datos de entrada: La entrada log
             * Proceso: 1 Comprobar que el objeto se ha creado 
             *          2 Comprobar la correctitud de todas las propiedades públicas:
             *              Concretamente la entrada que almacena la entrada del log debe ser nula (es inicio de sesión).
             * Resultado esperado: Todas las comprobaciones se verifican con los datos en cuestión.
             */
            Assert.IsNotNull(elLogin);
            Assert.AreEqual(elLogin.Id, 0);
            Assert.AreEqual(elLogin.Fecha, fecha);
            Assert.AreEqual(elLogin.Usuario, u);
            Assert.IsNull(elLogin.Entrada);

            /*
             * CP19: Probar la creación de una entrada log completa y correctamente de tipo acceso a entrada de credenciales.
             * Precondiciones: Entrada Log creado.
             * Datos de entrada: La entrada log
             * Proceso: 1 Comprobar que el objeto se ha creado 
             *          2 Comprobar la correctitud de todas las propiedades públicas:
             *              Concretamente la entrada que almacena la entrada del log debe no debe ser nula (acceso a entrada de credenciales).
             * Resultado esperado: Todas las comprobaciones se verifican con los datos en cuestión.
             */
            Assert.IsNotNull(elEntrada);
            Assert.AreEqual(elEntrada.Id, 1);
            Assert.AreEqual(elEntrada.Fecha, fecha);
            Assert.AreEqual(elEntrada.Usuario, u);
            Assert.AreEqual(elEntrada.Entrada, e);
        }

        [TestMethod]
        public void FormatoTest()
        {
            /*
             * CP20: Probar la representación textual de una EntradaLog de inicio de sesión respecto al formato requerido.
             * Precondiciones: EntradaLog creada (inicio de sesión)
             * Datos de entrada: -
             * Proceso: 1 Obtener la representación textual
             *          2 Comparar con una cadena creada correcta
             * Resultado esperado: La cadena generada coincide con la creada particularmente.
             */
            Assert.AreEqual(elEntrada.ToString(), $"A fecha y hora {fecha} el usuario con id {u.Id} accedió a {e}");

            /*
             * CP21: Probar la representación textual de una EntradaLog de acceso a entrada de credenciales respecto al formato requerido.
             * Precondiciones: EntradaLog creada (acceso a entrada de credenciales )
             * Datos de entrada: -
             * Proceso: 1 Obtener la representación textual
             *          2 Comparar con una cadena creada correcta
             * Resultado esperado: La cadena generada coincide con la creada particularmente.
             */
            Assert.AreEqual(elLogin.ToString(), $"A fecha y hora {fecha} el usuario con id {u.Id} accedió al sistema");
        }
    }
}