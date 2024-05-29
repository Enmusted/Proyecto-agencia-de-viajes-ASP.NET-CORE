using AgenciaViajes.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgenciaViajes.Models;

public partial class Vuelo : IValidatableObject
{
    public int IdVuelo { get; set; }

    public int? IdAerolinea { get; set; }

    [Required(ErrorMessage = "El campo Fecha Entrada es obligatorio.")]
    [FechaEntradaValidacion(ErrorMessage = "La fecha de entrada no es válida")]
    public DateTime FechaInicio { get; set; }

    [Required(ErrorMessage = "El campo Fecha Salida es obligatorio.")]
    public DateTime FechaFinal { get; set; }

    public int? IdAeropuertoOrigen { get; set; }

    public int? IdAeropuertoDestino { get; set; }


    [Required(ErrorMessage = "El campo Precio es obligatorio.")]
    public decimal? PrecioV { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Asiento> Asientos { get; set; } = new List<Asiento>();


    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (FechaFinal < FechaInicio)
        {
            yield return new ValidationResult("La fecha de salida debe ser posterior a la fecha de entrada.", new[] { "FechaFinal" });
        }

        if ((FechaFinal - FechaInicio).TotalMinutes < 20)
        {
            yield return new ValidationResult("La fecha de salida debe ser al menos 20 minutos posterior a la fecha de entrada.", new[] { "FechaFinal" });
        }
        if (IdAeropuertoDestino == IdAeropuertoOrigen)
        {
            yield return new ValidationResult("El aeropuerto de destino no puede ser igual al aeropuerto de origen.", new[] { "IdAeropuertoDestino", "IdAeropuertoOrigen" });
        }
    }



    public virtual Estado? EstadoNavigation { get; set; }

    public virtual Aerolinea? IdAerolineaNavigation { get; set; }

    public virtual Aeropuert? IdAeropuertoDestinoNavigation { get; set; }

    public virtual Aeropuert? IdAeropuertoOrigenNavigation { get; set; }

    public virtual ICollection<RegistroVuelo> RegistroVuelos { get; set; } = new List<RegistroVuelo>();
}
