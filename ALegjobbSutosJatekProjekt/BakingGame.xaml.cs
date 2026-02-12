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
        public int StepIndex { get; set; }
        public string TargetTag { get; set; }

        public ingredient(string tag, string src, int ind, string target)
        {
            TagName = tag;
            ImageSource = src;
            StepIndex = ind;
            TargetTag = target;

        }
    }



    public partial class BakingGame : Page
        
    {
        public List<Image> ingredientSlots;
        public BakingGame()
        {
            InitializeComponent();
            ingredientSlots = new List<Image> { ingredient0, ingredient1, ingredient2, ingredient3, ingredient4 };
            //ezt valamiert csak ide endgedi sziaaa
            Section1();
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

            var leccimukodj = hozzávalók.FirstOrDefault(h => h.TagName == (string)huzigalt.Tag);
            //if (leccimukodj.TargetTag==(string)Usualtarget.Tag
            
            
            
            if (leccimukodj != null && leccimukodj.StepIndex == step && draggedRect.IntersectsWith(targetRect))
            {
                MessageBox.Show("Belekerült a tálba! 🎉");
                background.Children.Remove(huzigalt);
                step++;
            }
            else if(step != hozzávalók.Count)
            {
                background.Children.Remove(huzigalt);
                eredetihely.Opacity = 1;
            }

            if (step == hozzávalók.Count)
            {
                section++;
                switch (section)
                {
                    default: Section1(); break;

                    case 1: Section1(); break;
                    case 2: Section2(); break;

                    case 3: ending(); break;
                }
            }

            huzigalt = null;
            eredetihely = null;

            background.MouseMove -= MouseMove;
            background.MouseLeftButtonUp -= MouseUp;
            background.ReleaseMouseCapture();

        }

        //mi dolgaink
        List<ingredient> hozzávalók = new List<ingredient>();
        int step = 0;
        int section = 1;

        public void Section1()
        {
            hozzávalók.Clear();
            step = 0;
            foreach (var i in ingredientSlots) { i.Opacity = 1; i.Source = null; i.Tag = ""; }



            Usualtarget.Tag = "bowl";
            Usualtarget.Source = new BitmapImage(new Uri("/bowl1.jpg", UriKind.Relative));
            
            hozzávalók.Add(new ingredient("egg", "/egg.jpg", 0, "bowl"));
            hozzávalók.Add(new ingredient("milk", "/milk.jpg", 1, "bowl"));
            hozzávalók.Add(new ingredient("???", "/mxstery.gif", 2, "bowl"));
            for(int i =0; i<hozzávalók.Count; i++)
            {
                ingredientSlots[i].Source = new BitmapImage(new Uri(hozzávalók[i].ImageSource, UriKind.Relative)); 
                ingredientSlots[i].Tag = hozzávalók[i].TagName;
            }


        }

        public void Section2()
        {
            hozzávalók.Clear();
            step = 0;
            foreach(var i in ingredientSlots) { i.Opacity = 1; i.Source = null; i.Tag = ""; }


            Usualtarget.Tag = "bowl";
            Usualtarget.Source = new BitmapImage(new Uri("/bowl1.jpg", UriKind.Relative));

            hozzávalók.Add(new ingredient("egg", "/eggcarton.jpg", 0, "bowl"));
            hozzávalók.Add(new ingredient("milk", "/woodenspoon.jpg", 1, "bowl"));
            hozzávalók.Add(new ingredient("sppon", "/woodenspoon.jpg", 2, "bowl"));
            hozzávalók.Add(new ingredient("balls", "/woodenspoon.jpg", 3, "bowl"));
            hozzávalók.Add(new ingredient("???", "/mxstery.gif", 4, "bowl"));
            for (int i = 0; i < hozzávalók.Count; i++)
            {
                ingredientSlots[i].Source = new BitmapImage(new Uri(hozzávalók[i].ImageSource, UriKind.Relative));
                ingredientSlots[i].Tag = hozzávalók[i].TagName;
            }
        }

        public void ending()
        {
            MessageBox.Show("Gratulálok, elkészült a süti! 🎉🍪");
            NavigationService.Navigate(new OutroPage());

        }

    }
}