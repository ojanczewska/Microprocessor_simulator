using System;

using System.Printing;

using System.Text;
using System.Windows.Forms;
using System.Management;



namespace zad2_175962
{
    class PrinterServices
    {
        public PrinterServices(short subprogram)
        {
            StringBuilder stringBuilder = new StringBuilder(10000);

            if (subprogram == 512)
            {
                var printerQuery = new ManagementObjectSearcher("SELECT * from Win32_Printer");
                foreach (var printer in printerQuery.Get())
                {
                    var name = printer.GetPropertyValue("Name");
                    var status = printer.GetPropertyValue("Status");
                    var isDefault = printer.GetPropertyValue("Default");
                    var isNetworkPrinter = printer.GetPropertyValue("Network");

                    stringBuilder.Append($"{name} (Status: {status}, Default: {isDefault}, Network: {isNetworkPrinter}\n");
                }

                MessageBox.Show(stringBuilder.ToString());
            }
        }
    }
}
