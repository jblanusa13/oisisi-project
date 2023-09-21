using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudSluzba.Observer;
using StudSluzba.Storage;

namespace StudSluzba.Entiteti.DAO
{
    internal class PredmetDAO : ISubject
    {
        // lista observera
        private readonly List<IObserver> _observers;
        // liste
        private readonly List<Predmet> _predmeti;
        private readonly List<Student> _studenti;
        private readonly List<StudentNepolozeniPredmet> _studentNepolozeniPredmet;
        // storage
        private readonly StoragePredmet _storage;
        private readonly StorageStudent _storageStudent;
        private readonly StorageProfesor _storageProfesor;
        private readonly StorageStudentNepolozeni _storageStudentNepolozeni;

        // konstruktor
        public PredmetDAO()
        {
            _observers = new List<IObserver>();

            _storage = new StoragePredmet();
            _storageProfesor = new StorageProfesor();
            _storageStudent = new StorageStudent();
            _storageStudentNepolozeni = new StorageStudentNepolozeni();

            _predmeti = _storage.CitanjePredmeta();
            _studenti = _storageStudent.CitanjeStudenta();
            _studentNepolozeniPredmet = _storageStudentNepolozeni.CitanjeNepolozenih();
        }

        //pronadji predmet u listi
        public int generisiID()
        {
            if (_predmeti.Count == 0)
                return 1;
            else
                return _predmeti[_predmeti.Count - 1].Id + 1;
        }
        public Predmet PronadjiPredmet(int idPredmeta)
        {
            Predmet predmet = _predmeti.Find(p => p.Id == idPredmeta);
            return predmet;
        }
        public Predmet PronadjiPredmetPoSifri(string sifraPredmeta)
        {
            Predmet predmet = _predmeti.Find(p => p.Sifra == sifraPredmeta);
            return predmet;
        }

        public List <Predmet> nadjiPoProfesoru(int idProfesora)
        {
            List<Predmet> predmeti = new List<Predmet>();
            foreach(Predmet predmet in _predmeti)
            {
                if (predmet.IdProfesora == idProfesora)
                { 
                    predmeti.Add(predmet);
                }
            }
            return predmeti;
        }

        // dodavanje
        public void DodavanjePredmeta(string sifra, string naziv, TipSemestra semestar, int godina, int espb, int idProfesora)
        {
            int id = generisiID();
            Predmet predmet = new Predmet(id,sifra, naziv, semestar, godina, espb, idProfesora);
            _predmeti.Add(predmet);
            _storage.CuvanjePredmeta(_predmeti);

            NotifyObservers();
        }
        // izmena
        
        public void IzmenaPredmeta(string sifra, string naziv, int godina, TipSemestra semestar, int espb, int idProfesora)
        {
            Predmet predmet = PronadjiPredmetPoSifri(sifra);
            predmet.Naziv = naziv;
            predmet.Godina = godina;
            predmet.Semestar = semestar;
            predmet.Espb = espb;
            predmet.IdProfesora = idProfesora;

            _storage.CuvanjePredmeta(_predmeti);
           
            NotifyObservers();
        }

        // brisanje
        public void BrisanjePredmeta(Predmet predmet)
        {
            _predmeti.Remove(predmet);
            _storage.CuvanjePredmeta(_predmeti);
            
            NotifyObservers();
        }

        // getAll
        public List<Predmet> GetAll()
        {
            return _predmeti;
        }

        public  void postaviIdProfesora(int idProfesora,int idPredmeta)
        {
            foreach (Predmet p in _predmeti)
            {
                if(p.Id == idPredmeta)
                {
                    p.IdProfesora = idProfesora;
                }
            }
            _storage.CuvanjePredmeta(_predmeti);
            NotifyObservers();
        }
        public void obrisiIdProfesora(int idPredmeta)
        {
            foreach (Predmet p in _predmeti)
            {
                if (p.Id == idPredmeta)
                {
                    p.IdProfesora = -1; 
                }
            }
            _storage.CuvanjePredmeta(_predmeti);
            NotifyObservers();
        }

        public List<Predmet> nadjiPredmeteKojeProfesorPredaje(int idProfesora)
        {
            List<Predmet> predmetiProfesora = new List<Predmet>();
            foreach(Predmet p in _predmeti)
            {
                if(p.IdProfesora == idProfesora)
                {
                    predmetiProfesora.Add(p);
                }
            }
            return predmetiProfesora;
        }

        public List<Predmet> nadjiPredmeteKojeNikoNePredaje()
        {
            List<Predmet> predmetiProfesora = new List<Predmet>();
            foreach (Predmet p in _predmeti)
            {
                if (p.IdProfesora == -1)
                {
                    predmetiProfesora.Add(p);
                }
            }
            return predmetiProfesora;
        }
        public void DodajNijePolozio(string indeks, string sifra)
        {
            Predmet predmet = PronadjiPredmetPoSifri(sifra);
            Student student = _studenti.Find(s => s.BrIndeksa == indeks);

            predmet.Nisu_polozili.Add(student);
        }

        // metode iz Subjecta
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
                observer.UpdatePredmet();
            }
        }
    }
}
