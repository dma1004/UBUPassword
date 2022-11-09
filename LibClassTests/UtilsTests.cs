using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibClass.Tests
{
    [TestClass()]
    public class UtilsTests
    {
        [TestMethod()]
        public void PasswordCorrectoTest()
        {
            /*
             * CP22: Probar que una contraseña por debajo de la longitud requerida no es válida.
             * Precondiciones: -
             * Datos de entrada: Pass
             * Proceso: 1 Comparar longitud de la contraseña con la longitud requerida
             * Resultado esperado: La contraseña no es suficientemente compleja.
             */
            Assert.IsFalse(Utils.PasswordCorrecto("Pass"));

            /*
             * CP23: Probar que una contraseña por encima de la longitud requerida no es válida.
             * Precondiciones: -
             * Datos de entrada: EstoEsUnaPassLarga
             * Proceso: 1 Comparar longitud de la contraseña con la longitud requerida
             * Resultado esperado: La contraseña es demasiado larga.
             */
            Assert.IsFalse(Utils.PasswordCorrecto("EstoEsUnaPassLarga"));
            /*
             * CP24: Probar que una contraseña dentro de la longitud es válida.
             * Precondiciones: -
             * Datos de entrada: P@aSSWord
             * Proceso: 1 Comparar longitud de la contraseña con la longitud requerida
             * Resultado esperado: La contraseña es correcta.
             */
            Assert.IsTrue(Utils.PasswordCorrecto("P@aSSWord"));
        }

        
        [TestMethod()]
        public void EsEmailTest()
        {
            /*
             * CP25: Probar que un email sin formato no es válido
             * Precondiciones: -
             * Datos de entrada: email&hola,es
             * Proceso: 1 Comprobar formato
             * Resultado esperado: No es un email.
             */
            Assert.IsFalse(Utils.EsEmail("email&hola,es"));

            /*
             * CP26: Probar que un email es válido
             * Precondiciones: -
             * Datos de entrada: email@hola.es
             * Proceso: 1 Comprobar formato
             * Resultado esperado: No es un email.
             */
            Assert.IsTrue(Utils.EsEmail("email@hola.es"));
        }

    }
}