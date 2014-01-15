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
using Wrapper;



namespace WPF_Test
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Stack<string> history;
        string pageActuelle;

        unsafe public MainWindow()
        {

            //Initialisation de l'historique
            history = new Stack<string>();
            pageActuelle = "accueil.xaml";
        }

        /// <summary>
        /// Change la page chargée dans la frame de la MainWindow
        ///     - gère l'historique avec une pile
        /// </summary>
        /// <param name="adresse">L'adresse (le nom) de la page à charger dans la fenêtre</param>
        public void changePage(String adresse){ 
            //Enregistrer la page actuelle dans la pile
            history.Push(pageActuelle);

            // Navigate to URI using the Source property
            pageActuelle = adresse;
            this.FramePrincipal.Source = new Uri(adresse, UriKind.Relative);
        }

        /// <summary>
        /// Utilisé pour revenir en arrière en utilisant la flèche en haut à gauche de l'écran
        /// </summary>
        public void goBack()
        {
            pageActuelle = history.Peek();
            this.FramePrincipal.Source = new Uri(history.Pop(), UriKind.Relative);          
        }

        /// <summary>
        /// Efface l'historique des pages visitées (mais remet dans l'historique la page d'accueil)
        /// </summary>
        public void clearHistory()
        {
            history.Clear();
            history.Push("Accueil.xaml");
        }
    }
}
