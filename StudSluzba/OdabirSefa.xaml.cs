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
    /// Interaction logic for OdabirSefa.xaml
    /// </summary>
    public partial class OdabirSefa : Window , INotifyPropertyChanged
    {
        private readonly KontrolerProfesor _kontrolerProfesor;
        private readonly KontrolerKatedra _kontrolerKatedra;
        private int _idKatedre { get; set; }
        public ObservableCollection<string> ListaMogucihSefova { get; set; }
        public List<Profesor> MoguciSefovi { get; set; }
        public Button _dodaj { get; set; }
        public Button _ukloni { get; set; }
        public OdabirSefa(KontrolerKatedra kontrolerKatedra,KontrolerProfesor kontrolerProfesor,int idKatedre,Button dodajBtn,Button ukloniBtn)
        {
            InitializeComponent();
            DataContext = this;

            _kontrolerProfesor = kontrolerProfesor;
            _kontrolerKatedra = kontrolerKatedra;
            _idKatedre = idKatedre;
            _dodaj = dodajBtn;
            _ukloni = ukloniBtn;

            
            MoguciSefovi = _kontrolerProfesor.NadjiMoguceSefove(_idKatedre);
            List<string> pomocni = new List<string>();
            
            foreach (Profesor p in MoguciSefovi)
            {
                string imeIPrezime = _kontrolerProfesor.ImeIPrezimeProfesora(p.Id);
                pomocni.Add(imeIPrezime);
            }
            ListaMogucihSefova = new ObservableCollection<string>(pomocni);
        }
        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxOdabir.SelectedItem != null)
            {
                string imeIPrezime = ListBoxOdabir.SelectedItem.ToString();
                Profesor profesor = _kontrolerProfesor.pronadjiProfesoraPoImenu(imeIPrezime);
                Katedra katedra = _kontrolerKatedra.PronadjiKatedru(_idKatedre);
                _kontrolerKatedra.PostaviSefa(profesor.BrojLicneKarte, _idKatedre);
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
    }
}


