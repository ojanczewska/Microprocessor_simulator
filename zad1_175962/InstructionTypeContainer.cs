using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad2_175962
{
    class InstructionTypeContainer
    {
        public Dictionary<int, string> instructionsData = new Dictionary<int, string>();

        public InstructionTypeContainer()
        {
            instructionsData.Add(0, Enum.GetName(typeof(InstructionTypes), InstructionTypes.ADD));
            instructionsData.Add(1, Enum.GetName(typeof(InstructionTypes), InstructionTypes.SUB));
            instructionsData.Add(2, Enum.GetName(typeof(InstructionTypes), InstructionTypes.MOV));
        }
    }
}
