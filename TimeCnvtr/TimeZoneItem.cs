using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace TimeCnvtr
{
    public class TimeZoneItem
    {
        private string _offset;

        public string Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                if (value != _offset)
                {
                    _offset = value;
                }
            }
        }

        public float fOffset
        {
            get
            {
                float output;
                if (float.TryParse(_offset, out output))
                    return output;
                else
                    return -25; //use -25, can't have timezone difference that large
            }
            set
            {
                string output = value.ToString();
                if (output != _offset)
                    _offset = output;
            }
        }

        public string tOffset
        {
            get
            {
                int minutes = (int)((fOffset - Math.Floor(fOffset)) * 60);
                int hours = (int)(Math.Floor(fOffset));
                string time = "";
                if (hours > 0)
                    time = "+" + hours.ToString();
                else
                    time = hours.ToString();

                if (minutes <= 0)
                    time += ":00";
                else
                    time += ":" + minutes.ToString();
                return time;
            }
            set { return; }
        }

        private string _shortName;

        public string ShortName
        {
            get
            {
                return _shortName;
            }
            set
            {
                if (value != _shortName)
                {
                    _shortName = value;
                }
            }
        }

        
	  private string _longName;
	  
	  public string LongName
	  {
			get
			{
				return _longName;
			}
			set
			{
				if (value != _longName)
					_longName = value;
			}
	   }
    }
}
