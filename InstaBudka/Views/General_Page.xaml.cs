
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using AForge.Imaging.Filters;
using InstaBudka.Utilities;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using Selenium.EFWExtensions;
using Image = System.Drawing.Image;
using Rectangle = System.Drawing.Rectangle;

namespace InstaBudka.Views
{
    /// <summary>
    /// Логика взаимодействия для General_Page.xaml
    /// </summary>
    public partial class General_Page : Page
    {

        private bool IsElementPresent(By by)
        {
            try
            {

                if (App.CurrentApp.Browser.FindElement(by).Displayed)
                    return true;
                else
                    return false;

            }
            catch
            {
                return false;
            }
        }



        public void ChangeAdress(string NewAdress)
        {

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




        DispatcherTimer timer2 = new DispatcherTimer();
        DispatcherTimer timer = new DispatcherTimer();
        private string _login;

        public General_Page(string login)
        {
            InitializeComponent();

            _login = login;

            Unloaded += OnUnloaded;
        }

        private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            timer.Stop();
            timer2.Stop();
        }

        private async void TimerOnTick2(object sender, EventArgs e)
        {
            //var wait = new WebDriverWait(App.CurrentApp.Browser, new TimeSpan(99, 0, 0));
            //var element = wait.Until(condition =>
            //{
            //    try
            //    {
            //        var elementToBeDisplayed = App.CurrentApp.Browser.FindElement(By.ClassName("_97aPb "));
            //        return elementToBeDisplayed.Displayed;



            //        //     ((IJavaScriptExecutor) Browser).ExecuteScript(
            //        //         @"
            //        // function replaceTag( element, newTag )
            //        // {
            //        //     var elementNew = document.createElement( newTag );
            //        //     elementNew.innerHTML = element.innerHTML;

            //        //     Array.prototype.forEach.call( element.attributes, function( attr ) {
            //        //         elementNew.setAttribute( attr.name, attr.value );
            //        //     });

            //        //     element.parentNode.insertBefore( elementNew, element );
            //        //     element.parentNode.removeChild( element );
            //        //     return elementNew;
            //        // }
            //        // var avs = document.getElementsByClassName('_2dbep qNELH kIKUG');
            //        // var name = document.getElementsByClassName('FPmhX notranslate nJAzx')[0];

            //        //for (index = 0; index < avs.length; ++index) {
            //        //     replaceTag(avs[index], 'div');
            //        //     avs[index].removeAttribute('href');
            //        // }
            //        // replaceTag(name, 'div');
            //        // name.removeAttribute('href');
            //        // document.getElementsByClassName('bY2yH')[0].style.visibility = 'hidden';
            //        // var icons = document.getElementsByClassName('dCJp8 afkep _0mzm-');
            //        // icons[0].style.visibility = 'hidden';
            //        // icons[1].style.visibility = 'hidden';
            //        // icons[2].style.visibility = 'hidden';
            //        // icons[3].style.visibility = 'hidden';
            //        // icons[4].style.visibility = 'hidden';
            //        // document.getElementsByClassName('sH9wk  _JgwE ')[0].style.visibility = 'hidden';
            //        // var answer = document.getElementsByClassName('FH9sR');
            //        // for (index = 0; index < answer.length; ++index) {
            //        //     answer[index].style.visibility = 'hidden';
            //        // }
            //        // var temp1 = document.getElementsByClassName('vcOH2')[0];
            //        // if (!temp1) {document.getElementsByClassName('_0mzm- sqdOP yWX7d    _8A5w5    ')[0].style.visibility = 'hidden';}
            //        // else {temp1.style.visibility = 'hidden';}

            //        // document.getElementsByClassName('k_Q0X NnvRN')[0].style.visibility = 'hidden';


            //        // var btn = document.createElement('button');
            //        // btn.style.height = '50px';
            //        // btn.textContent = 'Напечатать';
            //        // btn.setAttribute('class', 'btnnew');
            //        // var temp = document.getElementsByClassName('btnnew')[0];
            //        // if (!temp){document.getElementsByClassName('eo2As ')[0].appendChild(btn);}


            //    }




            //}


            if (IsElementPresent(By.ClassName("bY2yH")))
            {
                // Проверка, видос ли это
                if (IsElementPresent(By.ClassName("QvAa1 ")))
                {
                    ((IJavaScriptExecutor) App.CurrentApp.Browser).ExecuteScript(
                        "document.getElementsByClassName('ckWGn')[0].click();");
                    return;
                }

                timer2.Stop();

                try
                {
                    App.CurrentApp.Browser.Manage().Window.Minimize();
                    await SaveImage("1.jpeg", ImageFormat.Jpeg);

                    Window_Chosen_Photo wnd = new Window_Chosen_Photo(name, text, date );
                    App.CurrentApp.Browser.Manage().Window.Minimize();

                    wnd.ShowDialog();
                    
                   
                    //NavigationService.Navigate(new Chose_Page());

                }
                catch (ExternalException)
                {
                    //Something is wrong with Format -- Maybe required Format is not 
                    // applicable here
                }
                catch (ArgumentNullException)
                {
                    //MessageBox.Show("kek");
                    //Something wrong with Stream
                }
                // Дожидаемся пока на появится крестик у фотографии и нажимаем на него
                while (!IsElementPresent(By.ClassName("ckWGn")))
                {
                    Thread.Sleep(500);
                }

                ((IJavaScriptExecutor) App.CurrentApp.Browser).ExecuteScript(
                    "document.getElementsByClassName('ckWGn')[0].click();");
                timer2.Start();
            }
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            //проверяем адрес
            if (App.CurrentApp.Browser.Url == "https://www.instagram.com/" ||
                App.CurrentApp.Browser.Url == "https://www.instagram.com" ||
                App.CurrentApp.Browser.PageSource.Contains("К сожалению, эта страница недоступна.") ||
                App.CurrentApp.Browser.Url.Contains("stories"))
            {
                timer.Stop();

                App.CurrentApp.Browser.Url = "auto:blank";
                //Thread.Sleep(500);
                //int hwnd = WinAPI.FindWindow("Chrome_WidgetWin_1", null);
                App.CurrentApp.Browser.Manage().Window.Minimize();
                NavigationService.GoBack();
                //if (hwnd != 0) WinAPI.ShowWindow(hwnd, SW_HIDE);



                timer.Start();
            }


        }




