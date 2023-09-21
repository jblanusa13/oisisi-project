using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;

namespace StudentskaSluzba.Entiteti
{
    public enum Finansiranje
    { 
        B, //budzet
        S  //samofinansiranje
    }

    internal class Studenti : Serializable
    {
        public string BrIndeksa { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateOnly Datum_rodjenja { get; set; }
        public Adresa Adresa_stanovanja { get; set; }
        
        public string Kontakt_telefon { get; set; }
        public string Imejl { get; set; }
        public int Godina_upisa { get; set; }
        public int Trenutna_godina_studija { get; set; }
        public Finansiranje Status { get; set; }
        public double Prosecna_ocena { get; set; }
        public List<Ocena> Polozeni_ispiti { get; set; }
        public List<Nepolozeni_ispiti> Nepolozeni_predmeti { get; set; }

        public Studenti()
        {
            Polozeni_ispiti = new List<Ocena>();
            Nepolozeni_predmeti = new List<Nepolozeni_ispiti>();
            Adresa_stanovanja = new Adresa();
        }
        public override string ToString()
        {
            return String.Format("Broj indeksa: {0} | Ime: {1} | Prezime: {2} | Datum Rodjenja: {3} | Adresa stanovanja: {4} | Kontakt telefon: {5} | Imejl: {6} | Godina upisa: {7} | Trenutna godina studija: {8} | Status: {9} | Prosecna ocena: {10}\n", BrIndeksa, Ime, Prezime, Datum_rodjenja, Adresa_stanovanja, Kontakt_telefon, Imejl, Godina_upisa, Trenutna_godina_studija, Status, Prosecna_ocena );
        }

        public Studenti(string brIndeksa,string ime, string prz, DateOnly datum_rodjenja, Adresa adresa_stanovanja, string kontakt_telefon, string imejl, int godina_upisa, int trenutna_godina_studija, Finansiranje status, double prosecna_ocena)
        {
            BrIndeksa = brIndeksa;
            Ime = ime;
            Prezime = prz;
            Datum_rodjenja = datum_rodjenja;
            Adresa_stanovanja = adresa_stanovanja;
            Kontakt_telefon = kontakt_telefon;
            Imejl = imejl;
            Godina_upisa = godina_upisa;
            Trenutna_godina_studija = trenutna_godina_studija;
            Status = status;
            Prosecna_ocena = prosecna_ocena;
            Polozeni_ispiti = new List<Ocena>();
            Nepolozeni_predmeti = new List<Nepolozeni_ispiti>();
        }

        public string[] ToCSV()
        {
            string[] stringVrednosti =
            {
                BrIndeksa,
                Ime,
                Prezime,
                Datum_rodjenja.ToString(),
                Adresa_stanovanja.ToString(),
                Kontakt_telefon,
                Imejl,
                Godina_upisa.ToString(),
                Trenutna_godina_studija.ToString(),
                Status.ToString(),
                Prosecna_ocena.ToString()
            };
            return stringVrednosti;
        }

        public void FromCSV(string[] values)
        {
            BrIndeksa = values[0];
            Ime = values[1];
            Prezime = values[2];
            Datum_rodjenja = DateOnly.Parse(values[3]);
            string[] cela_adresa_stanovanja =values[4].Split("-");
            Adresa_stanovanja.FromCSV(cela_adresa_stanovanja);           
            Kontakt_telefon = values[5];
            Imejl = values[6];
            Godina_upisa = int.Parse(values[7]);
            Trenutna_godina_studija = int.Parse(values[8]);
            Enum.TryParse(values[9], out Finansiranje Status);
            Prosecna_ocena = double.Parse(values[10]); 
        }
    }
}
