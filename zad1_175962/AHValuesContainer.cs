using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad2_175962
{
    class AHValuesContainer
    {
        public Dictionary<string, short> AHValues = new Dictionary<string, short>();

        public AHValuesContainer()
        {
            AHValues.Add("00h", 0);
            AHValues.Add("01h", 256);
            AHValues.Add("02h", 512);
            AHValues.Add("03h", 768);
            AHValues.Add("04h", 1024);
            AHValues.Add("05h", 1280);
            AHValues.Add("06h", 1536);
            AHValues.Add("07h", 1792);
            AHValues.Add("08h", 2048);
            AHValues.Add("09h", 2304);
            AHValues.Add("10h", 2560);
        }
    }
}
