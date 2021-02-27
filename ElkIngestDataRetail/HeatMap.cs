using InfluxData.Net.Common.Enums;
using InfluxData.Net.InfluxDb;
using InfluxData.Net.InfluxDb.Models.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Json.Net;
using Newtonsoft.Json.Linq;

namespace ElkIngestDataRetail
{
    public class HeatMap
    {
        public async Task<FunctionResponseModel> GetHeatmapSendToELK(string CustId)
        {
            FunctionResponseModel _response = new FunctionResponseModel();
            var influxDbClient = new InfluxDbClient("http://influxurl:8086/", "username", "password", InfluxDbVersion.v_1_3);
            var ping = await influxDbClient.Diagnostics.PingAsync();

            try
            {
                if (ping.Success)
                {
                    List<GetHeatmapResponseModel> HeatMapList = new List<GetHeatmapResponseModel>();

                    var query1 = "SELECT time,CameraId,MemberCount,XCord,YCord,StoreId FROM \"MeasurementName\"  WHERE \"CustId\"='" + CustId + "'";

                    var response = await influxDbClient.Client.QueryAsync(query1, "RT_Tanishq_PROD");

                    IEnumerable<Serie> series = response;

                    foreach (Serie serie in series)
                    {

                        foreach (dynamic val in serie.Values)
                        {
                            var temp = val[0];
                            var time = DateTime.ParseExact(val[0].ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", null);

                            GetHeatmapResponseModel HeatMap = new GetHeatmapResponseModel
                            {
                                //Time = DateTime.ParseExact(val[0].ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", null),
                                Time = time,
                                CameraId = Convert.ToString(val[1]),
                                TotalNumber = Convert.ToInt32(val[2]),
                                XCord = Convert.ToString(val[3]),
                                YCord = Convert.ToString(val[4]),
                                StoreId = Convert.ToString(val[5]),
                            };
                            HeatMapList.Add(HeatMap);
                        }
                    }

                    foreach (var HMData in HeatMapList)
                    {
                        RetailHMData newData = new RetailHMData
                        {
                            XCord = Convert.ToDouble(HMData.XCord),
                            YCord = Convert.ToDouble(HMData.YCord),
                        };


                        var testData = JsonConvert.SerializeObject(newData);

                        RetailGroupFoolFallElk DataDetails = new RetailGroupFoolFallElk
                        {
                            AssetId = "Nano-01",
                            CameraId = HMData.CameraId,
                            CreatedAt = ((DateTimeOffset)HMData.Time).ToUnixTimeSeconds(),
                            CustId = CustId,
                            SiteId = HMData.StoreId,
                            Table = "Heatmap",
                            timestamp = HMData.Time,
                            data = newData
                        };

                        var ElkObject = new ELKHelper();
                        ElkObject.InsertRetailDataToElkDateWise(DataDetails);
                    }

                    _response.Message = JsonConvert.SerializeObject(HeatMapList);
                    _response.Statuscode = "OK";
                    return _response;
                }
                else
                {
                    _response.Message = "InfluxDB NOT RUNNING";

                    _response.Statuscode = "EXCEPTION";
                    return _response;
                }
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.Statuscode = "EXCEPTION";
                return _response;
            }

        }
    }


    public class GetHeatmapResponseModel
    {
        public DateTime Time { get; set; }
        public int TotalNumber { get; set; }
        public string CameraId { get; set; }
        public string XCord { get; set; }
        public string YCord { get; set; }
        public string StoreId { get; set; }
    }
}
