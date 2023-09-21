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

namespace StudSluzba
{
    /// <summary>
    /// Interaction logic for OdabirProfesora.xaml
    /// </summary>
    public partial class OdabirProfesora : Window, INotifyPropertyChanged
    {
        private readonly KontrolerProfesor _kontrolerProfesor;
        private readonly KontrolerPredmet _kontrolerPredmet;
        private int _idPredmeta { get; set; }
        public ObservableCollection<string> ListaProfesora { get; set; }
        public List<Profesor> SviProfesori { get; set; }

        public Button _dodaj { get; set; }

        public Button _ukloni { get; set; }

        public OdabirProfesora(KontrolerPredmet kontrolerPredmet, KontrolerProfesor kontrolerProfesor, int idPredmeta,Button dodajBtn,Button ukloniBtn, string culture, App app)
        {
            InitializeComponent();
            DataContext = this;

            
            app = (App)Application.Current;
            app.ChangeLanguage(culture);

            _kontrolerProfesor = kontrolerProfesor;
            _kontrolerPredmet = kontrolerPredmet;
            _idPredmeta = idPredmeta;
            _dodaj = dodajBtn;
            _ukloni = ukloniBtn;
            SviProfesori = _kontrolerProfesor.GetAllProfessors();
            List<string> pomocni = new List<string>();

            foreach (Profesor p in SviProfesori)
            {
                string imeIPrezime = _kontrolerProfesor.ImeIPrezimeProfesora(p.Id);
                pomocni.Add(imeIPrezime);
            }
            ListaProfesora = new ObservableCollection<string>(pomocni);


        }
        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            string imeIPrezime = ListBoxOdabir.SelectedItem.ToString();
            Profesor profesor = _kontrolerProfesor.pronadjiProfesoraPoImenu(imeIPrezime);
            Predmet predmet = _kontrolerPredmet.nadjiPredmet(_idPredmeta);
            _kontrolerPredmet.PostaviIdProfesora(profesor.Id, _idPredmeta);
            _kontrolerProfesor.DodajPredmetProfesoru(predmet, profesor.Id);
            _dodaj.IsEnabled = false;
            _ukloni.IsEnabled = true;
          
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
