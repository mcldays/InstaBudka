using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
            NameImage = $"{Directory.GetCurrentDirectory()}\\1.jpeg";
        }

        private void MakeScreenElement(FrameworkElement elem)
        {
            RenderTargetBitmap renderTargetBitmap =
                new RenderTargetBitmap((int)elem.Width, (int)elem.Height, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(elem);
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            using (Stream fileStream = File.Create("fileKolazh.png"))
            {
                pngImage.Save(fileStream);
            }


        }


        public static readonly DependencyProperty ScreenImageProperty = DependencyProperty.Register(
            "ScreenImage", typeof(string), typeof(Window_Chosen_Photo), new PropertyMetadata(Directory.GetCurrentDirectory()+"screen.jpg"));

        public string ScreenImage
        {
            get => (string) GetValue(ScreenImageProperty);
            set => SetValue(ScreenImageProperty, value);
        }

        public static readonly DependencyProperty NameImageProperty = DependencyProperty.Register(
            "NameImage", typeof(string), typeof(Window_Chosen_Photo), new PropertyMetadata(default(string)));

        public string NameImage
        {
            get => (string) GetValue(NameImageProperty);
            set => SetValue(NameImageProperty, value);
        }

        private ICommand _printCommand;
        public ICommand PrintCommand => _printCommand ?? (_printCommand = new Command((c =>
        {
            MakeScreenElement(Border);
            PrintDocument pd = new PrintDocument();
            //пробуй и true и false
            pd.OriginAtMargins = false;
            pd.PrintPage += PrintPage;
            pd.Print();

            (App.Current.MainWindow as MainWindow).Frame1.Navigate(new Chose_Page());
            Close();
        }

    )));

        private void PrintPage(object o, PrintPageEventArgs e)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(Directory.GetCurrentDirectory() + "\\readyFile.png");
            System.Drawing.Point loc = new System.Drawing.Point(0, 0);
            e.Graphics.DrawImage(img, loc);
        }
    }
}
