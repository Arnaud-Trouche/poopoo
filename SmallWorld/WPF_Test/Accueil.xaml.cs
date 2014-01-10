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
        public Accueil()
        {            
            InitializeComponent();
            MainWindow parent = (Application.Current.MainWindow as MainWindow);
            //parent.Closing += parent_Closing;
        }

        private void Nouvelle_Partie_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (Application.Current.MainWindow as MainWindow);
            parent.changePage("Difficulte.xaml"); 
        }

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
                //TODO faire la restauration !
                MessageBox.Show("Pas chargé :p");
            }
        }

        private void Quitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void parent_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
