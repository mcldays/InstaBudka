
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
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Image = System.Drawing.Image;
using Rectangle = System.Drawing.Rectangle;

namespace InstaBudka.Views
{
    /// <summary>
    /// Логика взаимодействия для General_Page.xaml
    /// </summary>
    public partial class General_Page : Page
    {
        IWebDriver Browser;

        public General_Page()
        {
            InitializeComponent();

            Browser = new OpenQA.Selenium.Chrome.ChromeDriver();
            Browser.Manage().Window.Maximize();
            Browser.Manage().Window.FullScreen();

            Browser.Navigate()
                .GoToUrl(
                    "https://www.instagram.com/serkser70/"); //для теста пиздовали на страницу Сереги. надо между  Chose_Page  и этой сделать промежуточную, где
            //вводится ник человека или хаштег. по которому ищут публикации
            ((IJavaScriptExecutor) Browser).ExecuteScript("document.body.style.zoom='150%';");
            ((IJavaScriptExecutor) Browser).ExecuteScript(
                "document.getElementsByClassName('_8Rna9  _3Laht ')[0].parentElement.removeChild(document.getElementsByClassName('_8Rna9  _3Laht ')[0])");
            ((IJavaScriptExecutor) Browser).ExecuteScript(
                "document.getElementsByClassName('r9-Os')[0].parentElement.removeChild(document.getElementsByClassName('r9-Os')[0])");
            ((IJavaScriptExecutor) Browser).ExecuteScript(
                "document.getElementsByClassName('fx7hk')[0].parentElement.removeChild(document.getElementsByClassName('fx7hk')[0])");
            ((IJavaScriptExecutor) Browser).ExecuteScript(
                "document.getElementsByClassName('BY3EC ')[0].parentElement.removeChild(document.getElementsByClassName('BY3EC ')[0])");


            ((IJavaScriptExecutor)Browser).ExecuteScript(
                @"
                var classHren = '      tHaIX            Igw0E     IwRSH   pmxbr     YBx95       _4EzTm                                      CIRqI                  IY_1_                           XfCBB             HcJZg        O1flK D8xaz  _7JkPY  TxciK  N9d2H ZUqME ';
                if(document.getElementsByClassName(classHren)[0]) document.getElementsByClassName(classHren)[0].parentElement.removeChild(document.getElementsByClassName(classHren)[0]);
                document.getElementsByClassName('LWmhU _0aCwM')[0].parentElement.removeChild(document.getElementsByClassName('LWmhU _0aCwM')[0]);
                document.getElementsByClassName('oJZym')[0].children[0].removeAttribute('href');
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

            ");

            //убираем все лишние кнопки. чтобы челик далеко не ушел
            var a = Browser.PageSource;
            //событие открытия одной из фотографий, обычно я ставил тут точку остановы, тыкал на фотку и продолжал тесы, нужно щелчок по определенному фото вынести в отдельное событие

            while (true)
            {
                var wait = new WebDriverWait(Browser, new TimeSpan(99, 0, 0));
                var element = wait.Until(condition =>
                {
                    try
                    {
                        var elementToBeDisplayed = Browser.FindElement(By.ClassName("_97aPb "));
                        return elementToBeDisplayed.Displayed;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return false;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });
                if(element is false) continue;
                
            }

            var c = Browser.FindElement(By.ClassName("C4VMK"));
            if (c != null)
            TakesScreenshot(Browser, Browser.FindElement(By.ClassName("C4VMK"))); //Подпись + хаштэги скриншот делаем в папку с exe
            try
            {

                SaveImage("1.jpeg", ImageFormat.Jpeg);

            }
            catch (ExternalException)
            {
                //Something is wrong with Format -- Maybe required Format is not 
                // applicable here
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("kek");
                //Something wrong with Stream
            }
        }

        public void SaveImage(string filename, ImageFormat format)
        {
           var a= (string)((IJavaScriptExecutor) Browser).ExecuteScript(
                "return document.getElementsByClassName('KL4Bh')[a].children[0].getAttribute('src')");//тут мы сэйвим картинку. которую открыли. но тут указан индекс нулевой для теста,но надо получать актуальный у блока фотки. на которую нажали и выгружать оттуда фотку
           //вообще лучше отдельным окном повесить где-нибудь в углу кнопку печать и на нее команду , куски кода которой можно спиздить в kolazh page
           //надо соединить фото, которое печатаем и его подпись с хаштэгами и вывести это на печать
            WebClient client = new WebClient();
                //Адрес картинки  class FFVAD, свойсто src
            Stream stream = client.OpenRead(a);
            Bitmap bitmap; bitmap = new Bitmap(stream);

            if (bitmap != null)
                bitmap.Save(filename, format);

            stream.Flush();
            stream.Close();
            client.Dispose();
        }

        public void TakesScreenshot(IWebDriver driver, IWebElement element)
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".jpg";
            Byte[] byteArray = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
            Bitmap screenshot = new Bitmap(new System.IO.MemoryStream(byteArray));
            Rectangle croppedImage = new Rectangle(element.Location.X, element.Location.Y, element.Size.Width, element.Size.Height);
            screenshot = screenshot.Clone(croppedImage, screenshot.PixelFormat);
            screenshot.Save(String.Format(fileName, ImageFormat.Jpeg));
        }

    }
}
