﻿using System;
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
            taille = 10;
            s = "";
            InitializeComponent();
            //Appel à la DLL et mise à jour Label

            /*wrap = new WrapperAlgo();
            int* tab = wrap.creationCarte(taille);
            int posa = wrap.positionnerJoueur(tab, taille, taille - 1);
            int posb = wrap.positionnerJoueur(tab, taille, taille*(taille-1));
            */
            /*
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                   if (posa == i * taille + j) {
                       s += "A";
                   }
                   if (posb == i * taille + j)
                   {
                       s +="B";
                   }
                   switch(tab[i * taille + j])
                    {
                       case 0:
                        s += "¤¤       ";
                        break;
                       case 1:
                        s += "~~       ";
                        break;
                       case 2:
                        s += "||||       ";
                        break;
                       case 3:
                        s += "^^       ";
                        break;
                       case 4:
                        s += "__       ";
                        break; 
                    }
                }
                s += "\n\n";
            }
            labello.Content = s;
             * */
        }
    }
}