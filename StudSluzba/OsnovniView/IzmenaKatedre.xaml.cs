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

namespace StudSluzba.OsnovniView
{
    /// <summary>
    /// Interaction logic for IzmenaKatedre.xaml
    /// </summary>
    public partial class IzmenaKatedre : Window, INotifyPropertyChanged, IDataErrorInfo, IObserver
    {
        private readonly KontrolerKatedra _kontrolerKatedra;
        private readonly KontrolerProfesor _kontrolerProfesor;
        private int _Id;

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
        private string _sefKatedre;
        public string SefKatedre
        {
            get => _sefKatedre;
            set
            {
                if (value != _sefKatedre)
                {
                    _sefKatedre = value;
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
                        return "Naziv katedre je obavezan!";
                    Match match = _StringRegex.Match(Naziv);
                    if (!match.Success)
                        return "Naziv se sastoji od slova";
                }
                return null;

            }
        }
        private readonly string[] _validiranaObelezja = {"Naziv" };
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
        public IzmenaKatedre(Katedra odabranaKatedra,KontrolerProfesor kontrolerProfesor,KontrolerKatedra kontrolerKatedra, string culture, App app)
        {
            InitializeComponent();
            DataContext = this;


            app = (App)Application.Current;
            app.ChangeLanguage(culture);

            _kontrolerKatedra = kontrolerKatedra;
            _kontrolerKatedra.Subscribe(this);
            _kontrolerProfesor = kontrolerProfesor;
            _Id = odabranaKatedra.Id;
           
            Sifra = odabranaKatedra.Sifra;
            Naziv = odabranaKatedra.Naziv;



            if (odabranaKatedra.SefKatedre == -1)
            {
                DodajBtn.IsEnabled = true;
                UkloniBtn.IsEnabled = false;
                SefKatedre = "";
            }
            else
            {
                SefKatedre = odabranaKatedra.SefKatedre.ToString();
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
                  _kontrolerKatedra.IzmenaKatedre(_Id,Sifra, Naziv ,int.Parse(SefKatedre));               
                  Close();
            }
            else
            {
                MessageBox.Show("Ne može se izmeniti katedra,podaci nisu validno popunjeni!");
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Dodaj_Sefa_Click(object sender, RoutedEventArgs e)
        {
            OdabirSefa odabirSefa = new OdabirSefa(_kontrolerKatedra, _kontrolerProfesor, _Id, DodajBtn, UkloniBtn);
            odabirSefa.Show();

        }
        private void Ukloni_Sefa_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult poruka = MessageBox.Show("Da li ste sigurni?", "Ukloni šefa", MessageBoxButton.YesNo);

            switch (poruka)
            {
                case MessageBoxResult.Yes:
                    Katedra k = _kontrolerKatedra.PronadjiKatedru(_Id);
                   
                    _kontrolerKatedra.ObrisiSefa(_Id);
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
            Katedra k = _kontrolerKatedra.PronadjiKatedru(_Id);
            SefKatedre = k.SefKatedre.ToString();
        }
        public void UpdatePredmet()
        {
            throw new NotImplementedException();
        }
    }
}

