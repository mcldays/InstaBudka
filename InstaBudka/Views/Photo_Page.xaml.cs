using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InstaBudka.Utilities;

namespace InstaBudka.Views
{
    /// <summary>
    /// Логика взаимодействия для Photo_Page.xaml
    /// </summary>
    public partial class Photo_Page : Page
    {
        public Photo_Page()
        {
            InitializeComponent();
            PhotoIndex = 0;
        }


        VideoCaptureDevice LocalWebCam;
        public FilterInfoCollection LoaclWebCamsCollection;
        private int WMID;

        void Cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            System.Drawing.Image img = (Bitmap) eventArgs.Frame.Clone();
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            BitmapImage bi = new BitmapImage();

            bi.BeginInit();
            bi.StreamSource = ms;

            bi.EndInit();
            bi.Freeze();

            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                frameHolder.Source = bi;
                bi2 = bi;
            }));
        }

        private void Photo_Page_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoaclWebCamsCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (LoaclWebCamsCollection.Count != 0)
            {
                LocalWebCam = new VideoCaptureDevice(LoaclWebCamsCollection[0].MonikerString);
                LocalWebCam.NewFrame += new NewFrameEventHandler(Cam_NewFrame);
                LocalWebCam.Start();
            }
            else
            {
                MessageBox.Show("Камера не подключена");
            }
            
        }

        public static readonly DependencyProperty bi2Property = DependencyProperty.Register(
            "bi2", typeof(BitmapImage), typeof(Photo_Page), new PropertyMetadata(default(BitmapImage)));

        public BitmapImage bi2
        {
            get { return (BitmapImage) GetValue(bi2Property); }
            set { SetValue(bi2Property, value); }
        }
        private ICommand _backCommand;
        public ICommand BackCommand => _backCommand ?? (_backCommand = new Command((c =>
            {
                LocalWebCam.Stop();
                NavigationService.Navigate(new Chose_Page());
            }
        )));


        private ICommand _photoCommand;
        public ICommand PhotoCommand => _photoCommand ?? (_photoCommand = new Command((c =>
          {
              KolazhButton.Visibility = Visibility.Collapsed;
              CountdownBorder.Visibility = Visibility.Visible;
              PhotoButton.Visibility = Visibility.Collapsed;
              PhotoAgainButton.Visibility = Visibility.Collapsed;
              PhotoPath1 = null;
              PhotoPath2 = null;
              PhotoPath3 = null;
              StartCountdown(CountdownDisplay);
            }
        )));

        private ICommand _kolazhCommand;
        public ICommand KolazhCommand => _kolazhCommand ?? (_kolazhCommand = new Command(c =>
         {
             LocalWebCam.Stop();
             NavigationService.Navigate(new Kolazh_Page(PhotoPath1,PhotoPath2,PhotoPath3));
         }
         ));


        private ICommand _testCommand;
        public ICommand TestCommand => _testCommand ?? (_testCommand = new Command((c =>
             {
                 PrintDialog printDlg = new PrintDialog();
                 printDlg.PrintVisual(TestGrid, "Grid Printing.");
             }
         )));
       

        private void StartCountdown(FrameworkElement target)
        {
            if(PhotoIndex!=3)
            {
                var countdownAnimation = new StringAnimationUsingKeyFrames();

                for (var i = 3; i >= 0; i--)
                {
                    var keyTime = TimeSpan.FromSeconds(3 - i);
                    var frame = new DiscreteStringKeyFrame(i.ToString(), KeyTime.FromTimeSpan(keyTime));
                    countdownAnimation.KeyFrames.Add(frame);
                }

                countdownAnimation.KeyFrames.Add(new DiscreteStringKeyFrame(" ",
                    KeyTime.FromTimeSpan(TimeSpan.FromSeconds(6))));
                Storyboard.SetTargetName(countdownAnimation, target.Name);
                Storyboard.SetTargetProperty(countdownAnimation, new PropertyPath(TextBlock.TextProperty));

                var countdownStoryboard = new Storyboard();
                countdownStoryboard.Children.Add(countdownAnimation);
                countdownStoryboard.Completed += CountdownTimer_Completed;
                countdownStoryboard.Begin(this);
            }
        }

        public static readonly DependencyProperty PhotoIndexProperty = DependencyProperty.Register(
            "PhotoIndex", typeof(int), typeof(Photo_Page), new PropertyMetadata(default(int)));

        public int PhotoIndex
        {
            get { return (int) GetValue(PhotoIndexProperty); }
            set { SetValue(PhotoIndexProperty, value); }
        }

        private void CountdownTimer_Completed(object sender, EventArgs e)
        {
            string PhotoAdress = "Photo " + DateTime.Now.ToLongTimeString().Replace(":", ".") + ".png";
            //LocalWebCam.Stop();
            using (FileStream stream = new FileStream(PhotoAdress, FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bi2));
                encoder.Save(stream);
            }

            switch (PhotoIndex)
            {
                case 0:
                    PhotoPath1 = Directory.GetCurrentDirectory() + "\\" + PhotoAdress;
                    PhotoIndex++;
                    StartCountdown(CountdownDisplay);
                    break;
                case 1:
                    PhotoPath2 = Directory.GetCurrentDirectory() + "\\" + PhotoAdress;
                    PhotoIndex++;
                    StartCountdown(CountdownDisplay);
                    break;
                case 2:
                    PhotoPath3 = Directory.GetCurrentDirectory() + "\\" + PhotoAdress;
                    PhotoIndex = 0;
                    string[] myArr = new[] {PhotoPath1, PhotoPath2, PhotoPath2};
                    KolazhButton.Visibility = Visibility.Visible;
                    CountdownBorder.Visibility = Visibility.Hidden;
                    PhotoAgainButton.Visibility = Visibility.Visible;
                    PhotoButton.Visibility = Visibility.Collapsed;

                    break;

            }
            
            LocalWebCam.Start();
            
        }

        public static readonly DependencyProperty PhotoPath1Property = DependencyProperty.Register(
            "PhotoPath1", typeof(string), typeof(Photo_Page), new PropertyMetadata(default(string)));

        public string PhotoPath1
        {
            get { return (string) GetValue(PhotoPath1Property); }
            set { SetValue(PhotoPath1Property, value); }
        }

        public static readonly DependencyProperty PhotoPath2Property = DependencyProperty.Register(
            "PhotoPath2", typeof(string), typeof(Photo_Page), new PropertyMetadata(default(string)));

        public string PhotoPath2
        {
            get { return (string) GetValue(PhotoPath2Property); }
            set { SetValue(PhotoPath2Property, value); }
        }

        public static readonly DependencyProperty PhotoPath3Property = DependencyProperty.Register(
            "PhotoPath3", typeof(string), typeof(Photo_Page), new PropertyMetadata(default(string)));

        public string PhotoPath3
        {
            get { return (string) GetValue(PhotoPath3Property); }
            set { SetValue(PhotoPath3Property, value); }
        }


        


        
    }
}
