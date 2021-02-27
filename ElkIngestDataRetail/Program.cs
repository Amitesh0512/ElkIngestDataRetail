using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElkIngestDataRetail
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var GetHeatMapOBJ = new HeatMap();
                var x = Task.Run(() => GetHeatMapOBJ.GetHeatmapSendToELK("test-asd-asdasd-******-asdasdasdas")).Result;

                var GetDemographicsObj = new GetDemoographics();
                var y = Task.Run(() => GetDemographicsObj.GetDemographicsSendToELK("yyyysydgysgd-dasda-asd-8casda4-*******")).Result;

                GroupFootFall.GroupFootFallFun();

                var obj = new ELKHelper();
                obj.DeleteDataFromElk();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
