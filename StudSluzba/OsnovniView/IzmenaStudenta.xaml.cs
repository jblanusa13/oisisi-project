using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
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
    /// Interaction logic for IzmenaStudenta.xaml
    /// </summary>
    public partial class IzmenaStudenta : Window, INotifyPropertyChanged, IDataErrorInfo, IObserver
    {
        private App _app;
        private string _culture;

        private readonly KontrolerStudent _kontroler;
        private readonly KontrolerPredmet _kontrolerPredmet;
        private readonly KontrolerAdresa _kontrolerAdresa;

        public ObservableCollection<Predmet> NepolozeniPredmeti { get; set; }
        public ObservableCollection<Ocena> Ocene { get; set; }
        public Predmet OdabraniPredmet { get; set; }
        public Ocena OdabranaOcena { get; set; }

        private string _indeks;
        public string Indeks
        {
            get => _indeks;
            set
            {
                if (value != _indeks)
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
                if (value != _ime)
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

        private double _prosecnaOcena;
        public double ProsecnaOcena
        {
            get => _prosecnaOcena;
            set
            {
                if (value != _prosecnaOcena)
                {
                    _prosecnaOcena = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _ukupnoEspb;
        public int UkupnoEspb
        {
            get => _ukupnoEspb;
            set
            {
                if(value != _ukupnoEspb)
                {
                    _ukupnoEspb = value;
                    OnPropertyChanged();
                }
            }
        }

        public IzmenaStudenta(Student odabraniStudent, KontrolerStudent kontroler, KontrolerPredmet kontrolerPredmet, KontrolerAdresa kontrolerAdresa, string culture, App app)
        {
            InitializeComponent();
            DataContext = this;
            _kontrolerAdresa = kontrolerAdresa;

            _app = app;
            _culture = culture;
            _app = (App)Application.Current;
            _app.ChangeLanguage(_culture);


            Indeks = odabraniStudent.BrIndeksa;
            Ime = odabraniStudent.Ime;
            Prezime = odabraniStudent.Prezime;
            DatumRodjenja = odabraniStudent.DatumRodjenja.ToString();          
            Adresa adresa = _kontrolerAdresa.PronadjiAdresu(odabraniStudent.AdresaStanovanjaId);
            StanovanjeUlica = adresa.Ulica;
            StanovanjeBroj = adresa.Broj;
            StanovanjeGrad = adresa.Grad;
            StanovanjeDrzava = adresa.Drzava;
            BrojTelefona = odabraniStudent.KontaktTelefon;
            Imejl = odabraniStudent.Imejl;
            GodinaUpisa = odabraniStudent.GodinaUpisa.ToString();
            GodinaStudija = odabraniStudent.TrenutnaGodinaStudija.ToString();
            Status = odabraniStudent.Status.ToString();
            

            _kontroler = kontroler;
            _kontroler.Subscribe(this);

            _kontrolerPredmet = kontrolerPredmet;
           

            NepolozeniPredmeti = new ObservableCollection<Predmet>(_kontroler.NadjiNepolozenePredmete(Indeks));
            Ocene = new ObservableCollection<Ocena>(_kontroler.NadjiOceneStudenta(Indeks));

            ProsecnaOcena = _kontroler.NadjiProsecnuOcenu(Indeks);
            UkupnoEspb = _kontroler.NadjiUkupnoEspbStudenta(Indeks);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // izmena
        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid)
            {
                int GodinaUpisaInt = int.Parse(GodinaUpisa);
                int GodinaStudijaInt = int.Parse(GodinaStudija);
                DateOnly datum;
                DateOnly.TryParse(DatumRodjenja, out datum);
                Finansiranje finansiranje;
                Enum.TryParse(Status, out finansiranje);
                _kontroler.IzmenaStudenta(Ime, Prezime, datum, StanovanjeUlica,StanovanjeBroj,StanovanjeGrad,StanovanjeDrzava, BrojTelefona, Imejl, Indeks, GodinaUpisaInt, GodinaStudijaInt, finansiranje, ProsecnaOcena);
                Close();
            }
            else
            {
                MessageBox.Show("Student se ne moze izmeniti, jer nisu sva polja validno popunjena.");
            }
            
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        // nepolozeni
        private void DodajPredmetStudentu_Click(object sender, RoutedEventArgs e)
        {
            DodavanjePredmetaStudentu dodavanjePredmetaStudentu = new DodavanjePredmetaStudentu(_kontroler, _kontrolerPredmet, Indeks, _culture, _app);
            dodavanjePredmetaStudentu.Show();
        }

        private void BrisanjeNepolozenogPredmeta_Click(object sender, RoutedEventArgs e)
        {
            if (OdabraniPredmet != null)
                {
                    MessageBoxResult odgovor = MessageBox.Show("Da li ste sigurni da želite da izbrišete predmet?", "Brisanje predmeta", MessageBoxButton.YesNo);
                    
                    switch (odgovor)
                    {
                        case MessageBoxResult.Yes:
                        _kontroler.BrisanjeNepolozenogPredmeta(Indeks, OdabraniPredmet.Sifra);
                            break;
                        default:
                            break;
                    }
            }
        }

        private void UnosOcene_Click(object sender, RoutedEventArgs e)
        {
            if (OdabraniPredmet != null)
            {
                UnosOcene unosOcene = new UnosOcene(OdabraniPredmet, _kontroler, Indeks, _culture, _app);
                unosOcene.Show();
            }
        }

        private void PonistavanjeOcene_Click(object sender, RoutedEventArgs e)
        {
            if(OdabranaOcena != null)
            {
                MessageBoxResult odgovor = MessageBox.Show("Da li ste sigurni da želite da poništite ocenu?", "Poništavanje ocene", MessageBoxButton.YesNo);

                switch (odgovor)
                {
                    case MessageBoxResult.Yes:
                        int idPredmeta = OdabranaOcena.IdPredmeta;
                        _kontroler.BrisanjeOceneStudenta(Indeks, idPredmeta);
                        _kontroler.DodajNepolozeniPredmet(Indeks, idPredmeta);
                        break;
                    default:
                        break;
                }
            }
        }

        public void UpdateStudent()
        {
            if (_kontroler.GetOneStudent(Indeks) == null)
            {
                return;
            }
                NepolozeniPredmeti.Clear();
                foreach (Predmet predmet in _kontroler.NadjiNepolozenePredmete(Indeks))
                {
                    NepolozeniPredmeti.Add(predmet);
                }

                Ocene.Clear();
                foreach (Ocena ocena in _kontroler.NadjiOceneStudenta(Indeks))
                {
                    Ocene.Add(ocena);
                }

                ProsecnaOcena = _kontroler.NadjiProsecnuOcenu(Indeks);
                _kontroler.PostaviProsecnuOcenu(Indeks, ProsecnaOcena);
                UkupnoEspb = _kontroler.NadjiUkupnoEspbStudenta(Indeks);
            
        }

        public void UpdateProfesor()
        {
            throw new NotImplementedException();
        }

        public void UpdatePredmet()
        {
            throw new NotImplementedException();
        }

        public void UpdateKatedra()
        {
            throw new NotImplementedException();
        }



        // validacija
        private Regex _StringRegex = new Regex("[A-Za-z]+");
        private Regex _DatumRegex = new Regex("(0?[1-9]|[12][0-9]|3[01])\\.(0?[1-9]|1[012])\\.[1-2][0-9]{3}\\.?$");
        private Regex _BrTelefonaRegex = new Regex("0[0-9]{2}/[0-9]{3}-[0-9]{3}$");
        private Regex _ImejlRegex = new Regex("[a-z][a-z0-9\\.]*@[a-z]+\\.[a-z]+$");
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
                        return "Imejl bi trebao biti u formatu: xxxx@xxxx.xxx";
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
                    if (odabranaGodina == null)

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


        private readonly string[] _validiranaObelezja = { "DatumRodjenja","Ime", "Prezime", "StanovanjeUlica", "StanovanjeBroj", "StanovanjeGrad", "StanovanjeDrzava", "BrojTelefona", "Imejl", "GodinaUpisa" };

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
