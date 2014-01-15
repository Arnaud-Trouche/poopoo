using Code;
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
    /// Logique d'interaction pour Accueil.xaml
    /// </summary>
    public partial class Accueil : Page
    {
        /// <summary>
        /// Constructeur d'accueil.xaml
        /// </summary>
        public Accueil()
        {            
            InitializeComponent();
            MainWindow parent = (Application.Current.MainWindow as MainWindow);
        }

        /// <summary>
        /// Handler du clic sur nouvelle partie
        ///     - ouverture de la page difficulte.xaml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Nouvelle_Partie_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (Application.Current.MainWindow as MainWindow);
            parent.changePage("Difficulte.xaml");
        }

        /// <summary>
        /// Handler clic sur restaurer partie
        ///     - ouverture de la boite de dialogue ouvrir un ficheir
        ///     - appel à la méthode de restauration
        ///     ) ouverture de la carte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Restaurer_Partie_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Partie"; // Default file name
            dlg.DefaultExt = ".sw"; // Default file extension
            dlg.Filter = "Parties SmallWolrd (.sw)|*.sw"; // Filter files by extension
            dlg.AddExtension = true;
            dlg.Title = "Retrouver une partie";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                Jeu.INSTANCE.charger(dlg.FileName);
                MainWindow parent = (Application.Current.MainWindow as MainWindow);
                parent.changePage("Carte.xaml");
            }
        }

        /// <summary>
        /// handler click bouton quitter partie : appel à application.current.shutdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Quitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// handler click bouton tutoriel :
        ///     - définition d'attributs "aléatoires"
        ///     - lancement de la partie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tuto_Click(object sender, RoutedEventArgs e)
        {
            //On définit les propriétés pour le tutoriel
            MonteurPartie.INSTANCE.Difficulte = Constants.DEMO;
            MonteurPartie.INSTANCE.J1 = "Joueur1";
            MonteurPartie.INSTANCE.J2 = "Joueur2";
            MonteurPartie.INSTANCE.P1 = Constants.NAIN;
            MonteurPartie.INSTANCE.P2 = Constants.GAULOIS;
            MonteurPartie.INSTANCE.initialiser();
            MainWindow parent = (Application.Current.MainWindow as MainWindow);
            parent.changePage("Tutoriel.xaml");
        }


    }
}
