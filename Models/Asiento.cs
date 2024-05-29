using System;
using System.Collections.Generic;

namespace AgenciaViajes.Models;

public partial class Asiento
{
    public int IdAsiento { get; set; }

    public string? NumAsiento { get; set; }

    public string? Clase { get; set; }

    public string? Disponibilidad { get; set; }

    public decimal? PrecioV { get; set; }

    public int? IdVuelo { get; set; }

    public virtual Vuelo? IdVueloNavigation { get; set; }

    public virtual ICollection<RegistroVuelo> RegistroVuelos { get; set; } = new List<RegistroVuelo>();
}
