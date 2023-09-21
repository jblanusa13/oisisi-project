using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;

namespace StudentskaSluzba.Entiteti
{
    internal class Katedra : Serializable
    {
        public string SifraKatedre { get; set; }
        public string NazivKatedre { get; set; }
        public string SefKatedre { get; set; }
        public List<Profesor> SpisakProfesora {get; set; }

        public Katedra()
        {
            SpisakProfesora = new List<Profesor>();
        }
        public Katedra(string sifra_katedre , string naziv_katedre , string sef_katedre )
        {
            SifraKatedre = sifra_katedre;
            NazivKatedre = naziv_katedre;
            SefKatedre = sef_katedre;
            SpisakProfesora = new List<Profesor>();
        }
        public override string ToString()
        {
            return String.Format(" Sifra Katedre: {0} | Naziv Katedre: {1} | SefKatedre: {2}\n", SifraKatedre, NazivKatedre, SefKatedre);
        }
        public string[] ToCSV()
        {
            string[] stringVrednosti =
            {
                SifraKatedre ,
                NazivKatedre ,
                SefKatedre
            };
            return stringVrednosti;
        }

        public void FromCSV(string[] values)
        {
            SifraKatedre = values[0];
            NazivKatedre = values[1];
            SefKatedre = values[2];
        }












    }
}
