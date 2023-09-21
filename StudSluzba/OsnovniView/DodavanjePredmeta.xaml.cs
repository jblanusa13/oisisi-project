using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
using System.Text.RegularExpressions;

namespace StudSluzba
{
    /// <summary>
    /// Interaction logic for DodavanjePredmeta.xaml
    /// </summary>
    public partial class DodavanjePredmeta : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly KontrolerPredmet _kontroler;

        private string _sifra;
        public string Sifra
        {
            get => _sifra;
            set
            {
                if(value != _sifra)
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
                if(value != _godina)
                {
                    _godina = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _semestar;
        public string Semestar
        {
            get => _semestar;
            set
            {
                if(value != _semestar)
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
                if(value != _espb)
                {
                    _espb = value;
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
                        return "Šifra predmeta je obavezna!";
                    if (_kontroler.nadjiPredmetPoSifri(Sifra) != null)
                        return "Predmet sa ovom šifrom već postoji!";


                }
                else if (columnName == "Naziv")
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
                    if(Espb == 0 || Espb>30)
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


                    Object odabranSemestar = CB2.SelectedItem;
                    if (odabranSemestar == null)

                        return "Odaberite semestar!";

                }
                return null;

            }
        }
        private readonly string[] _validiranaObelezja = {"Sifra" , "Naziv" , "Espb", "Godina", "Semestar"};

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
        public DodavanjePredmeta(KontrolerPredmet kontroler, string culture, App app)
        {
            InitializeComponent();
            DataContext = this;

            app = (App)Application.Current;
            app.ChangeLanguage(culture);

            _kontroler = kontroler;
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
                TipSemestra TipS;
                Enum.TryParse(Semestar, out TipS);
                _kontroler.DodavanjePredmeta(Sifra, Naziv, TipS, Godina, Espb, -1);
                Close();

            }
            else
            {
                MessageBox.Show("Predmet se ne može dodati jer nisu svi podaci validno popunjeni!");
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
