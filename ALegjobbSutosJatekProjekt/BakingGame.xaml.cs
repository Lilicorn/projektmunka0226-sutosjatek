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
        public int Repeats { get; set; }
        public string  instructionsText { get; set; }

        public ingredient(string tag, string src, int ind, string instructions)
        {
            TagName = tag;
            ImageSource = src;
            StepIndex = ind;
            Repeats = 1;
            instructionsText = instructions;
        }
        public ingredient(string tag, string src, int ind, int repeat, string instructions)
        {
            TagName = tag;
            ImageSource = src;
            StepIndex = ind;
            Repeats = repeat;
            instructionsText = instructions;
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
            
            if (leccimukodj != null && leccimukodj.StepIndex == step && draggedRect.IntersectsWith(targetRect))
            {
                background.Children.Remove(huzigalt);
                if (leccimukodj.Repeats > 1)
                {
                    repeater++;

                    if (repeater >= leccimukodj.Repeats)
                    {
                        step++;
                        ingredientSlots[leccimukodj.StepIndex].Source = new BitmapImage(new Uri($"{hozzávalók[leccimukodj.StepIndex].ImageSource}{repeater}.png", UriKind.Relative));
                        eredetihely.Opacity = 0.3;
                        repeater = 0;
                        Instructions.Content = hozzávalók[step].instructionsText;


                    }
                    else
                    {
                        ingredientSlots[leccimukodj.StepIndex].Source = new BitmapImage(new Uri($"{hozzávalók[leccimukodj.StepIndex].ImageSource}{repeater}.png", UriKind.Relative));
                        eredetihely.Opacity = 1;
                    }
                }
                else
                {
                    eredetihely.Opacity = 0.3;
                    step++;
                    if(step!= hozzávalók.Count)
                    {
                        Instructions.Content = hozzávalók[step].instructionsText;
                    }
                }

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
        int repeater = 0;
        int section = 1;

        

        public void Section1()
        {
            hozzávalók.Clear();
            step = 0;
            repeater = 0;
            foreach (var i in ingredientSlots) { i.Opacity = 1; i.Source = null; i.Tag = ""; }


            Usualtarget.Source = new BitmapImage(new Uri("/assets/bowl1.jpg", UriKind.Relative));
            
            hozzávalók.Add(new ingredient("egg", "/assets/egg.jpg", 0, "add egg"));
            hozzávalók.Add(new ingredient("milk", "/assets/milk.jpg", 1, "add milk"));
            hozzávalók.Add(new ingredient("???", "/assets/mxstery.gif", 2, "add construction gif"));

            for (int i = 0; i < hozzávalók.Count; i++)
            {
                if (hozzávalók[i].Repeats > 1)
                {
                    ingredientSlots[i].Source = new BitmapImage(new Uri($"{hozzávalók[i].ImageSource}{repeater}.png", UriKind.Relative));
                }
                else
                {
                    ingredientSlots[i].Source = new BitmapImage(new Uri(hozzávalók[i].ImageSource, UriKind.Relative));
                }
                ingredientSlots[i].Tag = hozzávalók[i].TagName;
            }

            Instructions.Content = hozzávalók[step].instructionsText;

        }

        public void Section2()
        {
            hozzávalók.Clear();
            step = 0;
            repeater = 0;
            foreach(var i in ingredientSlots) { i.Opacity = 1; i.Source = null; i.Tag = ""; }


            Usualtarget.Tag = "bowl";
            Usualtarget.Source = new BitmapImage(new Uri("/assets/bowl1.jpg", UriKind.Relative));

            hozzávalók.Add(new ingredient("eggs", "/assets/eggcarton_", 0, 3, "add eggs x3"));
            hozzávalók.Add(new ingredient("milk", "/assets/woodenspoon.jpg", 1, "stir"));
            hozzávalók.Add(new ingredient("spoon", "/assets/woodenspoon.jpg", 2, "stir again"));
            hozzávalók.Add(new ingredient("balls", "/assets/woodenspoon.jpg", 3, "i said stir hoe"));
            hozzávalók.Add(new ingredient("???", "/assets/mxstery.gif", 4, "paradox by exmidsion"));

            for (int i = 0; i < hozzávalók.Count; i++)
            {
                if (hozzávalók[i].Repeats > 1)
                {
                    ingredientSlots[i].Source = new BitmapImage(new Uri($"{hozzávalók[i].ImageSource}0.png", UriKind.Relative));
                }
                else
                {
                    ingredientSlots[i].Source = new BitmapImage(new Uri(hozzávalók[i].ImageSource, UriKind.Relative));
                }
                ingredientSlots[i].Tag = hozzávalók[i].TagName;
            }

            Instructions.Content = hozzávalók[step].instructionsText;

        }

        public void ending()
        {
            Instructions.Content = "congrats bitch";
            NavigationService.Navigate(new OutroPage());

        }

    }
}