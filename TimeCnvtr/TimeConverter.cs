using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;

namespace TimeCnvtr
{

    public class TimeConverter : INotifyPropertyChanged
    {
        public TimeZoneItem[] timezones
        { get; private set; }
        private TimeZoneItem _toTimeZone;
        public TimeZoneItem toTimeZone
        {
            get
            { return _toTimeZone; }
            set
            {
                if (value != _toTimeZone)
                {
                    _toTimeZone = value;
                    NotifyPropertyChanged("toTimeZone");
                }
            }
        }

        private TimeZoneItem _frmTimeZone;
        public TimeZoneItem frmTimeZone
        {
            get
            {
                return _frmTimeZone;
            }
            set
            {
                if (value != _frmTimeZone)
                {
                    _frmTimeZone = value;
                    NotifyPropertyChanged("frmTimeZone");
                }
            }
        }

        public string disFrmTimeZone
        {
            get
            {
                return _frmTimeZone.LongName;
            }
            set { }
        }
        public string disToTimeZone
        {
            get
            {
                return _toTimeZone.LongName;
            }
            set { }
        }
        private Dictionary<string, int> offsetTable;

        public TimeConverter() : this(null, null) { }

        public TimeConverter(TimeZoneItem fromTZ, TimeZoneItem toTZ)
        {
            if (fromTZ != null)
                frmTimeZone = fromTZ;

            if (toTZ != null)
                toTimeZone = toTZ;

            this.timezones = new TimeZoneItem[98];
            LoadTimeZones();

            offsetTable = new Dictionary<string, int>();
            buildOffsetTable();
            Debug.WriteLine(offsetTable.Keys.Count);
        }


