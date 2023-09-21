using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;

namespace StudSluzba.Entiteti
{
    public enum Finansiranje
    {
        B, //budzet
        S //samofinansiranje
    }

    public class Student : ISerializable
    {
        public int Id { get; set; }
        public string BrIndeksa { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateOnly DatumRodjenja { get; set; }
        public int AdresaStanovanjaId { get; set; }
        public string KontaktTelefon { get; set; }
        public string Imejl { get; set; }
        public int GodinaUpisa { get; set; }
        public int TrenutnaGodinaStudija { get; set; }
        public Finansiranje Status { get; set; }
        public double ProsecnaOcena { get; set; }
        public List<Ocena> PolozeniIspiti { get; set; }
        public List<Predmet> NepolozeniPredmeti { get; set; }

        public Student()
        {
            PolozeniIspiti = new List<Ocena>();
            NepolozeniPredmeti = new List<Predmet>();
        }

        // public override string ToString()
        // {
        //     return string.Format("ID: {0} | Broj indeksa: {1} | Ime: {1} | Prezime: {2} | Datum Rodjenja: {3} | Adresa stanovanja: {4} | Kontakt telefon: {5} | Imejl: {6} | Godina upisa: {7} | Trenutna godina studija: {8} | Status: {9} | Prosecna ocena: {10}\n", BrIndeksa, Ime, Prezime, DatumRodjenja, AdresaStanovanja, KontaktTelefon, Imejl, GodinaUpisa, TrenutnaGodinaStudija, Status, ProsecnaOcena);
        // }

        public Student(int id, string ime, string prz, DateOnly datum_rodjenja, int adresa_stanovanja_id, string kontakt_telefon, string imejl, string brIndeksa, int godina_upisa, int trenutna_godina_studija, Finansiranje status, double prosecna_ocena)
        {
            Id = id;
            Ime = ime;
            Prezime = prz;
            DatumRodjenja = datum_rodjenja;
            AdresaStanovanjaId = adresa_stanovanja_id;
            KontaktTelefon = kontakt_telefon;
            Imejl = imejl;
            BrIndeksa = brIndeksa;
            GodinaUpisa = godina_upisa;
            TrenutnaGodinaStudija = trenutna_godina_studija;
            Status = status;
            ProsecnaOcena = prosecna_ocena;
            PolozeniIspiti = new List<Ocena>();
            NepolozeniPredmeti = new List<Predmet>();
        }

        public string[] ToCSV()
        {
            string[] stringVrednosti =
            {
                Id.ToString(),
                BrIndeksa,
                Ime,
                Prezime,
                DatumRodjenja.ToString(),
                AdresaStanovanjaId.ToString(),
                KontaktTelefon,
                Imejl,
                GodinaUpisa.ToString(),
                TrenutnaGodinaStudija.ToString(),
                Status.ToString(),
                ProsecnaOcena.ToString().Replace(',', '.')
            };
            return stringVrednosti;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            BrIndeksa = values[1];
            Ime = values[2];
            Prezime = values[3];
            DatumRodjenja = DateOnly.Parse(values[4]);
            AdresaStanovanjaId = int.Parse(values[5]);
            KontaktTelefon = values[6];
            Imejl = values[7];
            GodinaUpisa = int.Parse(values[8]);
            TrenutnaGodinaStudija = int.Parse(values[9]);
            Status = (Finansiranje)Enum.Parse(typeof(Finansiranje), values[10]);
            ProsecnaOcena = double.Parse(values[11]);
        }
    }
}
