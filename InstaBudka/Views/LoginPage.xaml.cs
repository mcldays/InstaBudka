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
using VICH_Johnson.Utilities;

namespace InstaBudka.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {


        public string login { get; set; }
        public ICommand _instagramCommand;

        

        public ICommand InstagramCommand => _instagramCommand ?? (_instagramCommand = new Command((c =>
        {
            NavigationService.Navigate(new General_Page(login));
        }
        )));


        public LoginPage()
        {
            InitializeComponent();
        }
    }
}
