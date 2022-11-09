using System;
using System.Collections.Generic;

namespace LibClass
{
    public class Entrada
    {
        private readonly Int16 id;
        private Usuario dueño;
        private string email;
        private string password;
        private string descripción;
        private ISet<Usuario> usuariosAutorizados;

        public Entrada(Int16 id, Usuario dueño, string email, string password, string descripción)
        {
            this.id = id;
            this.dueño = dueño;
            this.email = email;
            this.password = password;
            this.descripción = descripción;

            usuariosAutorizados = new HashSet<Usuario>();
        }

        // Getters y setters

        public Int16 Id
        {
            get => id;
        }

        public Usuario Dueño
        {
            get => dueño;
            set => dueño = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }

        public string Descripción
        {
            get => descripción;
            set => descripción = value;
        }

        // Demás métodos
        /// <summary>
        /// Este método autoriza el acceso a un usuario.
        /// </summary>
        /// <param name="usuario">Objeto de tipo Usuario del usuario que se quiere dar acceso.</param>
        /// <returns>
        /// true si el usuario a dar acceso no es el propio dueño y si no estaba ya autorizado, false en caso contario.
        /// </returns>
        public bool Autorizar(Usuario usuario)
        {
            return usuario != dueño && usuariosAutorizados.Add(usuario);
        }

        /// <summary>
        /// Este método quita el acceso a un usuario.
        /// </summary>
        /// <param name="usuario">
        /// Objeto usuario a desautorizar
        /// </param>
        /// <returns>
        /// true si quita el acceso, false en caso contrario.
        /// </returns>
        public bool Desautorizar(Usuario usuario)
        {
            return usuariosAutorizados.Remove(usuario);
        }

        /// <summary>
        /// Este método comprueba si un usuario tiene acceso a la entrada.
        /// </summary>
        /// <param name="usuario">usuario que se quiere comprobar.</param>
        /// <returns>
        /// true si tiene acceso, false en caso contrario.
        /// </returns>
        public bool EstaAutorizado(Usuario usuario)
        {
            return usuariosAutorizados.Contains(usuario) || usuario == dueño;
        }

        /// <summary>
        /// Este método transforma el objeto Entrada en una representación textual.
        /// </summary>
        /// <returns>
        /// Cadena de caracteres que representa a la entrada.
        /// </returns>
        public override string ToString()
        {
            return $"Entrada | Dueño: {dueño} | Descripción: {descripción} | Email: {email} | Contraseña: {password}";
        }
    }
}