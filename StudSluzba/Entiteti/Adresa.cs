using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;

namespace StudSluzba.Entiteti
{
    public class Adresa : ISerializable
    {
        public int Id { get; set; }
        public string Ulica { get; set; }
        public string Broj { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }
 

        public Adresa()
        {

        }

        public Adresa(int id,string ulica, string broj, string grad, string drzava)
        {
            Id = id;
            Ulica = ulica;
            Broj = broj;
            Grad = grad;
            Drzava = drzava;

        }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}-{3}-{4}", Id, Ulica, Broj, Grad, Drzava);
        }

        public string[] ToCSV()
        {
            string[] stringVrednosti =
            {
                Id.ToString(),
                Ulica ,
                Broj ,
                Grad ,
                Drzava

            };
            return stringVrednosti;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Ulica = values[1];
            Broj = values[2];
            Grad = values[3];
            Drzava = values[4];

        }
    }
}
