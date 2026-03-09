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
            Section1();
        }

        Image eredetihely = null;
        Image huzigalt = null;
        Point offset;
        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            eredetihely = sender as Image;

            // Klón létrehozása
            huzigalt = new Image();
            var leccimukodj = hozzávalók.FirstOrDefault(h => h.TagName == (string)eredetihely.Tag);
            
            if (leccimukodj.Repeats > 1)
            {
                huzigalt.Source = new BitmapImage(new Uri($"{leccimukodj.ImageSource}single.png", UriKind.Relative));
                eredetihely.Source = new BitmapImage(new Uri($"{leccimukodj.ImageSource}{repeater+1}.png", UriKind.Relative));

            }
            else 
            { 
                huzigalt.Source = eredetihely.Source = new BitmapImage(new Uri($"{leccimukodj.ImageSource}.png", UriKind.Relative));
                eredetihely.Source = new BitmapImage(new Uri($"{leccimukodj.ImageSource}_empty.png", UriKind.Relative));
            }

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

        private async void MouseMove(object sender, MouseEventArgs e)
        {
            if (huzigalt == null || transitionphase ) return;

            Point position = e.GetPosition(background);

            Canvas.SetLeft(huzigalt, position.X - offset.X);
            Canvas.SetTop(huzigalt, position.Y - offset.Y);
        }

        private async void MouseUp(object sender, MouseButtonEventArgs e)
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
                        eredetihely.Source = new BitmapImage(new Uri($"{leccimukodj.ImageSource}{repeater}.png", UriKind.Relative));
                        eredetihely.Opacity = 0.3;
                        repeater = 0;
                        if (step != hozzávalók.Count)
                        {
                            instructionupdater();

                            await usualtargetupdater();
                        }

                    }
                    else
                    {
                        await UNusualtargetupdater();
                        eredetihely.Source = new BitmapImage(new Uri($"{leccimukodj.ImageSource}{repeater}.png", UriKind.Relative));
                        eredetihely.Opacity = 1;
                        instructionupdater();

                    }
                }
                else
                { 
                    step++;
                    eredetihely.Opacity = 0.3;
                    eredetihely.Source = new BitmapImage(new Uri($"{leccimukodj.ImageSource}_empty.png", UriKind.Relative));

                    if (step!= hozzávalók.Count)
                    {
                        await usualtargetupdater();
                        instructionupdater();

                    }
                }

            }
            else if(step != hozzávalók.Count)
            {
                background.Children.Remove(huzigalt);

                if (leccimukodj.StepIndex < step)
                {
                    eredetihely.Opacity = 0.3;

                    if (leccimukodj.Repeats > 1)
                    {
                        eredetihely.Source = new BitmapImage(new Uri($"{leccimukodj.ImageSource}{leccimukodj.Repeats}.png", UriKind.Relative));
                    }
                    else
                    {
                        eredetihely.Source = new BitmapImage(new Uri($"{leccimukodj.ImageSource}_empty.png", UriKind.Relative));
                    }
                }
                else
                {
                    eredetihely.Opacity = 1;

                    if (leccimukodj.Repeats > 1)
                    {
                        eredetihely.Source = new BitmapImage(new Uri($"{leccimukodj.ImageSource}0.png", UriKind.Relative));
                    }
                    else
                    {
                        eredetihely.Source = new BitmapImage(new Uri($"{leccimukodj.ImageSource}.png", UriKind.Relative));
                    }

                }

            }

            if (step == hozzávalók.Count)
            {
                section++;
                step = 0;
                repeater = 0;
                await usualtargetupdater();
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

        List<ingredient> hozzávalók = new List<ingredient>();
        int step = 0;
        int repeater = 0;
        int section = 1;
        int picoffset = 0;
        bool transitionphase = false;



        public async void Section1()
        {
            picoffset = 0;

            theCleaner();

            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/1_0.png", UriKind.Relative));


            hozzávalók.Add(new ingredient("flour", "/ingredientassets/flour", 0, "add flour"));
            hozzávalók.Add(new ingredient("baking powder", "/ingredientassets/bakingpowder", 1, "add baking powder"));
            hozzávalók.Add(new ingredient("cocoa powder", "/ingredientassets/cocoa", 2, "add cocoa powder"));
            hozzávalók.Add(new ingredient("salt", "/ingredientassets/salt", 3, "add salt"));
            hozzávalók.Add(new ingredient("mixer", "/ingredientassets/mixer", 4, "mix dry ingredients"));

            await Task.Delay(1);

            theMarginFixer();
            theFiller();
        }


        public async void Section2()
        {
            picoffset = 0;

            theCleaner();

            hozzávalók.Add(new ingredient("butter", "/ingredientassets/butter_", 0, 2, "add butter x"));
            hozzávalók.Add(new ingredient("sugar", "/ingredientassets/sugar", 1, "add sugar"));
            hozzávalók.Add(new ingredient("egg", "/ingredientassets/egg_", 2, 3, "add eggs x"));
            hozzávalók.Add(new ingredient("mixer", "/ingredientassets/mixer", 3, "mix wet ingredients"));

            await usualtargetupdater();

            transitionphase = true;
            Instructions.Content = "switching bowl...";
            await Task.Delay(1000);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/2_fix_1.png", UriKind.Relative));
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/2_fix_2.png", UriKind.Relative));
            await Task.Delay(500);
            transitionphase = false;

            theMarginFixer();
            theFiller();

        }


        public async void Section3()
        {
            picoffset = 1;

            theCleaner();

            hozzávalók.Add(new ingredient("milk", "/ingredientassets/milk", 0, "add milk"));
            hozzávalók.Add(new ingredient("vanilla", "/ingredientassets/vanilla", 1, "add vanilla"));
            hozzávalók.Add(new ingredient("mixer", "/ingredientassets/mixer", 2, "mix"));

            await usualtargetupdater();

            theMarginFixer();
            theFiller();

        }

        public async void Section4()
        {
            picoffset = 1;

            theCleaner();

            hozzávalók.Add(new ingredient("drybowl", "/ingredientassets/drybowl", 0, "combine mixtures"));
            hozzávalók.Add(new ingredient("mixer", "/ingredientassets/mixer", 1, "mix again"));

            await usualtargetupdater();

            transitionphase = true;
            await Task.Delay(1000);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/4_fix.png", UriKind.Relative));
            await Task.Delay(500);
            transitionphase = false;

            theMarginFixer(); 
            theFiller();

        }

        public async void Section5()
        {
            picoffset = 2;

            theCleaner();

            hozzávalók.Add(new ingredient("batter", "/ingredientassets/batter", 0, "pour batter into tray"));

            await usualtargetupdater();
            transitionphase = true;
            Instructions.Content = "preparing tray...";
            await Task.Delay(1000);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/5_fix_1.png", UriKind.Relative));
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/5_fix_2.png", UriKind.Relative));
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/5_fix_3.png", UriKind.Relative));
            await Task.Delay(500);
            transitionphase = false;

            theMarginFixer(); 
            theFiller();


        }

        public async void Section6()
        {
            picoffset = 2;

            theCleaner();

            hozzávalók.Add(new ingredient("batter in tray", "/ingredientassets/fulltray", 0, "put tray into oven"));


            await usualtargetupdater();

            transitionphase = true;
            await Task.Delay(1000);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/6_fix_1.png", UriKind.Relative));
            Instructions.Content = "preparing oven...";
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/6_fix_2.png", UriKind.Relative));
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/6_fix_3.png", UriKind.Relative));
            await Task.Delay(500);
            transitionphase = false;

            theMarginFixer();
            theFiller();

        }

        public async void Section7()
        {
            picoffset = 2;

            theCleaner();

            hozzávalók.Add(new ingredient("cream", "/ingredientassets/cream", 0, "add whipping cream"));

            await usualtargetupdater();


            transitionphase = true;
            Instructions.Content = "while the cake is baking,\n lets make some cream!";
            await Task.Delay(2000);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/7_fix_1.png", UriKind.Relative));
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/7_fix_2.png", UriKind.Relative));
            await Task.Delay(500);
            transitionphase = false;

            theMarginFixer();
            theFiller();

        }

        public async void Section8()
        {
            picoffset = 2;

            theCleaner();

            hozzávalók.Add(new ingredient("mixer", "/ingredientassets/mixer", 0, "whip to foam"));

            await usualtargetupdater();

            transitionphase = true;
            Instructions.Content = "wait for it to boil...";
            for (int j = 1; j <= 2; j++)
            {
                for (int i = 1; i <= 4; i++)
                {
                    Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/uselessloop_{i}.png", UriKind.Relative));
                    await Task.Delay(250);
                }
                await Task.Delay(250);
            }
            await Task.Delay(250);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/8_fix_1.png", UriKind.Relative));
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/8_fix_2.png", UriKind.Relative));
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/8_fix_3.png", UriKind.Relative));
            await Task.Delay(500);
            transitionphase = false;

            theMarginFixer();
            theFiller();

        }

        public async void Section9()
        {
            picoffset = 0;

            theCleaner();

            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/{section}_{step}_{repeater}.png", UriKind.Relative));

            hozzávalók.Add(new ingredient("grate", "/ingredientassets/grate", 0, "put grate into bowl"));
            hozzávalók.Add(new ingredient("chocolate", "/ingredientassets/chocolate_", 1, 3, "grate chocolate x"));
            hozzávalók.Add(new ingredient("butter", "/ingredientassets/butter_", 2, 2, "add butter x"));
            hozzávalók.Add(new ingredient("mixer", "/ingredientassets/mixer", 3, "mix"));

            await usualtargetupdater();

            transitionphase = true;
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/9_fix_1.png", UriKind.Relative));
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/9_fix_2.png", UriKind.Relative));
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/9_fix_3.png", UriKind.Relative));
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/9_fix_4.png", UriKind.Relative));
            await Task.Delay(500);
            transitionphase = false;

            theMarginFixer();
            theFiller();

        }

        public async void Section10()
        {
            picoffset = 1;

            theCleaner();

            hozzávalók.Add(new ingredient("CREAM bowl", "/ingredientassets/creambowl", 0, "combine bowls"));
            hozzávalók.Add(new ingredient("wooden spoon", "/ingredientassets/woodenspoon", 1, "stir gently"));

            await usualtargetupdater();

            transitionphase = true;
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/10_fix.png", UriKind.Relative));
            transitionphase = false;

            theMarginFixer();
            theFiller();

        }

        public async void Section11()
        {
            theCleaner();
            Instructions.Opacity=0;
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/11_0.png", UriKind.Relative));
            await Task.Delay(500);
                Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/11_1.png", UriKind.Relative));
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/11_2.png", UriKind.Relative));
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"", UriKind.Relative));
            await Task.Delay(500);

            theMarginFixer();
            Instructions.Opacity = 1;
            Instructions.Content = "oven is done!";
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/11_3.png", UriKind.Relative));
            await Task.Delay(500); 
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/11_4.png", UriKind.Relative));
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/11_5.png", UriKind.Relative));
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/11_6.png", UriKind.Relative));
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/11_7.png", UriKind.Relative));
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/11_8.png", UriKind.Relative));
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/11_9.png", UriKind.Relative));
            await Task.Delay(500);

            section++;
            step = 0;
            repeater = 0;

            Section12();
        }

        public void Section12()
        {
            picoffset = 2;

            theCleaner();

            hozzávalók.Add(new ingredient("knife", "/ingredientassets/knife", 0, "slice cake"));

            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/12_0.png", UriKind.Relative));

            theMarginFixer();
            theFiller();


        }

        public async void Section13()
        {
            picoffset = 1;

            theCleaner();

            hozzávalók.Add(new ingredient("creambowl", "/ingredientassets/creambowl_", 0, 4, "spread cream x"));
            hozzávalók.Add(new ingredient("layer", "/ingredientassets/cakelayer", 1, "stack cake layers"));

            await usualtargetupdater();

            theMarginFixer();
            theFiller();

        }


        public async void ending()
        {
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/end.png", UriKind.Relative));
            Instructions.Content = "congratulations! you're done!";
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
                    ingredientSlots[i + picoffset].Source = new BitmapImage(new Uri($"{hozzávalók[i].ImageSource}.png", UriKind.Relative));
                }

                ingredientSlots[i + picoffset].Tag = hozzávalók[i].TagName;
            }

            instructionupdater();
        }
        public async Task usualtargetupdater()
        {
            if (step > hozzávalók.Count) {  return; }
            transitionphase = true;
            if (hozzávalók[step].Repeats > 1)
            {
                Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/{section}_{step}_{repeater}_t.png", UriKind.Relative));
                await Task.Delay(1500);
                Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/{section}_{step}_{repeater}.png", UriKind.Relative));
                

            }
            else
            {
                Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/{section}_{step}_t.png", UriKind.Relative));
                await Task.Delay(1500);
                Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/{section}_{step}.png", UriKind.Relative));
               

            }
            transitionphase = false;
        }

        public async Task UNusualtargetupdater()
        {
            transitionphase = true;

            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/{section}_{step}_{repeater}_t.png", UriKind.Relative));
            await Task.Delay(500);
            Usualtarget.Source = new BitmapImage(new Uri($"/targetassets/{section}_{step}_{repeater}.png", UriKind.Relative));
          
            transitionphase = false;
        }


        public void instructionupdater()
        {
            if (hozzávalók[step].Repeats > 1)
            {
                Instructions.Content = $"{hozzávalók[step].instructionsText}{hozzávalók[step].Repeats - repeater}";

            }
            else
            {
                Instructions.Content = hozzávalók[step].instructionsText;
            }
        }

        public void theMarginFixer()
        {
            Instructions.Padding = new Thickness(this.ActualWidth * 0.05, this.ActualHeight * 0.03, this.ActualWidth * 0.05, this.ActualHeight * 0.03);
            Instructions.FontSize = this.ActualHeight * 0.06;


            if (picoffset == 2)
            {
                ingredientsection.Margin = new Thickness(this.ActualWidth * 0.1 * -1, this.ActualHeight * 0.06, this.ActualWidth * 0.1 * -1, this.ActualHeight * 0.16 * -1);
                Usualtarget.Margin = new Thickness(this.ActualHeight * 0.08 * -1, this.ActualHeight * 0.2 * -1, this.ActualHeight * 0.08 * -1, this.ActualHeight * 0.2);

            }
            else if (section == 11)
            {
                Usualtarget.Margin = new Thickness(this.ActualWidth * 0.2 * -1, this.ActualHeight * 0.1 * -1, this.ActualWidth * 0.25, this.ActualHeight * 0.1);

                ingredientsection.Margin = new Thickness(0, this.ActualHeight * 0.1 * -1, 0, this.ActualHeight * 0.1);

            }
            else
            {
                Usualtarget.Margin = new Thickness(this.ActualHeight * 0.08 * -1, this.ActualHeight * 0.2 * -1, this.ActualHeight * 0.08 * -1, this.ActualHeight * 0.2);
                ingredientsection.Margin = new Thickness(0, this.ActualHeight * 0.1 * -1, 0, this.ActualHeight * 0.1);
            }
        }

        private void theMarginUpdater(object sender, SizeChangedEventArgs e)
        {
            Instructions.Padding = new Thickness(this.ActualWidth * 0.05, this.ActualHeight * 0.03, this.ActualWidth * 0.05, this.ActualHeight * 0.03);
            Instructions.FontSize = this.ActualHeight * 0.06;


            if (picoffset == 2)
            {
                ingredientsection.Margin = new Thickness(this.ActualWidth *0.1  * -1, this.ActualHeight * 0.06, this.ActualWidth * 0.1 * -1, this.ActualHeight * 0.16 * -1);
                Usualtarget.Margin = new Thickness(this.ActualHeight * 0.08 * -1, this.ActualHeight * 0.2 * -1, this.ActualHeight * 0.08 * -1, this.ActualHeight * 0.2);

            }
            else if (section == 11)
            {
                Usualtarget.Margin = new Thickness(this.ActualWidth * 0.2 * -1, this.ActualHeight *0.1  * -1, this.ActualWidth * 0.25, this.ActualHeight * 0.1);
                ingredientsection.Margin = new Thickness(0, this.ActualHeight * 0.1 * -1, 0, this.ActualHeight * 0.1);

            }
            else
            {
                Usualtarget.Margin = new Thickness(this.ActualHeight * 0.08 * -1, this.ActualHeight *0.2 * -1, this.ActualHeight * 0.08 * -1, this.ActualHeight *0.2);
                ingredientsection.Margin = new Thickness(0, this.ActualHeight * 0.1 * -1, 0, this.ActualHeight * 0.1);
            }
        }
    }
}