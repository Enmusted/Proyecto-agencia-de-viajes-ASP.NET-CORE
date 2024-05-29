using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgenciaViajes.Models;

public partial class RegistroVuelo
{
    public int IdRvuelo { get; set; }

    [Required(ErrorMessage = "El campo Precio es obligatorio.")]
    [RegularExpression(@"^\d+(\,\d+)?(\.\d+)?$", ErrorMessage = "El campo debe ser un valor numérico.")]
    public decimal? PrecioT { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio.")]
    public int? IdVuelo { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio.")]
    public int? IdAsiento { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio.")]
    public string? Dni { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Detallecompra> Detallecompras { get; set; } = new List<Detallecompra>();

    public virtual Usuario? DniNavigation { get; set; }

    public virtual Estado? EstadoNavigation { get; set; }

    public virtual Asiento? IdAsientoNavigation { get; set; }

    public virtual Vuelo? IdVueloNavigation { get; set; }
}
