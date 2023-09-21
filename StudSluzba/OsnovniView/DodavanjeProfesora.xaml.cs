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
using StudSluzba.Konverzija;
namespace StudSluzba
{
    /// <summary>
    /// Interaction logic for DodavanjeProfesora.xaml
    /// </summary>
    public partial class DodavanjeProfesora : Window , INotifyPropertyChanged , IDataErrorInfo
    {
  
        private readonly KontrolerProfesor _kontroler;
        private readonly KontrolerAdresa _kontrolerAdresa;

        private string _prezime;
        public string Prezime
        {
            get => _prezime;
            set
            {
                if(value == null)
                {
                    MessageBox.Show("Mora biti popunjeno");
                }
                if (value != _prezime)
                {
                    _prezime = value;
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
                if (value != _ime)
                {
                    _ime = value;
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
                if (value != _brojTelefona)
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
        private string _kancelarijaUlica ;
        public string  KancelarijaUlica
        {
            get =>  _kancelarijaUlica;
            set
            {
                if (value != _kancelarijaUlica)
                {
                    _kancelarijaUlica = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _kancelarijaBroj;
        public string KancelarijaBroj
        {
            get => _kancelarijaBroj;
            set
            {
                if (value != _kancelarijaBroj)
                {
                    _kancelarijaBroj = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _kancelarijaGrad;
        public string KancelarijaGrad
        {
            get => _kancelarijaGrad;
            set
            {
                if (value != _kancelarijaGrad)
                {
                    _kancelarijaGrad = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _kancelarijaDrzava;
        public string KancelarijaDrzava
        {
            get => _kancelarijaDrzava;
            set
            {
                if (value != _kancelarijaDrzava)
                {
                    _kancelarijaDrzava = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _brojLicneKarte;
        public string BrojLicneKarte
        {
            get => _brojLicneKarte;
            set
            {
                if (value != _brojLicneKarte)
                {
                    _brojLicneKarte = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _zvanje;
        public string Zvanje
        {
            get => _zvanje;
            set
            {
                if (value != _zvanje)
                {
                    _zvanje = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _godineStaza;
        public string GodineStaza
        {
            get => _godineStaza;
            set
            {
                if (value != _godineStaza)
                {
                    _godineStaza = value;
                    OnPropertyChanged();
                }
            }
        }

        private Regex _StringRegex = new Regex("[A-Za-z]+");
        private Regex _DatumRegex = new Regex("(0?[1-9]|[12][0-9]|3[01])\\.(0?[1-9]|1[012])\\.[1-2][0-9]{3}\\.?$");
        private Regex _BrTelefonaRegex = new Regex("0[0-9]{2}/[0-9]{3}-[0-9]{3}$");
        private Regex _BrAdresaRegex = new Regex("[0-9]+[a-zA-Z]*");
        private Regex _ImejlRegex = new Regex("[a-z][a-z0-9\\.]*@[a-z]+\\.[a-z]+$");
        private Regex _BrLicneKarteRegex = new Regex("[0-9]{9,9}$");
        private Regex _ZvanjeRegex = new Regex("[A-Z]+[_A-Z]+$");

        public String Error => null;

        public string this[string columnName]
        {
            get
            {
                if(columnName == "Prezime")
                {
                    if (string.IsNullOrEmpty(Prezime))
                        return "Unesite prezime!";

                    Match match = _StringRegex.Match(Prezime);
                    if (!match.Success)
                        return "Prezime se sastoji od slova";

                }
                else if (columnName == "Ime")
                {
                    if (string.IsNullOrEmpty(Ime))
                        return "Unesite ime!";

                    Match match = _StringRegex.Match(Ime);
                    if (!match.Success)
                        return "Ime se sastoji od slova";

                }
                else if (columnName == "DatumRodjenja")
                {
                    if (string.IsNullOrEmpty(DatumRodjenja.ToString()))
                        return "Unesite datum rodjenja!";
                    Match match = _DatumRegex.Match(DatumRodjenja);
                    if (!match.Success)
                        return "Datum mora biti u formatu: DD.MM.YYYY";

                }
                else if (columnName == "StanovanjeUlica")
                {
                    if (string.IsNullOrEmpty(StanovanjeUlica))
                        return "Unesite ulicu!";
                    Match match = _StringRegex.Match(StanovanjeUlica);
                    if (!match.Success)
                        return "Ulica se sastoji od slova";
                }
                else if (columnName == "StanovanjeBroj")
                {
                    if (string.IsNullOrEmpty(StanovanjeBroj))
                        return "Unesite broj!";
                    Match match = _BrAdresaRegex.Match(StanovanjeBroj);
                    if (!match.Success)
                        return "Kućni broj mora početi cifrom!";

                }
                else if (columnName == "StanovanjeGrad")
                {
                    if (string.IsNullOrEmpty(StanovanjeGrad))
                        return "Unesite grad!";

                    Match match = _StringRegex.Match(StanovanjeGrad);
                    if (!match.Success)
                        return "Grad se sastoji od slova";
                }
                else if (columnName == "StanovanjeDrzava")
                {
                    if (string.IsNullOrEmpty(StanovanjeDrzava))
                        return "Unesite drzavu!";

                    Match match = _StringRegex.Match(StanovanjeDrzava);
                    if (!match.Success)
                        return "Drzava se sastoji od slova";

                }
                else if (columnName == "BrojTelefona")
                {
                    if (string.IsNullOrEmpty(BrojTelefona))
                        return "Unesite broj telefona!";
                    Match match1 = _BrTelefonaRegex.Match(BrojTelefona);
                    if (!match1.Success)
                        return "Broj mora biti u formatu: 0xx/xxx-xxx";

                }
                else if (columnName == "Imejl")
                {
                    if (string.IsNullOrEmpty(Imejl))
                        return "Unesite imejl!";
                    Match match2 = _ImejlRegex.Match(Imejl);
                    if (!match2.Success)
                        return "Imejl mora biti u formatu: xxxx@xxxx.xxx";

                }
                else if (columnName == "KancelarijaUlica")
                {
                    if (string.IsNullOrEmpty(KancelarijaUlica))
                        return "Unesite ulicu!";

                    Match match = _StringRegex.Match(KancelarijaUlica);
                    if (!match.Success)
                        return "Ulica se sastoji od slova";
                }
                else if (columnName == "KancelarijaBroj")
                {
                    if (string.IsNullOrEmpty(KancelarijaBroj))
                        return "Unesite broj!";
                    Match match = _BrAdresaRegex.Match(KancelarijaBroj);
                    if (!match.Success)
                        return "Broj adrese kancelarije mora početi cifrom!";

                }
                else if (columnName == "KancelarijaGrad")
                {
                    if (string.IsNullOrEmpty(KancelarijaGrad))
                        return "Unesite grad!";
                    Match match = _StringRegex.Match(KancelarijaGrad);
                    if (!match.Success)
                        return "Grad se sastoji od slova";


                }
                else if (columnName == "KancelarijaDrzava")
                {
                    if (string.IsNullOrEmpty(KancelarijaDrzava))
                        return "Unesite drzavu!";
                    Match match = _StringRegex.Match(KancelarijaDrzava);
                    if (!match.Success)
                        return "Drzava se sastoji od slova";

                }
                else if (columnName == "BrojLicneKarte")
                {
                    if (string.IsNullOrEmpty(BrojLicneKarte))
                        return "Unesite broj lične karte!";
                    if (_kontroler.pronadjiProfesora(BrojLicneKarte) != null)
                        return "Već postoji profesor sa datim brojem lične karte!";
                    Match match3 = _BrLicneKarteRegex.Match(BrojLicneKarte);
                    if (!match3.Success)
                        return "Broj licne karte mora imati 9 cifara!";

                }
                else if (columnName == "Zvanje")
                {
                    if (string.IsNullOrEmpty(Zvanje))
                        return "Unesite zvanje!";

                    Match match = _ZvanjeRegex.Match(Zvanje);
                    if (!match.Success)
                        return "Zvanje mora biti u formatu: XXX_XXX ili XXXX";

                }
                else if (columnName == "GodineStaza")
                {
                    if (string.IsNullOrEmpty(GodineStaza))
                        return "Unesite godine staža!";
                    double GodineStazaD;
                    if (!double.TryParse(GodineStaza, out GodineStazaD))
                        return "Godina mora biti realan broj!";
                    if (double.Parse(GodineStaza) > 45)
                        return "Godine staža ne mogu biti veće od 45!";

                }
                return null;

            }
        }


        private readonly string[] _validatedProperties = { "Prezime" ,"Ime" ,"DatumRodjenja", "StanovanjeUlica", "StanovanjeBroj", "StanovanjeGrad", "StanovanjeDrzava", "BrojTelefona", "Imejl", "KancelarijaUlica", "KancelarijaBroj", "KancelarijaGrad", "KancelarijaDrzava", "BrojLicneKarte", "Zvanje", "GodineStaza"};

        public bool IsValid
        {
            get
            {
                foreach(var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }
                return true;
            }
        }



        public DodavanjeProfesora(KontrolerProfesor kontroler,KontrolerAdresa kontrolerAdresa, string culture, App app)
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
                double GodineStazaD = double.Parse(GodineStaza);
                DateOnly Datum;
                DateOnly.TryParse(DatumRodjenja, out Datum);
                Adresa adresa = _kontrolerAdresa.DodavanjeAdrese(StanovanjeUlica, StanovanjeBroj, StanovanjeGrad, StanovanjeDrzava);
                Adresa adresaK = _kontrolerAdresa.DodavanjeAdrese(KancelarijaUlica, KancelarijaBroj, KancelarijaGrad, KancelarijaDrzava);
                _kontroler.DodavanjeProfesora(BrojLicneKarte, Prezime, Ime, Datum, adresa.Id ,BrojTelefona, Imejl, adresaK.Id, Zvanje, GodineStazaD, -1);
                Close();
            }
            else
            {
                MessageBox.Show("Profesor se ne moze dodati, jer nisu sva polja validno popunjena.");
            }
           
            
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

