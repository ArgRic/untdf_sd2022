using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNTDF.SucursalClient.Model
{
    public class Sucursal
    {
        public readonly string Nombre;
        public readonly string Ubicacion;
        public double Multiplicador;
        public IEnumerable<Nafta> Naftas;

        public Sucursal(string nombre, string ubicacion, double multiplicador, IEnumerable<Nafta> naftas)
        {
            Nombre = nombre;
            Ubicacion = ubicacion;
            Multiplicador = multiplicador;
            Naftas = naftas;
        }

        public string ReporteEstadoActual()
        {
            string output = $"[{Ubicacion}] {Nombre}\n";
            foreach (Nafta nafta in Naftas)
            {
                double precio = nafta.Precio * Multiplicador;
                output += $"{nafta.Nombre} - ${precio.ToString("0.000")}\n";
            }
            return output;
        }
    }
}
