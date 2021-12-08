using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Microprocessor_simulator
{
    public partial class KeyReader : Form
    {
        public int keyValue;
        public KeyReader()
        {
            InitializeComponent();
            string text = "Push down the keyboard to read it, its value will be saved in ASCII code in the AL register.";
            label1.Text = text;
        }

        private void KeyReader_Load(object sender, EventArgs e)
        {

        }
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            keyValue = e.KeyValue;
            this.Close();
        }
    }
}
