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
using InstaBudka.Views;

namespace InstaBudka
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            Frame1.NavigationService.Navigate(new Chose_Page(), UriKind.Relative);

            
            Widthh = SystemParameters.PrimaryScreenWidth;
            Heightt = SystemParameters.PrimaryScreenHeight;
        }
        

        public static readonly DependencyProperty WidthhProperty = DependencyProperty.Register(
            "Widthh", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));

        public double Widthh
        {
            get { return (double) GetValue(WidthhProperty); }
            set { SetValue(WidthhProperty, value); }
        }

        public static readonly DependencyProperty HeighttProperty = DependencyProperty.Register(
            "Heightt", typeof(double), typeof(MainWindow), new PropertyMetadata(default(double)));

        public double Heightt
        {
            get { return (double) GetValue(HeighttProperty); }
            set { SetValue(HeighttProperty, value); }
        }

        private void Frame1_OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            var ta = new ThicknessAnimation();
            ta.Duration = TimeSpan.FromSeconds(0.2);

            ta.DecelerationRatio = 0.6;
            ta.To = new Thickness(0, 0, 0, 0);
            if (e.NavigationMode == NavigationMode.New)
            {
                ta.From = new Thickness(500, 200, 500, 200);
            }
            else if (e.NavigationMode == NavigationMode.Back)
            {
                ta.From = new Thickness(-500, -200, -500, -200);
            }
            (e.Content as Page).BeginAnimation(MarginProperty, ta);
        }


        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
