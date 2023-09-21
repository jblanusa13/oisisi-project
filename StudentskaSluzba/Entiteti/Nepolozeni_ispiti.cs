using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;

namespace StudentskaSluzba.Entiteti
{
    internal class Nepolozeni_ispiti : Serializable
    {
        public string Br_indeksa { get; set; }
        public string Sifra_predmeta { get; set; }
        public DateOnly Datum_polaganja { get; set; }

        public Nepolozeni_ispiti() { }

        public Nepolozeni_ispiti(string br_indeksa, string sifra_predmeta, DateOnly datum_polaganja)
        {
            Br_indeksa = br_indeksa;
            Sifra_predmeta = sifra_predmeta;
            Datum_polaganja = datum_polaganja;
        }

        public string[] ToCSV()
        {
            string[] stringVrednosti =
            {
                Br_indeksa,
                Sifra_predmeta,
                Datum_polaganja.ToString()
            };
            return stringVrednosti;
        }
        public void FromCSV(string[] values)
        {
            Br_indeksa = values[0];
            Sifra_predmeta = values[1];
            Datum_polaganja = DateOnly.Parse(values[2]);
        }
    }
}
