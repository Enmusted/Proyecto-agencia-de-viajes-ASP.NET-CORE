using System;
using System.ComponentModel.DataAnnotations;

namespace AgenciaViajes.Validations
{
    public class FechaSalidaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime FechaEntrada)
            {
                DateTime fechaActual = DateTime.Now;
                int tiempo = 1;
                DateTime Fechaminima = fechaActual.AddDays(+tiempo);

                if (FechaEntrada<Fechaminima)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}