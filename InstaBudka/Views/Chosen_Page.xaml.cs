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
    /// Логика взаимодействия для Chosen_Page.xaml
    /// </summary>
    public partial class Chosen_Page : Page
    {
        public Chosen_Page()
        {
            InitializeComponent();
        }

        private ICommand _printCommand;
        public ICommand PrintCommand => _printCommand ?? (_printCommand = new Command((c =>
           {
               
           }
       )));

    }
}
