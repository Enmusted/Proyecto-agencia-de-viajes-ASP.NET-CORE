using System;
using System.ComponentModel.DataAnnotations;

namespace AgenciaViajes.Validations
{
    public class FechaNacimientoValidaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime fechaNacimiento)
            {
                DateTime fechaActual = DateTime.Now;
                int edadMinima = 100;
                int edadMaxima = 18;
                DateTime fechaNacimientoMinima = fechaActual.AddYears(-edadMinima);
                DateTime fechaNacimientoMaxima = fechaActual.AddYears(-edadMaxima);

                if (fechaNacimiento >= fechaNacimientoMinima && fechaNacimiento <= fechaNacimientoMaxima)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}