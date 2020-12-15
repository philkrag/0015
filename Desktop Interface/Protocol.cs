using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_Interface
{
    class Protocol
    {
        // ////////////////////////////////////////////////////////////////////////////////////
        // Function:            Message Breakdown
        // Version:             1.000
        // Last Modified By:    2020-12-15
        // Last Modified By:    Phillip Kraguljac

        public static void MessageBreakdown()
        {
            for (int i = 0; i < 100; i++)
            {
                if (GlobVar.workFlowRegister[i, 1] == "Received")
                {
                    string[] workFlowRegisterBreakdown = GlobVar.workFlowRegister[i, 0].Split(';');

                    for (int ii = 0; ii < workFlowRegisterBreakdown.GetLength(0) - 1; ii++)
                    {
                        string[] minorArray = workFlowRegisterBreakdown[ii].Split(':');
                        string indexHeading = minorArray[0];
                        string indexValue = minorArray[1];

                        switch (indexHeading)
                        {
                            case "IDX": GlobVar.deviceID = Convert.ToInt32(indexValue); break;
                            case "MOVX": GlobVar.xMovement = Convert.ToInt32(indexValue); break;
                            case "MOVY": GlobVar.yMovement = Convert.ToInt32(indexValue); break;
                            //case "MOVZ": GlobVar.zMovement = Convert.ToInt32(""); break;

                            case "GWL": GlobVar.gardenWaterLevel = Convert.ToInt32(indexValue); break;
                            case "GAT": GlobVar.gardenAmbientAirTemperature = Convert.ToInt32(indexValue); break;
                            case "GAH": GlobVar.gardenAmbientAirHumidty = Convert.ToInt32(indexValue); break;
                            case "GAW": GlobVar.gardenAmbientAirWindSpeed = Convert.ToInt32(indexValue); break;
                            case "GWP": GlobVar.gardenWaterPumpSpeed = Convert.ToInt32(indexValue); break;
                            case "GAP": GlobVar.gardenAirFlowPumpSpeed = Convert.ToInt32(indexValue); break;

                            default:
                                break;
                        }
                    }
                }
                GlobVar.workFlowRegister[i, 1] = "Completed";
            }
        }
    }
}
