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

namespace TimeCnvtr
{
    public partial class TimeZoneList : PhoneApplicationPage
    {
        private string selection;
        public TimeZoneList()
        {
            InitializeComponent();
            DataContext = App.Converter;
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selection.Equals("from"))
            {
                App.Converter.frmTimeZone = (TimeZoneItem)listBox1.SelectedItem;
            }
            else if (selection.Equals("to"))
            {
                App.Converter.toTimeZone = (TimeZoneItem)listBox1.SelectedItem;
            }
            NavigationService.GoBack();
            
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            selection = this.NavigationContext.QueryString["selection"];
        }
    }
}