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
            
            
            int tailleCarte = 5; //TODO MonteurPartie.INSTANCE.Difficulte;
            int tailleTuile = 50; // Fixé dans le poly

            // on initialise la Grid (mapGrid défini dans le xaml) à partir de la map du modèle (engine)
            WrapperAlgo wrap = new WrapperAlgo();
            int[] map = { 1,4,3,2,0,0,4,0,2,2,3,4,1,3,1,3,0,2,0,1,1,2,4,3,4 };
             
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
                    var element = creerTuile(c, map[x * tailleCarte + y]);
                    mapGrid.Children.Add(element);
                }
            }
            //updateUnitUI();
        }

        private Grid creerTuile(Coord c, int type)
        {
            var tuile = new Grid();
            //Ajout des colonnes et lignes
            tuile.ColumnDefinitions.Add(new ColumnDefinition());
            tuile.ColumnDefinitions.Add(new ColumnDefinition());
            tuile.ColumnDefinitions.Add(new ColumnDefinition());
            tuile.RowDefinitions.Add(new RowDefinition());
            tuile.RowDefinitions.Add(new RowDefinition());
            tuile.RowDefinitions.Add(new RowDefinition());
            //TODO ajouter unités
            // à changer comme le cours, référence vers la case
            switch (type)
            {
                case Constants.DESERT:
                    tuile.Background = Brushes.BlanchedAlmond;
                    break;

                case Constants.EAU:
                    tuile.Background = Brushes.DarkBlue;
                    var e = new Ellipse();
                    e.Fill = Brushes.White;
                    e.Width = 15;
                    e.Height = 15;
                    Grid.SetColumn(e, 0);
                    Grid.SetRow(e, 0);
                    e.MouseLeftButtonDown += new MouseButtonEventHandler(unite_MouseLeftButtonDown);
                    tuile.Children.Add(e);
                    break;

                case Constants.FORET:
                    tuile.Background = Brushes.DarkGreen;
                    break;

                case Constants.MONTAGNE:
                    tuile.Background = Brushes.Maroon;
                    break;

                case Constants.PLAINE:
                    tuile.Background = Brushes.Green;
                    break;
            }
            // mise à jour des attributs (column et Row) référencant la position dans la grille à rectangle
            Grid.SetColumn(tuile, c.getX());
            Grid.SetRow(tuile, c.getY());

            tuile.Margin = new System.Windows.Thickness(1);
            // TODO rectangle.Tag = 
            // enregistrement d'un écouteur d'evt sur le rectangle : 
            // source = rectangle / evt = MouseLeftButtonDown / délégué = rectangle_MouseLeftButtonDown
            tuile.MouseLeftButtonDown += new MouseButtonEventHandler(case_MouseLeftButtonDown);
            return tuile;
        }

        private void case_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void placerUnites()
        {
            int tailleCarte = 5;//TODO git MonteurPartie.INSTANCE.Difficulte;
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
                    var element = creerTuile(c, map[x * tailleCarte + y]);
                    mapGrid.Children.Add(element);
                }
            }
        }
    }
}
