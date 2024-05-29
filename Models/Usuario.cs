using AgenciaViajes.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgenciaViajes.Models;

public partial class Usuario
{

    [Required(ErrorMessage = "El campo DNI es obligatorio.")]
    [RegularExpression(@"^\d{8}$", ErrorMessage = "El DNI debe tener exactamente 8 dígitos.")]
    public string Dni { get; set; } = null!;


    [Required(ErrorMessage = "El campo Correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "")]
    [CorreoValido(ErrorMessage = "La dirección de correo electrónico no es válida.")]
    public string Correo { get; set; } = null!;


    [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
    [MinLength(3, ErrorMessage = "La contraseña debe tener un mínimo de 3 caracteres.")]
    [MaxLength(50, ErrorMessage = "La contraseña debe tener un máximo de 50 caracteres.")]
    public string Contraseña { get; set; } = null!;


    [Required(ErrorMessage = "El campo Nombres es obligatorio.")]
    [StringLength(50, ErrorMessage = "El campo Nombres no debe exceder los 50 caracteres.")]
    [MinLength(2, ErrorMessage = "El campo Nombres debe tener al menos 2 caracteres.")]
    [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚ ]+$", ErrorMessage = "El campo Nombres solo debe contener letras, espacios y tildes.")]
    public string Nombres { get; set; } = null!;


    [Required(ErrorMessage = "El campo Apellidos es obligatorio.")]
    [StringLength(50, ErrorMessage = "El campo Apellidos no debe exceder los 50 caracteres.")]
    [MinLength(2, ErrorMessage = "El campo Apellidos debe tener al menos 2 caracteres.")]
    [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚ ]+$", ErrorMessage = "El campo Nombres solo debe contener letras, espacios y tildes.")]
    public string Apellidos { get; set; } = null!;

    [Required(ErrorMessage = "El campo Fecha Nacimiento es obligatorio.")]
    [FechaNacimientoValida(ErrorMessage = "La fecha de nacimiento no es válida.")]
    public DateTime FechaNacimiento { get; set; }


    [Required(ErrorMessage = "El campo Numero es obligatorio.")]
    [RegularExpression(@"^\d{9}$", ErrorMessage = "El campo Número debe tener exactamente 9 dígitos.")]
    public string? Numero { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Detallecompra> Detallecompras { get; set; } = new List<Detallecompra>();

    public virtual Estado? EstadoNavigation { get; set; }

    public virtual ICollection<RegistroVuelo> RegistroVuelos { get; set; } = new List<RegistroVuelo>();

    public virtual ICollection<ReservaHotel> ReservaHotels { get; set; } = new List<ReservaHotel>();
}
