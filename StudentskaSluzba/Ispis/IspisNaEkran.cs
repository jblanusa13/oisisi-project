using StudentskaSluzba.Entiteti;
using StudentskaSluzba.Serijalizacija;

namespace StudentskaSluzba.Ispis
{
    internal class IspisNaEkran
    {
        public UpravljanjeEntitetima upravljanje = new UpravljanjeEntitetima();

        public bool izbor1, izbor2;


        // rukovanje netacnim upisima
        public int upisInt() 
        {
            int broj;
            string unos = System.Console.ReadLine();
            while(!int.TryParse(unos, out broj))
            {
                System.Console.WriteLine("Nije ceo broj, probajte ponovo: ");
                unos = System.Console.ReadLine();
            }


            return broj;
        }
        public double upisDouble() 
        {
            double broj;
            string unos = System.Console.ReadLine();
            while (!double.TryParse(unos, out broj))
            {
                System.Console.WriteLine("Nije broj, probajte ponovo: ");
                unos = System.Console.ReadLine();
            }


            return broj;
        }
        public DateOnly upisDatuma() 
        {
            DateOnly datum;

            string unos = System.Console.ReadLine();
            while (!DateOnly.TryParse(unos, out datum))
            {
                System.Console.WriteLine("Nije datum, probajte u formatu dd.mm.gggg. : ");
                unos = System.Console.ReadLine();
            }


            return datum;
        }

        public Finansiranje upisStatusa()
        {
            Finansiranje status;

            while (true)
            {
                string unos = System.Console.ReadLine();

                if (!Enum.TryParse(unos, true, out status))
                {
                    System.Console.WriteLine("Nije pravilno uneseno, za budzet unesite B, a za samofinansiranje S");
                }
                else break;
            }
            return status;
        }

        public TipSemestra upisSemestra()
        {
            TipSemestra semestar;

            while (true)
            {
                string unos = System.Console.ReadLine();

                if (!Enum.TryParse(unos, out semestar))
                {
                    System.Console.WriteLine("Nije pravilno uneseno, unesite Zimski ili Letnji");
                }
                else break;
               

            }
            return semestar;
        }

        public IspisNaEkran()
        {
            upravljanje = new UpravljanjeEntitetima();
            izbor1 = true;
            izbor2 = true;
            
        }


        public void setIzbor1(bool vrednost)
        {
            izbor1 = vrednost;
        }

        public void setIzbor2(bool vrednost)
        {
            izbor2 = vrednost;
        }


        public void prviIzbor() 
        {
            System.Console.WriteLine("Izaberite broj: ");
            System.Console.WriteLine("1: Upis");
            System.Console.WriteLine("2: Izmena");
            System.Console.WriteLine("3: Brisanje");
            System.Console.WriteLine("4: Prikaz");
            System.Console.WriteLine("0: Izlaz");
        }

        public void pokretanjeIzbora()
        {
            
            while (izbor1)
            {
                prviIzbor();
                string unos = System.Console.ReadLine();
                if (unos == "0")
                    break;

                switch (unos)
                {
                    case "1":
                        setIzbor1(false);
                        setIzbor2(true);
                        citanjeUnosIzbora();
                        
                        break;
                    case "2":
                        setIzbor1(false);
                        setIzbor2(true);
                        citanjeIzmenaIzbora();
                        
                        break;
                    case "3":
                        setIzbor1(false);
                        setIzbor2(true);
                        citanjeBrisanjeIzbora();
                        
                        break;
                    case "4":
                        setIzbor1(false);
                        setIzbor2(true);
                        citanjePrikazIzbora();         
                        break;
                    default:
                        break;
                }
            }
        }

        public void UnosIzbor()
        {
            System.Console.WriteLine("Izaberite broj: ");
            System.Console.WriteLine("1: Unos studenta");
            System.Console.WriteLine("2: Unos profesora");
            System.Console.WriteLine("3: Unos predmeta");
            System.Console.WriteLine("4: Unos ocene");
            System.Console.WriteLine("5: Unos katedre");
            System.Console.WriteLine("6: Unos adrese");
            System.Console.WriteLine("7: Vrati se na pocetni meni");
            System.Console.WriteLine("0: Izlaz");
        }

