using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Json.Net;
using Newtonsoft.Json.Linq;

namespace ElkIngestDataRetail
{
    public class Models
    {

    }
    /// <summary>
    /// prod_tnq_Date
    /// </summary>
    public class RetailGroupFoolFallElk
    {
        public long CreatedAt { get; set; }
        public string Table { get; set; }
        public string CustId { get; set; }
        public DateTime timestamp { get; set; }
        public string AssetId { get; set; }
        public string SiteId { get; set; }
        public string CameraId { get; set; }
        public dynamic data { get; set; }

    }

    public class RetailGroupFoolFallElkUpdate
    {
        public string Id { get; set; }
        public long CreatedAt { get; set; }
        public string Table { get; set; }
        public string CustId { get; set; }
        public DateTime timestamp { get; set; }
        public string AssetId { get; set; }
        public string SiteId { get; set; }
        public string CameraId { get; set; }
        public dynamic data { get; set; }

    }

    public class RetailHeatMapElk
    {
        public long CreatedAt { get; set; }
        public string Table { get; set; }
        public string CustId { get; set; }
        public DateTime timestamp { get; set; }
        public string AssetId { get; set; }
        public string SiteId { get; set; }
        public string CameraId { get; set; }
        public RetailHMData data { get; set; }

    }


    public class RetailGroupFootFallElk
    {
        public DateTime Time { get; set; }
        public string AssetId { get; set; }
        public string SiteId { get; set; }
        public string CameraId { get; set; }
        public string Direction { get; set; }
        public string GroupId { get; set; }
        public int Membercount { get; set; }
        public string Type { get; set; }
        public bool Escorted { get; set; }
    }

    public class RetailGffData
    {
        public string Direction { get; set; }
        public string GroupId { get; set; }
        public int Membercount { get; set; }
        public string Type { get; set; }
        public bool Escorted { get; set; }
    }

    public class RetailDemographicsDataElk
    {
        public DateTime Time { get; set; }
        public string SiteId { get; set; }
        public string AssetId { get; set; }
        public string CameraId { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }

    public class RetailElkDemoData
    {
        public int Age { get; set; }
        public string Gender { get; set; }
    }

    public class TableOccupancyDetailsElk
    {
        public string AssetId { get; set; }
        public string SiteId { get; set; }
        public int TableNo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Membercount { get; set; }
        public string CameraId { get; set; }
    }

    public class RetailTOData
    {
        public int TableNo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Membercount { get; set; }
    }

    //public class RetailHeatMapElk
    //{
    //    public string AssetId { get; set; }
    //    public string SiteId { get; set; }
    //    public string CameraId { get; set; }
    //    public DateTime time { get; set; }
    //    public RetailHMData Cords { get; set; }
    //}

    public class RetailHMData
    {
        public double XCord { get; set; }
        public double YCord { get; set; }
    }

}
