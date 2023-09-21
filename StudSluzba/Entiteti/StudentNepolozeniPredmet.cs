using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;

namespace StudSluzba.Entiteti
{
    public class StudentNepolozeniPredmet : ISerializable
    {
        public int IdStudenta { get; set; }
        public int IdPredmeta { get; set; }

        public StudentNepolozeniPredmet() { }

        public StudentNepolozeniPredmet(int idStudenta, int idPredmeta)
        {
            IdStudenta = idStudenta;
            IdPredmeta = idPredmeta;
        }

        public string[] ToCSV()
        {
            string[] stringVrednosti =
            {
                IdStudenta.ToString(),
                IdPredmeta.ToString()
            };
            return stringVrednosti;
        }
        public void FromCSV(string[] values)
        {
            IdStudenta = int.Parse(values[0]);
            IdPredmeta = int.Parse(values[1]);
        }
    }
}
