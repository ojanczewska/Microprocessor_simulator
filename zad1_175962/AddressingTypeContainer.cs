using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad2_175962
{
    class AddressingTypeContainer
    {
        public Dictionary<int, string> addressesData = new Dictionary<int, string>();

        public AddressingTypeContainer()
        {
            addressesData.Add(0, Enum.GetName(typeof(AddressingTypes), AddressingTypes.REG));
            addressesData.Add(1, Enum.GetName(typeof(AddressingTypes), AddressingTypes.IMM));
        }
    }
}
