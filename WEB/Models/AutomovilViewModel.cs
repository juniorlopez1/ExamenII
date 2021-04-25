using System;
using System.Collections.Generic;

#nullable disable

namespace Web.Models

/* Se tomo del EF. Ahora es un ViewModel */
{
    public partial class AutomovilViewModel
    {
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public byte Capacidad { get; set; }
        public string TipoMarcha { get; set; }

        /* */
        public override string ToString()
        {
            return $"Automovil: Placa {Placa}, Marca {Marca}, Capacidad {Capacidad}, Tipo Marcha {TipoMarcha}";
        }
    }
}
