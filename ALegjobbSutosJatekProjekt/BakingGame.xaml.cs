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
            offset = new Point(draggedImage.Width / 2, draggedImage.Height / 2);

            Canvas.SetLeft(draggedImage, position.X - offset.X);
            Canvas.SetTop(draggedImage, position.Y - offset.Y);

            Table.Children.Add(draggedImage);

            originalImage.Opacity = 0.3;

            Table.MouseMove += Table_MouseMove;
            Table.MouseLeftButtonUp += Table_MouseUp;
        }

    }
}