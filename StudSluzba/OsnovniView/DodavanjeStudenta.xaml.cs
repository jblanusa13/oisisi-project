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
using System.Xml.Linq;
using StudSluzba.Entiteti;
using StudSluzba.Kontroler;
using StudSluzba.Konverzija;

namespace StudSluzba
{
    /// <summary>
    /// Interaction logic for DodavanjeStudenta.xaml
    /// </summary>
    public partial class DodavanjeStudenta : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly KontrolerStudent _kontroler;
        private readonly KontrolerAdresa _kontrolerAdresa;

        private string _indeks;
        public string Indeks
        {
            get => _indeks;
            set
            {
                if(value != _indeks)
                {
                    _indeks = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _ime;
        public string Ime
        {
            get => _ime;
            set
            {
                if(value != _ime)
                {
                    _ime = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _prezime;
        public string Prezime
        {
            get => _prezime;
            set
            {
                if (value != _prezime)
                {
                    _prezime = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _datumRodjenja;
        public string DatumRodjenja
        {
            get => _datumRodjenja;
            set
            {
                if (value != _datumRodjenja)
                {
                    _datumRodjenja = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _stanovanjeUlica;
        public string StanovanjeUlica
        {
            get => _stanovanjeUlica;
            set
            {
                if (value != _stanovanjeUlica)
                {
                    _stanovanjeUlica = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _stanovanjeBroj;
        public string StanovanjeBroj
        {
            get => _stanovanjeBroj;
            set
            {
                if (value != _stanovanjeBroj)
                {
                    _stanovanjeBroj = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _stanovanjeGrad;
        public string StanovanjeGrad
        {
            get => _stanovanjeGrad;
            set
            {
                if (value != _stanovanjeGrad)
                {
                    _stanovanjeGrad = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _stanovanjeDrzava;
        public string StanovanjeDrzava
        {
            get => _stanovanjeDrzava;
            set
            {
                if (value != _stanovanjeDrzava)
                {
                    _stanovanjeDrzava = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _brojTelefona;
        public string BrojTelefona
        {
            get => _brojTelefona;
            set
            {
                if(value != _brojTelefona)
                {
                    _brojTelefona = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _imejl;
        public string Imejl
        {
            get => _imejl;
            set
            {
                if (value != _imejl)
                {
                    _imejl = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _godinaUpisa;
        public string GodinaUpisa
        {
            get => _godinaUpisa;
            set
            {
                if (value != _godinaUpisa)
                {
                    _godinaUpisa = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _godinaStudija;
        public string GodinaStudija
        {
            get => _godinaStudija;
            set
            {
                if (value != _godinaStudija)
                {
                    _godinaStudija = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                if (value != _status)
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }


        public DodavanjeStudenta(KontrolerStudent kontroler,KontrolerAdresa kontrolerAdresa, string culture, App app)
        {
            InitializeComponent();
            DataContext = this;

            app = (App)Application.Current;
            app.ChangeLanguage(culture);

            _kontroler = kontroler;
            _kontrolerAdresa = kontrolerAdresa;
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
                int GodinaUpisaInt = int.Parse(GodinaUpisa);
                DateOnly Datum;
                DateOnly.TryParse(DatumRodjenja, out Datum);
                Finansiranje finansiranje;
                Enum.TryParse(Status, out finansiranje);
                Adresa adresa =_kontrolerAdresa.DodavanjeAdrese(StanovanjeUlica, StanovanjeBroj, StanovanjeGrad, StanovanjeDrzava);
                _kontroler.DodavanjeStudenta(Ime, Prezime, Datum, adresa.Id, BrojTelefona, Imejl, Indeks, GodinaUpisaInt, int.Parse(GodinaStudija), finansiranje);
                Close();
            }
            else
            {
                MessageBox.Show("Student se ne moze napraviti, jer nisu sva polja validno popunjena.");
            }
        }
       
        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        // validacija
        private Regex _StringRegex = new Regex("[A-Za-z]+");
        private Regex _DatumRegex = new Regex("(0?[1-9]|[12][0-9]|3[01])\\.(0?[1-9]|1[012])\\.[1-2][0-9]{3}\\.?$");
        private Regex _BrTelefonaRegex = new Regex("0[0-9]{2}/[0-9]{3}-[0-9]{3}$");
        private Regex _ImejlRegex = new Regex("[a-z][a-z0-9\\.]*@[a-z]+\\.[a-z]+$");
        private Regex _IndeksRegex = new Regex("[A-Z ]{2}[0-9]{1,3}/20[0-2][0-9]$");
        private Regex _GodinaRegex = new Regex("(1[0-9]{3}$)|(20[01][0-9]$)|(202[0123]$)");
        private Regex _BrAdresaRegex = new Regex("[0-9]+[a-zA-Z]*");


        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Ime")
                {
                    if (string.IsNullOrEmpty(Ime))
                        return "Ime je obavezno";

                    Match match = _StringRegex.Match(Ime);
                    if (!match.Success)
                        return "Ime se sastoji od slova";
                }
                else if (columnName == "Prezime")
                {
                    if (string.IsNullOrEmpty(Prezime))
                        return "Prezime je obavezno";

                    Match match = _StringRegex.Match(Prezime);
                    if (!match.Success)
                        return "Prezime se sastoji od slova";
                }
                else if (columnName == "DatumRodjenja")
                {
                    
                    if (string.IsNullOrEmpty(DatumRodjenja))
                        return "Datum rodjenja je obavezan";
                    Match match = _DatumRegex.Match(DatumRodjenja);
                    if (!match.Success)
                        return "Datum bi trebao biti u formatu: DD.MM.YYYY";
                }

                else if (columnName == "StanovanjeUlica")
                {
                    if (string.IsNullOrEmpty(StanovanjeUlica))
                        return "Ulica je obavezna!";

                    Match match = _StringRegex.Match(StanovanjeUlica);
                    if (!match.Success)
                        return "Ulica se sastoji od slova";

                }
                else if (columnName == "StanovanjeBroj")
                {
                    if (string.IsNullOrEmpty(StanovanjeBroj))
                        return "Broj je obavezan!";
                    Match match = _BrAdresaRegex.Match(StanovanjeBroj);
                    if (!match.Success)
                        return "Kućni broj mora početi cifrom!";

                }
                else if (columnName == "StanovanjeGrad")
                {
                    if (string.IsNullOrEmpty(StanovanjeGrad))
                        return "Grad je obavezan!";

                    Match match = _StringRegex.Match(StanovanjeGrad);
                    if (!match.Success)
                        return "Grad se sastoji od slova";

                }
                else if (columnName == "StanovanjeDrzava")
                {
                    if (string.IsNullOrEmpty(StanovanjeDrzava))
                        return "Drzava je obavezna!";

                    Match match = _StringRegex.Match(StanovanjeDrzava);
                    if (!match.Success)
                        return "Drzava se sastoji od slova";

                }
                else if (columnName == "BrojTelefona")
                {
                    if (string.IsNullOrEmpty(BrojTelefona))
                        return "Broj telefona je obavezan";

                    Match match = _BrTelefonaRegex.Match(BrojTelefona);
                    if (!match.Success)
                        return "Broj bi trebao biti u formatu: 0xx/xxx-xxx";
                }
                else if (columnName == "Imejl")
                {
                    if (string.IsNullOrEmpty(Imejl))
                        return "Imejl je obavezan";

                    Match match = _ImejlRegex.Match(Imejl);
                    if (!match.Success)
                        return "Imejl bi trebao biti u formatu: xxx@xxxx.xxx";
                }
                else if (columnName == "Indeks")
                {
                    if (string.IsNullOrEmpty(Indeks))
                        return "Indeks je obavezan";
                    if (_kontroler.nadjiStudenta(Indeks) != null)
                        return "Student sa ovim brojem indeksa već postoji!";

                    Match match = _IndeksRegex.Match(Indeks);
                    if (!match.Success)
                        return "Indeks bi trebao biti u formatu: XY 123/YYYY";
                }
                else if (columnName == "GodinaUpisa")
                {
                    if (string.IsNullOrEmpty(GodinaUpisa))
                        return "Godina upisa je obavezna";

                    int GodinaUpisaInt;
                    if (!int.TryParse(GodinaUpisa, out GodinaUpisaInt))
                    {
                        return "Godina mora biti ceo broj";
                    }

                    Match match = _GodinaRegex.Match(GodinaUpisa);
                    if (!match.Success)
                        return "Primer godine: 2020";

                }
                else if (columnName == "GodinaStudija")
                {


                    Object odabranaGodina = CB1.SelectedItem;
                        if (odabranaGodina == null )
                    
                            return "Odaberite godinu studija!";

                }
                else if (columnName == "Status")
                {


                    Object odabranStatus = CB2.SelectedItem;
                    if (odabranStatus == null)

                        return "Odaberite način finansiranja!";

                }
                return null;
            }

                
        }
        

        private readonly string[] _validiranaObelezja = { "Ime", "Prezime", "StanovanjeUlica","StanovanjeBroj","StanovanjeGrad","StanovanjeDrzava","BrojTelefona", "Imejl", "Indeks", "GodinaUpisa", "DatumRodjenja", "GodinaStudija", "Status"};

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
