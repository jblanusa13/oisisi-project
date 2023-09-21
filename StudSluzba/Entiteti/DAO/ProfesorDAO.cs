using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using StudSluzba.Observer;
using StudSluzba.Storage;

namespace StudSluzba.Entiteti.DAO
{
    internal class ProfesorDAO : ISubject
    {
        private readonly List<IObserver> _observers;
        private readonly List<Profesor> _profesori;
        private readonly StorageProfesor _storage;
 

        public ProfesorDAO()
        {
            _observers = new List<IObserver>();
            _storage = new StorageProfesor();          
            _profesori = _storage.CitanjeProfesora();
        }
        public int generisiID()
        {
            if (_profesori.Count == 0)
                return 1;
            else
                return _profesori[_profesori.Count - 1].Id + 1;
        }

        public void DodavanjeProfesora(string brojLicne, string prezime, string ime, DateOnly datumRodjenja, int adresaStanovanjaId, string telefon, string imejl, int adresaKancelarijeId, string zvanje, double godineStaza, int idKatedre)
        {
            int id = generisiID();
            Profesor profesor = new Profesor(id,brojLicne, prezime, ime, datumRodjenja, adresaStanovanjaId, telefon, imejl, adresaKancelarijeId, zvanje, godineStaza, idKatedre);
            _profesori.Add(profesor);
            _storage.CuvanjeProfesora(_profesori);
            NotifyObservers();
        }

        public void IzmenaProfesora(string brojLicne, string prezime, string ime, DateOnly datumRodjenja, int adresaStanovanjaId, string telefon, string imejl, int adresaKancelarijeId, string zvanje, double godineStaza, int idKatedre)
        {
            Profesor profesor =  _profesori.Find(p => p.BrojLicneKarte == brojLicne) ;
            profesor.Ime = ime;
            profesor.Prezime = prezime;
            profesor.DatumRodjenja = datumRodjenja;
            profesor.AdresaStanovanjaId = adresaStanovanjaId;
            profesor.KontaktTelefon = telefon;
            profesor.Imejl = imejl;
            profesor.AdresaKancelarijeId = adresaKancelarijeId;
            profesor.Zvanje = zvanje;
            profesor.GodineStaza = godineStaza;
            profesor.IdKatedre = idKatedre;
            _storage.CuvanjeProfesora(_profesori);
            NotifyObservers();

        }

        public void BrisanjeProfesora(Profesor profesor)
        {
            _profesori.Remove(profesor);
            _storage.CuvanjeProfesora(_profesori);
            NotifyObservers();
        }


        public List<Profesor> GetAll()
        {
            return _profesori;
        }

        public Profesor Nadji(string brojLicneKarte)
        {
            Profesor profesor = _profesori.Find(p => p.BrojLicneKarte == brojLicneKarte);
            return profesor;
        }
        public Profesor NadjiPoId(int id)
        {
            Profesor profesor = _profesori.Find(p => p.Id == id);
            return profesor;
        }
        public Profesor NadjiPoImenu(string imeIPrezime)
        {
            string[] niz = imeIPrezime.Split(" ");
            string ime = niz[0];
            string prezime = niz[1];
            Profesor profesor = _profesori.Find(p => ((p.Ime == ime) && (p.Prezime == prezime)));
            return profesor;
        }

        public string VratiImeIPrezime(int idProfesora)
        {
           string imeIPrezime;
           Profesor profesor = _profesori.Find(p => p.Id == idProfesora);
           if (profesor == null)
           {
             imeIPrezime = "";
           }
           else
           {
            imeIPrezime = profesor.Ime + " " + profesor.Prezime;
           }
        
            return imeIPrezime;
        }


        public void DodajPredmet(Predmet predmet,int idProfesora)
        {
            Profesor profesor = _profesori.Find(p => p.Id == idProfesora);
            profesor.SpisakPredmeta.Add(predmet);
            _storage.CuvanjeProfesora(_profesori);
            NotifyObservers();
        }

        public void UkloniPredmet(Predmet predmet, int idProfesora)
        {
            Profesor profesor = _profesori.Find(p => p.Id == idProfesora);
            profesor.SpisakPredmeta.Remove(predmet);
            _storage.CuvanjeProfesora(_profesori);
            NotifyObservers();
        }
        public List<Profesor> NadjiMoguceSefove(int  idKatedre)
        {
            List<Profesor> moguciSefovi = new List<Profesor>();
            foreach(Profesor p in _profesori)
            {
                if((p.IdKatedre == idKatedre) && (p.GodineStaza >= 5) && (p.Zvanje == "REDOVNI_PROFESOR" || p.Zvanje == "VANREDNI_PROFESOR"))
                {
                    moguciSefovi.Add(p);
                }
            }
            return moguciSefovi;
        }
        public void DodajKatedru(int idKatedre,int idProfesora)
        {
            Profesor profesor = _profesori.Find(p => p.Id == idProfesora);
            profesor.IdKatedre = idKatedre;
            _storage.CuvanjeProfesora(_profesori);
            NotifyObservers();
        }

        public void UkloniKatedru(int idProfesora)
        {
            Profesor profesor = _profesori.Find(p => p.Id == idProfesora);
            profesor.IdKatedre = -1;
            _storage.CuvanjeProfesora(_profesori);
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
                observer.UpdateProfesor();
            }
        }


    }
}