        public void citanjeUnosIzbora()
        {
            while (izbor2)
            {
                
                UnosIzbor();
                string unos = System.Console.ReadLine();
                if (unos == "0")
                    break;

                switch (unos)
                {
                    case "1":
                        UnosStudenta();
                        break;
                    case "2":
                        UnosProfesora();
                        break;
                    case "3":
                        UnosPredmeta();
                        break;
                    case "4":
                        UnosOcene();
                        break;
                    case "5":
                        UnosKatedre();
                        break;
                    case "6":
                        UnosAdrese();
                        break;
                    case "7" :
                        setIzbor1(true);
                        setIzbor2(false);
                        break;
                    default:
                        break;
                }
            }
        }

        public void IzmenaIzbor()
        {
            System.Console.WriteLine("Izaberite broj: ");
            System.Console.WriteLine("1: Izmena studenta");
            System.Console.WriteLine("2: Izmena profesora");
            System.Console.WriteLine("3: Izmena predmeta");
            System.Console.WriteLine("4: Izmena ocene");
            System.Console.WriteLine("5: Izmena katedre");
            System.Console.WriteLine("6: Izmena adrese");
            System.Console.WriteLine("7: Vrati se na pocetni meni");
            System.Console.WriteLine("0: Izlaz");
        }

        public void citanjeIzmenaIzbora()
        {
            while (izbor2)
            {
                IzmenaIzbor();
                string unos = System.Console.ReadLine();
                if (unos == "0")
                    break;

                switch (unos)
                {
                    case "1":
                        IzmenaStudenta();
                        break;
                    case "2":
                        IzmenaProfesora();
                        break;
                    case "3":
                        IzmenaPredmeta();
                        break;
                    case "4":
                        IzmenaOcene();
                        break;
                    case "5":
                        IzmenaKatedre();
                        break;
                    case "6":
                        IzmenaAdrese();
                        break;
                    case "7":
                        setIzbor1(true);
                        setIzbor2(false);
                        break;
                    default:
                        break;
                }
            }
        }
        public void BrisanjeIzbor()
        {
            System.Console.WriteLine("Izaberite broj: ");
            System.Console.WriteLine("1: Brisanje studenta");
            System.Console.WriteLine("2: Brisanje profesora");
            System.Console.WriteLine("3: Brisanje predmeta");
            System.Console.WriteLine("4: Brisanje ocene");
            System.Console.WriteLine("5: Brisanje katedre");
            System.Console.WriteLine("6: Brisanje adrese");
            System.Console.WriteLine("7: Vrati se na pocetni meni");
            System.Console.WriteLine("0: Izlaz");
        }

        public void citanjeBrisanjeIzbora()
        {
            while (izbor2)
            {
                BrisanjeIzbor();
                string unos = System.Console.ReadLine();
                if (unos == "0")
                    break;

                switch (unos)
                {
                    case "1":
                        BrisanjeStudenta();
                        break;
                    case "2":
                        BrisanjeProfesora();
                        break;
                    case "3":
                        BrisanjePredmeta();
                        break;
                    case "4":
                        BrisanjeOcene();
                        break;
                    case "5":
                        BrisanjeKatedre();
                        break;
                    case "6":
                        BrisanjeAdrese();
                        break;
                    case "7":
                        setIzbor1(true);
                        setIzbor2(false);
                        break;
                    default:
                        break;
                }
            }
        }

        public void PrikazIzbor()
        {
            System.Console.WriteLine("Izaberite broj: ");
            System.Console.WriteLine("1: Prikaz studenta");
            System.Console.WriteLine("2: Prikaz profesora");
            System.Console.WriteLine("3: Prikaz predmeta");
            System.Console.WriteLine("4: Prikaz ocene");
            System.Console.WriteLine("5: Prikaz katedre");
            System.Console.WriteLine("6: Prikaz adrese");
            System.Console.WriteLine("7: Vrati se na pocetni meni");
            System.Console.WriteLine("0: Izlaz");
        }
        public void citanjePrikazIzbora()
        {
            while (izbor2)
            {
                PrikazIzbor();
                string unos = System.Console.ReadLine();
                if (unos == "0")
                    break;

                switch (unos)
                {
                    case "1":
                        PrikazStudenta();
                        break;
                    case "2":
                        PrikazProfesora();
                        break;
                    case "3":
                        PrikazPredmeta();
                        break;
                    case "4":
                        PrikazOcene();
                        break;
                    case "5":
                        PrikazKatedre();
                        break;
                    case "6":
                        PrikazAdrese();
                        break;
                    case "7":
                        setIzbor1(true);
                        setIzbor2(false);
                        break;
                    default:
                        break;
                }
            }
        }

