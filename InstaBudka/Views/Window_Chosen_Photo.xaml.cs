﻿using System;
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
            NameImage = new BitmapImage(new Uri($"file:///{Directory.GetCurrentDirectory()}\\1.jpeg"));
            if(File.Exists("screen.jpg"))
            ScreenImage =new BitmapImage(new Uri("file:///"+Directory.GetCurrentDirectory() + "\\screen.jpg"));
        }

        private void MakeScreenElement(FrameworkElement elem)
        {
            RenderTargetBitmap renderTargetBitmap =
                new RenderTargetBitmap((int)elem.Width, (int)elem.Height, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(elem);
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            using (Stream fileStream = File.Create("readyFile.png"))
            {
                pngImage.Save(fileStream);
            }

        }


        public static readonly DependencyProperty ScreenImageProperty = DependencyProperty.Register(
            "ScreenImage", typeof(BitmapImage), typeof(Window_Chosen_Photo), new PropertyMetadata(default(BitmapImage)));

        public BitmapImage ScreenImage
        {
            get => (BitmapImage) GetValue(ScreenImageProperty);
            set => SetValue(ScreenImageProperty, value);
        }

        public static readonly DependencyProperty NameImageProperty = DependencyProperty.Register(
            "NameImage", typeof(BitmapImage), typeof(Window_Chosen_Photo), new PropertyMetadata(default(BitmapImage)));

        public BitmapImage NameImage
        {
            get => (BitmapImage) GetValue(NameImageProperty);
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
            PrintDocument pd = new PrintDocument();
            //пробуй и true и false
            pd.OriginAtMargins = false;
            pd.PrintPage += PrintPage;
            PrintDialog printDialog = new PrintDialog();
            Border.Visibility = Visibility.Hidden;
            Border.VerticalAlignment = VerticalAlignment.Top;
            Border.HorizontalAlignment = HorizontalAlignment.Left;

            // Увеличить размер в 5 раз
            Border.Margin = new Thickness(0, -50, 0, 0);
            TransformGroup group = new TransformGroup();
            //group.Children.Add(new RotateTransform(270));
            group.Children.Add(new ScaleTransform(0.625, 0.625));

            Border.LayoutTransform = group;
            // Определить поля
            int pageMargin = 0;

            // Получить размер страницы
            System.Windows.Size pageSize = new System.Windows.Size(printDialog.PrintableAreaWidth,
                printDialog.PrintableAreaHeight);

            // Инициировать установку размера элемента
            Border.Measure(pageSize);
            Border.Arrange(new Rect(pageMargin, pageMargin, pageSize.Width, pageSize.Height));
            Border.Visibility = Visibility.Visible;
            MakeScreenElement(Border);

            pd.Print();
            App.CurrentApp.Browser.Manage().Window.Minimize();
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
