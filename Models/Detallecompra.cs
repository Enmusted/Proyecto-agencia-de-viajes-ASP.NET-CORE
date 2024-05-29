using AgenciaViajes.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgenciaViajes.Models;

public partial class Detallecompra 
{
    public int NumeroVenta { get; set; }


    [Required(ErrorMessage = "El campo es obligatorio.")]

    public string? Dni { get; set; }

    public int? IdPrh { get; set; }

    public int? IdRvuelo { get; set; }

    public int? IdProducto { get; set; }


    [Required(ErrorMessage = "El campo Numero es obligatorio.")]
    [RegularExpression(@"^\d{16}$", ErrorMessage = "El campo Tarjeta de Crédito debe tener exactamente 16 dígitos.")]
    public string TarjetaCredito { get; set; } = null!;


    [Required(ErrorMessage = "El campo Numero es obligatorio.")]
    [RegularExpression(@"^(0[1-9]|1[0-2])/\d{2}$", ErrorMessage = "El formato debe ser MM/YY.")]
    [FechaVencimiento(ErrorMessage = "La fecha de vencimiento no es válida.")]
    public string TVencimiento { get; set; } = null!;


    [Required(ErrorMessage = "El campo Numero es obligatorio.")]
    [RegularExpression(@"^\d{3}$", ErrorMessage = "El campo CVV debe tener exactamente 3 dígitos.")]
    public string Cvv { get; set; } = null!;

    public DateTime? FechaCompra { get; set; }

    public decimal? Total { get; set; }

    public virtual Usuario? DniNavigation { get; set; }

    public virtual ReservaHotel? IdPrhNavigation { get; set; }

    public virtual Tour? IdProductoNavigation { get; set; }

    public virtual RegistroVuelo? IdRvueloNavigation { get; set; }
}
