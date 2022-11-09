using LibClass;
using System;
using System.Collections.Generic;

namespace Datos
{
    public interface ICapaDatos
    {
        /// <summary>
        /// Este método añade un usuario a la base de datos.
        /// </summary>
        /// <param name="nombre">nombre del usuario.</param>
        /// <param name="apellido">apellido del usuario.</param>
        /// <param name="email">email del usuario.</param>
        /// <param name="esGestor">flag para indicar si es gestor.</param>
        /// <param name="password">contraseña de la cuenta del usuario.</param>
        /// <returns>
        /// El usuario creado y añadido
        /// </returns>
        Usuario AñadirUsuario(string nombre, string apellido, string email, bool esGestor, string password);

        /// <summary>
        /// Este método obtiene la referencia
        /// a un usuario mediante el email.
        /// </summary>
        /// <param name="email">Email del supuesto usuario.</param>
        /// <returns>
        /// El usuario si se encuentra en la base de datos
        /// o null si no se encuentra.
        /// </returns>
        Usuario ObtenerUsuario(string email);

        /// <summary>
        /// Este método obtiene la referencia
        /// a un usuario mediante su Id.
        /// </summary>
        /// <param name="id">Id del supuesto usuario.</param>
        /// <returns>
        /// El usuario si se encuentra en la base de datos
        /// o null si no se encuentra.
        /// </returns>
        Usuario ObtenerUsuario(Int16 id);

        /// <summary>
        /// Este método devuelve todos los usuarios que se encuentran en la base de datos.
        /// </summary>
        /// <returns>
        /// Lista con todos los usuarios.
        /// </returns>
        List<Usuario> ObtenerTodosUsuarios();

        /// <summary>
        /// Este método elimina un usuario de la base de datos mediante su email.
        /// </summary>
        /// <param name="email">Email del usuario a eliminar.</param>
        /// <returns>
        /// true si lo ha eliminado (existía), falso en caso contrario.
        /// </returns>
        bool BorraUsuario(string email);

        /// <summary>
        /// Este método elimina un usuario de la base de datos mediante su Id.
        /// </summary>
        /// <param name="id">Id del usuario a eliminar.</param>
        /// <returns>
        /// true si lo ha eliminado (existía), falso en caso contrario.
        /// </returns>
        bool BorraUsuario(Int16 id);

        /// <summary>
        /// Este método consulta el número de usuarios que hay en la base de datos.
        /// </summary>
        /// <returns>
        /// Número de usuarios en la base de datos.
        /// </returns>
        int NumeroUsuarios();


        /*
         * Métodos relacionados con las EntradasLog
         */
        /// <summary>
        /// Este método crea e introduce una entrada del log en la base de datos.
        /// </summary>
        /// <param name="idUsuario">Id del usuario que se quiere registrar.</param>
        /// <param name="entrada">El objeto entrada si el acceso 
        /// es a una entrada o null si es un inicio de sesión.</param>
        void CrearEntradaLog(Int16 idUsuario, Entrada entrada);

        /// <summary>
        /// Este método devuelve todas las entradas del log que se han registrado.
        /// </summary>
        /// <returns>Lista de EntradaLog.</returns>
        List<EntradaLog> ObtenerTodasEntradasLog();


        /// <summary>
        /// Este método crea una entrada de credenciales.
        /// </summary>
        /// <param name="descripcion">Descripción de la entrada (de qué son las credenciales)</param>
        /// <param name="idUsuario">Id del usuario dueño de la entrada de crenciales.</param>
        /// <param name="password">Contraseña para compartir.</param>
        /// <returns>
        /// Entrada creada e introducida en la base de datos o null si el usuario no existe.
        /// </returns>
        Entrada CrearEntrada(string descripcion, Int16 idUsuario, string email, string password);

        /// <summary>
        /// Este método obtiene una Entrada mediante su Id.
        /// </summary>
        /// <param name="idEntrada">Id de la entrada.</param>
        /// <returns>
        /// Entrada conrrespondiente a la Id o null si no existe.
        /// </returns>
        Entrada ObtenerEntrada(Int16 idEntrada);

        /// <summary>
        /// Este método obtiene todas las entradas de credenciales almacenadas
        /// en la base de datos.
        /// </summary>
        /// <returns>Lista con todas las entradas de credenciales.</returns>
        List<Entrada> ObtenerTodasEntradas();

        /// <summary>
        /// Este método elimina una entrada diréctamente con el Objeto.
        /// </summary>
        /// <param name="e">Objeto entrada que se quiere eliminar.</param>
        /// <returns>
        /// true si ha podido eliminar la entrada o false en caso contrario.
        /// </returns>
        bool BorrarEntrada(Entrada e);

        /// <summary>
        /// Este método borra una entrada mediante su Id en la base de datos.
        /// </summary>
        /// <param name="idEntrada">Id de la entrada en la base de datos.</param>
        /// <returns>
        /// true si la entrada con ese id se encontraba en la base de datos, false en caso contrario.
        /// </returns>
        bool BorrarEntrada(Int16 idEntrada);

        /// <summary>
        /// Este método comprueba si un usuario mediante su Id puede acceder a una entrada.
        /// </summary>
        /// <param name="en">Entrada a acceder.</param>
        /// <param name="idUsuario">Id del usuario que intenta acceder.</param>
        /// <returns>
        /// true si puede acceder a la entrada o false en caso contrario.
        /// </returns>
        bool PuedeAccederEntrada(Entrada en, Int16 idUsuario);

        /// <summary>
        /// Este método concede acceso a una entrada al usuario correspondiente con su Id.
        /// </summary>
        /// <param name="en">Entrada a la que dar acceso.</param>
        /// <param name="idUsuario">Id del usuario al que se le de acceso.</param>
        /// <returns>
        /// true si concede acceso, false en caso contrario.
        /// </returns>
        bool DarAcceso(Entrada en, Int16 idUsuario);

        /// <summary>
        /// Este método quita acceso a una entrada al usuario correspondiente con su Id.
        /// </summary>
        /// <param name="en">Entrada a la que quitar acceso.</param>
        /// <param name="idUsuario">Id del usuario al que se le quite el acceso.</param>
        /// <returns>
        /// true si quita el acceso, false en caso contrario.
        /// </returns>
        bool QuitarAcceso(Entrada en, Int16 idUsuario);

    }
}