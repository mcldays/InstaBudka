using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            if (App.CurrentApp.Browser == null)
            {
                App.CurrentApp.Browser = new OpenQA.Selenium.Chrome.ChromeDriver();
                App.CurrentApp.Browser.Manage().Window.Maximize();

                App.CurrentApp.Browser.Manage().Window.FullScreen();
            }



            InitializeComponent();
            Frame1.NavigationService.Navigate(new Chose_Page(), UriKind.Relative);

            
            Widthh = SystemParameters.PrimaryScreenWidth;
            Heightt = SystemParameters.PrimaryScreenHeight;
            //this.Cursor = Cursors.None; 
            Process process = Process.Start(new ProcessStartInfo
            {
                FileName = "taskkill",
                Arguments = "/F /IM explorer.exe",
                CreateNoWindow = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden
            });
            process?.WaitForExit();
            Closing += (e, a) =>
            {

                Process.Start(Path.Combine(Environment.GetEnvironmentVariable("windir"), "explorer.exe"));
            };
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
                ta.From = new Thickness(700, 200, 700, 200);
            }
            else if (e.NavigationMode == NavigationMode.Back)
            {
                ta.From = new Thickness(-700, -200, -700, -200);
            }
            (e.Content as Page).BeginAnimation(MarginProperty, ta);
        }


        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
