using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgenciaViajes.Models;

public partial class Aeropuert
{
    public int IdAeropuerto { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio.")]
    [StringLength(50, ErrorMessage = "El campo no debe exceder los 50 caracteres.")]
    [MinLength(2, ErrorMessage = "El campo debe tener al menos 2 caracteres.")]
    public string Aeropuerto { get; set; } = null!;

    [Required(ErrorMessage = "El campo es obligatorio.")]
    [StringLength(50, ErrorMessage = "El campo no debe exceder los 50 caracteres.")]
    [MinLength(2, ErrorMessage = "El campo debe tener al menos 2 caracteres.")]
    public string Provincia { get; set; } = null!;

    [Required(ErrorMessage = "El campo es obligatorio.")]
    [StringLength(30, ErrorMessage = "El campo no debe exceder los 30 caracteres.")]
    [MinLength(2, ErrorMessage = "El campo debe tener al menos 2 caracteres.")]
    public string Pais { get; set; } = null!;

    public string? Estado { get; set; }

    public virtual Estado? EstadoNavigation { get; set; }

    public virtual ICollection<Vuelo> VueloIdAeropuertoDestinoNavigations { get; set; } = new List<Vuelo>();

    public virtual ICollection<Vuelo> VueloIdAeropuertoOrigenNavigations { get; set; } = new List<Vuelo>();
}
