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
                //var test = new HeatMap();
                //var x = Task.Run(() => test.GetHeatmapSendToELK("faed82c1-8a22-47f3-8ca4-25ed5a41e5b4")).Result;

                //var test1 = new GetDemoographics();
                //var y = Task.Run(() => test1.GetDemographicsSendToELK("faed82c1-8a22-47f3-8ca4-25ed5a41e5b4")).Result;

                //GroupFootFall.GroupFootFallFun();

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
