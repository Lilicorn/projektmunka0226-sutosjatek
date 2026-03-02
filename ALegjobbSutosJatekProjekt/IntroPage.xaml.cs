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
using System.Diagnostics;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Threading;

namespace ALegjobbSutosJatekProjekt
{
    /// <summary>
    /// Interaction logic for IntroPage.xaml
    /// </summary>
    public partial class IntroPage : Page
    {
        public IntroPage()
        {
            InitializeComponent();

            DispatcherTimer tájmör = new DispatcherTimer();
            tájmör.Interval = TimeSpan.FromSeconds(0.75);
            bool stage = false;

            tájmör.Tick += (s, e) =>
            {
                if (!stage)
                {
                    introspriteanim.ScaleY = 1.03;
                }
                else
                {
                    introspriteanim.ScaleY = 1.01;

                }
                stage = !stage;
            };
            tájmör.Start();

        }

        private void Play_Click(object sender, RoutedEventArgs e) { 
               NavigationService.Navigate(new TransitionPage());
               //NavigationService.Navigate(new DecoratingGame());
        }


        private void more_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://sutos.jatek-online.hu/");
        }

        
    }
}
