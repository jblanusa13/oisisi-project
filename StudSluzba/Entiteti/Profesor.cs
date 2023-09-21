using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace StudSluzba.Entiteti
{
    public class Profesor : ISerializable
    {
        public int Id { get; set; }
        public string Prezime { get; set; }
        public string Ime { get; set; }
        public DateOnly DatumRodjenja { get; set; }
        public int AdresaStanovanjaId { get; set; } 
        public string KontaktTelefon { get; set; }
        public string Imejl { get; set; }
        public int AdresaKancelarijeId { get; set; }
        public string BrojLicneKarte { get; set; }
        public string Zvanje { get; set; }
        public double GodineStaza { get; set; }
        public int IdKatedre { get; set; }
        public List<Predmet> SpisakPredmeta { get; set; }
        public Profesor()
        {
            SpisakPredmeta = new List<Predmet>();
        }

        public Profesor(int id,string brojLicne, string prezime, string ime, DateOnly datumRodjenja, int adresaStanovanjaId, string telefon, string imejl, int adresaKancelarijeId, string zvanje, double godineStaza, int idKatedre)
        {
            Id = id;
            Prezime = prezime;
            Ime = ime;
            DatumRodjenja = datumRodjenja;
            AdresaStanovanjaId = adresaStanovanjaId;
            KontaktTelefon = telefon;
            Imejl = imejl;
            AdresaKancelarijeId = adresaKancelarijeId;
            BrojLicneKarte = brojLicne;
            Zvanje = zvanje;
            GodineStaza = godineStaza;
            IdKatedre = idKatedre;
            SpisakPredmeta = new List<Predmet>();
           
        }

        public string[] ToCSV()
        {

            string[] stringVrednosti =
            {
                Id.ToString(),
                BrojLicneKarte,
                Ime,
                Prezime,               
                DatumRodjenja.ToString(),
                AdresaStanovanjaId.ToString(),
                KontaktTelefon,
                Imejl,
                AdresaKancelarijeId.ToString(),
                Zvanje,
                GodineStaza.ToString(),
                IdKatedre.ToString()
            };
            return stringVrednosti;

        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            BrojLicneKarte = values[1];
            Ime = values[2];
            Prezime = values[3];
            DatumRodjenja = DateOnly.Parse(values[4]);
            AdresaStanovanjaId = int.Parse(values[5]);
            KontaktTelefon = values[6];
            Imejl = values[7];
            AdresaKancelarijeId = int.Parse(values[8]);
            Zvanje = values[9];
            GodineStaza = double.Parse(values[10]);
            IdKatedre = int.Parse(values[11]);
        }
    }
    

}
