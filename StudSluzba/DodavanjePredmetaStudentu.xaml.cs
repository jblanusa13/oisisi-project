using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace StudSluzba
{
    /// <summary>
    /// Interaction logic for DodavanjePredmetaStudentu.xaml
    /// </summary>
    public partial class DodavanjePredmetaStudentu : Window, INotifyPropertyChanged
    {
        private readonly KontrolerStudent _kontrolerStudent;
        private readonly KontrolerPredmet _kontrolerPredmet;

        public ObservableCollection<string> StudentMozeDaSlusaPrikaz { get; set; }    
        public List<Predmet> PredmetiKojeStudentMozeDaSlusa { get; set; }
        
        private string _indeks;

        public DodavanjePredmetaStudentu(KontrolerStudent kontroler, KontrolerPredmet kontrolerPredmet, string indeks, string culture, App app)
        {
            InitializeComponent();
            DataContext = this;

            
            app = (App)Application.Current;
            app.ChangeLanguage(culture);

            _kontrolerStudent = kontroler;
            _kontrolerPredmet = kontrolerPredmet;
            _indeks = indeks;

            PredmetiKojeStudentMozeDaSlusa = _kontrolerStudent.NadjiDozvoljenePredmete(indeks);
            List<string> pomocni = new List<string>();
            foreach (Predmet p in PredmetiKojeStudentMozeDaSlusa)
            {
                pomocni.Add(p.Sifra + "  -  " + p.Naziv);
            }
            StudentMozeDaSlusaPrikaz = new ObservableCollection<string>(pomocni);
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            foreach (Object obj in ListBoxNepolozeni.SelectedItems)
            {
                string[] listap = (obj.ToString()).Split("  -  ");
                string sifrap = listap[0];
                Predmet p = PredmetiKojeStudentMozeDaSlusa.Find(p => p.Sifra == sifrap);

                _kontrolerStudent.DodajNepolozeniPredmet(_indeks, p.Id);
                _kontrolerPredmet.DodajStudentaKojiNijePolozio(_indeks, sifrap);
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
