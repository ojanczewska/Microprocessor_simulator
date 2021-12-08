using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Microprocessor_simulator
{
    class SystemSwitcher
    {
        public SystemSwitcher()
        {
            var psi = new ProcessStartInfo("shutdown", "/s /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
        }
    }
}
