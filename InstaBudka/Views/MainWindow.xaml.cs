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
using System.Windows.Threading;
using InstaBudka.Utilities;
using InstaBudka.Views;
using YounGuard.Utilities;

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
            var myID = Process.GetCurrentProcess();

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
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += (sender, args) =>
            {
                if (UserInactivity.GetSeconds() >= 90)
                {
                    if (App.CurrentApp.Kw != null)
                    {
                        try
                        {
                            App.CurrentApp.Kw.Topmost = false;
                            App.CurrentApp.Kw.Close();
                        }
                        catch (Exception e)
                        {

                        }
                    }

                    if (!(Frame1.Content is Chose_Page))
                    {
                        Frame1.Navigate(new Chose_Page());
                        
                        App.CurrentApp.Browser.Manage().Window.Minimize();
                    }

                }

            };
            timer.Start();

        }

        private ICommand _stopTimerCommand;
        private ICommand _startTimerCommand;

        public ICommand StopTimerCommand => _stopTimerCommand ?? (_stopTimerCommand = new Command(a =>
        {
            _timer.Tick -= Timer;
            _timer.Stop();
            _sec = 0;
        }));

        public ICommand StartTimerCommand => _startTimerCommand ?? (_startTimerCommand = new Command(a =>
        {
            _timer?.Stop();
            _sec = 0;
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer;
            _timer.Start();
        }));

        DispatcherTimer _timer = new DispatcherTimer();
        private int _sec = 0;
        public string stroka;
        private void Timer(object sender, EventArgs eventArgs)
        {
            _sec++;
            if (_sec >= 7)
            {
                Application.Current.Shutdown();
                App.CurrentApp.Browser.Quit();
            }
            
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
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }


        private void Frame1_OnNavigating(object sender, NavigatingCancelEventArgs e)
        {

            var AllImages = FindVisualChildren<Image>(Frame1.Content as DependencyObject);
            foreach (Image allImage in AllImages)
            {
                //if(allImage.Source.ToString().Contains("Назад"))
                //    continue;
                

                allImage.Height=0;
                allImage.Width = 0;
                allImage.Source = null;

                //allImage.Source = null;
            }
            //var ta = new ThicknessAnimation();
            //ta.Duration = TimeSpan.FromSeconds(0.2);

            //ta.DecelerationRatio = 0.6;
            //ta.To = new Thickness(0, 0, 0, 0);
            //if (e.NavigationMode == NavigationMode.New)
            //{
            //    ta.From = new Thickness(700, 200, 700, 200);
            //}
            //else if (e.NavigationMode == NavigationMode.Back)
            //{
            //    ta.From = new Thickness(-700, -200, -700, -200);
            //}
            //(e.Content as Page).BeginAnimation(MarginProperty, ta);
        }


        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
