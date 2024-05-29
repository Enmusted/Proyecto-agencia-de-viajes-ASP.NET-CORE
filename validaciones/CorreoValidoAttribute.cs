using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AgenciaViajes.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class CorreoValidoAttribute : ValidationAttribute
    {
        public CorreoValidoAttribute()
        {
            ErrorMessage = "La dirección de correo electrónico no es válida.";
        }
        public override bool IsValid(object value)
        {
            if (value is string correo)
            {
                // Define la expresión regular para validar la dirección de correo electrónico
                string expresionRegular = @"^(?=.{1,64}@.{4,255}$)([a-zA-Z0-9][a-zA-Z0-9._-]*[a-zA-Z0-9]|[a-zA-Z0-9])" +
                          @"@([a-zA-Z0-9.-]+[a-zA-Z]{2,4}\.)[a-zA-Z]{2,4}$";
                // Comprueba si el correo cumple con la expresión regular
                if (Regex.IsMatch(correo, expresionRegular))
                {
                    return true;
                }
            }

            return false;
        }
    }
}

