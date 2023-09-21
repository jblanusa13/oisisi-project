using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using StudSluzba.Kontroler;
using StudSluzba.Observer;
using StudSluzba.Storage;

namespace StudSluzba.Entiteti.DAO
{
    internal class StudentDAO : ISubject
    {
        // lista observera
        private readonly List<IObserver> _observers;
        // liste
        private readonly List<Student> _studenti;
        private readonly List<Predmet> _predmeti;
        private readonly List<StudentNepolozeniPredmet> _studentNepolozeniPredmet;
        private readonly List<Ocena> _ocene;
        // storage
        private readonly StorageStudent _storage;
        private readonly StoragePredmet _storagePredmet;
        private readonly StorageStudentNepolozeni _storageStudentNepolozeni;
        private readonly StorageOcena _storageOcena;

        // konstruktor
        public StudentDAO()
        {
            _observers = new List<IObserver>();

            _storage = new StorageStudent();
            _storagePredmet = new StoragePredmet();
            _storageStudentNepolozeni = new StorageStudentNepolozeni();
            _storageOcena = new StorageOcena();

            _studenti = _storage.CitanjeStudenta();
            _predmeti = _storagePredmet.CitanjePredmeta();
            _studentNepolozeniPredmet = _storageStudentNepolozeni.CitanjeNepolozenih();
            _ocene = _storageOcena.CitanjeOcena();

            
            foreach(Student student in _studenti)
            {
                student.ProsecnaOcena = NadjiProsecnu(student.BrIndeksa);
            }
            _storage.CuvanjeStudenata(_studenti);
        }

        
        public Student PronadjiStudenta(string brIndeksa)
        {
            Student student = _studenti.Find(s => s.BrIndeksa == brIndeksa);
            return student;
        }

        public List<Student> GetAll()
        {
            return _studenti;
        }

        public Student GetStudent(string indeks)
        {
            Student student = PronadjiStudenta(indeks);
            if (student == null)
                return null;
            return student;
        }

        public int generisiID()
        {
            if (_studenti.Count == 0)
                return 1;
            else
                return _studenti[_studenti.Count - 1].Id + 1;
        }


        public void DodavanjeStudenta(string ime, string prz, DateOnly datumRodjenja, int  adresaStanovanjaId, string kontaktTelefon, string imejl, string brIndeksa, int godinaUpisa, int trenutnaGodinaStudija, Finansiranje status, double prosecnaOcena)
        {
            int id = generisiID();
            Student student = new Student(id, ime, prz, datumRodjenja, adresaStanovanjaId, kontaktTelefon, imejl, brIndeksa, godinaUpisa, trenutnaGodinaStudija, status, prosecnaOcena);
            _studenti.Add(student);
            _storage.CuvanjeStudenata(_studenti);
            NotifyObservers();
        }

        public void IzmenaStudenta(string ime, string prz, DateOnly datumRodjenja, int adresaStanovanjaId, string kontaktTelefon, string imejl, string brIndeksa, int godinaUpisa, int trenutnaGodinaStudija, Finansiranje status, double prosecnaOcena)
        {
            Student student = PronadjiStudenta(brIndeksa);
            student.Ime = ime;
            student.Prezime = prz;
            student.DatumRodjenja = datumRodjenja;
            student.AdresaStanovanjaId = adresaStanovanjaId;
            student.KontaktTelefon = kontaktTelefon;
            student.Imejl = imejl;
            student.GodinaUpisa = godinaUpisa;
            student.TrenutnaGodinaStudija = trenutnaGodinaStudija;
            student.Status = status;
            student.ProsecnaOcena = prosecnaOcena;

            _storage.CuvanjeStudenata(_studenti);
            NotifyObservers(); 
        }
        
        public void BrisanjeStudenta(Student student)
        {
            _studenti.Remove(student);
            _storage.CuvanjeStudenata(_studenti);

            NotifyObservers();

            List<Predmet> nepolozeni = NadjiNepolozene(student.BrIndeksa);
            if (nepolozeni != null)
            {
                foreach (Predmet predmet in nepolozeni.ToList())
                {
                    StudentNepolozeniPredmet studentPredmet = _studentNepolozeniPredmet.Find(sp => sp.IdStudenta == student.Id && sp.IdPredmeta == predmet.Id);
                    BrisanjeNepolozenog(student.BrIndeksa, predmet.Sifra);
                }
            }

            List<Ocena> polozeni = NadjiOcene(student.BrIndeksa);
            if (polozeni != null)
            {
                foreach (Ocena ocena in polozeni.ToList())
                {
                    BrisanjeOcene(student.BrIndeksa, ocena.IdPredmeta);
                }
            }
        }



