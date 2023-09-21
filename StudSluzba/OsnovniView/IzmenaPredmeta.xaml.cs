using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StudSluzba.Entiteti;
using StudSluzba.Kontroler;
using StudSluzba.Observer;



namespace StudSluzba
{
    /// <summary>
    /// Interaction logic for IzmenaPredmeta.xaml
    /// </summary>
    public partial class IzmenaPredmeta : Window, INotifyPropertyChanged, IDataErrorInfo, IObserver
    {
        private App _app;
        private string _culture;
        private readonly KontrolerPredmet _kontroler;
        private readonly KontrolerProfesor _kontrolerProfesor;

        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _sifra;
        public string Sifra
        {
            get => _sifra;
            set
            {
                if (value != _sifra)
                {
                    _sifra = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _naziv;
        public string Naziv
        {
            get => _naziv;
            set
            {
                if (value != _naziv)
                {
                    _naziv = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _godina;
        public int Godina
        {
            get => _godina;
            set
            {
                if (value != _godina)
                {
                    _godina = value;
                    OnPropertyChanged();
                }
            }
        }

        private TipSemestra _semestar;
        public TipSemestra Semestar
        {
            get => _semestar;
            set
            {
                if (value != _semestar)
                {
                    _semestar = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _espb;
        public int Espb
        {
            get => _espb;
            set
            {
                if (value != _espb)
                {
                    _espb = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _profesorIme;
        public string ProfesorIme
        {
            get => _profesorIme;
            set
            {
                if (value != _profesorIme)
                {
                    _profesorIme = value;
                    OnPropertyChanged();
                }
            }
        }

        private Regex _StringRegex = new Regex("[A-Za-z]+");
        public String Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Naziv")
                {
                    if (string.IsNullOrEmpty(Naziv))
                        return "Naziv predmeta je obavezan!";

                    Match match = _StringRegex.Match(Naziv);
                    if (!match.Success)
                        return "Naziv se sastoji od slova";

                }
                else if (columnName == "Espb")
                {
                    if (string.IsNullOrEmpty(Espb.ToString()))
                        return "Espb je obavezan!";
                    if (Espb == 0 || Espb > 30)
                        return "Espb mora biti između 1 i 30!";

                }
                else if (columnName == "Godina")
                {

                    Object odabranaGodina = CB1.SelectedItem;
                    if (odabranaGodina == null)
                        return "Odaberite godinu!";

                }
                else if (columnName == "Semestar")
                {

                    Object obj = CB2.SelectedItem;
                    if (obj == null)
                        return "Odaberite semestar!";
                   

                }
                return null;
            }
        }
        private readonly string[] _validiranaObelezja = {"Naziv", "Espb" ,"Semestar"};

        public bool IsValid
        {
            get
            {
                foreach (var obelezje in _validiranaObelezja)
                {
                    if (this[obelezje] != null)
                        return false;
                }
                return true;
            }
        }
        public IzmenaPredmeta(Predmet odabraniPredmet, KontrolerPredmet kontroler, KontrolerProfesor kontrolerProfesor, string culture, App app)
        {
            InitializeComponent();
            DataContext = this;

            _app = app;
            _culture = culture;
            _app = (App)Application.Current;
            _app.ChangeLanguage(_culture);

            _kontroler = kontroler;
            _kontroler.Subscribe(this);
            _kontrolerProfesor = kontrolerProfesor;

            Id = odabraniPredmet.Id;
            Sifra = odabraniPredmet.Sifra;
            Naziv = odabraniPredmet.Naziv;
            Godina = odabraniPredmet.Godina;
            Semestar = odabraniPredmet.Semestar;
            Espb = odabraniPredmet.Espb;
            ProfesorIme = _kontrolerProfesor.ImeIPrezimeProfesora(odabraniPredmet.IdProfesora);

            if (odabraniPredmet.IdProfesora == -1)
            {
                DodajBtn.IsEnabled = true;
                UkloniBtn.IsEnabled = false;
            }
            else
            {
                DodajBtn.IsEnabled = false;
                UkloniBtn.IsEnabled = true;
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid)
            {
                if (ProfesorIme == "")
                {
                    _kontroler.IzmenaPredmeta(Sifra, Naziv, Semestar, Godina, Espb, -1);
                    
                }
                else
                {
                    Profesor p = _kontrolerProfesor.pronadjiProfesoraPoImenu(ProfesorIme);
                    _kontroler.IzmenaPredmeta(Sifra, Naziv, Semestar, Godina, Espb, p.Id);
                    
                }
                Close();
            }
            else
            {
                MessageBox.Show("Ne može se izmeniti predmet,podaci nisu validno popunjeni!");
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Dodaj_Profesora_Click(object sender, RoutedEventArgs e)
        {
            OdabirProfesora odabirProfesora = new OdabirProfesora(_kontroler,_kontrolerProfesor,Id,DodajBtn,UkloniBtn, _culture, _app);
            odabirProfesora.Show();

        }
        private void Ukloni_Profesora_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult poruka = MessageBox.Show("Da li ste sigurni?", "Ukloni profesora", MessageBoxButton.YesNo);

            switch (poruka)
            {
                case MessageBoxResult.Yes:
                    Predmet p = _kontroler.nadjiPredmet(Id);
                    _kontrolerProfesor.UkloniPredmetProfesoru(p, p.IdProfesora);
                    _kontroler.ObrisiIdProfesora(Id);
                    DodajBtn.IsEnabled = true;
                    UkloniBtn.IsEnabled = false;
                    break;
                default:
                    break;
             
            }
        }
        public void UpdateProfesor()
        {
            throw new NotImplementedException();
        }

        public void UpdateStudent()
        {
            throw new NotImplementedException();
        }
        public void UpdateKatedra()
        {
            throw new NotImplementedException();
        }
        public void UpdatePredmet()
        {
            Predmet p = _kontroler.nadjiPredmet(Id);
            ProfesorIme = _kontrolerProfesor.ImeIPrezimeProfesora(p.IdProfesora);
        }
    }
}
