using AgenciaViajes.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgenciaViajes.Models;

public partial class ReservaHotel : IValidatableObject
{
    public int IdPrh { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio.")]
    public string? Dni { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio.")]
    public int? IdHotel { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio.")]
    public int? IdProductoTh { get; set; }

    [Required(ErrorMessage = "El campo Fecha Entrada es obligatorio.")]
    [FechaEntradaValidacion(ErrorMessage = "La fecha de entrada no es válida")]
    public DateTime FechaEntrada { get; set; }

    [Required(ErrorMessage = "El campo Fecha Salida es obligatorio.")]
    public DateTime FechaSalida { get; set; }

    public decimal Total { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Detallecompra> Detallecompras { get; set; } = new List<Detallecompra>();


    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (FechaSalida <= FechaEntrada)
        {
            yield return new ValidationResult("La fecha de salida debe ser posterior a la fecha de entrada.", new[] { "FechaSalida" });
        }
    }

    public virtual Usuario? DniNavigation { get; set; }

    public virtual Estado? EstadoNavigation { get; set; }

    public virtual Hotel? IdHotelNavigation { get; set; }

    public virtual Thabitacion? IdProductoThNavigation { get; set; }
}
