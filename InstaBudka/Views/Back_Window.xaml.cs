
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
using System.Windows.Shapes;
using VICH_Johnson.Utilities;

namespace InstaBudka.Views
{
    /// <summary>
    /// Логика взаимодействия для Back_Window.xaml
    /// </summary>
    public partial class Back_Window : Window
    {
        public Back_Window()
        {
            InitializeComponent();
        }



        private ICommand _backCommand;

        public ICommand BackCommand => _backCommand ?? (_backCommand = new Command((c =>
              {

                  Close();
                   (App.Current.MainWindow as MainWindow).Topmost = true;
                   

               }
           )));

    }
}
