using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
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
            huzigalt.Width = eredetihely.ActualWidth;
            huzigalt.Height = eredetihely.ActualHeight;
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
                        ingredientSlots[leccimukodj.StepIndex + picoffset].Source = new BitmapImage(new Uri($"{hozzávalók[leccimukodj.StepIndex].ImageSource}{repeater}.png", UriKind.Relative)); // atirtam relativera hogy ne keljen mindent beirni ugh
                        eredetihely.Opacity = 0.3;
                        repeater = 0;
                        Instructions.Content = hozzávalók[step].instructionsText;
                        usualtargetupdater();


                    }
                    else
                    {
                        ingredientSlots[leccimukodj.StepIndex + picoffset].Source = new BitmapImage(new Uri($"{hozzávalók[leccimukodj.StepIndex].ImageSource}{repeater}.png", UriKind.Relative));
                        eredetihely.Opacity = 1;
                        Instructions.Content = $"{hozzávalók[step].instructionsText}{hozzávalók[leccimukodj.StepIndex].Repeats-repeater}";

                    }
                }
                else
                {
                    eredetihely.Opacity = 0.3;
                    step++;

                    if(step!= hozzávalók.Count)
                    {
                        usualtargetupdater();
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
                step = 0;
                repeater = 0;
                usualtargetupdater();
                
                switch (section)
                {
                    default: Section1(); break;

                    case 1: Section1(); break;
                    case 2: Section2(); break;
                    case 3: Section3(); break;
                    case 4: Section4(); break;
                    case 5: Section5(); break;
                    case 6: Section6(); break;
                    case 7: Section7(); break;
                    case 8: Section8(); break;
                    case 9: Section9(); break;
                    case 10: Section10(); break;
                    case 11: Section11(); break;
                    case 12: Section12(); break;
                    case 13: Section13(); break;

                    case 14: ending(); break;
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
        int picoffset = 0;



        public void Section1()
        {
            picoffset = 0;

            theCleaner();

            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/1_0.png", UriKind.Relative));

            hozzávalók.Add(new ingredient("flour", "/ingredientassets/flour.png", 0, "add flour"));
            hozzávalók.Add(new ingredient("baking powder", "/ingredientassets/bakingpowder.png", 1, "add baking powder"));
            hozzávalók.Add(new ingredient("cocoa powder", "/ingredientassets/cocoa.png", 2, "add cocoa powder"));
            hozzávalók.Add(new ingredient("salt", "/ingredientassets/salt.png", 3, "add salt"));
            hozzávalók.Add(new ingredient("mixer", "/ingredientassets/mixer.png", 4, "mix dry ingredients"));

            theFiller();
        }


        public void Section2()
        {
            picoffset = 0;

            theCleaner();

            hozzávalók.Add(new ingredient("butter", "/ingredientassets/butter_", 0, 2, "add butter x"));
            hozzávalók.Add(new ingredient("sugar", "/ingredientassets/sugar.png", 1, "add sugar"));
            hozzávalók.Add(new ingredient("egg", "/ingredientassets/egg_", 2, 3, "add eggs x"));
            hozzávalók.Add(new ingredient("mixer", "/ingredientassets/mixer.png", 3, "mix wet ingredients"));

            theFiller();
            usualtargetupdater();
        }


        public void Section3()
        {
            picoffset = 1;

            theCleaner();

            hozzávalók.Add(new ingredient("milk", "/ingredientassets/milk.png", 0, "add milk"));
            hozzávalók.Add(new ingredient("vanilla", "/ingredientassets/vanilla.png", 1, "add vanilla"));
            hozzávalók.Add(new ingredient("mixer", "/ingredientassets/mixer.png", 2, "mix"));

            theFiller();
            usualtargetupdater();
        }

        public void Section4()
        {
            picoffset = 1;

            theCleaner();

            hozzávalók.Add(new ingredient("drybowl", "/ingredientassets/drybowl.png", 0, "combine mixtures"));
            hozzávalók.Add(new ingredient("mixer", "/ingredientassets/mixer.png", 1, "mix again"));

            theFiller();
            usualtargetupdater();
        }

        public void Section5()
        {
            picoffset = 2;

            theCleaner();

            hozzávalók.Add(new ingredient("batter", "/ingredientassets/batter.png", 0, "pour batter into tray"));

            theFiller();
            usualtargetupdater();
        }

        public void Section6()
        {
            picoffset = 2;

            theCleaner();

            hozzávalók.Add(new ingredient("batter in tray", "/ingredientassets/fulltray.png", 0, "put into oven"));

            theFiller();

            usualtargetupdater();
        }

        public void Section7()
        {
            picoffset = 2;

            theCleaner();

            hozzávalók.Add(new ingredient("cream", "/ingredientassets/cream.png", 0, "add whipping cream"));

            theFiller();

            usualtargetupdater();
        }

        public void Section8()
        {
            picoffset = 2;

            theCleaner();

            hozzávalók.Add(new ingredient("mixer", "/ingredientassets/mixer.png", 0, "whip to foam"));

            theFiller();

            usualtargetupdater();
        }

        public void Section9()
        {
            picoffset = 1;

            theCleaner();

            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/{section}_{step}_{repeater}.png", UriKind.Relative));

            hozzávalók.Add(new ingredient("grate", "/ingredientassets/grate.png", 0, "put grate into bowl"));
            hozzávalók.Add(new ingredient("chocolate", "/ingredientassets/chocolate_", 1, 3, "grate chocolate x"));
            hozzávalók.Add(new ingredient("butter", "/ingredientassets/butter_", 2, 2, "add butter x"));

            theFiller();
        }

        public void Section10()
        {
            picoffset = 1;

            theCleaner();

            hozzávalók.Add(new ingredient("choco bowl", "/ingredientassets/chocobowl.png", 0, "combine bowls"));
            hozzávalók.Add(new ingredient("wooden spoon", "/ingredientassets/woodenspoon.png", 1, "stir gently"));

            theFiller();
            usualtargetupdater();
        }

        //section ami csak valt 2-3 kepet a kesz tortarol
        public async void Section11()
        {
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/end.png", UriKind.Relative));
            await Task.Delay(2500);
        }

        public void Section12()
        {
            picoffset = 2;

            theCleaner();

            hozzávalók.Add(new ingredient("knife", "/ingredientassets/knife.png", 0, "slice cake"));

            theFiller();
            usualtargetupdater();
        }

        public void Section13()
        {
            picoffset = 1;

            theCleaner();

            hozzávalók.Add(new ingredient("creambowl", "/ingredientassets/creambowl_", 0, 4, "spread cream x"));
            hozzávalók.Add(new ingredient("layer", "/ingredientassets/cakelayer.png", 1, "stack cake layers"));

            theFiller();
            usualtargetupdater();
        }

        
        public async void ending()
        {
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/end.png", UriKind.Relative));
            Instructions.Content = "congratulato";
            App.phase= 2;

            await Task.Delay(2000);

            NavigationService.Navigate(new TransitionPage());
        }


        public void theCleaner()
        {
            hozzávalók.Clear();
            step = 0;
            repeater = 0;
            foreach (var i in ingredientSlots) { i.Opacity = 1; i.Source = null; i.Tag = ""; }
        }

        public void theFiller()
        {
            for (int i = 0; i < hozzávalók.Count; i++)
            {
                if (hozzávalók[i].Repeats > 1)
                {
                    ingredientSlots[i + picoffset].Source = new BitmapImage(new Uri($"{hozzávalók[i].ImageSource}0.png", UriKind.Relative));

                }
                else
                {
                    ingredientSlots[i + picoffset].Source = new BitmapImage(new Uri(hozzávalók[i].ImageSource, UriKind.Relative));
                }

                ingredientSlots[i + picoffset].Tag = hozzávalók[i].TagName;
            }
            Instructions.Content = hozzávalók[step].instructionsText;
            if (hozzávalók[step].Repeats > 1)
            {
                Instructions.Content += hozzávalók[step].Repeats.ToString();
            }
        }
        public async void usualtargetupdater()
        {
            if (step >= hozzávalók.Count) {  return; }
            if (hozzávalók[step].Repeats > 1)
            {
                Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/{section}_{step}_{repeater}.png", UriKind.Relative));

            }
            else
            {
                Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/{section}_{step}.png", UriKind.Relative));

            }
        }

    }
}