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
            if (App.phase == 1)
            {
                NavigationService.Navigate(new BakingGame());

            }
            else if (App.phase == 2)
            {
                NavigationService.Navigate(new DecoratingGame());

            }
        }
    }
}
