using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Serialization;
using StudentskaSluzba.Entiteti;

namespace StudentskaSluzba.Serijalizacija
{
    internal class UpravljanjeEntitetima 
    {
        private List<Predmeti> predmeti;
        private Serializer<Predmeti> serijalizacija_predmeta;

        private List<Studenti> studenti ;
        private Serializer<Studenti> serijalizacija_studenata;

        private List<Ocena> ocene ;
        private Serializer<Ocena> serijalizacija_ocena;

        private List<Nepolozeni_ispiti> nepolozeni ;
        private Serializer<Nepolozeni_ispiti> serijalizacija_nepolozenih;

        private List<Adresa> adrese ;
        private Serializer<Adresa> serijalizacija_adresa;

        private List<Katedra> katedre ;
        private Serializer<Katedra> serijalizacija_katedri;

        private List<Profesor> profesori ;
        private Serializer<Profesor> serijalizacija_profesora;

        //konstruktor, ucitavanje iz fajlova
        public UpravljanjeEntitetima()
        {
            serijalizacija_predmeta = new Serializer<Predmeti>();
            serijalizacija_studenata = new Serializer<Studenti>();
            serijalizacija_ocena = new Serializer<Ocena>();
            serijalizacija_nepolozenih = new Serializer<Nepolozeni_ispiti>();
            serijalizacija_adresa = new Serializer<Adresa>();
            serijalizacija_katedri = new Serializer<Katedra>();
            serijalizacija_profesora = new Serializer<Profesor>();

            //deserijalizacija (ucitavanje) 
           studenti = serijalizacija_studenata.fromCSV("studenti.txt");

           predmeti = serijalizacija_predmeta.fromCSV("predmeti.txt");

           ocene = serijalizacija_ocena.fromCSV("ocene.txt");

           nepolozeni = serijalizacija_nepolozenih.fromCSV("nepolozeni_ispiti.txt");

           adrese = serijalizacija_adresa.fromCSV("adrese.txt");

           katedre = serijalizacija_katedri.fromCSV("katedre.txt");

           profesori = serijalizacija_profesora.fromCSV("profesori.txt");


        }



        // serijalizacija
        public void cuvanjePredmeta()
        {
            serijalizacija_predmeta.toCSV("predmeti.txt", predmeti);
        }
        public void cuvanjeStudenata()
        {
            serijalizacija_studenata.toCSV("studenti.txt", studenti);
        }
        public void cuvanjeOcena()
        {
            serijalizacija_ocena.toCSV("ocene.txt", ocene);
        }

        public void cuvanjeNepolozenih()
        {
            serijalizacija_nepolozenih.toCSV("nepolozeni_ispiti.txt", nepolozeni);
        }

        public void cuvanjeAdresa()
        {
            serijalizacija_adresa.toCSV("adrese.txt", adrese);
        }

        public void cuvanjeKatedri()
        {
            serijalizacija_katedri.toCSV("katedre.txt", katedre);
        }

        public void cuvanjeProfesora()
        {
            serijalizacija_profesora.toCSV("profesori.txt", profesori);
        }



        //generisanje ID adrese
        public int generisiIDAdrese()
        {
            if (adrese.Count == 0) 
                return 0;
            else 
                return adrese[adrese.Count - 1].IDAdrese + 1;
        }



        //pronalazenje
        public Adresa pronadjiAdresu(int id)
        {
            Adresa adresa = adrese.Find(a => a.IDAdrese == id);
            return adresa;
        }

        public Profesor pronadjiProfesora(string brojLicne)
        {
            Profesor profesor = profesori.Find(p => p.BrojLicneKarte == brojLicne);
            return profesor;
        }

        public Studenti pronadjiStudenta(string brIndeksa)
        {
            Studenti student = studenti.Find(s => s.BrIndeksa == brIndeksa);
            return student;
        }

        public Predmeti pronadjiPredmet(string sifraPredmeta)
        {
            Predmeti predmet = predmeti.Find(p => p.Sifra == sifraPredmeta);
            return predmet;
        }

