using System;
using System.ComponentModel.DataAnnotations;

namespace AgenciaViajes.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class FechaVencimientoAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string fechaVencimiento)
            {
                if (DateTime.TryParseExact(fechaVencimiento, "MM/yy", null, System.Globalization.DateTimeStyles.None, out var parsedDate))
                {
                    // Obtén la fecha actual
                    DateTime fechaActual = DateTime.Now;

                    // Define el rango máximo y mínimo
                    DateTime fechaMinima = new DateTime(fechaActual.Year, fechaActual.Month, 1); // Primer día del mes actual
                    DateTime fechaMaxima = fechaMinima.AddYears(5).AddMonths(-1); // Último día del quinto año a partir del mes actual

                    // Comprueba si la fecha de vencimiento está en el rango permitido
                    return (parsedDate >= fechaMinima) && (parsedDate <= fechaMaxima);
                }
            }

            // Si el valor no es una fecha válida en el formato esperado, se considera inválido
            return false;
        }

    }
}
