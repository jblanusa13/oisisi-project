using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;
//using StudSluzba.Entiteti.Studenti;

namespace StudSluzba.Entiteti
{
    public enum TipSemestra
    {
        LETNJI,
        ZIMSKI
    }
    public class Predmet : ISerializable
    {
        public int Id { get; set; }
        public string Sifra { get; set; }
        public string Naziv { get; set; }
        public TipSemestra Semestar { get; set; }
        public int Godina { get; set; }
        public int IdProfesora { get; set; }
        public int Espb { get; set; }
        public List<Student> Polozili { get; set; }
        public List<Student> Nisu_polozili { get; set; }

        public Predmet()
        {
            Polozili = new List<Student>();
            Nisu_polozili = new List<Student>();
        }

        public Predmet(int id,string sifra, string naziv, TipSemestra semestar, int godina, int espb, int idProfesora)
        {
            Id = id;
            Sifra = sifra;
            Naziv = naziv;
            Semestar = semestar;
            Godina = godina;
            Espb = espb;
            IdProfesora = idProfesora;
            Polozili = new List<Student>();
            Nisu_polozili = new List<Student>();
            

        }

        public string[] ToCSV()
        {
            string[] stringVrednosti =
            {
                Id.ToString(),
                Sifra,
                Naziv,
                Semestar.ToString(),
                Godina.ToString(),
                IdProfesora.ToString(),
                Espb.ToString()
              
            };
            return stringVrednosti;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Sifra = values[1];
            Naziv = values[2];
            Semestar = (TipSemestra)Enum.Parse(typeof(TipSemestra), values[3]);
            Godina = int.Parse(values[4]);
            IdProfesora = int.Parse(values[5]);
            Espb = int.Parse(values[6]);
           
        }
    }
}
