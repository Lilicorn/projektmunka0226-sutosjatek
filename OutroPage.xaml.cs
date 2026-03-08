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
            InitializeComponent();

            if (CakeState.Top != null)
                top_frame.Source = new BitmapImage(new Uri(CakeState.Top, UriKind.Relative));

            if (CakeState.TopSide != null)
                topside_frame1.Source = new BitmapImage(new Uri(CakeState.TopSide, UriKind.Relative));

            if (CakeState.MidSide != null)
                midside_frame.Source = new BitmapImage(new Uri(CakeState.MidSide, UriKind.Relative));

            if (CakeState.BottomSide != null)
                bottomside_frame.Source = new BitmapImage(new Uri(CakeState.BottomSide, UriKind.Relative));

            if (CakeState.TopFrosting != null)
                topfrosting_frame.Source = new BitmapImage(new Uri(CakeState.TopFrosting, UriKind.Relative));

            if (CakeState.CoveringColor != null)
                coveringcolor_frame.Source = new BitmapImage(new Uri(CakeState.CoveringColor, UriKind.Relative));

            if (CakeState.Stand != null)
                stand_frame.Source = new BitmapImage(new Uri(CakeState.Stand, UriKind.Relative));
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