        public Katedra pronadjiKatedru(string sifraKatedre)
        {
            Katedra katedra = katedre.Find(k => k.SifraKatedre == sifraKatedre);
            return katedra;
        }



        // dodavanje
        public void dodatiStudenta(Studenti student)
        {
            studenti.Add(student);
            cuvanjeStudenata();
        }

        public void dodatiPredmet(Predmeti predmet)
        {
            predmeti.Add(predmet);
            cuvanjePredmeta();

            // uvezivanje predmeta sa profesorom koji ga predaje
            Profesor profesor = pronadjiProfesora(predmet.Br_licne_karte_profesora);
            profesor.SpisakPredmeta.Add(predmet);
            cuvanjeProfesora();
            
        }

        public void dodatiOcenu(Ocena ocena)
        {
            ocene.Add(ocena);
            cuvanjeOcena();

            // uvezivanje studenta sa polozenim ispitima
            Studenti polozio = studenti.Find(s => s.BrIndeksa == ocena.Br_indeksa);
            polozio.Polozeni_ispiti.Add(ocena);
            cuvanjeStudenata();

            // uvezivanje predmeta sa studentom koji ga je polozio
            Predmeti polozen = predmeti.Find(p => p.Sifra == ocena.PredmetSifra);
            polozen.Polozili.Add(polozio);
            cuvanjePredmeta();
        }

        public void dodatiNepolozene(Nepolozeni_ispiti nepolozeni_ispit)
        {
            nepolozeni.Add(nepolozeni_ispit);
            cuvanjeNepolozenih();

            // uvezivanje studenta sa nepolozenim ispitima
            Studenti nije_polozio = studenti.Find(s => s.BrIndeksa == nepolozeni_ispit.Br_indeksa);
            nije_polozio.Nepolozeni_predmeti.Add(nepolozeni_ispit);
            cuvanjeStudenata();
            

            // uvezivanje predmeta sa studentom koji ga nije polozio
            Predmeti nije_polozen = predmeti.Find(p => p.Sifra == nepolozeni_ispit.Sifra_predmeta);
            nije_polozen.Nisu_polozili.Add(nije_polozio);
            cuvanjeNepolozenih();
            
        }

        public void dodatiAdresu(Adresa adresa)
        {
            adresa.IDAdrese = generisiIDAdrese();
            adrese.Add(adresa);
            cuvanjeAdresa();
        }

        public void dodatiKatedru(Katedra katedra)
        {
            katedre.Add(katedra);
            cuvanjeKatedri();
            
        }

        public void dodatiProfesora(Profesor profesor)
        {
            profesori.Add(profesor);
            cuvanjeProfesora();
            // uvezivanje profesora sa katedrom 
            Katedra katedra = pronadjiKatedru(profesor.SifraKatedre);         
            katedra.SpisakProfesora.Add(profesor);
            cuvanjeKatedri();
        }


        
        // izmene entiteta
       public Studenti izmeniStudenta(Studenti novi_student)
        {
            Studenti stariStudent = studenti.Find(s => s.BrIndeksa == novi_student.BrIndeksa);
            if (stariStudent == null) return null;
            stariStudent.Ime = novi_student.Ime;
            stariStudent.Prezime = novi_student.Prezime;
            stariStudent.Datum_rodjenja = novi_student.Datum_rodjenja;
            stariStudent.Adresa_stanovanja = novi_student.Adresa_stanovanja;
            stariStudent.Kontakt_telefon = novi_student.Kontakt_telefon;
            stariStudent.Imejl = novi_student.Imejl;
            stariStudent.Godina_upisa = novi_student.Godina_upisa;
            stariStudent.Trenutna_godina_studija = novi_student.Trenutna_godina_studija;
            stariStudent.Status = novi_student.Status;
            stariStudent.Prosecna_ocena = novi_student.Prosecna_ocena;
            cuvanjeStudenata();
            return stariStudent;
        }

