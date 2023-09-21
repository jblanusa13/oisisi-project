using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;

namespace StudSluzba.Entiteti
{
    public class Katedra : ISerializable
    {
        public int Id { get; set; }
        public string Sifra { get; set; }
        public string Naziv { get; set; }
        public int SefKatedre { get; set; }
        public List<Profesor> SpisakProfesora { get; set; }

        public Katedra()
        {
            SpisakProfesora = new List<Profesor>();
        }
        public Katedra(int id,string sifra_katedre, string naziv_katedre, int sef_katedre)
        {
            Id = id;
            Sifra = sifra_katedre;
            Naziv = naziv_katedre;
            SefKatedre = sef_katedre;
            SpisakProfesora = new List<Profesor>();
        }
        public override string ToString()
        {
            return string.Format(" Sifra Katedre: {0} | Naziv Katedre: {1} | SefKatedre: {2}\n", Sifra, Naziv, SefKatedre);
        }
        public string[] ToCSV()
        {
            string[] stringVrednosti =
            {
                Id.ToString(),
                Sifra ,
                Naziv ,
                SefKatedre.ToString()
            };
            return stringVrednosti;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Sifra = values[1];
            Naziv = values[2];
            SefKatedre = int.Parse(values[3]);
        }












    }
}
