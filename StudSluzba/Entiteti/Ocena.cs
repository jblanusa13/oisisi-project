using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;

namespace StudSluzba.Entiteti
{
    public class Ocena : ISerializable
    {
        public int IdStudenta { get; set; }
        public int IdPredmeta { get; set; }
        public string SifraPredmeta { get; set; }
        public string Naziv { get; set; }
        public int Espb { get; set; }
        public int OcenaNaIspitu { get; set; }
        public DateOnly DatumPolaganja { get; set; }

        public Ocena() { }

        public Ocena(int idStudenta, int idPredmeta, string sifra, string naziv, int espb, int ocena, DateOnly datum_polaganja)
        {
            IdStudenta = idStudenta;
            IdPredmeta = idPredmeta;
            SifraPredmeta = sifra;
            Naziv = naziv;
            Espb = espb;
            OcenaNaIspitu = ocena;
            DatumPolaganja = datum_polaganja;
        }
        public string[] ToCSV()
        {
            string[] stringVrednosti =
            {
                IdStudenta.ToString(),
                IdPredmeta.ToString(),
                OcenaNaIspitu.ToString(),
                DatumPolaganja.ToString()
            };
            return stringVrednosti;
        }
        public void FromCSV(string[] values)
        {
            IdStudenta = int.Parse(values[0]);
            IdPredmeta = int.Parse(values[1]);
            OcenaNaIspitu = int.Parse(values[2]);
            DatumPolaganja = DateOnly.Parse(values[3]); 
        }
    }
}
