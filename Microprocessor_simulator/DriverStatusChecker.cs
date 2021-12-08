using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Microprocessor_simulator
{
    class DriverStatusChecker
    {
        public DriverStatusChecker(short subprogram)
        {
            StringBuilder stringBuilder = new StringBuilder(1500);

            if (subprogram == 256)
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (drive.IsReady)
                    {
                        stringBuilder.Append($"Disk name: {drive.Name}  Disk size: {drive.TotalSize} B\n");
                    }
                }
                MessageBox.Show(stringBuilder.ToString());
            }
        }
    }
}
