using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudSluzba.Entiteti;
using StudSluzba.Entiteti.DAO;
using StudSluzba.Observer;

namespace StudSluzba.Kontroler
{
    public class KontrolerPredmet
    {
        private readonly PredmetDAO _predmeti;

        public KontrolerPredmet()
        {
            _predmeti = new PredmetDAO();
        }

        public List<Predmet> GetAllPredmete()
        {
            return _predmeti.GetAll();
        }

        public void DodavanjePredmeta(string sifra, string naziv, TipSemestra semestar, int godina, int espb, int idProfesora)
        {
            _predmeti.DodavanjePredmeta(sifra, naziv, semestar, godina, espb, idProfesora);
        }

        public void IzmenaPredmeta(string sifra, string naziv, TipSemestra semestar, int godina, int espb, int idProfesora)
        {
            _predmeti.IzmenaPredmeta(sifra, naziv, godina, semestar, espb, idProfesora);
        }

        public void BrisanjePredmeta(Predmet predmet)
        {
            _predmeti.BrisanjePredmeta(predmet);
        }

        public Predmet nadjiPredmet(int idPredmeta)
        {
            return _predmeti.PronadjiPredmet(idPredmeta);
        }
        public Predmet nadjiPredmetPoSifri(string sifraPredmeta)
        {
            return _predmeti.PronadjiPredmetPoSifri(sifraPredmeta);
        }
        public void DodajStudentaKojiNijePolozio(string indeks, string sifra)
        {
            _predmeti.DodajNijePolozio(indeks, sifra);
        }

        public void Subscribe(IObserver observer)
        {
            _predmeti.Subscribe(observer);
        }

        public List<Predmet> NadjiPredmeteKojeProfesorPredaje(int idProfesora)
        {
            return _predmeti.nadjiPredmeteKojeProfesorPredaje(idProfesora);
        }

        public List<Predmet> NadjiPredmeteKojeNikoPredaje()
        {
            return _predmeti.nadjiPredmeteKojeNikoNePredaje();
        }
        public void PostaviIdProfesora(int idProfesora,int idPredmeta)
        {
            _predmeti.postaviIdProfesora(idProfesora, idPredmeta);
        }
        public void ObrisiIdProfesora(int idPredmeta)
        {
            _predmeti.obrisiIdProfesora(idPredmeta);
        }

    }
}
