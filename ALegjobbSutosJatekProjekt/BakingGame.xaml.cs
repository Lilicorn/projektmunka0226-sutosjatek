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
        public Image kinezet { get; set; }
        public Point StartPosition { get; set; }
        public Point TargetPosition { get; set; }
    }



    public partial class BakingGame : Page
    {
        public BakingGame()
        {
            InitializeComponent();
            preparation();
            section1();
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
            var ing = ingredients.FirstOrDefault(i => i.kinezet == huzigalando);
            var pos = e.GetPosition(GameCanvas);

            if (!huzigalszevalamit && huzigalando == null)
            {
                return;
            }
            else if (IsInTargetArea(pos, ing.TargetPosition))
            {
                // Jó helyre raktuk!
                Canvas.SetLeft(huzigalando, ing.TargetPosition.X);
                Canvas.SetTop(huzigalando, ing.TargetPosition.Y);
                MessageBox.Show($"{ing.nev} jó helyen!");
                huzigalszevalamit = false;
                huzigalando = null;
            }
            else
            {
                var position = e.GetPosition(GameCanvas);
                Canvas.SetLeft(huzigalando, position.X - (huzigalando.ActualWidth / 2));
                Canvas.SetTop(huzigalando, position.Y - (huzigalando.ActualHeight / 2));
            }

        }


        private bool IsInTargetArea(Point current, Point target)
        {
            double tolerance = 50; // +/- 20 pixel pontosság
            return Math.Abs(current.X - target.X) < tolerance &&
                   Math.Abs(current.Y - target.Y) < tolerance;
        }

        public void preparation()
        {
            // Hozzávalók definiálása
            ingredients.Add(new hozzávaló
            {
                nev = "egg",
                kinezet = Egg,
                StartPosition = new Point(50, 50),
                TargetPosition = new Point(100, 300)
            });

            ingredients.Add(new hozzávaló
            {
                nev = "milk",
                kinezet = Milk,
                StartPosition = new Point(150, 50),
                TargetPosition = new Point(200, 300)
            });

            // kezdő pozíciók beállítása
            foreach (var ing in ingredients)
            {
                Canvas.SetLeft(ing.kinezet, ing.StartPosition.X);
                Canvas.SetTop(ing.kinezet, ing.StartPosition.Y);

                // hozzárendeljük az egér eseményeket
                ing.kinezet.MouseDown += MouseDown;
                ing.kinezet.MouseMove += MouseMove;
            }
        }

        public void section1()
        {
            List<hozzávaló> section1 = new List<hozzávaló>();
            section1.Add(new hozzávaló
            {
                nev = "egg",
                kinezet = Egg,
                StartPosition = new Point(50, 50),
                TargetPosition = new Point(100, 300)
            });

            section1.Add(new hozzávaló
            {
                nev = "milk",
                kinezet = Milk,
                StartPosition = new Point(150, 50),
                TargetPosition = new Point(200, 300)
            });
        }
    }
}