        private void LoadTimeZones()
        {
            timezones[0] = new TimeZoneItem() { Offset = "-12", LongName = "International Date Line West" };
            timezones[1] = new TimeZoneItem() { Offset = "-11", LongName = "Coordinated Universal Time-11" };
            timezones[2] = new TimeZoneItem() { Offset = "-11", LongName = "Samoa" };
            timezones[3] = new TimeZoneItem() { Offset = "-10", LongName = "Hawaii" };
            timezones[4] = new TimeZoneItem() { Offset = "-9", LongName = "Alaska" };
            timezones[5] = new TimeZoneItem() { Offset = "-8", LongName = "Baja California" };
            timezones[6] = new TimeZoneItem() { Offset = "-8", LongName = "Pacific Time (US & Canada)" };
            timezones[7] = new TimeZoneItem() { Offset = "-7", LongName = "Arizona" };
            timezones[8] = new TimeZoneItem() { Offset = "-7", LongName = "Chihuahua,  La Pax, Mazatlan" };
            timezones[9] = new TimeZoneItem() { Offset = "-7", LongName = "Mountain Time (US & Canada)" };
            timezones[10] = new TimeZoneItem() { Offset = "-6", LongName = "Central America" };
            timezones[11] = new TimeZoneItem() { Offset = "-6", LongName = "Guadalajara, Mexico City, Monterrey" };
            timezones[12] = new TimeZoneItem() { Offset = "-6", LongName = "Saskatchewan" };
            timezones[13] = new TimeZoneItem() { Offset = "-5", LongName = "Bogota, Lima, Quito" };
            timezones[14] = new TimeZoneItem() { Offset = "-5", LongName = "Eastern Time (US & Canada)" };
            timezones[15] = new TimeZoneItem() { Offset = "-5", LongName = "Indiana (East)" };
            timezones[16] = new TimeZoneItem() { Offset = "-4.5", LongName = "Caracas" };
            timezones[17] = new TimeZoneItem() { Offset = "-4", LongName = "Atlantic Time (Canada)" };
            timezones[18] = new TimeZoneItem() { Offset = "-4", LongName = "Cuiaba" };
            timezones[19] = new TimeZoneItem() { Offset = "-4", LongName = "Georgetown, La Paz, Manaus, San Juan" };
            timezones[20] = new TimeZoneItem() { Offset = "-4", LongName = "Santiago" };
            timezones[21] = new TimeZoneItem() { Offset = "-3.5", LongName = "Newfoundland" };
            timezones[22] = new TimeZoneItem() { Offset = "-3", LongName = "Brasilia" };
            timezones[23] = new TimeZoneItem() { Offset = "-3", LongName = "Buenos Aires" };
            timezones[24] = new TimeZoneItem() { Offset = "-3", LongName = "Cayenne, Fortaleza" };
            timezones[25] = new TimeZoneItem() { Offset = "-3", LongName = "Greenland" };
            timezones[26] = new TimeZoneItem() { Offset = "-3", LongName = "Montevideo" };
            timezones[27] = new TimeZoneItem() { Offset = "-2", LongName = "Coordinated Universal Time-02" };
            timezones[28] = new TimeZoneItem() { Offset = "-2", LongName = "Mid-Atlantic" };
            timezones[29] = new TimeZoneItem() { Offset = "-1", LongName = "Azores" };
            timezones[30] = new TimeZoneItem() { Offset = "-1", LongName = "Cape Verde Is." };
            timezones[31] = new TimeZoneItem() { Offset = "0", LongName = "Casablanca" };
            timezones[32] = new TimeZoneItem() { Offset = "0", LongName = "Coordinated Universal Time" };
            timezones[33] = new TimeZoneItem() { Offset = "0", LongName = "Dublin, Edinburgh, Lisbon, London" };
            timezones[34] = new TimeZoneItem() { Offset = "0", LongName = "Monrovia, Reykjavik" };
            timezones[35] = new TimeZoneItem() { Offset = "1", LongName = "Amsterdam, Berlin, Bern Rome, Stockholm, Vienna" };
            timezones[36] = new TimeZoneItem() { Offset = "1", LongName = "Belgrade, Bratislava, Budapest, Ljublijana, Prague" };
            timezones[37] = new TimeZoneItem() { Offset = "1", LongName = "Brussels, Copenhagen, Madrid, Paris" };
            timezones[38] = new TimeZoneItem() { Offset = "1", LongName = "Sarajevo, Skopje, Warsaw, Zagreb" };
            timezones[39] = new TimeZoneItem() { Offset = "1", LongName = "West Coast Africa" };
            timezones[40] = new TimeZoneItem() { Offset = "1", LongName = "Windhoek" };
            timezones[41] = new TimeZoneItem() { Offset = "2", LongName = "Amman" };
            timezones[42] = new TimeZoneItem() { Offset = "2", LongName = "Athens, Bucharest" };
            timezones[43] = new TimeZoneItem() { Offset = "2", LongName = "Beirut" };
            timezones[44] = new TimeZoneItem() { Offset = "2", LongName = "Cairo" };
            timezones[45] = new TimeZoneItem() { Offset = "2", LongName = "Damascus" };
            timezones[46] = new TimeZoneItem() { Offset = "2", LongName = "Harare, Pretoria" };
            timezones[47] = new TimeZoneItem() { Offset = "2", LongName = "Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius" };
            timezones[48] = new TimeZoneItem() { Offset = "2", LongName = "Istanbul" };
            timezones[49] = new TimeZoneItem() { Offset = "2", LongName = "Jerusalem" };
            timezones[50] = new TimeZoneItem() { Offset = "2", LongName = "Minsk" };
            timezones[51] = new TimeZoneItem() { Offset = "3", LongName = "Baghdad" };
            timezones[52] = new TimeZoneItem() { Offset = "3", LongName = "Kaliningrad" };
            timezones[53] = new TimeZoneItem() { Offset = "3", LongName = "Kuwait, Riyadh" };
            timezones[54] = new TimeZoneItem() { Offset = "3", LongName = "Nairobi" };
            timezones[55] = new TimeZoneItem() { Offset = "3.5", LongName = "Tehran" };
            timezones[56] = new TimeZoneItem() { Offset = "4", LongName = "Abu Dhabi, Muscat" };
            timezones[57] = new TimeZoneItem() { Offset = "4", LongName = "Baku" };
            timezones[58] = new TimeZoneItem() { Offset = "4", LongName = "Moscow, St. Petersburg, Volgograd" };
            timezones[59] = new TimeZoneItem() { Offset = "4", LongName = "Port Louis" };
            timezones[60] = new TimeZoneItem() { Offset = "4", LongName = "Tbilisi" };
            timezones[61] = new TimeZoneItem() { Offset = "4", LongName = "Yerevan" };
            timezones[62] = new TimeZoneItem() { Offset = "4.5", LongName = "Kabul" };
            timezones[63] = new TimeZoneItem() { Offset = "5", LongName = "Islamabad, Karachi" };
            timezones[64] = new TimeZoneItem() { Offset = "5", LongName = "Tashkent" };
            timezones[65] = new TimeZoneItem() { Offset = "5.5", LongName = "Chennai, Kolkata, Mumbai, New Delhi" };
            timezones[66] = new TimeZoneItem() { Offset = "5.5", LongName = "Sri Jaywardenepura" };
            timezones[67] = new TimeZoneItem() { Offset = "5.75", LongName = "Kathmandu" };
            timezones[68] = new TimeZoneItem() { Offset = "6", LongName = "Astana" };
            timezones[69] = new TimeZoneItem() { Offset = "6", LongName = "Dhaka" };
            timezones[70] = new TimeZoneItem() { Offset = "6", LongName = "Ekaterinburg" };
            timezones[71] = new TimeZoneItem() { Offset = "6.5", LongName = "Yangon (Rangoon)" };
            timezones[72] = new TimeZoneItem() { Offset = "7", LongName = "Bangkok, Hanoi, Jakarta" };
            timezones[73] = new TimeZoneItem() { Offset = "7", LongName = "Novosibirsk" };
            timezones[74] = new TimeZoneItem() { Offset = "8", LongName = "Beijing, Chongqing, Hong Kong, Urumqi" };
            timezones[75] = new TimeZoneItem() { Offset = "8", LongName = "Krasnoyarsk" };
            timezones[76] = new TimeZoneItem() { Offset = "8", LongName = "Kuala Lumpur, Singapore" };
            timezones[77] = new TimeZoneItem() { Offset = "8", LongName = "Perth" };
            timezones[78] = new TimeZoneItem() { Offset = "8", LongName = "Taipei" };
            timezones[79] = new TimeZoneItem() { Offset = "8", LongName = "Ulaanbaatar" };
            timezones[80] = new TimeZoneItem() { Offset = "9", LongName = "Irkusk" };
            timezones[81] = new TimeZoneItem() { Offset = "9", LongName = "Osaka, Sapporo, Tokyo" };
            timezones[82] = new TimeZoneItem() { Offset = "9", LongName = "Seoul" };
            timezones[83] = new TimeZoneItem() { Offset = "9.5", LongName = "Adelaide" };
            timezones[84] = new TimeZoneItem() { Offset = "9.5", LongName = "Darwin" };
            timezones[85] = new TimeZoneItem() { Offset = "10", LongName = "Brisbane" };
            timezones[86] = new TimeZoneItem() { Offset = "10", LongName = "Canberra, Melbourne, Sydney" };
            timezones[87] = new TimeZoneItem() { Offset = "10", LongName = "Guam, Port Morseby" };
            timezones[88] = new TimeZoneItem() { Offset = "10", LongName = "Hobart" };
            timezones[89] = new TimeZoneItem() { Offset = "10", LongName = "Yakutsk" };
            timezones[90] = new TimeZoneItem() { Offset = "11", LongName = "Solomon Is., New Caledonia" };
            timezones[91] = new TimeZoneItem() { Offset = "11", LongName = "Vladivostok" };
            timezones[92] = new TimeZoneItem() { Offset = "12", LongName = "Auckland, Wellington" };
            timezones[93] = new TimeZoneItem() { Offset = "12", LongName = "Coordinated Universal Time+12" };
            timezones[94] = new TimeZoneItem() { Offset = "12", LongName = "Fiji" };
            timezones[95] = new TimeZoneItem() { Offset = "12", LongName = "Magadan" };
            timezones[96] = new TimeZoneItem() { Offset = "12", LongName = "Petropavlovsk-Kamchatsky - Old" };
            timezones[97] = new TimeZoneItem() { Offset = "13", LongName = "Nuku'alofa" };
        }

