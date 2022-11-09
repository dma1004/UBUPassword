using System;

namespace LibClass
{
    public class EntradaLog
    {
        private readonly Int16 id;
        private readonly DateTime fecha;
        private readonly Usuario usuario;
        private readonly Entrada entrada;

        /// <summary>
        /// Constructor que crea una entrada de log cuyo acceso fue a una entrada.
        /// </summary>
        /// <param name="id">id de la entrada del log.</param>
        /// <param name="fecha">fecha del suceso.</param>
        /// <param name="usuario">usuario que provocó el suceso.</param>
        /// <param name="entrada">entrada a la que se accedió.</param>
        public EntradaLog(Int16 id, DateTime fecha, Usuario usuario, Entrada entrada)
        {
            this.id = id;
            this.fecha = fecha;
            this.usuario = usuario;
            this.entrada = entrada;
        }

        /// <summary>
        /// Constructor que crea una entrada de log cuyo de un inicio de sesión.
        /// </summary>
        /// <param name="id">id de la entrada del log.</param>
        /// <param name="fecha">fecha del suceso.</param>
        /// <param name="usuario">usuario que provocó el suceso.</param>
        public EntradaLog(Int16 id, DateTime fecha, Usuario usuario)
        {
            this.id = id;
            this.fecha = fecha;
            this.usuario = usuario;
        }

        public Int16 Id
        {
            get => id;
        }

        public DateTime Fecha
        {
            get => fecha;
        }

        public Usuario Usuario
        {
            get => usuario;
        }

        public Entrada Entrada
        {
            get => entrada;
        }

        /// <summary>
        /// Este método transforma el objeto EntradaLog en una representación textual.
        /// </summary>
        /// <returns>
        /// Cadena de caracteres que representa la entrada del log.
        /// </returns>
        public override string ToString()
        {
            if (this.entrada != null)
            {
                return $"A fecha y hora {fecha} el usuario con id {usuario.Id} accedió a {entrada}";
            }
            else
            {
                return $"A fecha y hora {fecha} el usuario con id {usuario.Id} accedió al sistema";
            }
        }
    }
}
