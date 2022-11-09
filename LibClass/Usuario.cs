using System;

namespace LibClass
{
    public class Usuario
    {
        private readonly Int16 id;
        private string nombre;
        private string apellidos;
        private string email;
        private string password;
        private bool esGestor;
        private DateTime lastPassDate;

        /// <summary>
        /// Constructor de un usuario.
        /// </summary>
        /// <param name="id">Id del usuario.</param>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <param name="apellidos">Apellidos del usuario.</param>
        /// <param name="email">Email del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <param name="esGestor">Flag que indica si el usuario es gestor.</param>
        public Usuario(Int16 id, string nombre, string apellido, string email, string password, bool esGestor)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellidos = apellido;
            this.email = email;
            this.password = Utils.Encriptar(password);
            this.esGestor = esGestor;
            this.lastPassDate = DateTime.Today;
        }

        // Getters y setters

        public Int16 Id
        {
            get => id;
        }

        public string Nombre
        {
            get => nombre;
            set => nombre = value;
        }

        public string Apellidos
        {
            get => apellidos;
            set => apellidos = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public bool EsGestor
        {
            get => esGestor;
            set => esGestor = value;
        }

        public DateTime LastPassDate
        {
            get => lastPassDate;
            set => lastPassDate = value;
        }

        // Demás métodos
        /// <summary>
        /// Este método comprueba si una cadena de caracteres 
        /// coincide con la contraseña cifrada almacenada del usuario.
        /// </summary>
        /// <param name="enteredPassword">Cadena de caracteres (posible contraseña)</param>
        /// <returns>
        /// true si la cadena de caracteres cifrada coincide con la contraseña almacenada,
        /// falso en caso contrario.
        /// </returns>
        public bool ComprobarContraseña(string enteredPassword)
        {
            return Utils.Encriptar(enteredPassword) == password;
        }


        /// <summary>
        /// Este método permite modificar la contraseña actual a una nueva.
        /// Solo lo realiza si la actual coincide con el parámetro "anterior" introducido.
        /// </summary>
        /// <param name="anterior">Contraseña anterior en formato limpio.</param>
        /// <param name="nueva">Contraseña nueva en formato limpio.</param>
        /// <returns>
        /// true si la contraseña anterior coincide con la actual (y cambia a la nueva), false en caso contrario.
        /// </returns>
        public bool CambiarContraseña(string anterior, string nueva)
        {
            if (Utils.Encriptar(anterior) != password) return false;

            password = Utils.Encriptar(nueva);
            return true;
        }

        /// <summary>
        /// Este método comprueba si la contraseña ha caducado (han pasado 30 días desde
        /// la última vez que se modificó o creó).
        /// </summary>
        /// <returns>
        /// true si la contraseña ha caducado, false en caso contrario.
        /// </returns>
        public bool ContraseñaCaducada()
        {
            var vencimiento = lastPassDate.AddDays(30);
            return DateTime.Today > vencimiento;
        }

        /// <summary>
        /// Este método transforma el objeto Usuario en una representación textual.
        /// </summary>
        /// <returns>
        /// Cadena de caracteres que representa al usuario.
        /// </returns>
        public override string ToString()
        {
            return $"{nombre} {apellidos} ({email})";
        }
    }
}