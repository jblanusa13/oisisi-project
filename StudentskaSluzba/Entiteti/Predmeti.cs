using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;

namespace StudentskaSluzba.Entiteti
{
    enum TipSemestra
    {
        Letnji,
        Zimski
    }
    internal class Predmeti : Serializable
    {
        public string Sifra { get; set; }
        public string Naziv { get; set; }
        public TipSemestra Semestar { get; set; }
        public int Godina { get; set; }
        public string Br_licne_karte_profesora { get; set; }       
        public int Espb { get; set; }
        public List<Studenti> Polozili { get; set; }
        public List<Studenti> Nisu_polozili { get; set; }

        public Predmeti()
        {
            Polozili = new List<Studenti>();
            Nisu_polozili = new List<Studenti>();
        }
        public override string ToString()
        {
            return String.Format(" Sifra Predmeta: {0} | Naziv Predmeta: {1} | Semestar: {2} | Godina: {3} | Broj licne karte profesora: {4} | ESPB: {5}\n", Sifra, Naziv, Semestar, Godina, Br_licne_karte_profesora, Espb);
        }
        public Predmeti(string sifra, string naziv, TipSemestra semestar, int godina, int espb, string br_licne_profesora)
        {
            Sifra = sifra;
            Naziv = naziv;
            Semestar = semestar;
            Godina = godina;
            Espb = espb;
            Polozili = new List<Studenti>();
            Nisu_polozili = new List<Studenti>();
            Br_licne_karte_profesora = br_licne_profesora;

        }

        public string[] ToCSV()
        {
            string[] stringVrednosti =
            {
                Sifra,
                Naziv,
                Semestar.ToString(),
                Godina.ToString(),
                Espb.ToString(),
                Br_licne_karte_profesora
            };
            return stringVrednosti;
        }

        public void FromCSV(string[] values)
        {
            Sifra = values[0];
            Naziv = values[1];
            Enum.TryParse(values[2], out TipSemestra Semestar);
            Godina = int.Parse(values[3]);
            Espb = int.Parse(values[4]);
            Br_licne_karte_profesora = values[5];
        }
    }
}
