using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNTDF.SucursalClient.Model
{
    public static class ModelFactory
    {
        public static IEnumerable<Nafta> GetNaftasSampleModel()
        {
            return new List<Nafta>
            {
                new Nafta(1, "INFINIA", 300.0),
                new Nafta(2, "SUPER", 200.0),
                new Nafta(3, "INFINIA DIESEL", 250.0),
                new Nafta(4, "ULTRADIESEL", 200.0),
                new Nafta(4, "DIESEL 500", 190.0),
            };
        }

        public static IEnumerable<Sucursal> GetSucursalesSampleModel()
        {
            var naftas = GetNaftasSampleModel();
            return new List<Sucursal>(){
                new Sucursal("YPF Orion", "Ushuaia", 1.0, new List<Nafta>(naftas)),
                new Sucursal("YPF de la Ribera", "Ushuaia", 1.0, new List<Nafta>(naftas)),
                new Sucursal("YPF Frente al Indio", "Ushuaia", 1.0, new List<Nafta>(naftas)),
                new Sucursal("YPF Ruta 3", "Tolhuin", 2.0, new List<Nafta>(naftas)),
                new Sucursal("YPF Autosur", "Rio Grande", 1.0, new List<Nafta>(naftas)),
            };
        }
    }
}
