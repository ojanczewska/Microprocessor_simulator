using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Microprocessor_simulator
{
    class TimeReader
    {
        public TimeReader(short subprogram)
        {
            if (subprogram == 0)
            {
                MessageBox.Show($"RTC: {DateTime.Now.ToString()}");
            }
            if (subprogram == 512)
            {
                MessageBox.Show($"Today's hour: {DateTime.Now.ToString("HH:mm:ss: tt")}");
            }

            if (subprogram == 1024)
            {
                MessageBox.Show($"Today's date: {DateTime.Now.ToString("M/d/yyyy")}");
            }

        }
    }
}
