using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad2_175962
{
    class SourceTypeVontainercs
    {
        public Dictionary<int, string> sourcesData = new Dictionary<int, string>();

        public SourceTypeVontainercs()
        {
            sourcesData.Add(0, Enum.GetName(typeof(Registers), Registers.AX));
            sourcesData.Add(1, Enum.GetName(typeof(Registers), Registers.BX));
            sourcesData.Add(2, Enum.GetName(typeof(Registers), Registers.CX));
            sourcesData.Add(3, Enum.GetName(typeof(Registers), Registers.DX));
        }
    }
}
