using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InstaBudka
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App CurrentApp => App.Current as App;

       private  IWebDriver _browser;
       public IWebDriver Browser {
           get =>_browser;
           set { _browser = value; }

       }
    }
}
