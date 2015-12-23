using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPC
{
    public class WipParameterInfo
    {        
        public int Sequential { get; set; }        
        public string ItemID { get; set; }
        public string ItemCaption { get; set; }
        public string ParameterStandard { get; set; }
        public string ParameterUpperLimit { get; set; }
        public string ParameterLowerLimit { get; set; }
        public string ParameterValue { get; set; }
        public bool IsNumeric { get; set; } = false;
        public Decimal? ParameterLowerValue { get; set; } = 0;
        public Decimal? ParameterUpperValue { get; set; } = 0;
    }
}
