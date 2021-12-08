using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microprocessor_simulator
{
    class KeyboardServices
    {
        public int keyValue;
        public KeyboardServices(short subprogram)
        {
            if (subprogram == 0)
            {
                KeyReader keyReader = new KeyReader();
                
                keyReader.ShowDialog();
                keyValue = keyReader.keyValue;
            }
        }
    }
}
