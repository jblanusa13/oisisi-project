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
    public class KontrolerKatedra
    {
        private readonly KatedraDAO _katedre;

        public KontrolerKatedra()
        {
            _katedre = new KatedraDAO();
        }

        public Katedra PronadjiKatedru(int id)
        {          
            return _katedre.PronadjiKatedru(id);
        }
        public Katedra PronadjiKatedruPrekoSifre(string sifra)
        {
            return _katedre.PronadjiKatedruPrekoSifre(sifra);
        }

        public void DodavanjeKatedre(string sifra, string naziv, int sefKatedre)
        {
            _katedre.DodavanjeKatedre(sifra, naziv, sefKatedre);
        }


        public void IzmenaKatedre(int id, string sifra, string naziv, int sefKatedre)
        {
            _katedre.IzmenaKatedre(id, sifra, naziv, sefKatedre);
        }
        public void BrisanjeKatedre(Katedra katedra)
        {
            _katedre.BrisanjeKatedre(katedra);
        }
        public void DodavanjeProfesora(int id, Profesor profesor)
        {
            _katedre.DodavanjeProfesora(id,profesor);
        }
        public void UkloniProfesora(int idKatedre,Profesor profesor)
        {
            _katedre.UkloniProfesora(idKatedre,profesor);
        }
        public void ProveriSefa(int idSefa)
        {
            _katedre.ProveriSefa(idSefa);
        }
        public List<Katedra> GetAllKatedre()
        {
            return _katedre.GetAll();
        }
        public void PostaviSefa(string licnaP, int idK)
        {
            _katedre.PostaviSefa(licnaP, idK);
        }
        public void ObrisiSefa(int id)
        {
            _katedre.ObrisiSefa(id);
        }
        public void Subscribe(IObserver observer)
        {
            _katedre.Subscribe(observer);
        }
    }
}
