using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace www.Tests
{
    [TestClass]
    public class Sistema
    {
        private static IWebDriver[] drivers;

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            drivers = new IWebDriver[] {
                new ChromeDriver(),
                new FirefoxDriver()
                //new InternetExplorerDriver() 
                };
        }

        [ClassCleanup]
        public static void CleanupClass()
        {
            try
            {
                //driver.Quit();// quit does not close the window
                foreach (var driver in drivers)
                {
                    driver.Quit();
                }
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        /*
         * CP55: Probar que un usuario existente y credenciales correctas puede iniciar sesión.
         * Precondiciones: Página cargada.
         * Datos de entrada: Email: “ffx1004@alu.ubu.es” y Contraseña: “Contraseña1”.
         * Proceso: 1. Abrir navegador en http://localhost:58743/Inicio.aspx
                    2. Iniciar sesión con Email y Contraseña
                    3. Comprobar que se encuentra en la pestaña de Entradas (mediante el título en pantalla)
                    4. Cerrar sesión
                    5. Comprobar que se encuentra en la pestaña de Inicio de Sesión (mediante el título en pantalla)
         * Resultado esperado: Un usuario puede iniciar sesión accediendo a la pestaña de Entradas
         */
        [TestMethod]
        public void InicioDeSesionUsuarioTest()
        {

            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl("http://localhost:58743/Inicio.aspx");
                Assert.AreEqual("Inicio de sesión", driver.FindElement(By.Id("tltInicioSesion")).Text);
                driver.FindElement(By.Id("TBXUserName")).Clear();
                driver.FindElement(By.Id("TBXUserName")).SendKeys("ffx1004@alu.ubu.es");
                driver.FindElement(By.Id("TBXPassword")).Clear();
                driver.FindElement(By.Id("TBXPassword")).SendKeys("Contraseña1");
                driver.FindElement(By.Id("Entrar")).Click();
                Thread.Sleep(100);
                Assert.AreEqual("Entradas", driver.FindElement(By.Id("tltEntradas")).Text);
                driver.FindElement(By.Id("btnCerrarSesion")).Click();
                Thread.Sleep(100);
                Assert.AreEqual("Inicio de sesión", driver.FindElement(By.Id("tltInicioSesion")).Text);
            }
        }

        [TestMethod]
        public void InicioDeSesionContraseñaCaducadaTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl("http://localhost:58743/Inicio.aspx");
                Assert.AreEqual("Inicio de sesión", driver.FindElement(By.Id("tltInicioSesion")).Text);
                driver.FindElement(By.Id("TBXUserName")).Clear();
                driver.FindElement(By.Id("TBXUserName")).SendKeys("mmx1003@alu.ubu.es");
                driver.FindElement(By.Id("TBXPassword")).Clear();
                driver.FindElement(By.Id("TBXPassword")).SendKeys("123456siete");
                driver.FindElement(By.Id("Entrar")).Click();
                Thread.Sleep(100);
                Assert.AreEqual("Contraseña caducada (30 días), contacta con un Gestor", driver.FindElement(By.Id("lblerror")).Text);
            }
        }


        /*
         * CP56: Probar que un gestor existente y credenciales correctas puede iniciar sesión.
         * Precondiciones: Página cargada.
         * Datos de entrada: Email: “pperez@ubu.es” y Contraseña: “P@aSSWord”
         * Proceso: 1. Abrir navegador en http://localhost:58743/Inicio.aspx
                    2. Iniciar sesión con Email y Contraseña
                    3. Comprobar que se encuentra en la pestaña de Gestión (mediante el título en pantalla)
                    4. Cerrar sesión
                    5. Comprobar que se encuentra en la pestaña de Inicio de Sesión (mediante el título en pantalla)
         * Resultado esperado: Un gestor puede iniciar sesión accediendo a la pestaña de Gestión
         */
        [TestMethod]
        public void InicioDeSesionGestorTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl("http://localhost:58743/Inicio.aspx");
                Assert.AreEqual("Inicio de sesión", driver.FindElement(By.Id("tltInicioSesion")).Text);
                driver.FindElement(By.Id("TBXUserName")).Clear();
                driver.FindElement(By.Id("TBXUserName")).SendKeys("pperez@ubu.es");
                driver.FindElement(By.Id("TBXPassword")).Clear();
                driver.FindElement(By.Id("TBXPassword")).SendKeys("P@aSSWord");
                driver.FindElement(By.Id("Entrar")).Click();
                Thread.Sleep(100);
                Assert.AreEqual("Zona Gestión", driver.FindElement(By.Id("tltGestion")).Text);
                driver.FindElement(By.Id("btnCerrarSesion")).Click();
                Thread.Sleep(100);
                Assert.AreEqual("Inicio de sesión", driver.FindElement(By.Id("tltInicioSesion")).Text);
            }
        }

        /*
         * CP57: Probar que un gestor existente y credenciales correctas puede iniciar sesión.
         * Precondiciones: Página cargada.
         * Datos de entrada: Email: “” y Contraseña: “”
         * Proceso: 1. Abrir navegador en http://localhost:58743/Inicio.aspx
                    2. Iniciar sesión con Email y Contraseña
                    3. Comprobar que no se ha podido iniciar sesión mediante un mensaje de error: "Email incorrecto"
         * Resultado esperado: No se ha iniciado sesión y se obtiene mensaje de error
         */
        [TestMethod]
        public void InicioDeSesionBlancoTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl("http://localhost:58743/Inicio.aspx");
                Assert.AreEqual("Inicio de sesión", driver.FindElement(By.Id("tltInicioSesion")).Text);
                driver.FindElement(By.Id("TBXUserName")).Clear();
                driver.FindElement(By.Id("TBXPassword")).Clear();
                driver.FindElement(By.Id("Entrar")).Click();
                Thread.Sleep(100);
                Assert.AreEqual("Email incorrecto", driver.FindElement(By.Id("lblerror")).Text);
            }
        }

        [TestMethod]
        public void InicioDeSesionUsuarioInexistenteTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl("http://localhost:58743/Inicio.aspx");
                Assert.AreEqual("Inicio de sesión", driver.FindElement(By.Id("tltInicioSesion")).Text);
                driver.FindElement(By.Id("TBXUserName")).Clear();
                driver.FindElement(By.Id("TBXUserName")).SendKeys("noexiste@ubu.es");
                driver.FindElement(By.Id("TBXPassword")).Clear();
                driver.FindElement(By.Id("TBXPassword")).SendKeys("P@aSSWord");
                driver.FindElement(By.Id("Entrar")).Click();
                Thread.Sleep(100);
                Assert.AreEqual("Email incorrecto", driver.FindElement(By.Id("lblerror")).Text);
            }
        }

        /*
         * CP58: Probar que un gestor existente y credenciales correctas puede iniciar sesión.
         * Precondiciones: Página cargada.
         * Datos de entrada: Email: “pperez@ubu.es” y Contraseña: “P@aSSWord1”
         * Proceso: 1. Abrir navegador en http://localhost:58743/Inicio.aspx
                    2. Iniciar sesión con Email y Contraseña
                    3. Comprobar que no se ha podido iniciar sesión mediante un mensaje de error: "Contraseña incorrecta"
         * Resultado esperado: No se ha iniciado sesión y se obtiene mensaje de error
         */
        [TestMethod]
        public void InicioDeSesionUsuarioPassErroneaTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl("http://localhost:58743/Inicio.aspx");
                Assert.AreEqual("Inicio de sesión", driver.FindElement(By.Id("tltInicioSesion")).Text);
                driver.FindElement(By.Id("TBXUserName")).Clear();
                driver.FindElement(By.Id("TBXUserName")).SendKeys("pperez@ubu.es");
                driver.FindElement(By.Id("TBXPassword")).Clear();
                driver.FindElement(By.Id("TBXPassword")).SendKeys("P@aSSWord1");
                driver.FindElement(By.Id("Entrar")).Click();
                Thread.Sleep(100);
                Assert.AreEqual("Contraseña incorrecta", driver.FindElement(By.Id("lblerror")).Text);
            }
        }

        [TestMethod]
        public void GestorGestionDeUsuariosTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl("http://localhost:58743/Inicio.aspx");
                driver.FindElement(By.Id("TBXUserName")).Clear();
                driver.FindElement(By.Id("TBXUserName")).SendKeys("pperez@ubu.es");
                driver.FindElement(By.Id("TBXPassword")).Clear();
                driver.FindElement(By.Id("TBXPassword")).SendKeys("P@aSSWord");
                driver.FindElement(By.Id("Entrar")).Click();
                Thread.Sleep(100);
                Assert.AreEqual("Zona Gestión", driver.FindElement(By.Id("tltGestion")).Text);
                driver.FindElement(By.Id("ddlSelFuncionGestor")).Click();
                Thread.Sleep(100);
                new SelectElement(driver.FindElement(By.Id("ddlSelFuncionGestor"))).SelectByText("Gestión Usuarios");
                Thread.Sleep(100);
                Assert.AreEqual("Id. Usuario", driver.FindElement(By.XPath("//table[@id='gvGestionUsuarios']/tbody/tr/th")).Text);
                Assert.AreEqual("ffx1004@alu.ubu.es", driver.FindElement(By.XPath("//table[@id='gvGestionUsuarios']/tbody/tr[3]/td[4]")).Text);
                driver.FindElement(By.Id("btnCerrarSesion")).Click();
            }
        }

        [TestMethod]
        public void GestorLogTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl("http://localhost:58743/Inicio.aspx");
                driver.FindElement(By.Id("TBXUserName")).Clear();
                driver.FindElement(By.Id("TBXUserName")).SendKeys("pperez@ubu.es");
                driver.FindElement(By.Id("TBXPassword")).Clear();
                driver.FindElement(By.Id("TBXPassword")).SendKeys("P@aSSWord");
                driver.FindElement(By.Id("Entrar")).Click();
                Thread.Sleep(100);
                Assert.AreEqual("Zona Gestión", driver.FindElement(By.Id("tltGestion")).Text);
                driver.FindElement(By.Id("ddlSelFuncionGestor")).Click();
                Thread.Sleep(100);
                new SelectElement(driver.FindElement(By.Id("ddlSelFuncionGestor"))).SelectByText("Log");
                Thread.Sleep(100);
                Assert.AreEqual("Tipo de acceso", driver.FindElement(By.XPath("//table[@id='gvLog']/tbody/tr/th[4]")).Text);
                Assert.AreEqual("Fernando Fernández (ffx1004@alu.ubu.es)", driver.FindElement(By.XPath("//table[@id='gvLog']/tbody/tr[2]/td[3]")).Text);
                Assert.AreEqual("Inicio de sesión", driver.FindElement(By.XPath("//table[@id='gvLog']/tbody/tr[2]/td[4]")).Text);
                driver.FindElement(By.Id("btnCerrarSesion")).Click();
            }
        }

        [TestMethod]
        public void GestorIrEntradasTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl("http://localhost:58743/Inicio.aspx");
                driver.FindElement(By.Id("TBXUserName")).Clear();
                driver.FindElement(By.Id("TBXUserName")).SendKeys("pperez@ubu.es");
                driver.FindElement(By.Id("TBXPassword")).Clear();
                driver.FindElement(By.Id("TBXPassword")).SendKeys("P@aSSWord");
                driver.FindElement(By.Id("Entrar")).Click();
                Thread.Sleep(100);
                Assert.AreEqual("Zona Gestión", driver.FindElement(By.Id("tltGestion")).Text);
                driver.FindElement(By.Id("btnIrEntradas")).Click();
                Thread.Sleep(100);
                Assert.AreEqual("Entradas", driver.FindElement(By.Id("tltEntradas")).Text);
                Assert.AreEqual("Descripción", driver.FindElement(By.XPath("//table[@id='gvEntradas']/tbody/tr/th[2]")).Text);
                driver.FindElement(By.XPath("//input[@value='Ver contraseña']")).Click();
                Thread.Sleep(100);
                Assert.AreEqual("Has accedido a la entrada 0, su contraseña es: passEntrada1", driver.FindElement(By.Id("lblMostrarContraseña")).Text);
                driver.FindElement(By.Id("btnIrGestion")).Click();
                Thread.Sleep(100);
                Assert.AreEqual("Zona Gestión", driver.FindElement(By.Id("tltGestion")).Text);
                driver.FindElement(By.Id("btnCerrarSesion")).Click();
            }
        }

        [TestMethod]
        public void UsuarioAccedeEntradasTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl("http://localhost:58743/Inicio.aspx");
                driver.FindElement(By.Id("TBXUserName")).Clear();
                driver.FindElement(By.Id("TBXUserName")).SendKeys("ffx1004@alu.ubu.es");
                driver.FindElement(By.Id("TBXPassword")).Clear();
                driver.FindElement(By.Id("TBXPassword")).SendKeys("Contraseña1");
                driver.FindElement(By.Id("Entrar")).Click();
                Thread.Sleep(100);
                Assert.AreEqual("Entradas", driver.FindElement(By.Id("tltEntradas")).Text);
                Assert.AreEqual("Descripción", driver.FindElement(By.XPath("//table[@id='gvEntradas']/tbody/tr/th[2]")).Text);
                driver.FindElement(By.XPath("//input[@value='Ver contraseña']")).Click();
                Thread.Sleep(100);
                Assert.AreEqual("Has accedido a la entrada 0, su contraseña es: passEntrada1", driver.FindElement(By.Id("lblMostrarContraseña")).Text);
                driver.FindElement(By.Id("btnCerrarSesion")).Click();
            }
        }
    }
}
