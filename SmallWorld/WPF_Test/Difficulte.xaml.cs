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
        /// <summary>
        /// Constructeur de Difficulte
        /// </summary>
        public Difficulte()
        {
            InitializeComponent();
        }

        /// <summary>
        /// handler clic fleche retour en haut à gauche :
        ///     - réaction retour à la page précédente 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Top_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (Application.Current.MainWindow as MainWindow);
            parent.goBack();
            e.Handled = true;
        }

        /// <summary>
        /// handler clic sur la case Demo :
        ///     - définition de la difficulte à démo
        ///     - chargement de la page suivante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Demo_Click(object sender, RoutedEventArgs e)
        {
            MonteurPartie.INSTANCE.Difficulte = Constants.DEMO;
            MainWindow parent = (Application.Current.MainWindow as MainWindow);
            parent.changePage("Choix_Peuple.xaml");
            e.Handled = true;
        }

        /// <summary>
        /// handler clic sur la case Petite :
        ///     - définition de la difficulte à petite
        ///     - chargement de la page suivante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Petite_Click(object sender, RoutedEventArgs e)
        {
            MonteurPartie.INSTANCE.Difficulte = Constants.PETITE;
            MainWindow parent = (Application.Current.MainWindow as MainWindow);
            parent.changePage("Choix_Peuple.xaml");
            e.Handled = true;
        }

        /// <summary>
        /// handler clic sur la case Normale :
        ///     - définition de la difficulte à normale
        ///     - chargement de la page suivante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Normale_Click(object sender, RoutedEventArgs e)
        {
            MonteurPartie.INSTANCE.Difficulte = Constants.NORMALE;
            MainWindow parent = (Application.Current.MainWindow as MainWindow);
            parent.changePage("Choix_Peuple.xaml");
            e.Handled = true;
        }
    }
}
