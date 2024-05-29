using AgenciaViajes.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgenciaViajes.Models;

public partial class Tour : IValidatableObject
{
    public int IdProducto { get; set; }


    [Required(ErrorMessage = "El campo Precio es obligatorio.")]
    [RegularExpression(@"^\d+(\,\d+)?(\.\d+)?$", ErrorMessage = "El campo debe ser un valor numérico.")]
    public decimal Precio { get; set; }


    [Required(ErrorMessage = "El campo Descripción es obligatorio.")]
    [StringLength(250, ErrorMessage = "El campo Descripción no debe exceder los 250 caracteres.")]
    [MinLength(2, ErrorMessage = "El campo Descripción debe tener al menos 2 caracteres.")]
    public string Descripción { get; set; } = null!;


    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    [StringLength(50, ErrorMessage = "El campo Nombre no debe exceder los 50 caracteres.")]
    [MinLength(2, ErrorMessage = "El campo Nombre debe tener al menos 2 caracteres.")]
    public string NombreTour { get; set; } = null!;


    [Required(ErrorMessage = "El campo Destino es obligatorio.")]
    [StringLength(50, ErrorMessage = "El campo Destino no debe exceder los 50 caracteres.")]
    [MinLength(2, ErrorMessage = "El campo Destino debe tener al menos 2 caracteres.")]
    public string Destinos { get; set; } = null!;



    [Required(ErrorMessage = "El campo es obligatorio.")]
    [StringLength(30, ErrorMessage = "El campo no debe exceder los 30 caracteres.")]
    [MinLength(2, ErrorMessage = "El campo debe tener al menos 2 caracteres.")]
    public string Pais { get; set; } = null!;


    [Required(ErrorMessage = "El campo Fecha es obligatorio.")]
    [FechaEntradaValidacion(ErrorMessage = "La fecha no es válida")]
    public DateTime FechaInicio { get; set; }

    [Required(ErrorMessage = "El campo Fecha fin es obligatorio.")]
    public DateTime FechaFin { get; set; }

    [Display(Name = "Imagen")]
    public byte[]? DatosImagen { get; set; }


    [Required(ErrorMessage = "El campo Capacidad es obligatorio.")]
    [RegularExpression("^(30|[1-9]|[1-2][0-9])$", ErrorMessage = "El campo debe contener solo números del 1 al 30.")]
    public int Capacidad { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Detallecompra> Detallecompras { get; set; } = new List<Detallecompra>();


    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (FechaFin <= FechaInicio)
        {
            yield return new ValidationResult("La fecha de salida debe ser posterior a la fecha de entrada.", new[] { "FechaFin" });
        }
    }

    public virtual Estado? EstadoNavigation { get; set; }
}
