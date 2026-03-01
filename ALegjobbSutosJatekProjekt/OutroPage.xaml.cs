using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace ALegjobbSutosJatekProjekt
{
    /// <summary>
    /// Interaction logic for OutroPage.xaml
    /// </summary>
    public partial class OutroPage : Page
    {
        public OutroPage() 
        {

        }
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new IntroPage());
        }


        private void more_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://sutos.jatek-online.hu/");
        }

    }
}
