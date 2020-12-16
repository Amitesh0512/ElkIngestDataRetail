using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElkIngestDataRetail
{
    public class GroupFootFall
    {
        public static void GroupFootFallFun()
        {
            var newData = new RetailGffData
            {
                Type = "Customer",
                Escorted = true,
                GroupId = "GroupId-01",
                Membercount = 2,
                Direction = "In",
            };

            RetailGroupFoolFallElk data = new RetailGroupFoolFallElk
            {
                timestamp = DateTime.Now,
                CustId = "Tes111",
                CreatedAt = DateTimeOffset.Now.ToUnixTimeSeconds(),
                Table = "GroupFootFall",
                AssetId = "NanoId-02",
                CameraId = "Cam-01",
                SiteId = "Store-01",
                //data = JsonConvert.SerializeObject(newData)
            };

            var ElkObject = new ELKHelper();
            ElkObject.UpdateDataToElk(data);
        } 
    }

    //public static void GroupFootFallUpdate()
    //{
    //    var newData = new RetailGffData
    //    {
    //        Type = "Customer",
    //        Escorted = true,
    //        GroupId = "GroupId-01",
    //        Membercount = 2,
    //        Direction = "In",
    //    };

    //    RetailGroupFoolFallElk data = new RetailGroupFoolFallElk
    //    {
    //        timestamp = DateTime.Now,
    //        CustId = "Test0991",
    //        CreatedAt = DateTimeOffset.Now.ToUnixTimeSeconds(),
    //        Table = "GroupFootFall",
    //        AssetId = "NanoId-01",
    //        CameraId = "Cam-01",
    //        SiteId = "Store-01",
    //        //data = JsonConvert.SerializeObject(newData)
    //    };

    //    var ElkObject = new ELKHelper();
    //    ElkObject.UpdateDataToElk(data);
    //}
}