        private TimeSpan getTimeSpan(float input)
        {
            TimeSpan timeSpan = new TimeSpan();
            double minutes = Math.Floor(input) * 60;
            minutes += (Math.Abs(input) * 60 - minutes);
            timeSpan = TimeSpan.FromMinutes(minutes);
            return timeSpan;
        }

        private DateTime convertTimeZone(DateTime input, TimeZoneItem tz)
        {
            DateTime output = input;
            if (tz.fOffset > 0)
            {
                output = input + getTimeSpan(tz.fOffset);
                return output;
            }
            else
            {
                output = input - getTimeSpan(tz.fOffset);
                return output;
            }
        }

        public DateTime convertTime(DateTime inputTime)
        {
            TimeSpan timeDiff = getTimeSpan(toTimeZone.fOffset - frmTimeZone.fOffset);
            if ((toTimeZone.fOffset - frmTimeZone.fOffset) > 0)
            {
                return inputTime + timeDiff;
            }
            else
            {
                return inputTime - timeDiff;
            }
        }

        ///<summary>
        ///Formats text returned from API so that it's readable
        ///</summary>
        public string formatFromAPI(string apiTimeZone)
        {
            //Temporary
            return apiTimeZone;
        }

        ///<summary>
        ///trims the leading 0 in the string
        ///</summary>
        private string trimLeadingZero(string input)
        {
            int parsed;
            Int32.TryParse(input, out parsed);
            return parsed.ToString();
        }

        ///<summary>
        ///Returns a TimeZoneItem from a string
        ///</summary>
        public TimeZoneItem getTimeZoneItem(string input)
        {
            TimeZoneItem output = new TimeZoneItem();
            string[] words = (input.Split(new Char[] { ' ' }));
            int startSearch;

            offsetTable.TryGetValue(trimLeadingZero(words[0].Substring(3)), out startSearch);

            string tzName = "";
            for (int i = 1; i < words.Length; i++)
            {
                if (i > 1)
                    tzName += " ";
                tzName += words[i];
            }

            for (int i = startSearch; i < timezones.Length; i++)
            {
                if (timezones[i].LongName.Equals(tzName))
                {
                    output = timezones[i];
                    break;
                }
            }


            return output;
        }

        ///<summary>
        ///Builds a dictionary to store the starting index of each offset
        ///</summary>
        private void buildOffsetTable()
        {
            int pos = 0;
            foreach (TimeZoneItem tzi in timezones)
            {
                if (offsetTable.ContainsKey(tzi.Offset))
                { continue; }
                else
                {
                    offsetTable.Add(tzi.Offset, pos);
                }
                pos++;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
