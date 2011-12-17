using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.ComponentModel;

namespace TimeCnvtr
{
    public partial class MainPage : PhoneApplicationPage
    {
        long currentTimeTicks;
        bool conversionLock = false;
        const int FROM_CONVERSION = 0;
        const int TO_CONVERSION = 1;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            ContentPanel.DataContext = App.Converter;

            frmTime.Value = DateTime.Now;
            currentTimeTicks = DateTime.Now.Ticks;
            toTime.Value = DateTime.Now;

            System.Diagnostics.Debug.WriteLine(System.TimeZoneInfo.Local.ToString());
            dateChange.Text = string.Empty;
            App.Converter.PropertyChanged += new PropertyChangedEventHandler(Converter_PropertyChanged);
        }

        void Converter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Property Changed: " + e.PropertyName + " Source: " + sender.ToString());
            if (e.PropertyName.Equals("frmTimeZone"))
            {
                frmTimeZone.Text = App.Converter.frmTimeZone.LongName;
                if (!conversionLock)
                {
                    conversionLock = true;
                    Convert(FROM_CONVERSION);
                }
            }
            else if (e.PropertyName.Equals("toTimeZone"))
            {
                toTimeZone.Text = App.Converter.toTimeZone.LongName;
                if (!conversionLock)
                {
                    conversionLock = true;
                    Convert(TO_CONVERSION);
                }
            }
        }

        private void listFromBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/TimeZoneList.xaml?selection=from", UriKind.Relative));
        }

        private void listToBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/TimeZoneList.xaml?selection=to", UriKind.Relative));
        }

        private void useCrntFrmBtn_Click(object sender, RoutedEventArgs e)
        {
            frmTime.Value = DateTime.Now;
            App.Converter.frmTimeZone = App.Converter.getTimeZoneItem(System.TimeZoneInfo.Local.ToString());
            if (!conversionLock)
            {
                conversionLock = true;
                Convert(FROM_CONVERSION);
            }
        }

        private void useCrntToBtn_Click(object sender, RoutedEventArgs e)
        {
            frmTime.Value = DateTime.Now;
            App.Converter.toTimeZone = App.Converter.getTimeZoneItem(System.TimeZoneInfo.Local.ToString());
            if (!conversionLock)
            {
                conversionLock = true;
                Convert(TO_CONVERSION);
            }
        }


        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Convert(int direction)
        {
            DateTime convertedDateTime = new DateTime();
            DateTime fromDateTime = new DateTime();
            DateTime a;
            DateTime b;

            if (direction == FROM_CONVERSION)
                fromDateTime = new DateTime(frmTime.Value.Value.Ticks);
            else if (direction == TO_CONVERSION)
                fromDateTime = new DateTime (toTime.Value.Value.Ticks);

            convertedDateTime = App.Converter.convertTime(fromDateTime);

            if (direction == FROM_CONVERSION)
            {
                a = convertedDateTime;
                b = fromDateTime;
                toTime.Value = convertedDateTime;
            }
            else if (direction == TO_CONVERSION)
            {
                a = fromDateTime;
                b = convertedDateTime;
                frmTime.Value = convertedDateTime;
            }

            if (convertedDateTime.DayOfYear > fromDateTime.DayOfYear)
            {
                dateChange.Text = "Next Day";
            }
            else if (convertedDateTime.DayOfYear < fromDateTime.DayOfYear)
            {
                dateChange.Text = "Previous Day";
            }
            else if (convertedDateTime.DayOfYear == 1 && fromDateTime.DayOfYear > 2)
            {
                dateChange.Text = "Next Day";
            }
            else
            {
                dateChange.Text = string.Empty;
            }

            conversionLock = false;
        }

        private void frmTime_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            if (!conversionLock)
            {
                conversionLock = true;
                Convert(FROM_CONVERSION);
            }
        }

        private void toTime_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            if (!conversionLock)
            {
                conversionLock = true;
                Convert(TO_CONVERSION);
            }
        }
    }
}