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
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
            text_label.Text = "Aby wywołać przerwanie należy wybrać numer funkcji porzez załadowanie rozkazu MOV AH xxh,\r\na następnie wybranie przerwania INTxx."+
                "Przerwanie sie wywoła tylko wtedy,  gdy zostanie mu przypisana\r\n funkcja określona w powyższej tabeli, inne przerwania nie są wspomagane przez ten symulator.";


        }

        private void Info_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }
    }
}
