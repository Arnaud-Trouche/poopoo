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

        /// <summary>
        /// handler de la souris qui rentre sur une tuile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TuilePeuple_MouseEnter(object sender, MouseEventArgs e)
        {     
            //Affichage des titres
            PeupleInfoL1.Visibility = System.Windows.Visibility.Visible;
            PeupleInfoL2.Visibility = System.Windows.Visibility.Visible;
            PeupleInfoL3.Visibility = System.Windows.Visibility.Visible;

            RadioButton li = (sender as RadioButton);
            var p = li.Name.Substring(0, 1);
            switch (p)
            {
                case "N":
                    PeupleInfoNom.Content = "Nains";
                    PeupleInfoT1.Inlines.Add(new Run("Désert")); 
                    PeupleInfoT1.Inlines.Add(new LineBreak());     
                    PeupleInfoT1.Inlines.Add(new Run("Forêt")); 
                    PeupleInfoT1.Inlines.Add(new LineBreak());
                    PeupleInfoT1.Inlines.Add(new Run("Montagne"));  
                    PeupleInfoT1.Inlines.Add(new LineBreak());
                    PeupleInfoT1.Inlines.Add(new Run("Plaine"));

                    PeupleInfoT2.Inlines.Add(new Bold(new Run("+0 : ")));
                    PeupleInfoT2.Inlines.Add(new Run("Plaine"));   
                    PeupleInfoT2.Inlines.Add(new LineBreak());
                    PeupleInfoT2.Inlines.Add(new Bold(new Run("+1 : ")));
                    PeupleInfoT2.Inlines.Add(new Run("Désert, Montagne"));   
                    PeupleInfoT2.Inlines.Add(new LineBreak());
                    PeupleInfoT2.Inlines.Add(new Bold(new Run("+2 : ")));
                    PeupleInfoT2.Inlines.Add(new Run("Forêt"));

                    PeupleInfoT3.Inlines.Add(new Run("Les Nains ont la capacité de se déplacer de Montagne en Montagne, même si elles ne sont pas concomitantes"));
                    break;

                case "V":
                    PeupleInfoNom.Content = "Vikings";
                    PeupleInfoT1.Inlines.Add(new Run("Désert"));
                    PeupleInfoT1.Inlines.Add(new LineBreak());      
                    PeupleInfoT1.Inlines.Add(new Run("Eau"));   
                    PeupleInfoT1.Inlines.Add(new LineBreak());  
                    PeupleInfoT1.Inlines.Add(new Run("Forêt"));     
                    PeupleInfoT1.Inlines.Add(new LineBreak());
                    PeupleInfoT1.Inlines.Add(new Run("Montagne"));   
                    PeupleInfoT1.Inlines.Add(new LineBreak());
                    PeupleInfoT1.Inlines.Add(new Run("Plaine"));

                    PeupleInfoT2.Inlines.Add(new Bold(new Run("+0 : ")));
                    PeupleInfoT2.Inlines.Add(new Run("Eau, Désert"));   
                    PeupleInfoT2.Inlines.Add(new LineBreak());
                    PeupleInfoT2.Inlines.Add(new Bold(new Run("+1 : ")));
                    PeupleInfoT2.Inlines.Add(new Run("Forêt, Montagne, Plaine"));

                    PeupleInfoT3.Inlines.Add(new Run("Les Vikings récupèrent un point de score supplémentaire lorsque qu'ils sont à côté d'une case eau"));
                    break;

                case "G":
                    PeupleInfoNom.Content = "Gaulois"; 
                    PeupleInfoT1.Inlines.Add(new Run("Désert")); 
                    PeupleInfoT1.Inlines.Add(new LineBreak());     
                    PeupleInfoT1.Inlines.Add(new Run("Forêt"));    
                    PeupleInfoT1.Inlines.Add(new LineBreak());
                    PeupleInfoT1.Inlines.Add(new Run("Montagne")); 
                    PeupleInfoT1.Inlines.Add(new LineBreak());
                    PeupleInfoT1.Inlines.Add(new Run("Plaine"));

                    PeupleInfoT2.Inlines.Add(new Bold(new Run("+0 : ")));
                    PeupleInfoT2.Inlines.Add(new Run("Montagne"));   
                    PeupleInfoT2.Inlines.Add(new LineBreak());
                    PeupleInfoT2.Inlines.Add(new Bold(new Run("+1 : ")));
                    PeupleInfoT2.Inlines.Add(new Run("Désert, Forêt"));   
                    PeupleInfoT2.Inlines.Add(new LineBreak());
                    PeupleInfoT2.Inlines.Add(new Bold(new Run("+2 : ")));
                    PeupleInfoT2.Inlines.Add(new Run("Plaine"));

                    PeupleInfoT3.Inlines.Add(new Run("Les Gaulois ont la capacité de se déplacer 2 fois sur la plaine"));
                    break;
            }
        }

        private void TuilePeuple_MouseLeave(object sender, MouseEventArgs e)
        {
            PeupleInfoNom.Content = "";
            //masquage des titres
            PeupleInfoL1.Visibility = System.Windows.Visibility.Collapsed;
            PeupleInfoL2.Visibility = System.Windows.Visibility.Collapsed;
            PeupleInfoL3.Visibility = System.Windows.Visibility.Collapsed;
            PeupleInfoT1.Inlines.Clear();
            PeupleInfoT2.Inlines.Clear();
            PeupleInfoT3.Inlines.Clear();
        }
    }
}
