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

namespace StudSluzba
{
    /// <summary>
    /// Interaction logic for UnosOcene.xaml
    /// </summary>
    public partial class UnosOcene : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly KontrolerStudent _kontrolerStudent;

        private string _indeks;

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

        private string _ocena;
        public string Ocena
        {
            get => _ocena;
            set
            {
                if(value != _ocena)
                {
                    _ocena = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _datum;
        public string Datum
        {
            get => _datum;
            set
            {
                if (value != _datum)
                {
                    _datum = value;
                    OnPropertyChanged();
                }
            }
        }
        public UnosOcene(Predmet odabraniPredmet, KontrolerStudent kontrolerStudent, string indeks, string culture, App app)
        {
            InitializeComponent();
            DataContext = this;

            
            app = (App)Application.Current;
            app.ChangeLanguage(culture);

            Sifra = odabraniPredmet.Sifra;
            Naziv = odabraniPredmet.Naziv;

            _indeks = indeks;

            _kontrolerStudent = kontrolerStudent;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid)
            {
                int OcenaInt = int.Parse(Ocena);
                DateOnly datum;
                DateOnly.TryParse(Datum, out datum);
                _kontrolerStudent.DodajOcenuStudentu(_indeks, Sifra, Naziv, OcenaInt, datum);
                _kontrolerStudent.BrisanjeNepolozenogPredmeta(_indeks, Sifra);
                Close();
            }
            else
            {
                MessageBox.Show("Student se ne moze oceniti, jer nisu sva polja validno popunjena.");
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        // validacija
        private Regex _DatumRegex = new Regex("(0?[1-9]|[12][0-9]|3[01])\\.(0?[1-9]|1[012])\\.2[0-9]{3}\\.?$");

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Ocena")
                {
                    Object odabranaOcena = CB1.SelectedItem;
                    if (odabranaOcena == null)
                        return "Odaberite ocenu!";
                }
                else if (columnName == "Datum")
                {
                    if (string.IsNullOrEmpty(Datum))
                        return "Datum polaganja je obavezan";

                    Match match = _DatumRegex.Match(Datum);
                    if (!match.Success)
                        return "Datum mora biti: DD.MM.YYYY";
                }

                return null;
            }
        }


        private readonly string[] _validiranaObelezja = { "Ocena", "Datum" };

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
    }
}
