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
using Wrapper;

namespace WPF_Test
{
    /// <summary>
    /// Logique d'interaction pour Carte.xaml
    /// </summary>
    public partial class Carte : Page
    {
        //Définition des couleurs : Nain,Viking,Gaulois
        Dictionary<String,SolidColorBrush> couleurPeuple;
        //Constante pour la transparence des cases non possibles
        const double OPACITE_NON_POSSIBLE = 0.85;

        public unsafe Carte()
        {
            InitializeComponent();

            // On efface l'historique des pages pour le bouton de retour
            MainWindow w = Application.Current.MainWindow as MainWindow;
            w.clearHistory();
            w.MouseLeftButtonDown += new MouseButtonEventHandler(window_MouseLeftButtonDown);

            //Définition de la couleur de chaque peuple
            couleurPeuple = new Dictionary<string, SolidColorBrush>();
            couleurPeuple.Add("Nains", Brushes.Red);
            couleurPeuple.Add("Gaulois", Brushes.Yellow);
            couleurPeuple.Add("Vikings", Brushes.Orange);
            
        }

        /// <summary>
        /// Constructeur de la carte :
        ///   - création de la grille des cases 
        ///   - création des emplacements pour les unités
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e){
            int tailleCarte = Code.Carte.getTaille();

            //On crée les lignes et colonnes pour les 3 grilles          
            for (int y = 0; y < tailleCarte; y++)
            {
                //Grille de la carte (avec cases colorées)
                mapGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(48, GridUnitType.Pixel) });
                //Grille des transparences
                transGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(48, GridUnitType.Pixel) });
                //Grille des unités 3x plus grande pour permettre d'en mettre plusieurs
                uniteGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(16, GridUnitType.Pixel) } );
                uniteGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(16, GridUnitType.Pixel) });
                uniteGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(16, GridUnitType.Pixel) });
            }
            for (int x = 0; x < tailleCarte; x++)
            {
                mapGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(48, GridUnitType.Pixel) });
                transGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(48, GridUnitType.Pixel) });
                uniteGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(16, GridUnitType.Pixel) });
                uniteGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(16, GridUnitType.Pixel) });
                uniteGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(16, GridUnitType.Pixel) });
            }

            //Remplissage de la carte
            remplirCarte(tailleCarte);

            //Affichage des unités
            miseAJourUnites();

            //Initialisation des Tags
            miseAJourTags();
            //Ajout des tags liant les noms des joueurs et des peuples (ne vont pas évoluer)
            LabelJoueur1.Tag = Jeu.INSTANCE.J1.Nom;
            LabelJoueur2.Tag = Jeu.INSTANCE.J2.Nom;
            PeupleJoueur1.Tag = Jeu.INSTANCE.J1.Peuple.ToString();
            PeupleJoueur2.Tag = Jeu.INSTANCE.J2.Peuple.ToString();
        }

        /// <summary>
        /// Ajoute les cases physiques liées aux cases logiques sur la carte physique
        /// </summary>
        /// <param name="tailleCarte">La taille de la carte</param>
        private void remplirCarte(int tailleCarte)
        {
            for (int x = 0; x < tailleCarte; x++)
            {
                for (int y = 0; y < tailleCarte; y++)
                {
                    //// ON CREE UNE CASE DANS LA GRILLE DES CASES
                    Coord c = new Coord(x, y);
                    var element = creerTuile(c);
                    mapGrid.Children.Add(element);
                }
            }
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

             tuile.Stroke = Brushes.WhiteSmoke;
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
            //on nettoie avant au cas où (notamment pour l'ellipse de selection au premier tour)
            uniteGrid.Children.Clear();
            int i = 0;

            //On crée une liste de Joueurs pour ne ps écrire 2 fois la même chose
            List<Joueur> listeJ = new List<Joueur>();
            listeJ.Add(Jeu.INSTANCE.J1);
            listeJ.Add(Jeu.INSTANCE.J2);

            foreach (Joueur j in listeJ)
            {
                i = 0;
                foreach (Unite u in j.Peuple.Unites)
                {
                    int row = (u.Position.X) * 3;
                    int col = (u.Position.Y) * 3;

                    //création de l'ellipse
                    var e = new Ellipse();

                    //Sélection de la couleur
                    if (u.Peuple != Jeu.INSTANCE.JActif.Peuple) // Si elle n'est pas au joueur actif
                    {
                        e.Fill = Brushes.DarkGray;
                    }
                    else if (u.PointDeplacement == 0) // Si elle n'a plus de vie
                    {
                        e.Fill = Brushes.Black;
                    }
                    else // Sinon
                    {
                        e.Fill = couleurPeuple[Jeu.INSTANCE.JActif.Peuple.ToString()];
                    }
                    e.Width = 11;
                    e.Height = 11;

                    //lien entre l'ellipse et l'uniteLogique
                    e.Tag = u;

                    //Ajout de l'évènement lors du clic
                    e.MouseLeftButtonDown += new MouseButtonEventHandler(unite_MouseLeftButtonDown);

                    //Positionnement de l'unité
                    Grid.SetColumn(e, row + (i % 3));
                    Grid.SetRow(e, col + (i / 3));
                    uniteGrid.Children.Add(e);

                    i++;
                }
            }

            //Placer l'ellipse
            uniteGrid.Children.Add(UniteSelectionnee);
        }

        /// <summary>
        /// Rafraichissement de la carte
        ///     - déselectionner unité & case
        ///     - dégriser les cases pas possibles
        /// </summary>
        private void miseAJourUnites()
        {
            //Déselectionner l'unité
            UniteSelectionnee.Tag = null;
            UniteSelectionnee.Visibility = System.Windows.Visibility.Collapsed;

            //dé-opacifier la carte
            transGrid.Children.Clear();
            placerUnites();
        }

        /// <summary>
        /// Rafraichissement des Tags à la fin de Tour
        ///     - redefinir les tags pour la mise à jour
        ///     - uniquement ceux qui necessitent une mise à jour
        /// </summary>
        private void miseAJourTags()
        {
            //Coloration des cases d'infos des peuples
            if (Jeu.INSTANCE.JActif == Jeu.INSTANCE.J1)
            {
                InfosJoueur1.Background = couleurPeuple[Jeu.INSTANCE.J1.Peuple.ToString()];
                InfosJoueur2.Background = new BrushConverter().ConvertFromString("#FFF0F0F0") as System.Windows.Media.Brush;
            }
            else
            {
                InfosJoueur1.Background = new BrushConverter().ConvertFromString("#FFF0F0F0") as System.Windows.Media.Brush;
                InfosJoueur2.Background = couleurPeuple[Jeu.INSTANCE.J2.Peuple.ToString()];

            }

            //Ajout des tags liant le tour et le nom du joueur en cours
            LabelJoueur.Tag = Jeu.INSTANCE.JActif.Nom;
            LabelTourEnCours.Tag = Jeu.INSTANCE.NbToursActuels;
            LabelTotalTour.Tag = Jeu.INSTANCE.NbTours;


            // On met à jour le score
            Jeu.INSTANCE.updateScore();

            //Ajout des tags liant le score aux joueurs
            Score1.Tag = Jeu.INSTANCE.J1.Score;
            Score2.Tag = Jeu.INSTANCE.J2.Score;
        }


        /// <summary>
        /// Réaction à un clic sur une case de la carte
        ///     - une unité doit être sélectionnée et le déplacement possible
        ///     - mise à jour du rectangle sélectionné
        ///     - si déplacement possible, l'unité est déplacée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tuile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var rectangle = sender as Rectangle;
            //var caseLogique = rectangle.Tag as iCase;

            //Si une unité est selectionnée
            if (UniteSelectionnee.Visibility == System.Windows.Visibility.Visible) { 
                
                //Si le déplacement/attaque est possible (case non grisée) et que ce n'est pas la case où se trouve l'unité
                //Géré par le overlay transparent qui "masque" les autres cases du clic, ah ah ah

                //Récupération des coordonnées
                int col = Grid.GetColumn(rectangle);
                int row = Grid.GetRow(rectangle);

                //DEPLACEMENT/ATTAQUE
                (UniteSelectionnee.Tag as Unite).deplacer(new Coord(col,row));
                miseAJourUnites();

                //L'évènement a été traité, il ne faut pas appeler le handler de la fenêtre
                e.Handled = true;                
            }
        }

        /// <summary>
        /// Réaction à un clic sur une unité de la carte
        ///     - vérifier que c'est une unité du joueur en cours
        ///     - vérifier que l'unité a encore des points de déplacement
        ///     - mise à jour de l'ellipse sélectionnée
        ///     - afficher les suggestions de parcours
        ///     - afficher les infos de l'unité dans le panel à droite -> binding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void unite_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var ellipse = sender as Ellipse;
            var uniteLogique = ellipse.Tag as Unite;

            //Vérification qu'il lui reste des pts de déplacement et que l'unité appartient bien au joueur en cours 
            bool clickable = false;
            if(uniteLogique.PointDeplacement > 0)
            {
                if(uniteLogique.Peuple == Jeu.INSTANCE.JActif.Peuple)
                {
                    clickable = true;
                }
            }

            //On regarde si on reclique sur la même case, dans ce cas, on ne fait rien
            if (UniteSelectionnee.Tag == uniteLogique)
            {
                e.Handled = true;
            }
            else if (clickable)
            {
                //Si c'est une nouvelle unité, on nettoie la grille des transparences
                transGrid.Children.Clear();

                //Mise à jour de l'ellipse sélectionnée
                int col = Grid.GetColumn(ellipse);
                int row = Grid.GetRow(ellipse);
                Grid.SetColumn(UniteSelectionnee, col);
                Grid.SetRow(UniteSelectionnee, row);
                UniteSelectionnee.Tag = uniteLogique;
                UniteSelectionnee.Visibility = System.Windows.Visibility.Visible;

                //Mise en surbrillance des cases où le déplacement/attaque est possible, si pas déjà fait
                int[] cases = Jeu.INSTANCE.suggestionDeplacement(uniteLogique);
                int tailleCarte = Code.Carte.getTaille();
                for (int i = 0; i < tailleCarte; i++)
                {
                    for (int j = 0; j < tailleCarte; j++)
                    {
                        Coord c = new Coord(i, j);
                        if (cases[c.getIndiceTab1Dimension()] == 3)
                        {
                            Rectangle r = new Rectangle();
                            r.Opacity = OPACITE_NON_POSSIBLE;
                            r.Fill = Brushes.White;
                            Grid.SetColumn(r, i);
                            Grid.SetRow(r, j);
                            transGrid.Children.Add(r);
                        }
                    }
                }
                //L'évènement a été traité, il ne faut pas appeler le handler de la fenêtre
                e.Handled = true;
            }
        }


        /// <summary>
        ///  Réaction à un clic sur la fenêtre
        ///     - déselectionner unité
        ///     - déselectionner case
        ///     - dégriser les cases pas possibles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            miseAJourUnites();
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
            quitter_handler();
        }

        private void FinTour_Click(object sender, RoutedEventArgs e)
        {
            if (Jeu.INSTANCE.finTour()) {
                miseAJourUnites();
                miseAJourTags();
            }
            else
            {
                finPartie();
            }
        }

        private void quitter_handler()
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

            if (quitter)
            {
                MainWindow parent = (Application.Current.MainWindow as MainWindow);
                parent.goBack();
            }
        }

        private void finPartie()
        {
            MessageBox.Show("Fini !");
        }
    }
}
