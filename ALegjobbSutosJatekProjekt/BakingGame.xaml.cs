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

        Image eredetihely = null;
        Image huzigalt = null;
        Point offset;


        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            eredetihely = sender as Image;

            // Klón létrehozása
            huzigalt = new Image();
            huzigalt.Source = eredetihely.Source;
            huzigalt.Width = eredetihely.Width;
            huzigalt.Height = eredetihely.Height;

            // Pozíció kiszámolás
            Point position = e.GetPosition(Table);
            offset = new Point(huzigalt.Width / 2, huzigalt.Height / 2);

            Canvas.SetLeft(huzigalt, position.X - offset.X);
            Canvas.SetTop(huzigalt, position.Y - offset.Y);

            Table.Children.Add(huzigalt);

            eredetihely.Opacity = 0.3;

            Table.MouseMove += MouseMove;
            Table.MouseLeftButtonUp += MouseUp;
            Table.CaptureMouse();

        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (huzigalt == null) return;

            Point position = e.GetPosition(Table);

            Canvas.SetLeft(huzigalt, position.X - offset.X);
            Canvas.SetTop(huzigalt, position.Y - offset.Y);
        }

        private void MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (huzigalt == null) return;

            Rect draggedRect = new Rect(
                Canvas.GetLeft(huzigalt),
                Canvas.GetTop(huzigalt),
                huzigalt.Width,
                huzigalt.Height);


            GeneralTransform transform = Usualtarget.TransformToVisual(Table);
            Point topLeft = transform.Transform(new Point(0, 0));

            Rect targetRect = new Rect(
                topLeft.X,
                topLeft.Y,
                Usualtarget.ActualWidth,
                Usualtarget.ActualHeight);


            if (draggedRect.IntersectsWith(targetRect))
            {
                MessageBox.Show("Belekerült a tálba! 🎉");
                Table.Children.Remove(huzigalt);
            }
            else
            {
                Table.Children.Remove(huzigalt);
                eredetihely.Opacity = 1;
            }

            huzigalt = null;
            eredetihely = null;

            Table.MouseMove -= MouseMove;
            Table.MouseLeftButtonUp -= MouseUp;
            Table.ReleaseMouseCapture();

        }

    }
}