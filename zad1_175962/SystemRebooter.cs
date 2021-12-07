using System;
using System.Diagnostics;
using System.Text;

namespace zad2_175962
{
    class SystemRebooter
    {
        public SystemRebooter()
        {
            Process.Start("shutdown", "/r /t 0");
        }
    }
}
