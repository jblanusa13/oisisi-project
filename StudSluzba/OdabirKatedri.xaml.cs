using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using StudSluzba.Observer;


namespace StudSluzba
{
    /// <summary>
    /// Interaction logic for OdabirKatedri.xaml
    /// </summary>
    public partial class OdabirKatedri : Window, INotifyPropertyChanged,IObserver
    {
        private readonly KontrolerProfesor _kontrolerProfesor;
        private readonly KontrolerKatedra _kontrolerKatedra;
        private int _idProfesora { get; set; }
        public ObservableCollection<string> ListaKatedri { get; set; }
        public List<Katedra> SveKatedre { get; set; }

        public Button _dodaj { get; set; }

        public Button _ukloni { get; set; }

        public OdabirKatedri(KontrolerKatedra kontrolerKatedra, KontrolerProfesor kontrolerProfesor, int idProfesora, Button dodajBtn, Button ukloniBtn, string culture, App app)
        {
            InitializeComponent();
            DataContext = this;

           
            app = (App)Application.Current;
            app.ChangeLanguage(culture);

            _kontrolerKatedra = kontrolerKatedra;
            _kontrolerKatedra.Subscribe(this);
           
            _kontrolerProfesor = kontrolerProfesor;
            _idProfesora = idProfesora;
            _dodaj = dodajBtn;
            _ukloni = ukloniBtn;
            SveKatedre = _kontrolerKatedra.GetAllKatedre();
            List<string> pomocni = new List<string>();

            foreach (Katedra k in SveKatedre)
            {
                string prikaz = k.Sifra + "-" + k.Naziv;
                pomocni.Add(prikaz);
            }
            ListaKatedri = new ObservableCollection<string>(pomocni);


        }
        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxOdabir.SelectedItem != null)
            {
                string sifra = ListBoxOdabir.SelectedItem.ToString().Split("-")[0];
                int idKatedre = _kontrolerKatedra.PronadjiKatedruPrekoSifre(sifra).Id;
                _kontrolerProfesor.DodajKatedru(idKatedre, _idProfesora);
                Profesor profesor = _kontrolerProfesor.pronadjiProfesoraPoId(_idProfesora);
                _kontrolerKatedra.DodavanjeProfesora(idKatedre, profesor);
                _dodaj.IsEnabled = false;
                _ukloni.IsEnabled = true;
            }
            Close();
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

        public void UpdateStudent()
        {
            throw new NotImplementedException();
        }

        void IObserver.UpdateProfesor()
        {
            throw new NotImplementedException();
        }

        void IObserver.UpdatePredmet()
        {
            throw new NotImplementedException();
        }
        public void UpdateKatedra()
        {
            ListaKatedri.Clear();
            SveKatedre = _kontrolerKatedra.GetAllKatedre();
            List<string> pomocni = new List<string>();
            foreach (Katedra k in SveKatedre)
            {
                string prikaz = k.Sifra + "-" + k.Naziv;
                ListaKatedri.Add(prikaz);
            }
        }
    }
}
