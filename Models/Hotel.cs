using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgenciaViajes.Models;

public partial class Hotel
{
    public int IdHotel { get; set; }


    [Required(ErrorMessage = "El campo es obligatorio.")]
    [RegularExpression(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚüÜ\s]*$", ErrorMessage = "El campo debe contener letras, números y tildes")]
    [StringLength(50, ErrorMessage = "El campo no debe exceder los 50 caracteres.")]
    [MinLength(2, ErrorMessage = "El campo debe tener al menos 2 caracteres.")]
    public string Nombre { get; set; } = null!;



    [Required(ErrorMessage = "El campo es obligatorio.")]
    [StringLength(50, ErrorMessage = "El campo no debe exceder los 50 caracteres.")]
    [MinLength(2, ErrorMessage = "El campo debe tener al menos 2 caracteres.")]
    public string Provincia { get; set; } = null!;

    [Required(ErrorMessage = "El campo es obligatorio.")]
    [StringLength(50, ErrorMessage = "El campo no debe exceder los 50 caracteres.")]
    [MinLength(2, ErrorMessage = "El campo debe tener al menos 2 caracteres.")]
    public string Direccion { get; set; } = null!;

    public string? Estado { get; set; }

    public virtual Estado? EstadoNavigation { get; set; }

    public virtual ICollection<ReservaHotel> ReservaHotels { get; set; } = new List<ReservaHotel>();

    public virtual ICollection<Thabitacion> Thabitacions { get; set; } = new List<Thabitacion>();
}
