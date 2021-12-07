using System;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace zad2_175962
{
    class SerialPortServices
    {
        public SerialPortServices(short subprogram)
        {
            if (subprogram == 768)
            {
                string isOpen;
                string[] ports = SerialPort.GetPortNames();
                try
                {
                    SerialPort port = new SerialPort(ports[0]);
                    isOpen = (port.IsOpen) ? "OPENED" : "CLOSED";
                    MessageBox.Show($"Port is {isOpen}");

                }
                catch
                {
                    MessageBox.Show("Serial port has not been found.");
                }
            }
        }
    }
}
