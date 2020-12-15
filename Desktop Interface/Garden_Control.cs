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
    public partial class Garden_Control : Form
    {

        int Soil_Water_Level = 0;


        public Garden_Control()
        {
            InitializeComponent();
        }

        private void Garden_Control_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        // ////////////////////////////////////////////////////////// INPUT(S) - BUTTONS

        private void button1_Click(object sender, EventArgs e)
        {
            GlobVar.gardenWaterLevel = Convert.ToInt32(numericUpDown1.Value);
            //GlobVar.gardenAmbientAirTemperature = Convert.ToInt32(numericUpDown2.Value);
            //GlobVar.gardenAmbientAirHumidty = Convert.ToInt32(numericUpDown3.Value);
            //GlobVar.gardenAmbientAirWindSpeed = Convert.ToInt32(numericUpDown4.Value);
            ////GlobVar.waterTemprature = Convert.ToInt32(numericUpDown5.Value);
            //GlobVar.gardenWaterPumpSpeed = Convert.ToInt32(numericUpDown5.Value);
            //GlobVar.gardenAirFlowPumpSpeed = Convert.ToInt32(numericUpDown6.Value);
            



            sendOperationUpdate();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }


        // ////////////////////////////////////////////////////////// INPUT(S) - NUMERIC UP/DOWN(S)

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            
        }



        // ////////////////////////////////////////////////////////// FUNCTIONS

        private void updateTransmitString(string category, string value)
        {
            GlobVar.transmissionString = GlobVar.transmissionString + category + ":" + value + ";";
        }

        private void sendOperationUpdate()
        {
            GlobVar.transmissionString = "IDX:" + GlobVar.deviceID + ";";
            updateTransmitString("GWL", GlobVar.gardenWaterLevel.ToString());
            updateTransmitString("GAT", GlobVar.gardenAmbientAirTemperature.ToString());
            updateTransmitString("GAH", GlobVar.gardenAmbientAirHumidty.ToString());
            updateTransmitString("GAW", GlobVar.gardenAmbientAirWindSpeed.ToString());
            updateTransmitString("GWP", GlobVar.gardenWaterPumpSpeed.ToString());
            updateTransmitString("GAP", GlobVar.gardenAirFlowPumpSpeed.ToString());

            EthernetComms.sendUdp("127.0.0.1", 1100, GlobVar.transmissionString);
        }

        private void UpdateDisplay()
        {
            numericUpDown12.Value = GlobVar.gardenWaterLevel;
        }


        // ////////////////////////////////////////////////////////// BACKGROUND WORKER

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int index = 0;

        WaitingForData:

            GlobVar.workFlowRegister[index, 0] = EthernetComms.ReceiveUdp();
            GlobVar.workFlowRegister[index, 1] = "Received";
            index = index + 1;

            goto AnalysisData;

        AnalysisData:

            goto WaitingForData;
        }


        


            // ////////////////////////////////////////////////////////// TIMERS

            private void timer1_Tick(object sender, EventArgs e)
        {
            Protocol.MessageBreakdown();
            UpdateDisplay();
        }

        
    }
}
