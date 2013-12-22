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
    /// Logique d'interaction pour Choix_Peuple.xaml
    /// </summary>
    public partial class Choix_Peuple : Page
    {
        public Choix_Peuple()
        {
            InitializeComponent();
            Application.Current.MainWindow.Width = 480;
            Application.Current.MainWindow.Height = 540;
        }

        private void Back_Top_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (MainWindow)Application.Current.MainWindow;
            parent.goBack();
        }
    }
}
