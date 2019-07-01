using System;
using System.Collections.Generic;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InstaBudka.Utilities;
using InstaBudka.Views;

namespace InstaBudka
{
    /// <summary>
    /// Логика взаимодействия для Window_Chosen_Photo.xaml
    /// </summary>
    public partial class Window_Chosen_Photo : Window
    {
        public Window_Chosen_Photo()
        {
            int hwnd = General_Page.WinAPI.FindWindow("Chrome_WidgetWin_1", null);
            if (hwnd != 0) Chose_Page.WinAPI.ShowWindow(hwnd, 0);



            InitializeComponent();
            NameImage = "{Directory.GetCurrentDirectory()}\\1.jpeg";
        }

        public static readonly DependencyProperty NameImageProperty = DependencyProperty.Register(
            "NameImage", typeof(string), typeof(Window_Chosen_Photo), new PropertyMetadata(default(string)));

        public string NameImage
        {
            get => (string) GetValue(NameImageProperty);
            set => SetValue(NameImageProperty, value);
        }

        private ICommand _backCommand;
        public ICommand BackCommand => _backCommand ?? (_backCommand = new Command((c =>
                                               {
                                                   Close();

                                               }
                                           )));

        private ICommand _printCommand;
        public ICommand PrintCommand => _printCommand ?? (_printCommand = new Command((c =>
        {
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.UriSource = new Uri($"file:///{Directory.GetCurrentDirectory()}\\1.jpeg");
            bi.EndInit();

            var vis = new DrawingVisual();
            using (var dc = vis.RenderOpen())
            {
                dc.DrawImage(bi, new Rect { Width = bi.Width, Height = bi.Height });
            }

            var pdialog = new PrintDialog();
            if (pdialog.ShowDialog() == true)
            {
                pdialog.PrintVisual(vis, "Instagram");
            }

            (App.Current.MainWindow as MainWindow).Frame1.Navigate(new Window_Chosen_Photo());
            Close();
        }
    )));
    }
}
