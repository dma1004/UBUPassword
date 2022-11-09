using LibClass;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos
{
    public class DBPruebas : ICapaDatos
    {
        private Int16 nextIdUsuario;
        private Int16 nextIdEntrada;
        private Int16 nextIdEntradaLog;
        
        public Dictionary<string, Usuario> usuarios = new Dictionary<string, Usuario>();
        public Dictionary<Int16, EntradaLog> log = new Dictionary<short, EntradaLog>();
        public Dictionary<Int16, Entrada> entradas = new Dictionary<short, Entrada>();

        /// <summary>
        /// Constructor que simula la generación de datos en la base de datos
        /// para la aplicación.
        /// </summary>
        public DBPruebas()
        {
            AñadirUsuario("Pepito", "Pérez", "pperez@ubu.es", true, "P@aSSWord");
            AñadirUsuario("Fernando", "Fernández", "ffx1004@alu.ubu.es", false, "Contraseña1");
            AñadirUsuario("Martín", "Martínez", "mmx1003@alu.ubu.es", false, "123456siete");
            Usuario caducado = ObtenerUsuario("mmx1003@alu.ubu.es"); //Para los casos con contraseña caducada
            caducado.LastPassDate = caducado.LastPassDate.AddDays(-40);

            CrearEntrada("Netflix", 0, "pperez@ubu.es", "passEntrada1");
            CrearEntrada("AmazonPrime", 0, "pperez@ubu.es", "passEntrada2");
            CrearEntrada("HBO", 0, "pperez@ubu.es", "passEntrada3");


            Entrada entr;
            Entrada entr2;
            entradas.TryGetValue(0, out entr);
            entradas.TryGetValue(1, out entr2);

            Usuario aux;
            usuarios.TryGetValue("ffx1004@alu.ubu.es", out aux);

            entr.Autorizar(aux);
            entr2.Autorizar(aux);


            CrearEntradaLog(aux.Id, null);
            CrearEntradaLog(aux.Id, entr);
            CrearEntradaLog(aux.Id, entr2);
        }

        public Usuario AñadirUsuario(string nombre, string apellido, string email, bool esGestor, string password)
        {
            bool emailCorrecto = Utils.EsEmail(email);
            bool passCorrecta = Utils.PasswordCorrecto(password);
            if (!emailCorrecto || !passCorrecta || usuarios.ContainsKey(email)) return null;


            Usuario nuevo = new Usuario(nextIdUsuario++, nombre, apellido, email, password, esGestor);
            usuarios.Add(email, nuevo);
            return nuevo;
        }

        public Usuario ObtenerUsuario(string email)
        {
            usuarios.TryGetValue(email, out Usuario usuario);
            return usuario;
        }

        public Usuario ObtenerUsuario(Int16 id)
        {
            return usuarios.Values.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Usuario> ObtenerTodosUsuarios()
        {
            return usuarios.Values.ToList();
        }

        public bool BorraUsuario(string email)
        {
            return usuarios.Remove(email);
        }

        public bool BorraUsuario(Int16 id)
        {
            Usuario u = ObtenerUsuario(id);
            if (u == null)
            {
                return false;
            }
            return usuarios.Remove(u.Email);
        }

        public int NumeroUsuarios()
        {
            return usuarios.Count;
        }

        public void CrearEntradaLog(Int16 idUsuario, Entrada entrada)
        {
            Usuario usuario = ObtenerUsuario(idUsuario);
            EntradaLog e;
            if (entrada == null)
            {
                e = new EntradaLog(nextIdEntradaLog++, DateTime.Now, usuario);
            }
            else
            {
                e = new EntradaLog(nextIdEntradaLog++, DateTime.Now, usuario, entrada);
            }
            log.Add(e.Id, e);
        }

        public List<EntradaLog> ObtenerTodasEntradasLog()
        {
            return log.Values.ToList();
        }

        public Entrada CrearEntrada(string descripcion, Int16 idUsuario, string email, string password)
        {
            Usuario usuario = ObtenerUsuario(idUsuario);
            if (usuario == null || !Utils.EsEmail(email)) return null;
            //Comprobar email
            Entrada en = new Entrada(nextIdEntrada++, usuario, email, password, descripcion);
            entradas.Add(en.Id, en);
            return en;
        }

        public Entrada ObtenerEntrada(Int16 idEntrada)
        {
            entradas.TryGetValue(idEntrada, out Entrada entrada);
            return entrada;
        }

        public List<Entrada> ObtenerTodasEntradas()
        {
            return entradas.Values.ToList();
        }

        public bool BorrarEntrada(Entrada e)
        {
            return BorrarEntrada(e.Id);
        }

        public bool BorrarEntrada(Int16 idEntrada)
        {
            return entradas.Remove(idEntrada);
        }

        public bool PuedeAccederEntrada(Entrada en, Int16 idUsuario)
        {
            return en.EstaAutorizado(ObtenerUsuario(idUsuario));
        }

        public bool DarAcceso(Entrada en, Int16 idUsuario)
        {
            return en.Autorizar(ObtenerUsuario(idUsuario));
        }

        public bool QuitarAcceso(Entrada en, Int16 idUsuario)
        {
            return en.Desautorizar(ObtenerUsuario(idUsuario));
        }

    }
}
