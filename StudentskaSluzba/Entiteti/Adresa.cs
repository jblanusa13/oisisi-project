using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;

namespace StudentskaSluzba.Entiteti
{
    internal class Adresa : Serializable
    {
        public string Ulica { get; set; }
        public string Broj { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }  
        public int IDAdrese {  get; set; }

        public Adresa() 
        {
          
        }

        public Adresa(int ID_Adrese,string ulica, string broj, string grad, string drzava)
        {
            IDAdrese = ID_Adrese;
            Ulica = ulica;
            Broj = broj;
            Grad = grad;
            Drzava = drzava;
            
        }

        public override string ToString()
        {
            return String.Format("{0}-{1}-{2}-{3}-{4}", IDAdrese, Ulica, Broj, Grad, Drzava);
        }

        public string[] ToCSV()
        {
            string[] stringVrednosti =
            {
                IDAdrese.ToString(),
                Ulica ,
                Broj ,
                Grad ,
                Drzava 
               
            };
            return stringVrednosti;
        }

        public void FromCSV(string[] values)
        {
            IDAdrese = int.Parse(values[0]);
            Ulica = values[1];
            Broj = values[2];
            Grad = values[3];
            Drzava = values[4];
            
        }
    }
}
