using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgenciaViajes.Models;

public partial class Aerolinea
{
    public int IdAerolinea { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio.")]
    [StringLength(30, ErrorMessage = "El campo no debe exceder los 30 caracteres.")]
    [MinLength(2, ErrorMessage = "El campo debe tener al menos 2 caracteres.")]
    public string Aerolinea1 { get; set; } = null!;

    public string? Estado { get; set; }

    public virtual Estado? EstadoNavigation { get; set; }

    public virtual ICollection<Vuelo> Vuelos { get; set; } = new List<Vuelo>();
}