        public async Task SaveImage(string filename, ImageFormat format)
        {

            await Task.Run((async () =>
            {

                while (true)
                {
                    try
                    {
                        var test1 = App.CurrentApp.Browser.PageSource;
                        var b = App.CurrentApp.Browser.FindElements(By.ClassName("FFVAD")).Last().GetAttribute("src");
                        var a = (string) ((IJavaScriptExecutor) App.CurrentApp.Browser).ExecuteScript(
                            "return document.getElementsByClassName('KL4Bh')[0].children[0].getAttribute('src')"); //тут мы сэйвим картинку. которую открыли. но тут указан индекс нулевой для теста,но надо получать актуальный у блока фотки. на которую нажали и выгружать оттуда фотку
                        //вообще лучше отдельным окном повесить где-нибудь в углу кнопку печать и на нее команду , куски кода которой можно спиздить в kolazh page
                        //надо соединить фото, которое печатаем и его подпись с хаштэгами и вывести это на печать
                        WebClient client = new WebClient();


                        //Адрес картинки  class FFVAD, свойсто src
                        Stream stream = client.OpenRead(b);
                        Bitmap bitmap;
                        bitmap = new Bitmap(stream);

                        bitmap.Save(filename, format);

                        stream.Flush();
                        stream.Close();
                        client.Dispose();
                        //TakesScreenshot(App.CurrentApp.Browser, App.CurrentApp.Browser.FindElements(By.ClassName("_9AhH0")).Last());
                        var test = App.CurrentApp.Browser.PageSource;
                        if (File.Exists("screen.jpg"))
                            File.Delete("screen.jpg");
                        //App.CurrentApp.Browser.Manage().Window.Size = new Size(1091,);
                        if (IsElementPresent(By.ClassName("C4VMK")))
                            TakesScreenshot(App.CurrentApp.Browser,
                                App.CurrentApp.Browser.FindElement(
                                    By.ClassName("C4VMK"))); //Подпись + хаштэги скриншот делаем в папку с exe
                        break;
                    }
                    catch
                    {
                    }
                }
            }));

        }

        private string text;
        private string date;
        private string name;
        public void TakesScreenshot(IWebDriver driver, IWebElement element)
        {
            text = (string) ((IJavaScriptExecutor) App.CurrentApp.Browser).ExecuteScript("return document.getElementsByClassName('C4VMK')[0].children[1].textContent");
            date = (string)((IJavaScriptExecutor)App.CurrentApp.Browser).ExecuteScript("return document.getElementsByClassName('FH9sR Nzb55')[0].getAttribute('title')");
            name = (string)((IJavaScriptExecutor)App.CurrentApp.Browser).ExecuteScript("return document.getElementsByClassName('FPmhX notranslate TlrDj')[0].text");


            //((IJavaScriptExecutor) App.CurrentApp.Browser).ExecuteScript("arguments[0].scrollIntoView(true);",
            //    App.CurrentApp.Browser.FindElement(By.ClassName("C4VMK")));
            //Thread.Sleep(100);

            //string fileName = "screen.jpg";
            //Byte[] byteArray = ((ITakesScreenshot) driver).GetScreenshot().AsByteArray;
            //Bitmap screenshot = new Bitmap(new System.IO.MemoryStream(byteArray));
            //Rectangle croppedImage = new Rectangle(element.Location.X, element.Location.Y, element.Size.Width,
            //    element.Size.Height > 374 ? 374 : element.Size.Height);
            //screenshot = screenshot.Clone(croppedImage, screenshot.PixelFormat);
            //screenshot.Save(String.Format(fileName, ImageFormat.Jpeg));
        }

