using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Interface
{
    public partial class Main_Menu : Form
    {
        public Main_Menu()
        {
            InitializeComponent();
        }


        // ////////////////////////////////////////////////////////// INPUT(S) - BUTTONS

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Close();
            Form Motion_Control = new Motion_Control();
            Motion_Control.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form Garden_Control = new Garden_Control();
            Garden_Control.Show();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            GlobVar.deviceID = Convert.ToInt32(numericUpDown1.Value);
        }
    }
}
