using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgenciaViajes.Models;

public partial class Vendedor
{
    [Required(ErrorMessage = "El campo Usuario es obligatorio.")]
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "El campo Usuario solo debe contener letras y números.")]
    [MinLength(3, ErrorMessage = "La Usuario debe tener un mínimo de 3 caracteres.")]
    [MaxLength(15, ErrorMessage = "La Usuario debe tener un máximo de 15 caracteres.")]
    public string Usuario { get; set; } = null!;


    [Required(ErrorMessage = "El campo Nombres es obligatorio.")]
    [StringLength(50, ErrorMessage = "El campo Nombres no debe exceder los 50 caracteres.")]
    [MinLength(2, ErrorMessage = "El campo Nombres debe tener al menos 2 caracteres.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ ]+$", ErrorMessage = "El campo Nombres solo debe contener letras, tildes y espacios.")]
    public string Nombres { get; set; } = null!;


    [Required(ErrorMessage = "El campo Apellidos es obligatorio.")]
    [StringLength(50, ErrorMessage = "El campo Apellidos no debe exceder los 50 caracteres.")]
    [MinLength(2, ErrorMessage = "El campo Apellidos debe tener al menos 2 caracteres.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ ]+$", ErrorMessage = "El campo Nombres solo debe contener letras, tildes y espacios.")]
    public string Apellidos { get; set; } = null!;


    [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
    [MinLength(3, ErrorMessage = "La contraseña debe tener un mínimo de 3 caracteres.")]
    [MaxLength(50, ErrorMessage = "La contraseña debe tener un máximo de 50 caracteres.")]
    public string Contraseña { get; set; } = null!;

    public string? Estado { get; set; }

    public virtual Estado? EstadoNavigation { get; set; }
}
