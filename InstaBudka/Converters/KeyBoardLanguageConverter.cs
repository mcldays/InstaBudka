using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;

namespace InstaBudka.Converters 
{
    class KeyBoardLanguageConverter: IMultiValueConverter
    {
        [ValueConversion(typeof(string), typeof(FlowDocument))]
        public class FlowDocumentConverter : IValueConverter
        {
            #region IValueConverter Members

            public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                var flowDocument = new FlowDocument();
                if (value != null)
                {
                    var xamlText = (string)value;
                    flowDocument = (FlowDocument)XamlReader.Parse(xamlText);
                }
                return flowDocument;
            }

            public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value == null) return string.Empty;
                var flowDocument = (FlowDocument)value;

                return XamlWriter.Save(flowDocument);
            }

            #endregion
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 0) return null;
            if (values[1] == null || (string) values[1]== "pic") return null;
            return Equals((CultureInfo)values[0], CultureInfo.GetCultureInfo("ru-RU")) ? _allWord[(string)values[1]] : (string)values[1];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }


        private readonly Dictionary<string, string> _allWord = new Dictionary<string, string>()
        {
            {"q","й"},
            {"w","ц" },
            {"e","у" },
            {"r","к" },
            {"t","е" },
            {"y","н" },
            {"u","г" },
            {"i","ш" },
            {"o","щ" },
            {"p","з" },
            {"a","ф" },
            {"s","ы" },
            {"d","в" },
            {"f","а" },
            {"g","п" },
            {"h","р" },
            {"j","о" },
            {"k","л" },
            {"l","д" },
            {"z","я" },
            {"x","ч" },
            {"c","с" },
            {"v","м" },
            {"b","и" },
            {"n","т" },
            {"m","ь" },
            {"`", "ё" },
            {"1", "1" },
            {"2", "2" },
            {"3", "3" },
            {"4", "4" },
            {"5", "5" },
            {"6", "6" },
            {"7", "7" },
            {"8", "8" },
            {"9", "9" },
            {"0", "0" },
            {"-", "-" },
            {"_", "_" },
            {"Back", "Удалить" },
            {"Tab", "Tab" },
            {"\\", "\\" },
            {"CapsLock", "CapsLock" },
            {"Enter", "Ввод" },
            {"Shift", "Shift" },
            {"Русский", "English" },
            {"@", "@" },
            {"[", "х" },
            {"]", "ъ" },
            {";", "ж" },
            {"'", "э" },
            {",", "б" },
            {".", "ю" },
            {" ", " " },
            {"?", "." },

        };
    }
}
