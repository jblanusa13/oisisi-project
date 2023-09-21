using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using StudSluzba.Entiteti;
using StudSluzba.Entiteti.DAO;
using StudSluzba.Observer;

namespace StudSluzba.Kontroler
{
    public class KontrolerProfesor
    {
        private readonly ProfesorDAO _profesori;
        private readonly AdresaDAO _adrese;
        public KontrolerProfesor()
        {
            _profesori = new ProfesorDAO();
            _adrese = new AdresaDAO();
        }

        public List<Profesor> GetAllProfessors()
        {
            return _profesori.GetAll();
        }
        public void DodavanjeProfesora(string brojLicne, string prezime, string ime, DateOnly datumRodjenja, int adresaId , string telefon, string imejl, int adresaKId, string zvanje, double godineStaza, int idKatedre)
        {
            
            _profesori.DodavanjeProfesora(brojLicne, prezime, ime, datumRodjenja, adresaId, telefon, imejl, adresaKId, zvanje, godineStaza, idKatedre);
        }

        public void IzmenaProfesora(string brojLicne, string prezime, string ime, DateOnly datumRodjenja, int adresasId, string telefon, string imejl,int adresakId, string zvanje, double godineStaza, int idKatedre)
        {

            Profesor p = _profesori.Nadji(brojLicne);
           
            _profesori.IzmenaProfesora(brojLicne, prezime, ime, datumRodjenja, adresasId, telefon, imejl, adresakId, zvanje, godineStaza, idKatedre); 
        }

        public void BrisanjeProfesora(Profesor profesor)
        {
            _profesori.BrisanjeProfesora(profesor);
        }

        public Profesor pronadjiProfesora(string brojLicne)
        {
            return _profesori.Nadji(brojLicne);
        }
        public Profesor pronadjiProfesoraPoId(int id)
        {
            return _profesori.NadjiPoId(id);
        }
        public Profesor pronadjiProfesoraPoImenu(string imeIPrezime)
        {
            return _profesori.NadjiPoImenu(imeIPrezime);
        }

        public string ImeIPrezimeProfesora(int idProfesora)
        {
            return _profesori.VratiImeIPrezime(idProfesora);
        }
        public void DodajPredmetProfesoru(Predmet predmet,int idProfesora)
        {
            _profesori.DodajPredmet(predmet,idProfesora);
        }
        public void DodajKatedru(int idKatedre, int idProfesora)
        {
            _profesori.DodajKatedru(idKatedre, idProfesora);
        }

        public void UkloniKatedru(int idProfesora)
        {
            _profesori.UkloniKatedru(idProfesora);
        }

        public void UkloniPredmetProfesoru(Predmet predmet, int idProfesora)
        {
            _profesori.UkloniPredmet(predmet, idProfesora);
        }
        public List<Profesor> NadjiMoguceSefove(int idKatedre)
        {
            return _profesori.NadjiMoguceSefove(idKatedre);
        }
        public void Subscribe(IObserver observer)
        {
            _profesori.Subscribe(observer);
        }

    }
}
