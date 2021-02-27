using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfluxData.Net.Common.Enums;
using InfluxData.Net.InfluxDb;
using InfluxData.Net.InfluxDb.Models.Responses;
using Newtonsoft.Json;

namespace ElkIngestDataRetail
{
    public class GetDemoographics
    {

        public async Task<FunctionResponseModel> GetDemographicsSendToELK(string CustId)
        {
            FunctionResponseModel _response = new FunctionResponseModel();
            var influxDbClient = new InfluxDbClient("http://influxurl:8086/", "username", "password", InfluxDbVersion.v_1_3);
            var ping = await influxDbClient.Diagnostics.PingAsync();

            try
            {
                if (ping.Success)
                {
                    List<GetDemographicsResponseModel> DemographicsList = new List<GetDemographicsResponseModel>();

                    var query1 = "SELECT time,CameraId,Age,Gender,StoreId FROM \"MeasurementName\"  WHERE \"CustId\"='" + CustId + "'";

                    var response = await influxDbClient.Client.QueryAsync(query1, "DataBaseName");

                    IEnumerable<Serie> series = response;

                    foreach (Serie serie in series)
                    {

                        foreach (dynamic val in serie.Values)
                        {
                            var temp = val[0];
                            var time = DateTime.ParseExact(val[0].ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", null);

                            GetDemographicsResponseModel Demographics = new GetDemographicsResponseModel
                            {
                                //Time = DateTime.ParseExact(val[0].ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", null),
                                Time = time,
                                CameraId = Convert.ToString(val[1]),
                                Gender = Convert.ToString(val[3]),
                                Age = Convert.ToInt32(val[2]),
                                StoreId = Convert.ToString(val[4]),
                            };
                            DemographicsList.Add(Demographics);
                        }
                    }

                    foreach(var demoData in DemographicsList)
                    {
                        RetailElkDemoData newData = new RetailElkDemoData
                        {
                            Age = demoData.Age,
                            Gender = demoData.Gender,
                        };

                        RetailGroupFoolFallElk DataDetails = new RetailGroupFoolFallElk
                        {
                            AssetId = "Nano-01",
                            CameraId = demoData.CameraId,
                            CreatedAt = ((DateTimeOffset)demoData.Time).ToUnixTimeSeconds(),
                            CustId = CustId,
                            SiteId = demoData.StoreId,
                            Table = "Demographics",
                            timestamp = demoData.Time,
                            data = newData,
                        };

                        var ElkObject = new ELKHelper();
                        ElkObject.InsertRetailDataToElkDateWise(DataDetails);
                    }

                    _response.Message = JsonConvert.SerializeObject(DemographicsList);
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
            catch(Exception ex)
            {
                _response.Message = ex.Message;
                _response.Statuscode = "EXCEPTION";
                return _response;
            }

        }
        
    }

    public class GetDemographicsResponseModel
    {
        public DateTime Time { get; set; }
        public string CameraId { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string StoreId { get; set; }
    }

     public class FunctionResponseModel
    {
        public string Statuscode { get; set; }
        public string Message { get; set; }
    }

}
