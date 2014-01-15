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
    /// Logique d'interaction pour Choix_Peuple.xaml
    /// </summary>
    public partial class Choix_Peuple : Page
    {
        String peuple1;
        String peuple2;

        /// <summary>
        /// Constructeur de Choix_Peuple.xaml
        /// </summary>
        public Choix_Peuple()
        {
            InitializeComponent();
            peuple1 = "";
            peuple2 = "";
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
        /// handler clic sur une case P1 :
        ///     - mise de la case cochée (sauf si le même peuple est coché pour l'autre peuple)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void P1_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = (sender as RadioButton);
            peuple1 = li.Name.Substring(0, 1);
            if (peuple2 == peuple1)
            {
                peuple1 = "";
                li.IsChecked = false;
            }
            e.Handled = true;
        }

        /// <summary>
        /// handler clic sur une case P2 :
        ///     - mise de la case cochée (sauf si le même peuple est coché pour l'autre peuple)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void P2_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = (sender as RadioButton);
            peuple2 = li.Name.Substring(0, 1);
            if (peuple1 == peuple2)
            {
                peuple2 = "";
                li.IsChecked = false;
            }
            e.Handled = true;
        }

        /// <summary>
        /// handler du clic sur la flèche en bas à droite de la fenêtre :
        ///     - transmission des infos au MonteurPartie
        ///     - changement de page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            bool erreur = false;
            // On efface un éventuel message d'erreur
            ErrorMsg.Content = "";
            //Affichage des messages d'erreur
            if (NameJoueur1.Text == NameJoueur2.Text) { ErrorMsg.Content = "Choisissez des noms différents"; erreur = true; }
            if (peuple2 == "") { ErrorMsg.Content = NameJoueur2.Text + " n'a pas choisi de peuple"; erreur = true; }
            if (peuple1 == "") { ErrorMsg.Content = NameJoueur1.Text + " n'a pas choisi de peuple"; erreur = true; }
            if (NameJoueur2.Text == "") { ErrorMsg.Content = "Le joueur 2 n' a pas de nom !"; erreur = true; }
            if (NameJoueur1.Text == "") { ErrorMsg.Content = "Le joueur 1 n' a pas de nom !"; erreur = true; }
            
            if (!erreur)
            {
                MonteurPartie.INSTANCE.J1 = NameJoueur1.Text;
                MonteurPartie.INSTANCE.J2 = NameJoueur2.Text;
                switch (peuple1)
                {
                    case "N":
                        MonteurPartie.INSTANCE.P1 = Constants.NAIN;
                        break;

                    case "V":
                        MonteurPartie.INSTANCE.P1 = Constants.VIKING;
                        break;

                    case "G":
                        MonteurPartie.INSTANCE.P1 = Constants.GAULOIS;
                        break;
                }
                switch (peuple2)
                {
                    case "N":
                        MonteurPartie.INSTANCE.P2 = Constants.NAIN;
                        break;

                    case "V":
                        MonteurPartie.INSTANCE.P2 = Constants.VIKING;
                        break;

                    case "G":
                        MonteurPartie.INSTANCE.P2 = Constants.GAULOIS;
                        break;
                }
                //On lance le montage de la partie
                MonteurPartie.INSTANCE.initialiser();
                MainWindow parent = (Application.Current.MainWindow as MainWindow);
                parent.changePage("Carte.xaml");
            }
            e.Handled = true;
        }
    }
}
