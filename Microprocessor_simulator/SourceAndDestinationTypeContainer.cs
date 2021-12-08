using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microprocessor_simulator
{
    class SourceAndDestinationTypeContainer
    {
        public Dictionary<int, string> sourcesAndDestinationsData = new Dictionary<int, string>();

        public SourceAndDestinationTypeContainer()
        {
            sourcesAndDestinationsData.Add(0, Enum.GetName(typeof(Registers), Registers.AX));
            sourcesAndDestinationsData.Add(1, Enum.GetName(typeof(Registers), Registers.BX));
            sourcesAndDestinationsData.Add(2, Enum.GetName(typeof(Registers), Registers.CX));
            sourcesAndDestinationsData.Add(3, Enum.GetName(typeof(Registers), Registers.DX));
        }
    }
}