        // upis

        public Studenti UpisStudenta()
        {
            System.Console.WriteLine("Broj indeksa studenta:");
            string brIndeksa = System.Console.ReadLine();
            while (upravljanje.pronadjiStudenta(brIndeksa) != null)
            {
                System.Console.WriteLine("Student sa datim brojem indeksa vec postoji,unesite drugi broj indeksa:");
                brIndeksa = System.Console.ReadLine();
            }
            System.Console.WriteLine("Ime studenta:");
            string ime = System.Console.ReadLine();
            System.Console.WriteLine("Prezime studenta:");
            string prezime = System.Console.ReadLine();
            System.Console.WriteLine("Datum rođenja studenta:");
            DateOnly datum_rodjenja = upisDatuma();
            System.Console.WriteLine("Adresa(upišite ID Adrese):");
            PrikazAdrese();
            int id_adrese = upisInt();
            Adresa adresa_stanovanja = new Adresa();
            if (upravljanje.pronadjiAdresu(id_adrese) != null)      //ako adresa ne postoji,pravi se prazna adresa
            {
                adresa_stanovanja = upravljanje.pronadjiAdresu(id_adrese);
            }
            System.Console.WriteLine("Kontakt telefon:");
            string kontakt_telefon = System.Console.ReadLine();
            System.Console.WriteLine("Imejl:");
            string imejl = System.Console.ReadLine();
            System.Console.WriteLine("Godina upisa:");
            int godina_upisa = upisInt();
            System.Console.WriteLine("Trenutna godina studija:");
            int trenutna_godina_studija = upisInt();
            System.Console.WriteLine("Status finansiranja:");
            Finansiranje status_finansiranja = upisStatusa();
            System.Console.WriteLine("Prosečna ocena:");
            double prosecna_ocena = upisDouble();
            Studenti novi_student = new Studenti(brIndeksa, ime, prezime, datum_rodjenja, adresa_stanovanja, kontakt_telefon, imejl, godina_upisa, trenutna_godina_studija, status_finansiranja, prosecna_ocena);
            return novi_student;
        }

        public void UnosStudenta() 
        {
            System.Console.WriteLine("Unos studenta:");
            Studenti novi_student = UpisStudenta();
            upravljanje.dodatiStudenta(novi_student);
        }

        public Profesor UpisProfesora()
        {
            System.Console.WriteLine("Broj lične karte:");
            string br_licne_karte = System.Console.ReadLine();

            // ovo nam nece valjati pri izmeni
            while (upravljanje.pronadjiProfesora(br_licne_karte) == null)
            {
                System.Console.WriteLine("Profesor sa datim brojem licne karte vec postoji,unesite drugi broj licne karte:");
                br_licne_karte = System.Console.ReadLine();
            }
            System.Console.WriteLine("Prezime:");
            string prezime = System.Console.ReadLine();
            System.Console.WriteLine("Ime:");
            string ime = System.Console.ReadLine();
            System.Console.WriteLine("Datum rođenja:");
            DateOnly datum_rodjenja = upisDatuma();
            System.Console.WriteLine("Adresa stanovanja (upišite ID Adrese):");
            PrikazAdrese();
            int id_adrese = int.Parse(System.Console.ReadLine());
            
            Adresa adresa_stanovanja = new Adresa();                //ako adresa ne postoji,pravi se prazna adresa
            if (upravljanje.pronadjiAdresu(id_adrese) != null)      
            {
                adresa_stanovanja = upravljanje.pronadjiAdresu(id_adrese);
            }
            System.Console.WriteLine("Kontakt telefon:");
            string kontakt_telefon = System.Console.ReadLine();
            System.Console.WriteLine("Imejl:");
            string imejl = System.Console.ReadLine();
            System.Console.WriteLine("Adresa kancelarije (upišite ID Adrese):");
            PrikazAdrese();
            int id_adrese_k = int.Parse(System.Console.ReadLine());
            Adresa adresa_kancelarije = new Adresa();
            if (upravljanje.pronadjiAdresu(id_adrese_k) != null)      //ako adresa ne postoji,pravi se prazna adresa
            {
                adresa_kancelarije = upravljanje.pronadjiAdresu(id_adrese_k);
            }
            System.Console.WriteLine("Zvanje:");
            string zvanje = System.Console.ReadLine();
            System.Console.WriteLine("Godine staža:");
            double godine_staza = upisDouble();
            System.Console.WriteLine("Sifra katedre:");
            PrikazKatedre();
            string sifraKatedre = System.Console.ReadLine(); 
            while (upravljanje.pronadjiKatedru(sifraKatedre) == null)
            {
                System.Console.WriteLine("Navedite sifru katedre koja postoji u listi!:");
                sifraKatedre = System.Console.ReadLine();
             
            }
            Profesor novi_profesor = new Profesor(br_licne_karte, prezime, ime, datum_rodjenja, adresa_stanovanja, kontakt_telefon, imejl, adresa_kancelarije, zvanje, godine_staza, sifraKatedre);
            return novi_profesor;
        }   
         
