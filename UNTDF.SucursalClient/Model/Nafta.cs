using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNTDF.SucursalClient.Model
{
    public class Nafta
    {
        public int Id { get; set; }
        public readonly string Nombre;
        public double Precio { get; }

        public Nafta(int id, string nombre, double precio)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
        }
    }
}
