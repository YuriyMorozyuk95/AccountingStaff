using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using Rfid.Properties;

namespace Rfid
{
    public partial class App
    {
        public App()
        {
            LanguageChanged += App_LanguageChanged;
            Languages.Clear();
            Languages.Add(new CultureInfo("en-US"));
            Languages.Add(new CultureInfo("uk-UA"));
            Languages.Add(new CultureInfo("ru-RU"));
        }

        public static List<CultureInfo> Languages { get; } = new List<CultureInfo>();

        public static CultureInfo Language
        {
            get { return Thread.CurrentThread.CurrentUICulture; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (value == Thread.CurrentThread.CurrentUICulture)
                {
                    return;
                }
                //2. Создаём ResourceDictionary для новой культуры
                Thread.CurrentThread.CurrentUICulture = value;
                var dict = new ResourceDictionary();
                switch (value.Name)
                {
                    case "uk-UA":
                    {
                        dict.Source = new Uri($"Resource/lang.{value.Name}.xaml", UriKind.Relative);
                        break;
                    }
                    case "ru-RU":
                    {
                        dict.Source = new Uri($"Resource/lang.{value.Name}.xaml", UriKind.Relative);
                        break;
                    }
                    default:
                    {
                        dict.Source = new Uri("Resource/lang.xaml", UriKind.Relative);
                        break;
                    }
                }
                //3. Находим старую ResourceDictionary и удаляем его и добавляем новую ResourceDictionary
                var oldDict = (from d in Current.Resources.MergedDictionaries
                    where d.Source != null && d.Source.OriginalString.StartsWith("Resource/lang.")
                    select d).First();

                if (oldDict != null)
                {
                    var index = Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Current.Resources.MergedDictionaries.Remove(oldDict);
                    Current.Resources.MergedDictionaries.Insert(index, dict);
                }
                else
                {
                    Current.Resources.MergedDictionaries.Add(dict);
                }
                LanguageChanged(Current, new EventArgs());
            }
        }

        public static event EventHandler LanguageChanged;

        private void App_LanguageChanged(object sender, EventArgs e)
        {
            Settings.Default.DefaultLanguage = Language;
        }

        private void App_LoadCompleted(object sender, NavigationEventArgs e)
        {
            Language = Settings.Default.DefaultLanguage;
        }
    }
}
