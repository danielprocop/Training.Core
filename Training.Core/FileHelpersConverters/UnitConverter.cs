using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Training.Core.FileHelpersConverters
{
    public class UnitConverter : ConverterBase
    {
        public override object StringToField(string from)
        {
            object value="";
           if (from == "ng/m³")
            {
                value= "ng_m3";
            }
           else if(from == "mg/m³")
            {
                value = "mg_m3";
            }
            else if (from == "µg/m³")
            {
                value = "µg_m3";
            }
            else
            {
                ThrowConvertException(from, "not valid value for unit");
            }

            return value;
        }
    }
}
