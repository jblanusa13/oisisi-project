using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;
using StudSluzba.Entiteti;
using StudSluzba.Kontroler;
using StudSluzba.Lokalizacija;
using StudSluzba.Observer;


namespace StudSluzba
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        private App app;
        private const string SRB = "sr-Latn-RS";
        private const string ENG = "en-US";
        private string _culture;

        private readonly KontrolerStudent _kontrolerStudent;

        private readonly KontrolerProfesor _kontrolerProfesor;

        private readonly KontrolerPredmet _kontrolerPredmet;

        private readonly KontrolerAdresa _kontrolerAdresa;

        private readonly KontrolerKatedra _kontrolerKatedra;

        private string unosPretrage { get; set; }
        public ObservableCollection<Student> Studenti { get; set; }
        public ObservableCollection<Profesor> Profesori { get; set; }
        public ObservableCollection<Predmet> Predmeti { get; set; }

        public ObservableCollection<Katedra> Katedre { get; set; }

        public Student OdabraniStudent { get; set; }
        public Predmet OdabraniPredmet { get; set; }
        public Profesor OdabraniProfesor { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            app = (App)Application.Current;
            app.ChangeLanguage(SRB);
            _culture = SRB;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.75);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.75);

            _kontrolerStudent = new KontrolerStudent();
            _kontrolerStudent.Subscribe(this);
            Studenti = new ObservableCollection<Student>(_kontrolerStudent.GetAllStudents());


            _kontrolerProfesor = new KontrolerProfesor();
            _kontrolerProfesor.Subscribe(this);
            Profesori = new ObservableCollection<Profesor>(_kontrolerProfesor.GetAllProfessors());

            _kontrolerPredmet = new KontrolerPredmet();
            _kontrolerPredmet.Subscribe(this);
            Predmeti = new ObservableCollection<Predmet>(_kontrolerPredmet.GetAllPredmete());

            _kontrolerAdresa = new KontrolerAdresa();


            _kontrolerKatedra = new KontrolerKatedra();

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            Vreme.Text = DateTime.Now.ToString("HH:mm dd.MM.yyyy");
        }

        private void Dodavanje_Click(object sender, RoutedEventArgs e)
        {

            if (Tab.SelectedItem.Equals(TabStudenti))
            {
                DodavanjeStudenta dodavanjeStudenta = new DodavanjeStudenta(_kontrolerStudent, _kontrolerAdresa, _culture, app);
                dodavanjeStudenta.Show();
            }
            else if (Tab.SelectedItem.Equals(TabProfesori))
            {
                DodavanjeProfesora dodavanjeProfesora = new DodavanjeProfesora(_kontrolerProfesor, _kontrolerAdresa, _culture, app);
                dodavanjeProfesora.Show();
            }
            else if (Tab.SelectedItem.Equals(TabPredmeti))
            {
                DodavanjePredmeta dodavanjePredmeta = new DodavanjePredmeta(_kontrolerPredmet, _culture, app);
                dodavanjePredmeta.Show();
            }

        }

        private void Izmena_Click(object sender, RoutedEventArgs e)
        {
            if (Tab.SelectedItem.Equals(TabStudenti))
            {
                if (OdabraniStudent != null)
                {
                    IzmenaStudenta izmenaStudenta = new IzmenaStudenta(OdabraniStudent, _kontrolerStudent, _kontrolerPredmet, _kontrolerAdresa, _culture, app);
                    izmenaStudenta.Show();
                }
            }
            if (Tab.SelectedItem.Equals(TabPredmeti))
            {
                if (OdabraniPredmet != null)
                {
                    IzmenaPredmeta izmenaPredmeta = new IzmenaPredmeta(OdabraniPredmet, _kontrolerPredmet, _kontrolerProfesor, _culture, app);
                    izmenaPredmeta.Show();
                }
            }
            if (Tab.SelectedItem.Equals(TabProfesori))
            {
                if (OdabraniProfesor != null)
                {
                    IzmenaProfesora izmenaProfesora = new IzmenaProfesora(OdabraniProfesor, _kontrolerProfesor, _kontrolerPredmet, _kontrolerAdresa, _kontrolerKatedra, _culture, app);
                    izmenaProfesora.Show();
                }
            }
        }

        private void Brisanje_Click(object sender, RoutedEventArgs e)
        {

            // referencijalne zavisnosti
            if (Tab.SelectedItem.Equals(TabStudenti))
            {
                if (OdabraniStudent != null)
                {
                    MessageBoxResult odgovor = MessageBox.Show("Da li ste sigurni da želite da izbrišete studenta?", "Brisanje studenta", MessageBoxButton.YesNo);

                    switch (odgovor)
                    {
                        case MessageBoxResult.Yes:
                            _kontrolerAdresa.BrisanjeAdrese(OdabraniStudent.AdresaStanovanjaId);
                            _kontrolerStudent.BrisanjeStudenta(OdabraniStudent);
                            break;
                        default:
                            break;
                    }

                }
            }
            else if (Tab.SelectedItem.Equals(TabPredmeti))
            {
                if (OdabraniPredmet != null)
                {

                    MessageBoxResult odgovor = MessageBox.Show("Da li ste sigurni da želite da izbrišete predmet?", "Brisanje predmeta", MessageBoxButton.YesNo);

                    switch (odgovor)
                    {
                        case MessageBoxResult.Yes:
                            _kontrolerPredmet.BrisanjePredmeta(OdabraniPredmet);
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (Tab.SelectedItem.Equals(TabProfesori))
            {
                if (OdabraniProfesor != null)
                {
                    // stavi DA/NE

                    MessageBoxResult odgovor = MessageBox.Show("Da li ste sigurni da želite da izbrišete profesora?", "Brisanje profesora", MessageBoxButton.YesNo);

                    switch (odgovor)
                    {
                        case MessageBoxResult.Yes:

                            foreach (Predmet p in _kontrolerPredmet.GetAllPredmete())
                            {
                                _kontrolerPredmet.ObrisiIdProfesora(p.Id);
                            }
                            _kontrolerAdresa.BrisanjeAdrese(OdabraniProfesor.AdresaStanovanjaId);
                            _kontrolerAdresa.BrisanjeAdrese(OdabraniProfesor.AdresaKancelarijeId);
                            _kontrolerKatedra.ProveriSefa(OdabraniProfesor.Id);
                            _kontrolerProfesor.BrisanjeProfesora(OdabraniProfesor);

                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void AzuriranjeListeStudenata()
        {
            Studenti.Clear();
            foreach (var student in _kontrolerStudent.GetAllStudents())
            {
                Studenti.Add(student);
            }
        }

        private void AzuriranjeListeProfesora()
        {
            Profesori.Clear();
            foreach (var profesor in _kontrolerProfesor.GetAllProfessors())
            {
                Profesori.Add(profesor);
            }
        }

        private void AzuriranjeListePredmeta()
        {
            Predmeti.Clear();
            foreach (var predmet in _kontrolerPredmet.GetAllPredmete())
            {
                Predmeti.Add(predmet);
            }
        }

        public void UpdateStudent()
        {
            AzuriranjeListeStudenata();
        }

        void IObserver.UpdateProfesor()
        {
            AzuriranjeListeProfesora();
        }

        void IObserver.UpdatePredmet()
        {
            AzuriranjeListePredmeta();
        }
        public void UpdateKatedra()
        {
            throw new NotImplementedException();
        }

        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemOpenStudenti_Click(object sender, RoutedEventArgs e)
        {
            Tab.SelectedItem = TabStudenti;
        }

        private void MenuItemOpenProfesori_Click(object sender, RoutedEventArgs e)
        {
            Tab.SelectedItem = TabProfesori;
        }
        private void MenuItemOpenPredmeti_Click(object sender, RoutedEventArgs e)
        {
            Tab.SelectedItem = TabPredmeti;
        }
        private void MenuItemOpenKatedre_Click(object sender, RoutedEventArgs e)
        {
            PrikazKatedri prikazKatedri = new PrikazKatedri(_kontrolerKatedra, _kontrolerProfesor, _culture, app);
            prikazKatedri.Show();
        }
        private void MenuItemClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        // sta ovde treba da se prikaze uopste
        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void MenuItemSerbian_Click(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(SRB);
            _culture = SRB;
        }

        private void MenuItemEnglish_Click(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(ENG);
            _culture = ENG;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.N))
                Dodavanje_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.S))
                MenuItemSave_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.T))
                MenuItemOpenStudenti_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.R))
                MenuItemOpenProfesori_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.P))
                MenuItemOpenPredmeti_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.C))
                MenuItemClose_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.E))
                Izmena_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D))
                Brisanje_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.A))
                MenuItemAbout_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.B))
                MenuItemSerbian_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.H))
                MenuItemEnglish_Click(sender, e);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            unosPretrage = TextBox1.Text;
        }
        private void Pretraga_Click(object sender,RoutedEventArgs e)
        {
                      
                string[] reci = unosPretrage.Split(",");
                int brojUnetihReci = reci.Length;
                string prva = reci[0].ToLower();
                string druga = "";
                string treca = "";
                if (brojUnetihReci == 2)
                {
                    druga = reci[1].ToLower();
                }
                else if (brojUnetihReci == 3)
                {
                    treca = reci[2].ToLower();
                }

                if (Tab.SelectedItem == TabStudenti)
                {
                    Studenti.Clear();
                    foreach (Student s in _kontrolerStudent.GetAllStudents())
                    {


                        if (s.Prezime.ToLowerInvariant().Contains(prva) && s.Ime.ToLowerInvariant().Contains(druga) && s.BrIndeksa.ToLowerInvariant().Contains(treca))
                        {
                            Studenti.Add(s);
                        }
                    }
                }
                if (Tab.SelectedItem == TabProfesori)
                {
                    Profesori.Clear();
                    foreach (Profesor p in _kontrolerProfesor.GetAllProfessors())
                    {
                        if (p.Prezime.ToLowerInvariant().Contains(prva) && p.Ime.ToLowerInvariant().Contains(druga))
                        {
                            Profesori.Add(p);
                        }
                    }
                }
                if (Tab.SelectedItem == TabPredmeti)
                {
                    Predmeti.Clear();
                    foreach (Predmet p in _kontrolerPredmet.GetAllPredmete())
                    {
                        if (p.Naziv.ToLowerInvariant().Contains(prva) && p.Sifra.ToLowerInvariant().Contains(druga))
                        {
                            Predmeti.Add(p);
                        }
                    }
                }
            
        }
    }
}
