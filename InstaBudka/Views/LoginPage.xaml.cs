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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InstaBudka.Utilities;

namespace InstaBudka.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {


        public string login { get; set; }
        public ICommand _instagramCommand;

        private ICommand _backCommand;
        public ICommand BackCommand => _backCommand ?? (_backCommand = new Command((c =>
             {
                 NavigationService.Navigate(new Chose_Page());

             }
         )));


        public ICommand InstagramCommand => _instagramCommand ?? (_instagramCommand = new Command((c =>
        {
            NavigationService?.Navigate(new General_Page(login));
        }
        )));
        
        private ICommand _searchByNickCommand;

        public ICommand SearchByNickCommand => _searchByNickCommand ?? (_searchByNickCommand = new Command((c =>
             {
                 NavigationService.Navigate(new General_Page(TextBoxNick.Text));
                 //LoadingBorder.Visibility = Visibility.Visible;
                 TextBoxHush.Text = string.Empty;
                 TextBoxNick.Text = string.Empty;
                 (App.Current.MainWindow as MainWindow).Topmost = false;
                 //LoadingBorder.Visibility = Visibility.Collapsed;

             }
         )));


                 private ICommand _searchByHushTagCommand;

        public ICommand SearchByHushTagCommand => _searchByHushTagCommand ?? (_searchByHushTagCommand = new Command((c =>
             {
                 NavigationService.Navigate(new General_Page("#"+TextBoxHush.Text.Replace("#", "")));
                 TextBoxHush.Text = string.Empty;
                 TextBoxNick.Text = string.Empty;
             }
         )));


        public LoginPage()
        {
            InitializeComponent();
        }
    }
}
