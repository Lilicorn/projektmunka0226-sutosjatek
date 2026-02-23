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
        public DecoratingGame()
        {
            InitializeComponent();
        }

        //   oldalso gombokra kattintva valt a listbox tartalma
        // (listak i think) illetve az is, hogy az egyes listbox elemekre
        // valo kattintassal eppen melyik framebe / "retegbe"
        // toltjuk be az adott kepet / "dekoraciot"
        //
        //   emellett arra is gondoltam hogy minden dekornak lesz
        // egy thumbnailje is csakhoyg lassa a kuncsaft mit rak eppen
        //   tehat listboxban a kep az "topfrosting_1_thumb" lesz,
        // a framebe meg a "topfrosting_1" lesz betoltve
        //          -->    topfrosting = epp melyik panel / reteg van kivalasztva
        //          -->    1 = listaban valo indexe (0 az a None opcio / ures imgsource)
        // mar csak azt nem tudom hoyg szukseg van-e osztalyokra itt de ezt te dontsd el bye
        //    ILLETVE4!!!!!! perspektiva miatt topside-bol 2 AZAZ KETTO frame is van, watch out
        //    topside_11 es topside_12
        // szerintem defautként legyen none (0) mert vegulis a nullarol van diszitve x

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
    }
}
