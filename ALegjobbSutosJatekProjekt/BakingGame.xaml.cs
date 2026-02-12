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

    public class ingredient
    {
        public string TagName { get; set; }
        public string ImageSource { get; set; }
        public int stepIndex { get; set; }
        public string TargetTag { get; set; }

        public ingredient(string tag, string src, int ind, string target)
        {
            TagName = tag;
            ImageSource = src;
            stepIndex = ind;
            TargetTag = target;
        }
    }



    public partial class BakingGame : Page
    {
        public BakingGame()
        {
            InitializeComponent();
        }

        //wpf logikak uyg nagjabol eeehehehehehhhehhhheeee
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
            huzigalt.Tag = eredetihely.Tag;

            // Pozíció kiszámolás
            Point position = e.GetPosition(background);
            offset = new Point(huzigalt.Width / 2, huzigalt.Height / 2);

            Canvas.SetLeft(huzigalt, position.X - offset.X);
            Canvas.SetTop(huzigalt, position.Y - offset.Y);

            background.Children.Add(huzigalt);

            eredetihely.Opacity = 0.3;

            background.MouseMove += MouseMove;
            background.MouseLeftButtonUp += MouseUp;
            background.CaptureMouse();

        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (huzigalt == null) return;

            Point position = e.GetPosition(background);

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


            GeneralTransform transform = Usualtarget.TransformToVisual(background);
            Point topLeft = transform.Transform(new Point(0, 0));

            Rect targetRect = new Rect(
                topLeft.X,
                topLeft.Y,
                Usualtarget.ActualWidth,
                Usualtarget.ActualHeight);

            //var leccimukodj = hozzávalók.FirstOrDefault(h => h.TagName == (string)huzigalt.Tag);
            //if (leccimukodj.TargetTag==(string)Usualtarget.Tag
            if (draggedRect.IntersectsWith(targetRect))
            {
                MessageBox.Show("Belekerült a tálba! 🎉");
                background.Children.Remove(huzigalt);
            }
            else
            {
                background.Children.Remove(huzigalt);
                eredetihely.Opacity = 1;
            }

            huzigalt = null;
            eredetihely = null;

            background.MouseMove -= MouseMove;
            background.MouseLeftButtonUp -= MouseUp;
            background.ReleaseMouseCapture();

        }

        //mi dolgaink
        List<ingredient> hozzávalók = new List<ingredient>();
        public void Section1()
        {
            hozzávalók.Clear();

        }

    }
}