        public void UnosProfesora()
        {
            System.Console.WriteLine("Unos profesora:");
            Profesor novi_profesor = UpisProfesora();
            upravljanje.dodatiProfesora(novi_profesor);
        }

        public Predmeti UpisPredmeta()
        {
            System.Console.WriteLine("Sifra predmeta:");
            string sifra_predmeta = System.Console.ReadLine();
            while (upravljanje.pronadjiPredmet(sifra_predmeta) != null)
            {
                System.Console.WriteLine("Predmet sa datom sifrom vec postoji,unesite drugu sifru:");
                sifra_predmeta = System.Console.ReadLine();
            }
            System.Console.WriteLine("Naziv predmeta :");
            string naziv_predmeta = System.Console.ReadLine();
            System.Console.WriteLine("Tip semestra:");
            TipSemestra semestar = upisSemestra();
            System.Console.WriteLine("Godina:");
            int godina = upisInt();
            System.Console.WriteLine("ESPB:");
            int espb = upisInt();
            upravljanje.prikaziProfesora();
            System.Console.WriteLine("Broj licne karte profesora koji predaje predmet:");
            string br_licne = System.Console.ReadLine();
            while (upravljanje.pronadjiProfesora(br_licne) == null)
            {
                System.Console.WriteLine("Navedite profesora koji postoji u listi!:");
                br_licne = System.Console.ReadLine();

            }

            Predmeti novi_predmet = new Predmeti(sifra_predmeta, naziv_predmeta, semestar, godina, espb, br_licne);
            return novi_predmet;
        }
         
        
        public void UnosPredmeta() 
        {
            System.Console.WriteLine("Unos predmeta:");
            Predmeti novi_predmet = UpisPredmeta();
            upravljanje.dodatiPredmet(novi_predmet);

        }
        public Ocena UpisOcene()
        {

            System.Console.WriteLine("Broj indeksa studenta:");
            string br_indeksa = System.Console.ReadLine();
            System.Console.WriteLine("Sifra predmeta :");
            string sifra_predmeta = System.Console.ReadLine();
            System.Console.WriteLine("Datum polaganja ispita:");
            DateOnly datum_polaganja = upisDatuma();
            System.Console.WriteLine("Brojcana vrednost ocene:");
            int brojcana_vrednost = upisInt();

            Ocena nova_ocena = new Ocena(br_indeksa, sifra_predmeta, brojcana_vrednost, datum_polaganja);

            return nova_ocena;
        }

        public void UnosOcene() 
        {
            System.Console.WriteLine("Unos ocene:");
            Ocena nova_ocena = UpisOcene();

            if (nova_ocena.Ocena_na_ispitu >= 6 && nova_ocena.Ocena_na_ispitu <= 10)
            {
                upravljanje.dodatiOcenu(nova_ocena);
                
            }
            else if(nova_ocena.Ocena_na_ispitu == 5)
            {
                Nepolozeni_ispiti nepolozen = new Nepolozeni_ispiti(nova_ocena.Br_indeksa, nova_ocena.PredmetSifra, nova_ocena.Datum_polaganja);
                upravljanje.dodatiNepolozene(nepolozen);
            }
            else
            {
                System.Console.WriteLine("Ocena mora biti >=5 i <=10!");
            }
        }
        public Katedra UpisKatedre()
        {
            System.Console.WriteLine("Šifra katedre:");
            string sifra_katedre = System.Console.ReadLine();
            while (upravljanje.pronadjiKatedru(sifra_katedre) != null)
            {
                System.Console.WriteLine("Katedra sa datom sifrom vec postoji,unesite drugu sifru:");
                sifra_katedre = System.Console.ReadLine();
            }
            System.Console.WriteLine("Naziv katedre:");
            string naziv_katedre = System.Console.ReadLine();
            System.Console.WriteLine("Šef katedre:");
            string sef_katedre = System.Console.ReadLine();

            Katedra nova_katedra = new Katedra(sifra_katedre, naziv_katedre, sef_katedre);

            return nova_katedra;
        }

