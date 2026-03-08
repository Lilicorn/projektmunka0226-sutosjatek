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
        private string currentCategory = "";
        public DecoratingGame()
        {
            InitializeComponent();
            LoadDecorImages("top");

        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            table.Margin = new Thickness(0, this.ActualHeight * (1 / 3.0), 0, 0);

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
            Button clicked = sender as Button; 

            currentCategory = clicked.Name.Replace("_loader", "");

            LoadDecorImages(currentCategory);
        }

        private void LoadDecorImages(string category)
        {
            decorListing.Items.Clear();

            for (int i = 1; i <= 4; i++)
            {
                string path = $"/decorassets/{category}/{category}_{i}_thumb.png";
                decorListing.Items.Add(path);
            }
        }

        private void decorListing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (decorListing.SelectedItem == null)
                return;

            string selectedImage = decorListing.SelectedItem.ToString().Replace("_thumb","");

            ApplyDecorToCake(selectedImage);
        }

        private void ApplyDecorToCake(string imagePath)
        {     
            BitmapImage bitmap = new BitmapImage(new Uri(imagePath, UriKind.Relative));

            switch (currentCategory)
            {
                case "top":
                    top_frame.Source = bitmap;
                    CakeState.Top = imagePath;
                    break;

                case "topside":
                    topside_frame1.Source = bitmap;
                    CakeState.TopSide = imagePath;
                    break;

                case "midside":
                    midside_frame.Source = bitmap;
                    CakeState.MidSide = imagePath;
                    break;

                case "bottomside":
                    bottomside_frame.Source = bitmap;
                    CakeState.BottomSide = imagePath;
                    break;

                case "topfrosting":
                    topfrosting_frame.Source = bitmap;
                    CakeState.TopFrosting = imagePath;
                    break;

                case "coveringcolor":
                    coveringcolor_frame.Source = bitmap;
                    CakeState.CoveringColor = imagePath;
                    break;

                case "stand":
                    stand_frame.Source = bitmap;
                    CakeState.Stand = imagePath;
                    break;
            }
        }

        private void Outro_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OutroPage());
        }
    }
    public static class CakeState
    {
        public static string Top;
        public static string TopSide;
        public static string MidSide;
        public static string BottomSide;
        public static string TopFrosting;
        public static string CoveringColor;
        public static string Stand;
    }
}