        public Predmeti izmeniPredmet(Predmeti novi_predmet)
        {
            Predmeti stariPredmet = predmeti.Find(p => p.Sifra == novi_predmet.Sifra);
            if (stariPredmet == null) return null;
            stariPredmet.Naziv = novi_predmet.Naziv;
            stariPredmet.Semestar = novi_predmet.Semestar;
            stariPredmet.Godina = novi_predmet.Godina;
            stariPredmet.Br_licne_karte_profesora = novi_predmet.Br_licne_karte_profesora;  //ovde menjati listu predmeta kod prof?
            stariPredmet.Espb = novi_predmet.Espb;
            cuvanjePredmeta();
            return stariPredmet;
        }

        public Profesor izmeniProfesora(Profesor noviProfesor)
        {         
            Profesor stariProfesor = profesori.Find(p => p.BrojLicneKarte == noviProfesor.BrojLicneKarte);
            if (stariProfesor == null) return null;
            stariProfesor.Ime = noviProfesor.Ime;
            stariProfesor.Prezime = noviProfesor.Prezime;
            stariProfesor.DatumRodjenja = noviProfesor.DatumRodjenja;
            stariProfesor.AdresaStanovanja = noviProfesor.AdresaStanovanja;
            stariProfesor.Imejl = noviProfesor.Imejl;
            stariProfesor.KontaktTelefon = noviProfesor.KontaktTelefon;
            stariProfesor.AdresaKancelarije = noviProfesor.AdresaKancelarije;
            stariProfesor.Zvanje = noviProfesor.Zvanje;
            stariProfesor.GodineStaza = noviProfesor.GodineStaza;
            stariProfesor.SifraKatedre = noviProfesor.SifraKatedre;                          
            //izmeniti inf o profesoru u  spisku profesora u klasi katedra?
            cuvanjeProfesora();
            return stariProfesor;
        }

        public Adresa izmeniAdresu(int id_adrese,string[] nova_adresa)
        {           
            Adresa stara = adrese.Find(a => a.IDAdrese == id_adrese);
            if (stara == null) return null;
            stara.Ulica = nova_adresa[0];
            stara.Broj = nova_adresa[1];
            stara.Grad = nova_adresa[2];
            stara.Drzava = nova_adresa[3];
            Profesor profesor1 = profesori.Find(p => p.AdresaStanovanja.IDAdrese == id_adrese);
            if (profesor1 != null)
            {
                profesor1.AdresaStanovanja.Ulica = nova_adresa[0];
                profesor1.AdresaStanovanja.Broj = nova_adresa[1];
                profesor1.AdresaStanovanja.Grad = nova_adresa[2];
                profesor1.AdresaStanovanja.Drzava = nova_adresa[3];
                cuvanjeProfesora();
            }
            Profesor profesor2 = profesori.Find(p => p.AdresaKancelarije.IDAdrese == id_adrese);
            if (profesor2 != null)
            {
                profesor2.AdresaKancelarije.Ulica = nova_adresa[0];
                profesor2.AdresaKancelarije.Broj = nova_adresa[1];
                profesor2.AdresaKancelarije.Grad = nova_adresa[2];
                profesor2.AdresaKancelarije.Drzava = nova_adresa[3];
                cuvanjeProfesora();
            }
            Studenti student = studenti.Find(p => p.Adresa_stanovanja.IDAdrese == id_adrese);
            if (profesor2 != null)
            {
                student.Adresa_stanovanja.Ulica = nova_adresa[0];
                student.Adresa_stanovanja.Broj = nova_adresa[1];
                student.Adresa_stanovanja.Grad = nova_adresa[2];
                student.Adresa_stanovanja.Drzava = nova_adresa[3];
                cuvanjeStudenata();
            }
            cuvanjeAdresa();
            return stara;
        }