        private async void General_Page_OnLoaded(object sender, RoutedEventArgs ee)
        {
            App.CurrentApp.Browser.Manage().Window.Maximize();

            App.CurrentApp.Browser.Manage().Window.FullScreen();
            int hwnd = WinAPI.FindWindow("Chrome_WidgetWin_1", null);

            if (hwnd != 0) WinAPI.ShowWindow(hwnd, 3);


            //try
            //{



            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += TimerOnTick;
            timer.Start();

            timer2.Interval = TimeSpan.FromMilliseconds(100);
            timer2.Tick += TimerOnTick2;
            timer2.Start();

            if (App.CurrentApp.Browser == null)
            {
                App.CurrentApp.Browser = new OpenQA.Selenium.Chrome.ChromeDriver();
                App.CurrentApp.Browser.Manage().Window.Maximize();

                App.CurrentApp.Browser.Manage().Window.FullScreen();



            }

            if (_login.Contains("#"))
            {
                App.CurrentApp.Browser.Navigate().GoToUrl("https://www.instagram.com/explore/tags/" +
                                                          _login.Replace("#", string.Empty) +
                                                          "/?hl = ru");

                while (true)
                {
                    try
                    {


                        // TODO Поставить проверку, загрузилась ли страница, и если загрузилась, то можно продолжать
                        //((IJavaScriptExecutor)App.CurrentApp.Browser).ExecuteScript("document.body.style.zoom='150%';");
                        ((IJavaScriptExecutor) App.CurrentApp.Browser).ExecuteScript(
                            "document.getElementsByClassName('_8Rna9  _3Laht ')[0].parentElement.removeChild(document.getElementsByClassName('_8Rna9  _3Laht ')[0])");
                        ((IJavaScriptExecutor) App.CurrentApp.Browser).ExecuteScript(
                            "document.getElementsByClassName('r9-Os')[0].parentElement.removeChild(document.getElementsByClassName('r9-Os')[0])");
                        ((IJavaScriptExecutor) App.CurrentApp.Browser).ExecuteScript(
                            @"
                    document.getElementsByClassName('LWmhU _0aCwM')[0].parentElement.removeChild(document.getElementsByClassName('LWmhU _0aCwM')[0]);
                    document.getElementsByClassName('id8oV ')[0].parentElement.removeChild(document.getElementsByClassName('id8oV ')[0]);
                    var classHren = '      tHaIX            Igw0E     IwRSH   pmxbr     YBx95       _4EzTm                                      CIRqI                  IY_1_                           XfCBB             HcJZg        O1flK D8xaz  _7JkPY  TxciK  N9d2H ZUqME ';
                    if(document.getElementsByClassName(classHren)[0]) document.getElementsByClassName(classHren)[0].parentElement.removeChild(document.getElementsByClassName(classHren)[0]);
                    
                    document.getElementsByClassName('oJZym')[0].removeChild(document.getElementsByClassName('oJZym')[0].children[0]);
                    var img = document.createElement('img');
                    img.setAttribute('src', 'https://image.flaticon.com/icons/png/512/93/93634.png');
                    img.setAttribute('width', '28px');
                    img.setAttribute('onclick', 'document.location.href = \'/\'');

                    document.getElementsByClassName('oJZym')[0].appendChild(img);

");

                        break;
                    }
                    catch (Exception e)
                    {
                        if (string.IsNullOrEmpty(App.CurrentApp.Browser.PageSource))
                        {
                            App.CurrentApp.Browser.Navigate().Refresh();
                            await Task.Delay(3000);
                        }
                        if (App.CurrentApp.Browser.PageSource.Contains("К сожалению, эта страница недоступна."))
                            break;
                    }
                }
            }
            else
            {
                App.CurrentApp.Browser.Navigate()
                    .GoToUrl(
                        "https://www.instagram.com/" + _login + "/");
                // TODO Поставить проверку, загрузилась ли страница, и если загрузилась, то можно продолжать
                //((IJavaScriptExecutor) App.CurrentApp.Browser).ExecuteScript("document.body.style.zoom='150%';");
                await Task.Delay(2000);
                while (true)
                {
                    try
                    {


                        ((IJavaScriptExecutor) App.CurrentApp.Browser).ExecuteScript(
                            "document.getElementsByClassName('_8Rna9  _3Laht ')[0].parentElement.removeChild(document.getElementsByClassName('_8Rna9  _3Laht ')[0])");
                        ((IJavaScriptExecutor) App.CurrentApp.Browser).ExecuteScript(
                            "document.getElementsByClassName('r9-Os')[0].parentElement.removeChild(document.getElementsByClassName('r9-Os')[0])");
                        ((IJavaScriptExecutor) App.CurrentApp.Browser).ExecuteScript(
                            "document.getElementsByClassName('fx7hk')[0].parentElement.removeChild(document.getElementsByClassName('fx7hk')[0])");
                        ((IJavaScriptExecutor) App.CurrentApp.Browser).ExecuteScript(
                            "document.getElementsByClassName('BY3EC ')[0].parentElement.removeChild(document.getElementsByClassName('BY3EC ')[0])");

                        ((IJavaScriptExecutor) App.CurrentApp.Browser).ExecuteScript(
                            "document.getElementsByClassName('LWmhU _0aCwM')[0].parentElement.removeChild(document.getElementsByClassName('LWmhU _0aCwM')[0]);");

                        ((IJavaScriptExecutor) App.CurrentApp.Browser).ExecuteScript(
                            @"var classHren = '      tHaIX            Igw0E     IwRSH   pmxbr     YBx95       _4EzTm                                      CIRqI                  IY_1_                           XfCBB             HcJZg        O1flK D8xaz  _7JkPY  TxciK  N9d2H ZUqME ';
                if (document.getElementsByClassName(classHren)[0]) document.getElementsByClassName(classHren)[0].parentElement.removeChild(document.getElementsByClassName(classHren)[0]);");




                        ((IJavaScriptExecutor) App.CurrentApp.Browser).ExecuteScript(
                            @"
                var classHren = '      tHaIX            Igw0E     IwRSH   pmxbr     YBx95       _4EzTm                                      CIRqI                  IY_1_                           XfCBB             HcJZg        O1flK D8xaz  _7JkPY  TxciK  N9d2H ZUqME ';
                if(document.getElementsByClassName(classHren)[0]) document.getElementsByClassName(classHren)[0].parentElement.removeChild(document.getElementsByClassName(classHren)[0]);
                

                document.getElementsByClassName('-nal3 ')[0].removeAttribute('href');
                document.getElementsByClassName('-nal3 ')[1].removeAttribute('href');
                document.getElementsByClassName('-nal3 ')[2].removeAttribute('href');

                function replaceTag( element, newTag )
                {
                    var elementNew = document.createElement( newTag );
                    elementNew.innerHTML = element.innerHTML;

                    Array.prototype.forEach.call( element.attributes, function( attr ) {
                        elementNew.setAttribute( attr.name, attr.value );
                    });

                    element.parentNode.insertBefore( elementNew, element );
                    element.parentNode.removeChild( element );
                    return elementNew;
                }
                
                replaceTag(document.getElementsByClassName('oJZym')[0].children[0], 'div');
                replaceTag(document.getElementsByClassName('-nal3 ')[0], 'div');
                replaceTag(document.getElementsByClassName('-nal3 ')[1], 'div');
                replaceTag(document.getElementsByClassName('-nal3 ')[2], 'div');
                

                document.getElementsByClassName('oJZym')[0].removeChild(document.getElementsByClassName('oJZym')[0].children[0]);
                var img = document.createElement('img');
                img.setAttribute('src', 'https://image.flaticon.com/icons/png/512/93/93634.png');
                img.setAttribute('width', '28px');
                img.setAttribute('onclick', 'document.location.href = \'/\'');

                document.getElementsByClassName('oJZym')[0].appendChild(img);

                document.getElementsByClassName('-vDIg')[0].parentElement.removeChild(document.getElementsByClassName('-vDIg')[0]);
                        
                var element=document.getElementsByClassName('_4bSq7')[0];
                if(element){element.parentElement.removeChild(element)}

");
                        break;
                    }
                    catch (Exception e)
                    {
                        //if (e.Message.Contains("Cannot read property 'parentElement' of undefined"))
                        //{
                        //    App.CurrentApp.Browser.Navigate().Refresh();
                        //    await Task.Delay(5000);

                        //}
                        App.CurrentApp.Browser.Manage().Window.Minimize();
                        NavigationService.Navigate(new Chose_Page());
                        break;

                        if (App.CurrentApp.Browser.PageSource.Contains("К сожалению, эта страница недоступна."))
                            break;

                    }
                }
            }
        }
    }
}
