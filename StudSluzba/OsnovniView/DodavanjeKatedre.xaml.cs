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

namespace StudSluzba.OsnovniView
{
    /// <summary>
    /// Interaction logic for DodavanjeKatedre.xaml
    /// </summary>
    public partial class DodavanjeKatedre : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly KontrolerKatedra _kontrolerKatedra;

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

        private Regex _StringRegex = new Regex("[A-Za-z]+");

        public String Error => null;



        public string this[string columnName]
        {
            get
            {
                if (columnName == "Sifra")
                {
                    if (string.IsNullOrEmpty(Sifra))
                        return "Šifra katedre je obavezna!";
                    if (_kontrolerKatedra.PronadjiKatedruPrekoSifre(Sifra) != null)
                        return "Katedra sa ovom šifrom već postoji!";


                }
                else if (columnName == "Naziv")
                {
                    if (string.IsNullOrEmpty(Naziv))
                        return "Naziv katedre je obavezan!";
                    Match match = _StringRegex.Match(Naziv);
                    if (!match.Success)
                        return "Naziv se sastoji od slova";
                }
                return null;

            }
        }
        private readonly string[] _validiranaObelezja = { "Sifra", "Naziv" };
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
        public DodavanjeKatedre(KontrolerKatedra kontrolerKatedra, string culture, App app)
        {
            InitializeComponent();
            DataContext = this;

           
            app = (App)Application.Current;
            app.ChangeLanguage(culture);


            _kontrolerKatedra = kontrolerKatedra;
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
                _kontrolerKatedra.DodavanjeKatedre(Sifra,Naziv,-1);
                Close();

            }
            else
            {
                MessageBox.Show("Katedra se ne može dodati jer nisu svi podaci validno popunjeni!");
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
