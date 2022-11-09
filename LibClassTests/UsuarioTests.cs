using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LibClass.Tests
{
    [TestClass()]
    public class UsuarioTests
    {
        private Usuario usuario;

        [TestInitialize]
        public void Initialize()
        {
            usuario = new Usuario(0, "Nombre", "Apellido", "email@email.com", "p@ssword", false);
        }

        [TestMethod()]
        public void UsuarioTest()
        {
            //IMPORTANTE: Se ha decidido abstraer las comprobaciones
            //de correctitud de emails y contraseña al controlador o base de datos

            /*
             * CP01: Probar la creación de un usuario completa y correctamente.
             * Precondiciones: Usuario a probar creado
             * Datos de entrada: El usuario
             * Proceso: 1 Comprobar que el objeto se ha creado 
             *          2 Comprobar la correctitud de todas las propiedades públicas.
             * Resultado esperado: Todas las comprobaciones se verifican con los datos en cuestión.
             */
            Assert.IsNotNull(usuario);
            Assert.AreEqual(usuario.Id, 0);
            Assert.AreEqual(usuario.Nombre, "Nombre");
            Assert.AreEqual(usuario.Apellidos, "Apellido");
            Assert.AreEqual(usuario.Email, "email@email.com");
            Assert.AreEqual(usuario.EsGestor, false);
            Assert.AreEqual(usuario.LastPassDate, DateTime.Today);

            /*
             * CP02: Probar la modificación de las propiedades de usuario (internamente setters y getters).
             * Precondiciones: Usuario a probar creado
             * Datos de entrada: Nombre, Apellidos, Email, Flag de gestor y Fecha nuevos.
             * Proceso: 1 Establecer las propiedades a los valores nuevos.
             *          2 Obtener las propiedades y verificar coincidencia con los datos de entrada.
             * Resultado esperado: Todos las propiedades se establecen y obtienen correctamente.
             */
            usuario.Nombre = "NuevoNombre";
            usuario.Apellidos = "NuevoApellido";
            usuario.Email = "nuevoemail@email.com";
            usuario.EsGestor = true;
            usuario.LastPassDate = new DateTime(2008, 3, 1, 0, 0, 0);

            Assert.AreEqual(usuario.Id, 0);
            Assert.AreEqual(usuario.Nombre, "NuevoNombre");
            Assert.AreEqual(usuario.Apellidos, "NuevoApellido");
            Assert.AreEqual(usuario.Email, "nuevoemail@email.com");
            Assert.AreEqual(usuario.EsGestor, true);
            Assert.AreEqual(usuario.LastPassDate, new DateTime(2008, 3, 1, 0, 0, 0));
        }

        [TestMethod]
        public void ComprobarContraseña()
        {   //Caso de prueba a parte para comprobar el cifrado y la privacidad (private) de las contraseñas
            /*
             * CP03: Probar la coincidencia con una contraseña, cuando esta es la correcta.
             * Precondiciones: Usuario con contraseña creado.
             * Datos de entrada: Contraseña a probar (la correcta).
             * Proceso: Consulta interna de comprobación:
             *              1 Cifrar contraseña introducida
             *              2 Comprobar si coincide con la almacenada
             * Resultado esperado: La contraseña coincide.
             */
            Assert.IsTrue(usuario.ComprobarContraseña("p@ssword"));

            //Caso de prueba a parte para comprobar el cifrado y la privacidad (private) de las contraseñas
            /*
             * CP04: Probar la coincidencia con una contraseña, cuando esta es no correcta.
             * Precondiciones: Usuario con contraseña creado.
             * Datos de entrada: Contraseña a probar (una incorrecta).
             * Proceso: Consulta interna de comprobación:
             *              1 Cifrar contraseña introducida
             *              2 Comprobar si coincide con la almacenada
             * Resultado esperado: La contraseña no coincide.
             */
            Assert.IsFalse(usuario.ComprobarContraseña("p@ssword2"));
        }

        [TestMethod]
        public void CambiarContraseña()
        {
            /*
             * CP05: Probar la modificación de contraseña cuando la anterior no coincide con la actual
             * Precondiciones: Usuario con contraseña creado.
             * Datos de entrada: Contraseña anterior (actual), Contraseña nueva.
             * Proceso: Consulta interna de comprobación:
             *              1 Encriptar contraseña anterior
             *              2 Comparar con contraseña nueva
             * Resultado esperado: La contraseña anterior no coincide con la actual.
             */
            Assert.IsFalse(usuario.CambiarContraseña("notP@ss", "nuevaP@ssword"));

            /*
             * CP06: Probar la modificación de contraseña cuando la anterior coincide con la actual
             * Precondiciones: Usuario con contraseña creado.
             * Datos de entrada: Contraseña anterior (actual), Contraseña nueva.
             * Proceso: 1 Consulta interna de comprobación:
             *              1.1 Encriptar contraseña anterior
             *              1.2 Comparar con contraseña nueva
             *              1.3 Encriptar la nueva contraseña
             *              1.4 Guardar nueva contraseñaç
             *          2 Comprobar que se ha establecido la contraseña
             * Resultado esperado: La contraseña anterior coincide con la actual
             *                      y la contraseña se ha establecido correctamente.
             */
            Assert.IsTrue(usuario.CambiarContraseña("p@ssword", "nuevaP@ssword"));
            Assert.IsTrue(usuario.ComprobarContraseña("nuevaP@ssword"));
        }

        [TestMethod]
        public void CaducidadTest()
        {
            /*
             * CP07: Probar la caducidad de un usuario recién creado.
             * Precondiciones: Usuario creado.
             * Datos de entrada: -
             * Proceso: Consulta interna de comprobación:
             *              1 Comprobar si la diferencia entre la fecha de hoy 
             *                y la almacenada es mayor a 30
             * Resultado esperado: Contraseña NO caducada.
             */
            Assert.IsFalse(usuario.ContraseñaCaducada());

            /*
             * CP08: Probar la caducidad de un usuario creado justo hace 30 días.
             * Precondiciones: Usuario con una contraseña de hace 30 días.
             * Datos de entrada: -
             * Proceso: 1 Modificar contraseña para restarle 30 días
             *          2 Consulta interna de comprobación:
             *              2.1 Comprobar si la diferencia entre la fecha de hoy 
             *                y la almacenada es mayor a 30
             * Resultado esperado: Contraseña NO caducada.
             */
            usuario.LastPassDate = DateTime.Today.AddDays(-30);
            Assert.IsFalse(usuario.ContraseñaCaducada());

            /*
             * CP09: Probar la caducidad de un usuario creado justo hace 30 días.
             * Precondiciones: Usuario con una contraseña de hace 30 días.
             * Datos de entrada: -
             * Proceso: 1 Modificar contraseña para restarle 31 días
             *          2 Consulta interna de comprobación:
             *              2.1 Comprobar si la diferencia entre la fecha de hoy 
             *                y la almacenada es mayor a 30
             * Resultado esperado: Contraseña caducada.
             */
            usuario.LastPassDate = DateTime.Today.AddDays(-31);
            Assert.IsTrue(usuario.ContraseñaCaducada());
        }


        
        [TestMethod]
        public void FormatoTest()
        {
            /*
             * CP10: Probar la representación textual de un Usuario respecto al formato requerido.
             * Precondiciones: Usuario creada
             * Datos de entrada: -
             * Proceso: 1 Obtener la representación textual
             *          2 Comparar con una cadena creada correcta
             * Resultado esperado: La cadena generada coincide con la creada particularmente.
             */
            Assert.AreEqual(usuario.ToString(), $"Nombre Apellido (email@email.com)");
        }
    }
}