        public List<Predmet> NadjiDozvoljene(string brIndeksa)
        {
            Student student = PronadjiStudenta(brIndeksa);
            List<Predmet> DozvoljeniPredmeti = new List<Predmet>();

            foreach(Predmet p in _predmeti)
            {
                if (!student.NepolozeniPredmeti.Contains(p) && (_ocene.Find(o => o.IdPredmeta == p.Id && o.IdStudenta == student.Id) == null))
                {
                    if(student.TrenutnaGodinaStudija <= p.Godina)
                    {
                        DozvoljeniPredmeti.Add(p);
                    }
                }
            }

            return DozvoljeniPredmeti;
        }

        public List<Predmet> NadjiNepolozene(string indeks)
        {
            Student student = PronadjiStudenta(indeks);
            student.NepolozeniPredmeti.Clear();
            foreach(StudentNepolozeniPredmet snp in _storageStudentNepolozeni.CitanjeNepolozenih())
            {
                if(snp.IdStudenta == student.Id)
                {
                    Predmet predmet = _predmeti.Find(p => p.Id == snp.IdPredmeta);
                    student.NepolozeniPredmeti.Add(predmet);
                }
            }
            return student.NepolozeniPredmeti;
        }

        public void DodajNepolozeni(string indeks, int idPredmeta)
        {
            Student student = PronadjiStudenta(indeks);

            _studentNepolozeniPredmet.Add(new StudentNepolozeniPredmet(student.Id, idPredmeta));
            _storageStudentNepolozeni.CuvanjeNepolozenih(_studentNepolozeniPredmet);
            NotifyObservers();
        }

        public void BrisanjeNepolozenog(string indeks, string sifra)
        {
            Student student = PronadjiStudenta(indeks);
            Predmet predmet = _predmeti.Find(p => p.Sifra == sifra);

            StudentNepolozeniPredmet studentPredmet = _studentNepolozeniPredmet.Find(sp => sp.IdStudenta == student.Id && sp.IdPredmeta == predmet.Id);
            _studentNepolozeniPredmet.Remove(studentPredmet);
            _storageStudentNepolozeni.CuvanjeNepolozenih(_studentNepolozeniPredmet);
            NotifyObservers();
        }


        public List<Ocena> NadjiOcene(string indeks)
        {
            Student student = PronadjiStudenta(indeks);
            student.PolozeniIspiti.Clear();
            foreach(Ocena ocena in _storageOcena.CitanjeOcena())
            {
                if (ocena.IdStudenta == student.Id)
                {
                    Predmet predmet = _predmeti.Find(p => p.Id == ocena.IdPredmeta);
                    ocena.SifraPredmeta = predmet.Sifra;
                    ocena.Naziv = predmet.Naziv;
                    ocena.Espb = predmet.Espb;
                    student.PolozeniIspiti.Add(ocena);
                }
            }
            return student.PolozeniIspiti;
        }

        public void DodajOcenu(string indeks, string sifra, string naziv, int ocena, DateOnly datum)
        {
            Student student = PronadjiStudenta(indeks);
            Predmet predmet = _predmeti.Find(p => p.Sifra == sifra);

            _ocene.Add(new Ocena(student.Id, predmet.Id, sifra, naziv, predmet.Espb, ocena, datum));
            _storageOcena.CuvanjeOcena(_ocene);
            NotifyObservers();
        }

        public void BrisanjeOcene(string indeks, int idPredmeta)
        {
            Student student = PronadjiStudenta(indeks);

            Ocena ocena = _ocene.Find(o => o.IdStudenta == student.Id && o.IdPredmeta == idPredmeta);
            _ocene.Remove(ocena);
            _storageOcena.CuvanjeOcena(_ocene);
            NotifyObservers();
        }


        public double NadjiProsecnu(string indeks)
        {
            double sumaOcena = 0;
            double brOcena = 0;
            double prosecnaOcena;
            foreach (Ocena ocena in NadjiOcene(indeks))
            {
                sumaOcena += ocena.OcenaNaIspitu;
                brOcena++;
            }
            
            prosecnaOcena = sumaOcena / brOcena;
            if (Double.IsNaN(prosecnaOcena))
            {
                return 0;
            }
            else
            {
                return Math.Round(prosecnaOcena, 2);
            }
        }

        public void PostaviProsecnu(string indeks, double prOcena)
        {
            Student student = PronadjiStudenta(indeks);
            student.ProsecnaOcena = prOcena;
            _storage.CuvanjeStudenata(_studenti);
        }
        
        public int NadjiUkupnoEspb(string indeks)
        {
            int ukupnoEspb = 0;
            foreach (Ocena ocena in NadjiOcene(indeks))
            {
                ukupnoEspb += ocena.Espb;
            }

            return ukupnoEspb;
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
            foreach(var observer in _observers)
            {
                observer.UpdateStudent();
            }
        }
    }
}
