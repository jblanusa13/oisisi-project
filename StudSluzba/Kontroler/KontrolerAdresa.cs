using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudSluzba.Entiteti;
using StudSluzba.Entiteti.DAO;
using StudSluzba.Observer;
using StudSluzba.Storage;


namespace StudSluzba.Kontroler
{
    public class KontrolerAdresa
    {
        private readonly AdresaDAO _adrese;

        public KontrolerAdresa()
        {
            _adrese = new AdresaDAO();
        }

        public Adresa PronadjiAdresu(int id)
        {
            return _adrese.PronadjiAdresu(id);
        }
        public void BrisanjeAdrese(int id)
        {
            _adrese.BrisanjeAdrese(id);
        }
        public Adresa DodavanjeAdrese(string ulica,string broj,string grad,string drzava)
        {
           return _adrese.DodavanjeAdrese(ulica,broj,grad,drzava);
        }
        public Adresa IzmenaAdrese(int id,string ulica,string broj,string grad,string drzava)
        {
            return _adrese.IzmenaAdrese(id,ulica,broj,grad,drzava);
        }
        public void Subscribe(IObserver observer)
        {
            _adrese.Subscribe(observer);
        }
    }
}
