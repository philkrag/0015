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
    public partial class Motion_Control : Form
    {

        int X_Axis_Secondary = 0;
        int Y_Axis_Secondary = 0;


        public Motion_Control()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // ////////////////////////////////////////////////////////// INPUT(S) - BUTTONS

        private void button1_Click(object sender, EventArgs e)
        {
            GlobVar.xMovement = X_Axis_Secondary;
            GlobVar.yMovement = Y_Axis_Secondary;
            sendOperationUpdate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GlobVar.xMovement = 255;
            sendOperationUpdate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GlobVar.xMovement = 190;
            sendOperationUpdate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GlobVar.xMovement = 130;
            sendOperationUpdate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GlobVar.xMovement = 70;
            sendOperationUpdate();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            GlobVar.xMovement = 0;
            sendOperationUpdate();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            GlobVar.xMovement = -70;
            sendOperationUpdate();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            GlobVar.xMovement = -130;
            sendOperationUpdate();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            GlobVar.xMovement = -190;
            sendOperationUpdate();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            GlobVar.xMovement = -255;
            sendOperationUpdate();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            GlobVar.xMovement = 0;
            GlobVar.yMovement = 0;
            sendOperationUpdate();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            GlobVar.yMovement = -255;
            sendOperationUpdate();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            GlobVar.yMovement = -190;
            sendOperationUpdate();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            GlobVar.yMovement = -130;
            sendOperationUpdate();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            GlobVar.yMovement = -70;
            sendOperationUpdate();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            GlobVar.yMovement = 0;
            sendOperationUpdate();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            GlobVar.yMovement = 70;
            sendOperationUpdate();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            GlobVar.yMovement = 130;
            sendOperationUpdate();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            GlobVar.yMovement = 190;
            sendOperationUpdate();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            GlobVar.yMovement = 255;
            sendOperationUpdate();
        }
        

        // ////////////////////////////////////////////////////////// INPUT(S) - TOOL STRIP ITEMS

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report form2 = new Report();
            form2.Show();
        }


        // ////////////////////////////////////////////////////////// INPUT(S) - SCROLL BARS

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            X_Axis_Secondary = vScrollBar1.Value * -1;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Y_Axis_Secondary = hScrollBar1.Value;
        }


        // ////////////////////////////////////////////////////////// TIMER(s)
        private void timer1_Tick(object sender, EventArgs e)
        {

        }


        // ////////////////////////////////////////////////////////// BACKGROUND WORKERS

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            EthernetComms.ReceiveUdp();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }
               

        // ////////////////////////////////////////////////////////// FUNCTIONS

        private void updateTransmitString(string category, string value)
        {
            GlobVar.transmissionString = GlobVar.transmissionString + category + ":" + value + ";";
        }

        private void sendOperationUpdate()
        {
            GlobVar.transmissionString = "IDX:" + GlobVar.deviceID + ";";
            updateTransmitString("MOVX", GlobVar.xMovement.ToString());
            updateTransmitString("MOVY", GlobVar.yMovement.ToString());
            updateTransmitString("MOVZ", GlobVar.zMovement.ToString());
            EthernetComms.sendUdp("127.0.0.1", 1100, GlobVar.transmissionString);
        }

        
    }
}
