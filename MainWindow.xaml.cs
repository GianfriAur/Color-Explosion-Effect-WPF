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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Color_Explosino
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Duration d = new Duration(TimeSpan.FromSeconds(2));
        Duration di = new Duration(TimeSpan.FromSeconds(1));
        int iCont = 1;
        int AlliCont = 0;
        private void Grid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition((Grid)sender);
            Grid Ng = new Grid() { Name = "Ng"+ AlliCont, Height = 800, Width = 1600, Margin = new Thickness(-800 + p.X, -400 + p.Y, 0, 0) };
            Ellipse Ne = new Ellipse() { Name = "Ne" + AlliCont, Height = 0, Width = 0, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Fill = getColor(iCont) };
            Ellipse NeI = new Ellipse() { Name = "NeI" + AlliCont, Height = 0, Width = 0, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Fill = getColor(iCont+3), Opacity=0.5 };
            RegisterName(Ng.Name, Ng);
            RegisterName(Ne.Name, Ne);
            RegisterName(NeI.Name, NeI);
            Ng.Children.Add(Ne);
            Ng.Children.Add(NeI);
            Container.Children.Add(Ng);

            DoubleAnimation adE = new DoubleAnimation() { From = 0, To = 1600 * 1.2, Duration = d, EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut } };
            DoubleAnimation adE2 = new DoubleAnimation() { From = 0, To = 1600 * 1.2, Duration = d, EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut } };
            DoubleAnimation adEI = new DoubleAnimation() { From = 0, To = 300, Duration = di, EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut } };
            DoubleAnimation adEI2 = new DoubleAnimation() { From = 0, To = 300, Duration = di, EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut } };
            DoubleAnimation adEI3 = new DoubleAnimation() { From = 0.7, To = 0, Duration = di, EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut } };
            var St = new Storyboard();
            var r = new Random();
            for (int i = 0; i != 40; i++)
                GenerateParticle(ref St, ref Ng,ref r, Ng.Name + "P" + i);


            St.Children.Add(adE);
            St.Children.Add(adE2);
            St.Children.Add(adEI);
            St.Children.Add(adEI2);
            St.Children.Add(adEI3);
            Storyboard.SetTargetName(adE, Ne.Name);
            Storyboard.SetTargetName(adE2, Ne.Name);
            Storyboard.SetTargetName(adEI, NeI.Name);
            Storyboard.SetTargetName(adEI2, NeI.Name);
            Storyboard.SetTargetName(adEI3, NeI.Name);
            Storyboard.SetTargetProperty(adE, new PropertyPath(Ellipse.HeightProperty));
            Storyboard.SetTargetProperty(adE2, new PropertyPath(Ellipse.WidthProperty));
            Storyboard.SetTargetProperty(adEI, new PropertyPath(Ellipse.HeightProperty));
            Storyboard.SetTargetProperty(adEI2, new PropertyPath(Ellipse.WidthProperty));
            Storyboard.SetTargetProperty(adEI3, new PropertyPath(Ellipse.OpacityProperty));
            St.Completed += (s,a)=> {
                Container.Children.Remove(Ng);
                this.UnregisterName(Ng.Name);
                this.UnregisterName(Ne.Name);
                this.UnregisterName(NeI.Name);
                MContainer.Background = Ne.Fill;
            };
            St.Begin(this, true);
            iCont++;
            AlliCont++;
        }


        public void GenerateParticle(ref Storyboard St, ref Grid C ,ref Random r,string name)
        {


            Ellipse EP = new Ellipse() { Name = name, Height = 50, Width = 50, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Fill = getColor(iCont + 3) };
            RegisterName(EP.Name, EP);
            C.Children.Add(EP);
            DoubleAnimation A1 = new DoubleAnimation() { From = 50, To = 0, Duration = di, EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut } };
            DoubleAnimation A2 = new DoubleAnimation() { From = 50, To = 0, Duration = di, EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut } };
            ThicknessAnimation A3 = new ThicknessAnimation() { From = new Thickness(0,0,0,0), To = new Thickness(r.Next(-300, +300), r.Next(-300, +300), 0, 0), Duration = di, EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut } };
            St.Children.Add(A1);
            St.Children.Add(A2);
            St.Children.Add(A3);
            Storyboard.SetTargetName(A1, EP.Name);
            Storyboard.SetTargetName(A2, EP.Name);
            Storyboard.SetTargetName(A3, EP.Name);
            Storyboard.SetTargetProperty(A1, new PropertyPath(Ellipse.HeightProperty));
            Storyboard.SetTargetProperty(A2, new PropertyPath(Ellipse.WidthProperty));
            Storyboard.SetTargetProperty(A3, new PropertyPath(Ellipse.MarginProperty));
            St.Completed += (s, a) => {
                this.UnregisterName(EP.Name);
            };
        }

        public string[] colors = new string[] { "#FF6138", "#FFBE53", "#2980B9", "#282741", };


        public Brush getColor(int i )
        {
            while (i > colors.Length-1) i -= colors.Length;
          //  iCont = i;
            return (SolidColorBrush)new BrushConverter().ConvertFromString(colors[i]);
        }
    }
}
