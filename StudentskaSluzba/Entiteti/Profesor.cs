using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;

namespace StudentskaSluzba.Entiteti
{
    internal class Profesor : Serializable
    {

        public string Prezime { get; set; }
        public string Ime { get; set; }
        public DateOnly DatumRodjenja { get; set; }
        public Adresa AdresaStanovanja { get; set; } = new Adresa();
        public string KontaktTelefon { get; set; }
        public string Imejl { get; set; }
        public Adresa AdresaKancelarije { get; set; } = new Adresa();
        public string BrojLicneKarte { get; set; }
        public string Zvanje { get; set; }
        public double GodineStaza { get; set; }
        public string SifraKatedre { get; set; }
        public List<Predmeti> SpisakPredmeta { get; set; }

        public Profesor()
        {
            SpisakPredmeta = new List<Predmeti>();
            
        }

        public override string ToString()
        {
            return String.Format(" Broj licne karte: {0} | Prezime: {1} | Ime: {2} | Datum Rodjenja: {3} | Adresa stanovanja: {4} | Kontakt telefon: {5} | Imejl: {6} | Adresa Kancelarije: {7} | Zvanje: {8} | Godine staza: {9} | Sifra Katedre: {10}\n", BrojLicneKarte, Prezime, Ime, DatumRodjenja, AdresaStanovanja, KontaktTelefon,Imejl, AdresaKancelarije, Zvanje, GodineStaza,SifraKatedre);
        }
        public Profesor(string broj_licne_karte,string prezime , string ime , DateOnly datum_rodjenja , Adresa adresa_stanovanja , string kontakt_telefon , string imejl , Adresa adresa_kancelarije , string zvanje , double godine_staza, string sifra_katedre )
        {
            Prezime = prezime;
            Ime = ime;
            DatumRodjenja = datum_rodjenja;
            AdresaStanovanja = adresa_stanovanja;
            KontaktTelefon = kontakt_telefon;
            Imejl = imejl;
            AdresaKancelarije = adresa_kancelarije;
            BrojLicneKarte = broj_licne_karte;
            Zvanje = zvanje;
            GodineStaza = godine_staza;
            SifraKatedre = sifra_katedre;
            SpisakPredmeta = new List<Predmeti>();
        }

        public string[] ToCSV()
        {

            string[] stringVrednosti =
            {
                Prezime,
                Ime,
                DatumRodjenja.ToString(),
                AdresaStanovanja.ToString(),
                KontaktTelefon,
                Imejl,
                AdresaKancelarije.ToString(),
                BrojLicneKarte,
                Zvanje,
                GodineStaza.ToString(),
                SifraKatedre
            };
            return stringVrednosti;

        }

        public void FromCSV(string[] values)
        {
            Prezime = values[0];          
            Ime = values[1];           
            DatumRodjenja = DateOnly.Parse(values[2]);          
            string[] cela_adresa_stanovanja = values[3].Split("-");          
            AdresaStanovanja.FromCSV(cela_adresa_stanovanja);           
            KontaktTelefon = values[4];           
            Imejl = values[5];           
            string[] cela_adresa_kancelarije = values[6].Split("-");
            AdresaKancelarije.FromCSV(cela_adresa_kancelarije);
            BrojLicneKarte = values[7];
            Zvanje = values[8];
            GodineStaza = double.Parse(values[9]);
            SifraKatedre = values[10];
        }
    }
}
