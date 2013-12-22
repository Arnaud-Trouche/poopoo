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
            Application.Current.MainWindow.Width = 480;
        }

        private void Back_Top_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (MainWindow)Application.Current.MainWindow;
            parent.goBack();
        }

        private void Demo_Click(object sender, RoutedEventArgs e)
        {
            //TODO enregistrer la difficulté à Demo
            MainWindow parent = (MainWindow)Application.Current.MainWindow;
            parent.changePage("Choix_Peuple.xaml");
        }

        private void Petite_Click(object sender, RoutedEventArgs e)
        {
            //TODO enregistrer la difficulté à Petite
            MainWindow parent = (MainWindow)Application.Current.MainWindow;
            parent.changePage("Choix_Peuple.xaml");
        }

        private void Normale_Click(object sender, RoutedEventArgs e)
        {
            //TODO enregistrer la difficulté à Normale
            MainWindow parent = (MainWindow)Application.Current.MainWindow;
            parent.changePage("Choix_Peuple.xaml");
        }
    }
}
