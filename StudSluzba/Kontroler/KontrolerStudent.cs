using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using StudSluzba.Entiteti;
using StudSluzba.Entiteti.DAO;
using StudSluzba.Observer;
using static System.Reflection.Metadata.BlobBuilder;


namespace StudSluzba.Kontroler
{
    public class KontrolerStudent
    {
        private readonly StudentDAO _studenti;
        private readonly AdresaDAO _adrese;
        public KontrolerStudent()
        {
            _studenti = new StudentDAO();
            _adrese = new AdresaDAO();
        }

        public List<Student> GetAllStudents()
        {
            return _studenti.GetAll();
        }

        public Student GetOneStudent(string indeks)
        {
            return _studenti.GetStudent(indeks);
        }

        public void DodavanjeStudenta(string ime, string prz, DateOnly datumRodjenja, int adresaId, string kontaktTelefon, string imejl, string brIndeksa, int godinaUpisa, int trenutnaGodinaStudija, Finansiranje status)
        {
            _studenti.DodavanjeStudenta(ime, prz, datumRodjenja, adresaId, kontaktTelefon, imejl, brIndeksa, godinaUpisa, trenutnaGodinaStudija, status, 0);
        }

        public void IzmenaStudenta(string ime, string prz, DateOnly datumRodjenja, string ulica, string broj, string grad, string drzava, string kontaktTelefon, string imejl, string brIndeksa, int godinaUpisa, int trenutnaGodinaStudija, Finansiranje status, double prosecnaOcena)
        {
            Student s = _studenti.PronadjiStudenta(brIndeksa);
            Adresa adresaStan = _adrese.IzmenaAdrese(s.AdresaStanovanjaId, ulica, broj, grad, drzava);
            _studenti.IzmenaStudenta(ime, prz, datumRodjenja, adresaStan.Id, kontaktTelefon, imejl, brIndeksa, godinaUpisa, trenutnaGodinaStudija, status, prosecnaOcena);
        }

        public void BrisanjeStudenta(Student student)
        {
            _studenti.BrisanjeStudenta(student);
        }

        public Student nadjiStudenta(string brIndeksa)
        {
            return _studenti.PronadjiStudenta(brIndeksa);
        }

        public List<Predmet> NadjiDozvoljenePredmete(string brIndeksa)
        {
            return _studenti.NadjiDozvoljene(brIndeksa);
        }

        public List<Predmet> NadjiNepolozenePredmete(string indeks)
        {
            return _studenti.NadjiNepolozene(indeks);
        }

        public void DodajNepolozeniPredmet(string indeks, int idPredmeta)
        {
            _studenti.DodajNepolozeni(indeks, idPredmeta);
        }

        public void BrisanjeNepolozenogPredmeta(string indeks, string sifra)
        {
            _studenti.BrisanjeNepolozenog(indeks, sifra);
        }


        public List<Ocena> NadjiOceneStudenta(string indeks)
        {
            return _studenti.NadjiOcene(indeks);
        }
        public void DodajOcenuStudentu(string indeks, string sifra, string naziv, int ocena, DateOnly datum)
        {
            _studenti.DodajOcenu(indeks, sifra, naziv, ocena, datum);
        }
        public void BrisanjeOceneStudenta(string indeks, int idPredmeta)
        {
            _studenti.BrisanjeOcene(indeks, idPredmeta);
        }


        public double NadjiProsecnuOcenu(string indeks)
        {
            return _studenti.NadjiProsecnu(indeks);
        }

        public void PostaviProsecnuOcenu(string indeks, double prOcena)
        {
            _studenti.PostaviProsecnu(indeks, prOcena);
        }

        public int NadjiUkupnoEspbStudenta(string indeks)
        {
            return _studenti.NadjiUkupnoEspb(indeks);
        }
        public void Subscribe(IObserver observer)
        {
            _studenti.Subscribe(observer);
        }

    }
}
