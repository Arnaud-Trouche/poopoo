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
        
            int taille = 15; //TODO MonteurPartie.INSTANCE.Difficulte;
            // on initialise la Grid (mapGrid défini dans le xaml) à partir de la map du modèle (engine)
            WrapperAlgo wrap = new WrapperAlgo();
            int* map = wrap.creationCarte(taille);
            for (int y = 0; y < taille; y++)
            {
                mapGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(40, GridUnitType.Pixel) });
            }
            for (int x = 0; x < taille; x++)
            {
                mapGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40, GridUnitType.Pixel) });
                for (int y = 0; y < taille; y++)
                {
                    // dans chaque case de la grille on ajoute une tuile (logique) matérialisée par un rectangle (physique)
                    // le rectangle possède une référence sur sa tuile
                    Coord c = new Coord(x, y);
                    var element = createRectangle(c, map[x*taille+y] );
                    mapGrid.Children.Add(element);
                }
            }
            //updateUnitUI();
        }

        private Rectangle createRectangle(Coord c, int type)
        {
            var rectangle = new Rectangle();
            switch (type)
            {
                case Constants.DESERT:
                    rectangle.Fill = Brushes.BlanchedAlmond;
                    break;

                case Constants.EAU:
                    rectangle.Fill = Brushes.DarkBlue;
                    break;

                case Constants.FORET:
                    rectangle.Fill = Brushes.DarkGreen;
                    break;

                case Constants.MONTAGNE:
                    rectangle.Fill = Brushes.Maroon;
                    break;

                case Constants.PLAINE:
                    rectangle.Fill = Brushes.Green;
                    break;
            }
            // mise à jour des attributs (column et Row) référencant la position dans la grille à rectangle
            Grid.SetColumn(rectangle, c.getX());
            Grid.SetRow(rectangle, c.getY());

            rectangle.Stroke = Brushes.LightGray;
            rectangle.StrokeThickness = 1;
            // enregistrement d'un écouteur d'evt sur le rectangle : 
            // source = rectangle / evt = MouseLeftButtonDown / délégué = rectangle_MouseLeftButtonDown
            rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(rectangle_MouseLeftButtonDown);
            return rectangle;
        }

        private void rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //TODO réaction à un clic
            //throw new NotImplementedException();
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
