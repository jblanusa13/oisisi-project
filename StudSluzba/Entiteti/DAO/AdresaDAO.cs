using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using StudSluzba.Observer;
using StudSluzba.Storage;

namespace StudSluzba.Entiteti.DAO
{
    class AdresaDAO : ISubject
    {
        private readonly List<IObserver> _observers;
        private readonly List<Adresa> _adrese;
        private readonly StorageAdresa _storageAdresa;
        public AdresaDAO()
        {
            _observers = new List<IObserver>();

            _storageAdresa = new StorageAdresa();
            _adrese = _storageAdresa.CitanjeAdresa();
        }
        public int generisiID()
        {
            if (_adrese.Count == 0)
                return 1;
            else
                return _adrese[_adrese.Count - 1].Id + 1;
        }
        public Adresa PronadjiAdresu(int id)
        {
            Adresa adresa = _adrese.Find(a => a.Id == id);
            return adresa;
        }
        public Adresa DodavanjeAdrese(string ulica, string broj, string grad,string drzava)
        {
            int id = generisiID();
            Adresa adresa = new Adresa(id, ulica, broj, grad, drzava);
            _adrese.Add(adresa);
            _storageAdresa.CuvanjeAdresa(_adrese);
            NotifyObservers();
            return adresa;
        }
        public Adresa IzmenaAdrese(int id,string ulica, string broj, string grad, string drzava)
        {
            Adresa adresa = _adrese.Find(a => a.Id == id);
            adresa.Ulica = ulica;
            adresa.Broj = broj;
            adresa.Grad = grad;
            adresa.Drzava = drzava;
            _storageAdresa.CuvanjeAdresa(_adrese);
            NotifyObservers();
            return adresa;
        }
        public void BrisanjeAdrese(int id)
        {
            Adresa adresa = _adrese.Find(a => a.Id == id);
            _adrese.Remove(adresa);
            _storageAdresa.CuvanjeAdresa(_adrese);
            NotifyObservers();
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.UpdateKatedra();
            }
        }

    }
}
