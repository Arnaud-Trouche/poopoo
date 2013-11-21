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
        WrapperAlgo wrap;
        int taille;
        String s;
        unsafe public MainWindow()
        {
            taille = 5;
            InitializeComponent();
            //Appel à la DLL et mise à jour Label
            wrap = new WrapperAlgo();
            int* tab = wrap.creationCarte(taille);
            for (int i = 0; i < taille; i++)
            {
                s = "";
                for (int j = 0; j < taille; j++)
                {
                    s += tab[i * taille + j];
                }
                System.Console.WriteLine(s);
            }
        }
    }
}
