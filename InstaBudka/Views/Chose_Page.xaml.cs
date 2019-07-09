using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InstaBudka.Utilities;

namespace InstaBudka.Views
{
    /// <summary>
    /// Логика взаимодействия для Chose_Page.xaml
    /// </summary>
    public partial class Chose_Page : Page
    {

        
        public Chose_Page()
        {
           
            InitializeComponent();
            const int SW_MAXIMIZE = 9;
            const int SW_MINIMIZE = 8;




            int hwnd = WinAPI.FindWindow("Chrome_WidgetWin_1", null);
            if (hwnd != 0) WinAPI.ShowWindow(hwnd, 0);


            WinAPI.ShowWindow(hwnd, 0);
            Loaded+= OnLoaded;


        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var test = Directory.GetCurrentDirectory();
            var allphoto = Directory.GetFiles(Directory.GetCurrentDirectory());
            foreach (string s in allphoto)
            {
                if(s.Contains(".jpg")||s.Contains(".png")||s.Contains(".jpeg"))
                try
                {
                    File.Delete(s);
                }
                catch (Exception e)
                {

                }
            }

        }


        public static class WinAPI

        {

            [DllImport("user32.dll", SetLastError = true)]
            internal static extern int FindWindow(string lpClassName, string lpWindowName);

            [DllImport("user32.dll", SetLastError = true)]
            internal static extern int ShowWindow(int hwnd, int nCmdShow);
            [DllImport("user32.dll")]
            private static extern
                bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        }



        private ICommand _photoCommand;
        public ICommand PhotoCommand => _photoCommand ?? (_photoCommand = new Command((c =>
           {
               App.CurrentApp.Kw = new Kollazh_Window();

               //NavigationService.Navigate(new Photo_Page());
               App.CurrentApp.Kw.Show();
               App.CurrentApp.Kw.Topmost = true;
           }
       )));

        private ICommand _instagramCommand;
       


        private ICommand _loginCommand;
        public ICommand LoginCommand => _instagramCommand ?? (_loginCommand = new Command((c =>
        {
            NavigationService.Navigate(new LoginPage());
        }
         )));

    }
}