        public void UnosKatedre() 
        {
            System.Console.WriteLine("Unos katedre:");
            Katedra nova_katedra = UpisKatedre();
            upravljanje.dodatiKatedru(nova_katedra);

        }
        public Adresa UpisAdrese()
        {
            System.Console.WriteLine("Unos adrese:\nUlica:");
            string ulica = System.Console.ReadLine();
            System.Console.WriteLine("Broj:");
            string broj = System.Console.ReadLine();
            System.Console.WriteLine("Grad:");
            string grad = System.Console.ReadLine();
            System.Console.WriteLine("Država:");
            string drzava = System.Console.ReadLine();

            Adresa nova_adresa = new Adresa(upravljanje.generisiIDAdrese(),ulica, broj, grad, drzava);

            return nova_adresa;
        }

        public void UnosAdrese() 
        {
            Adresa nova_adresa = UpisAdrese();
            upravljanje.dodatiAdresu(nova_adresa);
        }

        public void IzmenaStudenta()
        {
            upravljanje.prikaziStudenta();
            System.Console.WriteLine("Izmena studenta:");
            Studenti novi_student = UpisStudenta();

            if (upravljanje.izmeniStudenta(novi_student) != null)
            {
                System.Console.WriteLine("Uspešno izmenjeni podaci o studentu!");
            }
            else
            {
                System.Console.WriteLine("Izmena podataka o studentu nije uspešna!");
            }

        }

