using System;
using System.Diagnostics;
using System.Text;

namespace Microprocessor_simulator
{
    class SystemRebooter
    {
        public SystemRebooter()
        {
            Process.Start("shutdown", "/r /t 0");
        }
    }
}
