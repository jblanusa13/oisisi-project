using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using StudSluzba.OsnovniView;

namespace StudSluzba
{
    /// <summary>
    /// Interaction logic for PrikazKatedri.xaml
    /// </summary>
    public partial class PrikazKatedri : Window, IObserver
    {
        private App _app;
        private string _culture;

        private readonly KontrolerKatedra _kontrolerKatedra;
        private readonly KontrolerProfesor _kontrolerProfesor;
        public ObservableCollection<Katedra> Katedre { get; set; }
        public Katedra OdabranaKatedra { get; set; }
        public PrikazKatedri(KontrolerKatedra kontrolerKatedra, KontrolerProfesor kontrolerProfesor, string culture, App app)
        {
            InitializeComponent();
            DataContext = this;

            _app = app;
            _culture = culture;
            _app = (App)Application.Current;
            _app.ChangeLanguage(_culture);

            _kontrolerKatedra = kontrolerKatedra;
            _kontrolerProfesor = kontrolerProfesor;
            _kontrolerKatedra.Subscribe(this);
            Katedre = new ObservableCollection<Katedra>(_kontrolerKatedra.GetAllKatedre());
        }

        public void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeKatedre dodavanjeKatedre = new DodavanjeKatedre(_kontrolerKatedra, _culture, _app);
            dodavanjeKatedre.Show();
        }

        public void Ukloni_Click(object sender, RoutedEventArgs e)
        {
            if (OdabranaKatedra != null)
            {
                MessageBoxResult odgovor = MessageBox.Show("Da li ste sigurni?", "Brisanje katedre", MessageBoxButton.YesNo);

                switch (odgovor)
                {
                    case MessageBoxResult.Yes:
                        foreach (Profesor p in _kontrolerProfesor.GetAllProfessors())
                        {
                            if(p.IdKatedre == OdabranaKatedra.Id)
                            {
                                _kontrolerProfesor.UkloniKatedru(p.Id);
                            }
                        }
                        _kontrolerKatedra.BrisanjeKatedre(OdabranaKatedra);
                        break;
                    default:
                        break;
                }

            }
        }
        public void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if(OdabranaKatedra != null)
            {
                IzmenaKatedre izmenaKatedre = new IzmenaKatedre(OdabranaKatedra,_kontrolerProfesor,_kontrolerKatedra, _culture, _app);
                izmenaKatedre.Show();
            }
            else
            {
                MessageBox.Show("Morate odabrati katedru koju zelite izmeniti!");
            }
        }



        public void UpdateStudent()
        {
            throw new NotImplementedException();
        }
        public void UpdateProfesor()
        {
            throw new NotImplementedException();
        }
        public void UpdatePredmet()
        {
            //throw new NotImplementedException();
        }
        public void UpdateKatedra()
        {
            Katedre.Clear();
            foreach (Katedra k in _kontrolerKatedra.GetAllKatedre())
            {
                Katedre.Add(k);
            }
        }




    }
}
