using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zad2_175962
{
    public partial class Error : Form
    {
        public Error()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle; //restrict from resizing
            MaximizeBox = false;  //disable maximize button
            InitializeErrorMessage();
        }

        private void InitializeErrorMessage()
        {
            string errorMessage = "Zły format pliku!";
            label1.Text = errorMessage;
        }

        private void Error_Load(object sender, EventArgs e)
        {

        }
    }
}
