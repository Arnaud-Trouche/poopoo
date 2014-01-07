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
        public unsafe Carte()
        {
            InitializeComponent();

            // On efface l'historique des pages pour le bouton de retour
            (Application.Current.MainWindow as MainWindow).clearHistory();
            
            
            int tailleCarte = Code.Carte.getTaille();
            int tailleTuile = 50; // Fixé dans le poly

            // on initialise la Grid (mapGrid défini dans le xaml) à partir de la map du modèle (engine)             
            for (int y = 0; y < tailleCarte; y++)
            {
                mapGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(tailleTuile, GridUnitType.Pixel) });
            }
            for (int x = 0; x < tailleCarte; x++)
            {
                mapGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(tailleTuile, GridUnitType.Pixel) });
                for (int y = 0; y < tailleCarte; y++)
                {
                    // dans chaque case de la grille on ajoute une tuile (logique) matérialisée par un rectangle (physique)
                    // le rectangle possède une référence sur sa tuile
                    Coord c = new Coord(x, y);
                    var element = creerTuile(c);
                    mapGrid.Children.Add(element);
                }
            }

            creerGridUnites();
            //updateUnitUI();
        }

        private void creerGridUnites()
        {
            int tailleCarte = Code.Carte.getTaille();
            for (int y = 0; y < tailleCarte; y++)
            {
                uniteGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Pixel) });
            }
            for (int x = 0; x < tailleCarte; x++)
            {
                uniteGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Pixel) });
                for (int y = 0; y < tailleCarte; y++)
                {
                    // dans chaque case de la grille on ajoute une tuile (logique) matérialisée par un rectangle (physique)
                    // le rectangle possède une référence sur sa tuile
                    var stack = new StackPanel();
                    stack.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    Grid.SetColumn(stack, y); // oula oula
                    Grid.SetRow(stack, x);
                    mapGrid.Children.Add(stack);
                }
            }
        }
            
        private Grid creerTuile(Coord c)
        {
            var tuile = new Grid();
            var caseLogique = FabriqueCase.INSTANCE.obtenirCase(c);

            if(caseLogique is Desert)
                 tuile.Background = Brushes.BlanchedAlmond;
                    
             if(caseLogique is Eau)
                    tuile.Background = Brushes.DarkBlue;

             if(caseLogique is Foret)
                    tuile.Background = Brushes.DarkGreen;

             if(caseLogique is Montagne)
                    tuile.Background = Brushes.Maroon;

             if(caseLogique is Plaine)
                    tuile.Background = Brushes.Green;


            // mise à jour des attributs (column et Row) référencant la position dans la grille
            Grid.SetColumn(tuile, c.getX());
            Grid.SetRow(tuile, c.getY());

            tuile.Margin = new System.Windows.Thickness(1);
            tuile.Tag = caseLogique;
            // enregistrement d'un écouteur d'evt sur la tuile : 
            // source = rectangle / evt = MouseLeftButtonDown / délégué = rectangle_MouseLeftButtonDown
            tuile.MouseLeftButtonDown += new MouseButtonEventHandler(tuile_MouseLeftButtonDown);
            return tuile;
        }

        private void placerUnites()
        {
            /*
            for (int j = 0; j < 2; j++)
            {
                var joueur = MonteurPartie.INSTANCE.Joueurs[]
            }


                    var e = new Ellipse();
                    e.Fill = Brushes.White;
                    e.Width = 15;
                    e.Height = 15;
                    Grid.SetColumn(e, 0);
                    Grid.SetRow(e, 0);
                    e.MouseLeftButtonDown += new MouseButtonEventHandler(unite_MouseLeftButtonDown);
                    tuile.Children.Add(e);
        */}
        private void tuile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //TODO réaction à un clic
            //throw new NotImplementedException();
            Grid r = sender as Grid;
            r.Background = Brushes.Black;
        }

        private void unite_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse ellipse = sender as Ellipse;
            ellipse.Fill = Brushes.Red;

            e.Handled = true;
        }

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
