using System;
using System.Collections.Generic;

namespace AgenciaViajes.Models;

public partial class Credenciale
{
    public string Usuario { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string Rol { get; set; } = null!;
}
