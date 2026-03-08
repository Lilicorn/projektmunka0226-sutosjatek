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

namespace ALegjobbSutosJatekProjekt
{
    /// <summary>
    /// Interaction logic for TransitionPage.xaml
    /// </summary>
    public partial class TransitionPage : Page
    {
        public TransitionPage()
        {
            InitializeComponent();

        }
        private void help(object sender, RoutedEventArgs e)
        {
            if (App.phase == 1)
            {
                baking.Click += baking_Click;
                decor.Click -= decor_Click;
            }

            else if (App.phase == 2)
            {
                baking.Click -= baking_Click;
                decor.Click += decor_Click;
            }
        }
        private void baking_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BakingGame());
        }
        private void decor_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DecoratingGame());
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var cucc = new ThicknessConverter();
            Thickness leccives = (Thickness)cucc.ConvertFromString($"0 {this.ActualHeight * (1 / 3.0)} 0 0");
            table.Margin = leccives;
        }
    }
}
