using System;
using System.Collections.Generic;
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

namespace ALegjobbSutosJatekProjekt
{
    /// <summary>
    /// Interaction logic for DecoratingGame.xaml
    /// </summary>
    public partial class DecoratingGame : Page
    {
        private string currentCategory = ""; //WHERE THE FUCK ARE WE /j
        public DecoratingGame()
        {
            InitializeComponent();

        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e) // ez kell mert igy reszponziv az asztal es a gombospanel
        {
            var cucc = new ThicknessConverter();
            Thickness leccives = (Thickness)cucc.ConvertFromString($"0 {this.ActualHeight * (1 / 3.0)} 0 0");
            table.Margin = leccives;

            top_loader.Height = decorListing.ActualHeight * (1 / 7.0) / 1.2;
            topside_loader.Height = decorListing.ActualHeight * (1 / 7.0) / 1.2;
            midside_loader.Height = decorListing.ActualHeight * (1 / 7.0) / 1.2;
            bottomside_loader.Height = decorListing.ActualHeight * (1 / 7.0) / 1.2;
            topfrosting_loader.Height = decorListing.ActualHeight * (1 / 7.0) / 1.2;
            coveringcolor_loader.Height = decorListing.ActualHeight * (1 / 7.0) / 1.2;
            stand_loader.Height = decorListing.ActualHeight * (1 / 7.0) / 1.2;
        }

        private void Loader_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = sender as Button; // Need this for the button name

            // We get what we need from button,, minus the loader part 👍🔥
            currentCategory = clicked.Name.Replace("_loader", "");

            LoadDecorImages(currentCategory);
        }

        private void LoadDecorImages(string category)
        {
            decorListing.Items.Clear();

            for (int i = 1; i <= 4; i++)
            {
                string path = $"/decorassets/{category}/{category}_{i}.png";
                //HEADS UP THE IMAGES LOOK GOD AWFUL IN THE CURRENT STATE (they dont even resize currently),,
                //but it works thats all that matters we fix that when we get there,, maybe thumbnails?? or resize them correctly??
                decorListing.Items.Add(path);
            }
        }

        private void decorListing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (decorListing.SelectedItem == null) //yknow that one code we made where we needed this? yeah same shit
                return;

            string selectedImage = decorListing.SelectedItem.ToString();

            ApplyDecorToCake(selectedImage);
        }

        private void ApplyDecorToCake(string imagePath)
        {
            //,, I looked this up idk how BitmapImage works entirely but its good for images apperantnly so,,
            BitmapImage bitmap = new BitmapImage(new Uri(imagePath, UriKind.Relative));

            // Im forgeting switch exists but its easier this way
            switch (currentCategory)
            {
                case "top":
                    top_frame.Source = bitmap;
                    break;

                case "topside":
                    topside_frame1.Source = bitmap;
                    topside_frame2.Source = bitmap;
                    break;

                case "midside":
                    midside_frame.Source = bitmap;
                    break;

                case "bottomside":
                    bottomside_frame.Source = bitmap;
                    break;

                case "topfrosting":
                    topfrosting_frame.Source = bitmap;
                    break;

                case "coveringcolor":
                    coveringcolor_frame.Source = bitmap;
                    break;

                case "stand":
                    stand_frame.Source = bitmap;
                    break;
            }
        }
    }
}