        public Katedra izmeniKatedru(Katedra novaKatedra)
        {
            Katedra staraKatedra = katedre.Find(k => k.SifraKatedre == novaKatedra.SifraKatedre);
            if (staraKatedra == null) return null;
            staraKatedra.NazivKatedre = novaKatedra.NazivKatedre;
            staraKatedra.SefKatedre = novaKatedra.SefKatedre;
            cuvanjeKatedri();
            return staraKatedra;
        }

        public bool izmeniOcenu(Ocena novaOcena)
        {
            Ocena ocena = ocene.Find(p => p.PredmetSifra == novaOcena.PredmetSifra && p.Br_indeksa == novaOcena.Br_indeksa && p.Datum_polaganja == novaOcena.Datum_polaganja);
            if (ocena == null)
            {
                Nepolozeni_ispiti nepolozen_ispit = nepolozeni.Find(p => p.Sifra_predmeta == novaOcena.PredmetSifra && p.Br_indeksa == novaOcena.Br_indeksa && p.Datum_polaganja == novaOcena.Datum_polaganja);
                if (nepolozen_ispit == null) return false;
                
                // ako je polozio predmet, onda ga brisemo iz nepolozenih i ubacujemo u polozene
                if (novaOcena.Ocena_na_ispitu != 5)
                {
                    obrisiOcenu(nepolozen_ispit.Br_indeksa, nepolozen_ispit.Sifra_predmeta, nepolozen_ispit.Datum_polaganja, 5);
                    dodatiOcenu(novaOcena);
                }
            }
            else
            {
                // ako ipak nije polozio stavljamo ga u nepolozene, mada ne znam da li to treba
                if (novaOcena.Ocena_na_ispitu == 5)
                {
                    obrisiOcenu(ocena.Br_indeksa, ocena.PredmetSifra, ocena.Datum_polaganja, ocena.Ocena_na_ispitu);
                    Nepolozeni_ispiti nepolozeni_ispit = new Nepolozeni_ispiti(novaOcena.Br_indeksa, novaOcena.PredmetSifra, novaOcena.Datum_polaganja);
                    dodatiNepolozene(nepolozeni_ispit);
                }
                // ako mu se samo promenila ocena onda menjamo ocenu
                else
                {
                    ocena.Ocena_na_ispitu = novaOcena.Ocena_na_ispitu;
                }
            }

            return true;
        }




        //brisanje entiteta
        public Profesor obrisiProfesora(string brLicne)         
        {
            Profesor profesor = pronadjiProfesora(brLicne);
            if (profesor == null) return null;
            profesori.Remove(profesor);
            cuvanjeProfesora();

            // kad se obrise profesor, on ne treba vise da postoji ni u spisku profesora u katedri kojoj je
            // pripadao
            Katedra katedra = pronadjiKatedru(profesor.SifraKatedre);
            katedra.SpisakProfesora.Remove(profesor);
            cuvanjeKatedri();

            // moraju da se obrisu i svi predmeti koje je predavao
            foreach(Predmeti p in profesor.SpisakPredmeta)
            {
                if(p.Br_licne_karte_profesora == brLicne)
                {
                    predmeti.Remove(p);
                }
            }
            return profesor;
        }

        public Adresa obrisiAdresu(int ID)
        {
            Adresa adr = pronadjiAdresu(ID);
            if (adr == null) return null;
            adrese.Remove(adr);
            cuvanjeAdresa();

            // sta da se radi kada se obrise adresa na kojoj neko vec stanuje?
            return adr;
        }

        public Katedra obrisiKatedru(string sifraKatedre)
        {
            Katedra katedra = pronadjiKatedru(sifraKatedre);
            if (katedra == null) return null;
            katedre.Remove(katedra);
            cuvanjeKatedri();

            // kada se obrise katedra, moraju da se obrisu i svi profesori koji su joj pripadali
            foreach (Profesor p in katedra.SpisakProfesora) 
            { 
                if(p.SifraKatedre == sifraKatedre)
                {
                    profesori.Remove(p);
                }
            }

            cuvanjeProfesora();
            return katedra;
        }

