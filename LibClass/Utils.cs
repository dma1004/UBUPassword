using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace LibClass
{
    public static class Utils
    {
        /// <summary>
        /// Este método comprueba de forma simple una complejidad de contraseña.
        /// Debe tener entre 8 y 16 caracteres.
        /// </summary>
        /// <param name="pass">Cadena de caracteres de una contraseña</param>
        /// <returns>true si cumple con la complejidad, false en caso contrario</returns>
        public static bool PasswordCorrecto(string pass)
        {
            if (pass.Length < 8 || pass.Length > 16)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Este método comprueba si una cadena de caracteres tiene formato de email.
        /// </summary>
        /// <param name="email">Cadena de caracteres del supueso email</param>
        /// <returns>true si tiene formato de email, false en caso contrario</returns>
        public static bool EsEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }


        /// <summary>
        /// Este método genera un cifrado SHA256 a partir de una cadena.
        /// </summary>
        /// <param name="password">Cadena de caracteres correspondiente a una contraseña</param>
        /// <returns>Cadena de caracteres de la contraseña cifrada</returns>
        public static string Encriptar(string password)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
            SHA256 mySHA256 = SHA256.Create();
            bytes = mySHA256.ComputeHash(bytes);
            return (System.Text.Encoding.ASCII.GetString(bytes));
        }

    }
}