using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Interaction logic for BakingGame.xaml
    /// </summary>
    /// 

    public class hozzávaló
    {
        public string nev { get; set; }
        public string imagepath { get; set; }
        public Point StartPosition { get; set; }
        public string targettag { get; set; }
    }



    public partial class BakingGame : Page
    {
        public BakingGame()
        {
            InitializeComponent();
        }


        Image huzigalando = null;
        bool huzigalszevalamit = false;
        Point pozisön;

        List<hozzávaló> ingredients = new List<hozzávaló>();


        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            huzigalando = sender as Image;
            huzigalszevalamit = true;
            pozisön = e.GetPosition(GameCanvas);
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {

            if (!huzigalszevalamit && huzigalando == null)
            {
                return;
            }
                var position = e.GetPosition(GameCanvas);
                Canvas.SetLeft(huzigalando, position.X - (huzigalando.ActualWidth / 2));
                Canvas.SetTop(huzigalando, position.Y - (huzigalando.ActualHeight / 2));
           

        }

        private bool IsInTargetArea(Point current, Point target)
        {
            double tolerance = 50; // +/- 20 pixel pontosság
            return Math.Abs(current.X - target.X) < tolerance &&
                   Math.Abs(current.Y - target.Y) < tolerance;
        }
    }
}
