using System;
using System.ComponentModel.DataAnnotations;

namespace AgenciaViajes.Validations
{
    public class FechaEntradaValidacionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime FechaEntrada)
            {
                DateTime fechaActual = DateTime.Now;
                DateTime Fechaminima = fechaActual.AddDays(+1);

                if (FechaEntrada >= Fechaminima)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}