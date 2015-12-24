using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPC
{
    public class WipParameterInfo : INotifyPropertyChanged
    {

        public int Sequential { get; set; }
        public string ItemID { get; set; }
        public string ItemCaption { get; set; }
        public string DefaultValue { get; set; }
        public string UpperLimit { get; set; }
        public string LowerLimit { get; set; }
        public string ParameterValue { get; set; }
        public uint ValueType { get; set; } = 0;

        public string LimitRange
        {
            get
            {
                string val = string.Empty;
                string upper = string.IsNullOrWhiteSpace(UpperLimit) ? "0" : UpperLimit;
                string lower = string.IsNullOrWhiteSpace(LowerLimit) ? "0" : LowerLimit;

                switch (ValueType)
                {
                    case 0:
                        val = $"{Convert.ToInt32(LowerLimit)}~{Convert.ToInt32(UpperLimit)}";
                        break;
                    case 1:
                        val = $"{Convert.ToDouble(LowerLimit)}~{Convert.ToDouble(UpperLimit)}";
                        break;
                    default:
                        val = $"{UpperLimit},{LowerLimit}";
                        break;
                }

                return val;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
