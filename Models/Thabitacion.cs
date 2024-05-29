using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgenciaViajes.Models;

public partial class Thabitacion
{
    public int IdProductoTh { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio.")]
    public int? IdHotel { get; set; }

    [Required(ErrorMessage = "El campo Tipo Habitación es obligatorio.")]
    [StringLength(15, ErrorMessage = "El campo Tipo Habitación no debe exceder los 15 caracteres.")]
    [MinLength(5, ErrorMessage = "El campo Tipo Habitación debe tener al menos 5 caracteres.")]
    public string TipoHabitacion { get; set; } = null!;

    [Required(ErrorMessage = "El campo Habitacion es obligatorio.")]
    [RegularExpression(@"^[a-zA-Z][0-9]{3}$", ErrorMessage = "El campo debe contener una letra seguida de tres números.")]
    [StringLength(4, ErrorMessage = "El campo Habitacion debe tener exactamente 4 caracteres.")]
    public string Habitacion { get; set; } = null!;

    [Required(ErrorMessage = "El campo Precio es obligatorio.")]
    [RegularExpression(@"^\d+(\,\d+)?(\.\d+)?$", ErrorMessage = "El campo debe ser un valor numérico.")]
    public decimal Precio { get; set; }


    [Required(ErrorMessage = "El campo Cantidad Personas es obligatorio.")]
    [RegularExpression("^[1-4]+$", ErrorMessage = "El campo debe contener solo números del 1 al 4.")]
    public int CantidadPersonas { get; set; }

    public string? Disponibilidad { get; set; }

    public virtual Hotel? IdHotelNavigation { get; set; }

    public virtual ICollection<ReservaHotel> ReservaHotels { get; set; } = new List<ReservaHotel>();
}
