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
    /// Interaction logic for DodavanjePredmetaProfesoru.xaml
    /// </summary>
    public partial class DodavanjePredmetaProfesoru : Window , INotifyPropertyChanged 
    {

        private readonly KontrolerProfesor _kontrolerProfesor;
        private readonly KontrolerPredmet _kontrolerPredmet;
        private int _id { get; set; }
        public ObservableCollection<string> NePredajePredmete { get; set; }
        public List<Predmet> PredmetiKojeProfesorNePredaje { get; set; }
        
      
        
        public DodavanjePredmetaProfesoru(KontrolerPredmet kontrolerPredmet, KontrolerProfesor kontrolerProfesor,int Id, string culture, App app)
        {
            InitializeComponent();
            DataContext = this;

            
            app = (App)Application.Current;
            app.ChangeLanguage(culture);

            _kontrolerProfesor = kontrolerProfesor;
            _kontrolerPredmet = kontrolerPredmet;
            _id = Id;          
            
            PredmetiKojeProfesorNePredaje = _kontrolerPredmet.NadjiPredmeteKojeNikoPredaje();
            List<string> pomocni = new List<string>();
            
            foreach(Predmet p in PredmetiKojeProfesorNePredaje)
            {
                pomocni.Add(p.Sifra + "-" + p.Naziv);
            }
            NePredajePredmete = new ObservableCollection<string>(pomocni);
        }
        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {          
            foreach (Object obj in ListBox1.SelectedItems)
            {
                string[] listap = (obj.ToString()).Split("-");
                string sifrap = listap[0];
                Predmet predmet = _kontrolerPredmet.nadjiPredmetPoSifri(sifrap);
 
               _kontrolerProfesor.DodajPredmetProfesoru(predmet, _id);
                _kontrolerPredmet.PostaviIdProfesora(_id,predmet.Id);
 
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
    }
}
        