        public void IzmenaProfesora()
        {
            upravljanje.prikaziProfesora();
            System.Console.WriteLine("Izmena profesora- uneti broj licne karte profesora cije podatke menjate");
            Profesor novi_profesor = UpisProfesora();

            if(upravljanje.izmeniProfesora(novi_profesor) != null)
            {
                System.Console.WriteLine("Uspešno izmenjeni podaci o profesoru!");
            }
            else
            {
                System.Console.WriteLine("Izmena podataka o profesoru nije uspešna!");
            }    
        }
        public void IzmenaPredmeta()
        {

            upravljanje.prikaziPredmet(); 
            System.Console.WriteLine("Izmena predmeta:");
            Predmeti novi_predmet = UpisPredmeta();

            if (upravljanje.izmeniPredmet(novi_predmet) != null)
            {
                System.Console.WriteLine("Uspešno izmenjeni podaci o predmetu!");
            }
            else
            {
                System.Console.WriteLine("Izmena podataka o predmetu nije uspešna!");
            }



        }
        public void IzmenaOcene()
        {
            upravljanje.prikaziOcenu();
            System.Console.WriteLine("Izmena ocene:");

            Ocena nova_ocena = UpisOcene();

            if (upravljanje.izmeniOcenu(nova_ocena))
            {
                System.Console.WriteLine("Uspešno izmenjeni podaci o oceni!");
            }
            else
            {
                System.Console.WriteLine("Izmena podataka o oceni nije uspešna!");
            }

        }
        public void IzmenaKatedre()
        {
            upravljanje.prikaziKatedru();
            System.Console.WriteLine("Izmena katedre:");            
            Katedra nova_katedra = UpisKatedre();
            if(upravljanje.izmeniKatedru(nova_katedra) != null)
            {
                System.Console.WriteLine("Izmena katedre uspešna!");
            }
            else
            {
                System.Console.WriteLine("Izmena katedre neuspešna!");
            }
        }
        public void IzmenaAdrese()
        {
            upravljanje.prikaziAdresu();
            System.Console.WriteLine("Izmena adrese:\nUneti ID adrese koju želite menjati:\n");           
            int id_adrese = int.Parse(System.Console.ReadLine());            
            System.Console.WriteLine("Uneti novu adresu:\nUlica:");
            string ulica = System.Console.ReadLine();
            System.Console.WriteLine("Broj:");
            string broj = System.Console.ReadLine();
            System.Console.WriteLine("Grad:");
            string grad = System.Console.ReadLine();
            System.Console.WriteLine("Država:");
            string drzava = System.Console.ReadLine();
            string[] nova_adresa = { ulica, broj, grad, drzava };           
            if (upravljanje.izmeniAdresu(id_adrese,nova_adresa) != null)
            {
                System.Console.WriteLine("Izmena adrese uspešna!");
            }
            else
            {
                System.Console.WriteLine("Izmena adrese neuspešna!");
            }
        }
        public void BrisanjeStudenta()
        {
            upravljanje.prikaziStudenta();
            System.Console.WriteLine("Brisanje studenta:\nUneti broj indeksa studenta čije podatke želite obrisati:");
            string br_indeksa = System.Console.ReadLine();
            if(upravljanje.obrisiStudenta(br_indeksa) != null)
            {
                System.Console.WriteLine("Brisanje studenta uspešno!");
            }
            else
            {
                System.Console.WriteLine("Brisanje studenta neuspešno! Student ne postoji");
            }

        }
        public void BrisanjeProfesora()
        {
            upravljanje.prikaziProfesora();
            System.Console.WriteLine("Brisanje profesora:\nUneti broj licne karte profesora čije podatke želite obrisati:");
            string br_licne = System.Console.ReadLine();
            if(upravljanje.obrisiProfesora(br_licne) != null)
            {
                System.Console.WriteLine("Brisanje profesora uspešno!");
            }
            else
            {
                System.Console.WriteLine("Brisanje profesora neuspešno!Profesor ne postoji");
            }
        }
        public void BrisanjePredmeta()
        {
            upravljanje.prikaziPredmet();
            System.Console.WriteLine("Brisanje predmeta:\nUneti šifru predmeta čije podatke želite obrisati:");
            string sifra_predmeta = System.Console.ReadLine();
            if (upravljanje.obrisiPredmet(sifra_predmeta) != null)
            {
                System.Console.WriteLine("Brisanje predmeta uspešno!");
            }
            else
            {
                System.Console.WriteLine("Brisanje predmeta neuspešno! Predmet ne postoji");
            }

        }
        public void BrisanjeOcene()
        {
            upravljanje.prikaziOcenu();
            
            Ocena ocena = UpisOcene();

            if (!upravljanje.obrisiOcenu(ocena.Br_indeksa, ocena.PredmetSifra, ocena.Datum_polaganja, ocena.Ocena_na_ispitu))
            {
                System.Console.WriteLine("Brisanje ocene neuspešno! Nisu tačni uneti podaci");
            }
            else
            {
                System.Console.WriteLine("Brisanje ocene uspešno!");
            }
        }
        public void BrisanjeKatedre()
        {
            upravljanje.prikaziKatedru();
            System.Console.WriteLine("Brisanje katedre:\nUneti šifru katedre koju želite obrisati:");
            string sifra = (System.Console.ReadLine());
            
            if (upravljanje.obrisiKatedru(sifra) != null)
            {
                System.Console.WriteLine("Brisanje katedre uspešno!");
            }
            else
            {
                System.Console.WriteLine("Brisanje katedre neuspešno! Katedra ne postoji");
            }
        }
        public void BrisanjeAdrese()
        {
            upravljanje.prikaziAdresu();
            System.Console.WriteLine("Brisanje adrese:\nUneti ID adrese koju zelite obrisati:");
            int id_adrese = upisInt();
            
            if (upravljanje.obrisiAdresu(id_adrese) != null)
            {
                System.Console.WriteLine("Brisanje adrese uspešno!");
            }
            else
            {
                System.Console.WriteLine("Brisanje adrese neuspešno! Adresa ne postoji");
            }
        }
        public void PrikazStudenta()
        {
            upravljanje.prikaziStudenta();
        }       
        public void PrikazPredmeta()
        {
            upravljanje.prikaziPredmet();
        }
        public void PrikazOcene()
        {
            upravljanje.prikaziOcenu();
        }       
        public void PrikazAdrese()
        {
            upravljanje.prikaziAdresu();
        }
        public void PrikazKatedre()
        {
            upravljanje.prikaziKatedru();
        }
        public void PrikazProfesora()
        {
            upravljanje.prikaziProfesora();
        }
    }
}
