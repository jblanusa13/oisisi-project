using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudSluzba.Observer;
using StudSluzba.Storage;

namespace StudSluzba.Entiteti.DAO
{
    class KatedraDAO : ISubject
    {
        private readonly List<IObserver> _observers;
        private readonly List<Katedra> _katedre;
        private readonly List<Profesor> _profesori;

        private readonly StorageKatedra _storageKatedra;
        private readonly StorageProfesor _storageProfesor;

        public KatedraDAO()
        {
            _observers = new List<IObserver>();

            _storageKatedra = new StorageKatedra();
            _storageProfesor = new StorageProfesor();

            _katedre = _storageKatedra.CitanjeKatedri();
            _profesori = _storageProfesor.CitanjeProfesora();         
        }

        public int generisiID()
        {
            if (_katedre.Count == 0)
                return 1;
            else
                return _katedre[_katedre.Count - 1].Id + 1;
        }
        public Katedra PronadjiKatedru(int id)
        {
            Katedra katedra = _katedre.Find(k => k.Id == id);
            return katedra;
        }
        public Katedra PronadjiKatedruPrekoSifre(string sifra)
        {
            Katedra katedra = _katedre.Find(k => k.Sifra == sifra);
            return katedra;
        }

        public void DodavanjeKatedre(string sifra, string naziv, int sefKatedre)
        {
            int id = generisiID();
            Katedra katedra = new Katedra(id, sifra, naziv, sefKatedre);
            _katedre.Add(katedra);
            _storageKatedra.CuvanjeKatedri(_katedre);
            NotifyObservers();
        }


        public void IzmenaKatedre(int id,string sifra, string naziv, int sefKatedre)
        {
            Katedra katedra = PronadjiKatedru(id);
            katedra.Naziv = naziv;
            katedra.SefKatedre = sefKatedre;
            _storageKatedra.CuvanjeKatedri(_katedre);
            NotifyObservers();
        }
        public void BrisanjeKatedre(Katedra katedra)
        {
            _katedre.Remove(katedra);
            _storageKatedra.CuvanjeKatedri(_katedre);
            NotifyObservers();
        }
        public void DodavanjeProfesora(int id, Profesor profesor)
        {
            Katedra katedra = _katedre.Find(k => k.Id == id);
            katedra.SpisakProfesora.Add(profesor);
            _storageKatedra.CuvanjeKatedri(_katedre);
            NotifyObservers();

        }
        public void UkloniProfesora(int idKatedre,Profesor profesor)
        {
            Katedra katedra = _katedre.Find(k => k.Id == idKatedre);
            katedra.SpisakProfesora.Remove(profesor);
            _storageKatedra.CuvanjeKatedri(_katedre);
            NotifyObservers();

        }
        public List<Katedra> GetAll()
        {
            return _katedre;
        }
        public void PostaviSefa(string licnaP, int idK)
        {
            foreach (Katedra k in _katedre)
            {
                if (k.Id == idK)
                {
                    k.SefKatedre =int.Parse(licnaP) ;
                }
            }
            _storageKatedra.CuvanjeKatedri(_katedre);
            NotifyObservers();
        }
        public void ProveriSefa(int idSefa)
        {
            foreach (Katedra k in _katedre)
            {
                if (k.SefKatedre == idSefa)
                {
                    k.SefKatedre = -1;
                }
            }
            _storageKatedra.CuvanjeKatedri(_katedre);
            NotifyObservers();

        }
        public void ObrisiSefa(int id)
        {
            foreach (Katedra k in _katedre)
            {
                if (k.Id == id)
                {
                    k.SefKatedre = -1;
                }
            }
            _storageKatedra.CuvanjeKatedri(_katedre);
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
