﻿using System;
using System.Collections.Generic;

namespace DL;

public partial class Cine
{
    public int IdCine { get; set; }

    public string? Latitud { get; set; }

    public string? Longitud { get; set; }

    public string? Direccion { get; set; }

    public decimal? Venta { get; set; }

    public int? IdZona { get; set; }

    public virtual Zona? IdZonaNavigation { get; set; }
    public string Nombre { get; set; }
}
