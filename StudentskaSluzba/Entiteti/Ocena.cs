using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;

namespace StudentskaSluzba.Entiteti
{
    internal class Ocena : Serializable
    {
        public string Br_indeksa { get; set; }
        public string PredmetSifra { get; set; }
        public int Ocena_na_ispitu  { get;  set; }
        public DateOnly Datum_polaganja { get; set; }

        public Ocena() { }
        public override string ToString()
        {
            return String.Format("Broj indeksa: {0} | Sifra predmeta: {1} | Ocena na ispitu:{2} | Datum polaganja:{3}\n", Br_indeksa, PredmetSifra, Ocena_na_ispitu, Datum_polaganja);
        }
        public Ocena(string studentBr_indeksa, string predmetSifra, int ocena, DateOnly datum_polaganja)
        {
            Br_indeksa = studentBr_indeksa;
            PredmetSifra = predmetSifra;
            Ocena_na_ispitu = ocena;
            Datum_polaganja = datum_polaganja;
        }
        public string[] ToCSV()
        {
            string[] stringVrednosti =
            {
                Br_indeksa,
                PredmetSifra,
                Ocena_na_ispitu.ToString(),
                Datum_polaganja.ToString()
            };
            return stringVrednosti;
        }
        public void FromCSV(string[] values)
        {
            Br_indeksa = values[0];
            PredmetSifra=values[1];
            Ocena_na_ispitu = int.Parse(values[2]);
            Datum_polaganja = DateOnly.Parse(values[3]);
        }
    }
}
