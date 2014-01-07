using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Code;

namespace WPF_Test
{
    /// <summary>
    /// Logique d'interaction pour Difficulte.xaml
    /// </summary>
    public partial class Difficulte : Page
    {
        public Difficulte()
        {
            InitializeComponent();
        }

        private void Back_Top_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (Application.Current.MainWindow as MainWindow);
            parent.goBack();
            e.Handled = true;
        }

        private void Demo_Click(object sender, RoutedEventArgs e)
        {
            MonteurPartie.INSTANCE.Difficulte = Constants.DEMO;
            MainWindow parent = (Application.Current.MainWindow as MainWindow);
            parent.changePage("Choix_Peuple.xaml");
            e.Handled = true;
        }

        private void Petite_Click(object sender, RoutedEventArgs e)
        {
            MonteurPartie.INSTANCE.Difficulte = Constants.PETITE;
            MainWindow parent = (Application.Current.MainWindow as MainWindow);
            parent.changePage("Choix_Peuple.xaml");
            e.Handled = true;
        }

        private void Normale_Click(object sender, RoutedEventArgs e)
        {
            MonteurPartie.INSTANCE.Difficulte = Constants.NORMALE;
            MainWindow parent = (Application.Current.MainWindow as MainWindow);
            parent.changePage("Choix_Peuple.xaml");
            e.Handled = true;
        }
    }
}
