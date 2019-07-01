using InstaBudka.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace InstaBudka.Views
{
    /// <summary>
    /// Логика взаимодействия для Kolazh_Page.xaml
    /// </summary>
    public partial class Kolazh_Page : Page
    {
        public Kolazh_Page( string photo1,string photo2,string photo3)
        {
            InitializeComponent();
            Photo1 = photo1;
            Photo2 = photo2;
            Photo3 = photo3;
        }

        public static readonly DependencyProperty FonProperty = DependencyProperty.Register(
            "Fon", typeof(string), typeof(Kolazh_Page), new PropertyMetadata(default(string)));

        public string Fon
        {
            get { return (string) GetValue(FonProperty); }
            set { SetValue(FonProperty, value); }
        }

        public static readonly DependencyProperty AllFonsProperty = DependencyProperty.Register(
            "AllFons", typeof(List<string>), typeof(Kolazh_Page), new PropertyMetadata(default(List<string>)));

        public List<string> AllFons
        {
            get { return (List<string>) GetValue(AllFonsProperty); }
            set { SetValue(AllFonsProperty, value); }
        }

        public static readonly DependencyProperty Photo1Property = DependencyProperty.Register(
            "Photo1", typeof(string), typeof(Kolazh_Page), new PropertyMetadata(default(string)));

        public string Photo1
        {
            get { return (string) GetValue(Photo1Property); }
            set { SetValue(Photo1Property, value); }
        }

        public static readonly DependencyProperty Photo2Property = DependencyProperty.Register(
            "Photo2", typeof(string), typeof(Kolazh_Page), new PropertyMetadata(default(string)));

        public string Photo2
        {
            get { return (string) GetValue(Photo2Property); }
            set { SetValue(Photo2Property, value); }
        }

        public static readonly DependencyProperty Photo3Property = DependencyProperty.Register(
            "Photo3", typeof(string), typeof(Kolazh_Page), new PropertyMetadata(default(string)));

        public string Photo3
        {
            get { return (string) GetValue(Photo3Property); }
            set { SetValue(Photo3Property, value); }
        }

        private void Kolazh_Page_OnLoaded(object sender, RoutedEventArgs e)
        {
            AllFons = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Fons").ToList();
            MiniFons = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Fons\\Mini").ToList();

            List<FonModel> AllFonModels2 = new List<FonModel>();
            for (int i = 0; i < 15; i++)
            {
                FonModel FM = new FonModel(){PathToImage = MiniFons[i]};
                AllFonModels2.Add(FM);

            }

            AllFonModels = AllFonModels2;


        }

        public static readonly DependencyProperty MiniFonsProperty = DependencyProperty.Register(
            "MiniFons", typeof(List<string>), typeof(Kolazh_Page), new PropertyMetadata(default(List<string>)));

        public List<string> MiniFons
        {
            get { return (List<string>) GetValue(MiniFonsProperty); }
            set { SetValue(MiniFonsProperty, value); }
        }

        public static readonly DependencyProperty AllFonModelsProperty = DependencyProperty.Register(
            "AllFonModels", typeof(List<FonModel>), typeof(Kolazh_Page), new PropertyMetadata(default(List<FonModel>)));

        public List<FonModel> AllFonModels
        {
            get { return (List<FonModel>) GetValue(AllFonModelsProperty); }
            set { SetValue(AllFonModelsProperty, value); }
        }

        private ICommand _printCommand;
        public ICommand PrintCommand => _printCommand ?? (_printCommand = new Command((c =>
            {

                //PrintDocument pd = new PrintDocument();
                ////pd.DefaultPageSettings.PrinterSettings.PrinterName = "Printer Name";
                //pd.DefaultPageSettings.Landscape = true; //or false!
                //pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
                //pd.PrintPage += (sender, args) =>
                //{
                //    System.Drawing.Image i = System.Drawing.Image.FromFile("C: \\Users\\spira\\Desktop\\My\\work\\InstaBudka\\InstaBudka\\bin\\Debug\\1.jpg");
                //    System.Drawing.Rectangle m = args.PageBounds;


                //    m.Height = (int)((double)i.Height / (double)i.Width * (double)m.Width);

                //    args.Graphics.DrawImage(i, m);
                //};
                //pd.Print();
                

                PrintDialog printDialog = new PrintDialog();
                
                GlobalGrid.Visibility = Visibility.Hidden;
                PrintCanvas.VerticalAlignment = VerticalAlignment.Top;
                PrintCanvas.HorizontalAlignment = HorizontalAlignment.Left;
                // Увеличить размер в 5 раз
                PrintGrid.LayoutTransform = new RotateTransform(270);
                PrintCanvas.LayoutTransform = new ScaleTransform(0.625, 0.625);
                SecondGrid.Visibility = Visibility.Collapsed;
                // Определить поля
                int pageMargin = 0;

                // Получить размер страницы
                System.Windows.Size pageSize = new System.Windows.Size(printDialog.PrintableAreaWidth,
                    printDialog.PrintableAreaHeight);

                // Инициировать установку размера элемента
                PrintCanvas.Measure(pageSize);
                PrintCanvas.Arrange(new Rect(pageMargin, pageMargin, pageSize.Width, pageSize.Height));

                // Напечатать элемент
                printDialog.PrintVisual(PrintCanvas, "Распечатываем элемент Canvas");


                // Удалить трансформацию и снова сделать элемент видимым
                SecondGrid.Visibility = Visibility.Visible;

                PrintCanvas.LayoutTransform = null;
                PrintGrid.LayoutTransform = null;



                PrintCanvas.VerticalAlignment = VerticalAlignment.Top;
                PrintCanvas.HorizontalAlignment = HorizontalAlignment.Center;
                GlobalGrid.Visibility = Visibility.Visible;
            }
        )));


        private ICommand _changeFonCommand;
        public ICommand ChangeFonCommand => _changeFonCommand ?? (_changeFonCommand = new Command((c =>
        {
            Fon = AllFons[int.Parse(c.ToString())];
        }
        )));

        private ICommand _backCommand;
        public ICommand BackCommand => _backCommand ?? (_backCommand = new Command((c =>
              {
                  NavigationService.Navigate(new Photo_Page());

              }
          )));

        private ICommand _addTextCommand;
        public ICommand AddTextCommand => _addTextCommand ?? (_addTextCommand = new Command((c =>
            {
                TextBox.Text = "Введите описание";
                TextBox.Visibility = Visibility.Visible;
                AddText.Visibility = Visibility.Collapsed;
                DeleteText.Visibility = Visibility.Visible;

            }
        )));

        private ICommand _deleteTextCommand;
        public ICommand DeleteTextCommand => _deleteTextCommand ?? (_deleteTextCommand = new Command((c =>
                   {
                       TextBox.Text = "Введите описание";
                       TextBox.Visibility = Visibility.Collapsed;
                       AddText.Visibility = Visibility.Visible;
                       DeleteText.Visibility = Visibility.Collapsed;
                   }
         )));



    }
}