        public Predmeti obrisiPredmet(string sifra)
        {
            Predmeti predmet = pronadjiPredmet(sifra);
            if (predmet == null) return null;
            predmeti.Remove(predmet);
            cuvanjePredmeta();

            // da se obrise iz listi u studentu
            return predmet;
        }
        public Studenti obrisiStudenta(string br_indeksa)
        {
            Studenti student = pronadjiStudenta(br_indeksa);
            if (student == null) return null;
            studenti.Remove(student);
            cuvanjeStudenata();

            // da se obrise iz listi u predmetu

            return student;
        }
        public bool obrisiOcenu(string br_indeksa_studenta, string sifra_predmeta, DateOnly datum_ispita, int ocena_na_ispitu)
        {
            Ocena ocena = ocene.Find(p => p.PredmetSifra == sifra_predmeta && p.Br_indeksa == br_indeksa_studenta && p.Datum_polaganja == datum_ispita && p.Ocena_na_ispitu == ocena_na_ispitu);
            if (ocena == null)
            {
                Nepolozeni_ispiti nepolozen_ispit = nepolozeni.Find(p => p.Sifra_predmeta == sifra_predmeta && p.Br_indeksa == br_indeksa_studenta && p.Datum_polaganja == datum_ispita);
                if (nepolozen_ispit == null) return false;
                nepolozeni.Remove(nepolozen_ispit);
                cuvanjeNepolozenih();
                return true;

                // predmet brise se iz studentovih nepolozenih predmeta
                Studenti nije_polozio = pronadjiStudenta(br_indeksa_studenta);
                nije_polozio.Nepolozeni_predmeti.Remove(nepolozen_ispit);
                cuvanjeStudenata();


                // student brise se iz spiska studenata koji nisu polozili predmet
                Predmeti nije_polozen = pronadjiPredmet(sifra_predmeta);
                nije_polozen.Nisu_polozili.Remove(nije_polozio);
                cuvanjeNepolozenih();
            }
            else
            {
                ocene.Remove(ocena);
                cuvanjeOcena();
                return true;

                // premdet brise se iz spiska polozenih ispita studenta
                Studenti polozio = pronadjiStudenta(br_indeksa_studenta);
                polozio.Polozeni_ispiti.Remove(ocena);
                cuvanjeStudenata();

                // student brise se iz spiska studenata koji su polozili predmet
                Predmeti polozen = pronadjiPredmet(sifra_predmeta);
                polozen.Polozili.Remove(polozio);
                cuvanjePredmeta();

            }
        }


        public void prikaziAdresu()

        {
            if (adrese.Count() == 0)
            {
                System.Console.WriteLine("Lista adresa je prazna!");
            }
           
            foreach (Adresa a in adrese)
            {
                System.Console.WriteLine(a);
            }
        }

        public void prikaziKatedru()
        {
            if (katedre.Count()==0)
            {
                System.Console.WriteLine("Lista katedri je prazna!");
            }
            foreach (Katedra k in katedre)
            {
                System.Console.WriteLine(k);
            }
        }

        public void prikaziProfesora()
        {
            if (profesori.Count()==0)
            {
                System.Console.WriteLine("Lista profesora je prazna");
            }
            foreach (Profesor p in profesori)
            {
                System.Console.WriteLine(p);
            }
        }
        public void prikaziStudenta()
        {
            if (studenti.Count() == 0)
            {
                System.Console.WriteLine("Lista studenata je prazna");
            }
            foreach (Studenti s in studenti)
            {
                System.Console.WriteLine(s);
            }
        }
        public void prikaziOcenu()
        {
            if (ocene.Count() == 0)
            {
                System.Console.WriteLine("Lista ocena je prazna");
            }
            foreach (Ocena o in ocene)
            {
                System.Console.WriteLine(o);
            }
        }

        public void prikaziPredmet()
        {
            if (predmeti.Count() == 0)
            {
                System.Console.WriteLine("Lista predmeta je prazna");
            }
            foreach (Predmeti p in predmeti)
            {
                System.Console.WriteLine(p);
            }
        }




    }
}
