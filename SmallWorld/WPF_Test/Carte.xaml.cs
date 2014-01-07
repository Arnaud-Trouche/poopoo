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
using Wrapper;

namespace WPF_Test
{
    /// <summary>
    /// Logique d'interaction pour Carte.xaml
    /// </summary>
    public partial class Carte : Page
    {
        //Définition des couleurs : Nain,Viking,Gaulois
        SolidColorBrush[] couleurPeuple = {Brushes.Red, Brushes.Yellow, Brushes.Orange};
        public unsafe Carte()
        {
            InitializeComponent();

            // On efface l'historique des pages pour le bouton de retour
            MainWindow w = Application.Current.MainWindow as MainWindow;
            w.clearHistory();
            w.MouseLeftButtonDown += new MouseButtonEventHandler(window_MouseLeftButtonDown);

            //Définition de la couleur des Joueurs
            LabelJoueur1.Foreground = couleurPeuple[MonteurPartie.INSTANCE.P1];
            LabelJoueur2.Foreground = couleurPeuple[MonteurPartie.INSTANCE.P2];
        }

        /// <summary>
        /// Constructeur de la carte :
        ///   - création de la grille des cases 
        ///   - création des emplacements pour les unités
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e){
            int tailleCarte = Code.Carte.getTaille();

            // on initialise la Grid (mapGrid défini dans le xaml) à partir de la map du modèle (engine)             
            for (int y = 0; y < tailleCarte; y++)
            {
                mapGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(48, GridUnitType.Pixel) });
                //grille des unités 3x plus grande pour permettre d'en mettre plusieurs
                uniteGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(16, GridUnitType.Pixel) } );
                uniteGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(16, GridUnitType.Pixel) });
                uniteGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(16, GridUnitType.Pixel) });
            }
            for (int x = 0; x < tailleCarte; x++)
            {
                mapGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(48, GridUnitType.Pixel) });
                uniteGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(16, GridUnitType.Pixel) });
                uniteGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(16, GridUnitType.Pixel) });
                uniteGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(16, GridUnitType.Pixel) });

                for (int y = 0; y < tailleCarte; y++)
                {
                    //// ON CREE UNE CASE DANS LA GRILLE DES CASES
                    Coord c = new Coord(x, y);
                    var element = creerTuile(c);
                    mapGrid.Children.Add(element);
                }
            }

            //Ajouter les unités
            placerUnites();

            //Ajout des tags liant le score aux joueurs
            Score1.Tag = MonteurPartie.INSTANCE.Joueur1.Score;
            Score2.Tag = MonteurPartie.INSTANCE.Joueur2.Score;
        }
            
        /// <summary>
        /// Crée un Rectangle physique qui représente la case, avec un tag sur la case logique
        /// </summary>
        /// <param name="c">La coordonée de la case à créer</param>
        /// <returns></returns>
        private Rectangle creerTuile(Coord c)
        {
            var tuile = new Rectangle();
            var caseLogique = FabriqueCase.INSTANCE.obtenirCase(c);

            if (caseLogique is Desert)
                tuile.Fill = Brushes.Bisque;
                    
             if(caseLogique is Eau)
                    tuile.Fill = Brushes.SkyBlue;

             if(caseLogique is Foret)
                    tuile.Fill = Brushes.DarkGreen;

             if (caseLogique is Montagne)
                 tuile.Fill = Brushes.BurlyWood;

             if(caseLogique is Plaine)
                    tuile.Fill = Brushes.Green;

             tuile.Stroke = Brushes.LightGray;
             tuile.StrokeThickness = 1;

            // mise à jour des attributs (column et Row) référencant la position dans la grille
            Grid.SetColumn(tuile, c.X);
            Grid.SetRow(tuile, c.Y);

            //lien avec la case logique
            tuile.Tag = caseLogique;

            // enregistrement d'un écouteur d'evt sur la tuile : 
            tuile.MouseLeftButtonDown += new MouseButtonEventHandler(tuile_MouseLeftButtonDown);
            return tuile;
        }

        /// <summary>
        /// Fonction qui place les unités sur la carte
        /// </summary>
        private void placerUnites()
        {
            int j1 = 0;
            int j2 = 0;

            //Placer les unités du Joueur 1
            foreach (Unite unite in MonteurPartie.INSTANCE.Joueur1.Peuple.Unites) {
                int row = (unite.Position.X)*3;
                int col = (unite.Position.Y)*3;

                //création de l'ellipse
                var e = new Ellipse();
                e.Fill = couleurPeuple[MonteurPartie.INSTANCE.P1];
                e.Width = 11;                
                e.Height = 11;

                //lien entre l'ellipse et l'uniteLogique
                e.Tag = unite;

                //Ajout de l'évènement lors du clic
                e.MouseLeftButtonDown += new MouseButtonEventHandler(unite_MouseLeftButtonDown);

                //Positionnement de l'unité
                Grid.SetColumn(e, row+(j1%3));
                Grid.SetRow(e, col+(j1/3));
                uniteGrid.Children.Add(e);

                j1++;
            }

            //Placer les unités du Joueur 2
            foreach (Unite unite in MonteurPartie.INSTANCE.Joueur2.Peuple.Unites)
            {
                int row = (unite.Position.X)*3;
                int col = (unite.Position.Y)*3;

                //création de l'ellipse
                var e = new Ellipse();
                e.Fill = couleurPeuple[MonteurPartie.INSTANCE.P2];
                e.Width = 11;
                e.Height = 11;

                //lien entre l'ellipse et l'uniteLogique
                e.Tag = unite;

                //Ajout de l'évènement lors du clic
                e.MouseLeftButtonDown += new MouseButtonEventHandler(unite_MouseLeftButtonDown);

                //Positionnement de l'unité
                Grid.SetColumn(e, row + (j2 % 3));
                Grid.SetRow(e, col + (j2 / 3));
                uniteGrid.Children.Add(e);

                j2++;
            }
        }

        /// <summary>
        /// Réaction à un clic sur une case de la carte
        ///     - mise à jour du rectangle sélectionné
        ///     - une unité doit être sélectionnée
        ///     - si déplacement possible, l'unité est déplacée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tuile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var rectangle = sender as Rectangle;
            var caseLogique = rectangle.Tag as iCase;

            //Si une unité est selectionnée
            if (UniteSelectionnee.Visibility == System.Windows.Visibility.Visible) {            
                //Mise à jour du rectangle sélectionné
                int col = Grid.GetColumn(rectangle);
                int row = Grid.GetRow(rectangle);
                Grid.SetColumn(CaseSelectionnee, col);
                Grid.SetRow(CaseSelectionnee, row);
                CaseSelectionnee.Tag = caseLogique;
                CaseSelectionnee.Visibility = System.Windows.Visibility.Visible;

                //L'évènement a été traité, il ne faut pas appeler le handler de la fenêtre
                e.Handled = true;
            }
        }

        /// <summary>
        /// Réaction à un clic sur une unité de la carte
        ///     - mise à jour de l'ellipse sélectionnée
        ///     - vérifier que c'est une unité du joueur en cours
        ///     - afficher les suggestions de parcours
        ///     - afficher les infos de l'unité dans le panel à droite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void unite_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var ellipse = sender as Ellipse;
            var uniteLogique = ellipse.Tag as Unite;

            //Mise à jour de l'ellipse sélectionnée
            int col = Grid.GetColumn(ellipse);
            int row = Grid.GetRow(ellipse);
            Grid.SetColumn(UniteSelectionnee, col);
            Grid.SetRow(UniteSelectionnee, row);
            UniteSelectionnee.Tag = uniteLogique;
            UniteSelectionnee.Visibility = System.Windows.Visibility.Visible;

            //L'évènement a été traité, il ne faut pas appeler le handler de la fenêtre
            e.Handled = true;
        }


        /// <summary>
        ///  Réaction à un clic sur la fenêtre
        ///     - déselectionner unité
        ///     - déselectionner case
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Déselectionner l'unité
            UniteSelectionnee.Tag = null;
            UniteSelectionnee.Visibility = System.Windows.Visibility.Collapsed;

            //Déselectionner la case
            CaseSelectionnee.Tag = null;
            CaseSelectionnee.Visibility = System.Windows.Visibility.Collapsed;
        }


        /// <summary>
        /// Clic sur le bouton en haut à gauche
        ///     - Retour au menu
        ///     - Possibilité de sauvegarder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Top_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Si vous quittez le jeu, la progression sera perdue. Voulez-vous sauvegarder ?";
            string caption = "Quitter la partie";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult MsgBoxResult = MessageBox.Show(messageBoxText, caption, button, icon);
            bool sauver = false;
            bool quitter = false;
            switch (MsgBoxResult)
            {
                case MessageBoxResult.Yes:
                    sauver = true;
                    quitter = true;
                    break;
                case MessageBoxResult.No:
                    quitter = true;
                    break;

                case MessageBoxResult.Cancel:
                    break;
            }

            if (sauver)
            {
                // Configure save file dialog box
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "Partie"; // Default file name
                dlg.DefaultExt = ".sw"; // Default file extension
                dlg.Filter = "Parties SmallWolrd (.sw)|*.sw"; // Filter files by extension
                dlg.AddExtension = true;
                dlg.OverwritePrompt = true;
                dlg.Title = "Sauvegarder la partie";

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    //TODO faire la sauvegarde !
                    MessageBox.Show("Pas sauvegardé :p");
                }
                else
                {
                    quitter = false;
                }
            }

            if (quitter) { 
                MainWindow parent = (Application.Current.MainWindow as MainWindow);
                parent.goBack();
            }
        }

        
    }
}
