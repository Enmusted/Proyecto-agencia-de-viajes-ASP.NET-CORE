using System;
using System.Collections.Generic;

namespace AgenciaViajes.Models;

public partial class Estado
{
    public string Estado1 { get; set; } = null!;

    public virtual ICollection<Aerolinea> Aerolineas { get; set; } = new List<Aerolinea>();

    public virtual ICollection<Aeropuert> Aeropuerts { get; set; } = new List<Aeropuert>();

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();

    public virtual ICollection<RegistroVuelo> RegistroVuelos { get; set; } = new List<RegistroVuelo>();

    public virtual ICollection<ReservaHotel> ReservaHotels { get; set; } = new List<ReservaHotel>();

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    public virtual ICollection<Vendedor> Vendedors { get; set; } = new List<Vendedor>();

    public virtual ICollection<Vuelo> Vuelos { get; set; } = new List<Vuelo>();
}
