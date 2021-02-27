using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElkIngestDataRetail
{
    public class ELKHelper
    {

        /// <summary>
        /// Data is Getting Inserted Properly, it is working as expected
        /// </summary>
        /// <param name="groupfootfall"></param>
        public void InsertRetailDataToElk(RetailGroupFoolFallElk groupfootfall)
        {
            try
            {
                var uri = new Uri("http://127.0.0.1:9200/");
                var settings = new ConnectionSettings(uri);
                var client = new ElasticClient(settings);
                var day = DateTimeOffset.Now.Day;
                var month = DateTimeOffset.Now.Month;
                var year = DateTimeOffset.Now.Year;

                string indexType = "elktest1";
                var DateInString = String.Concat(year, month, day);
                var indexName = String.Concat(indexType, "_", DateInString);

                settings.DefaultIndex(indexName);
                var clientResponse = client.IndexDocument(groupfootfall);

                if (clientResponse.IsValid)
                {
                    System.Threading.Thread.Sleep(500);
                    Console.WriteLine("Done");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void InsertRetailDataToElkDateWise(RetailGroupFoolFallElk groupfootfall)
        {
            try
            {
                var uri = new Uri("https://localhost:7200/");
                var settings = new ConnectionSettings(uri);
                var client = new ElasticClient(settings);
                var day = groupfootfall.timestamp.Date.ToString("dd");
                var month = groupfootfall.timestamp.Date.ToString("MM");
                var year = groupfootfall.timestamp.Year;

                string indexType = "_prod";
                var DateInString = String.Concat(year, month, day);
                var indexName = String.Concat(indexType, "_", DateInString);

                settings.DefaultIndex(indexName);
                var clientResponse = client.IndexDocument(groupfootfall);

                //string GroupFootFallData = "{\"Membercount\":\"4\",  \"Direction\":\"entry\", \"Type\":\"Customer\"}";
                ////var clientResponse = client.Index(GroupFootFallData, i => i.Index(indexName));
                //var clientResponse = client.IndexDocument(GroupFootFallData);


                if (clientResponse.IsValid)
                {
                    System.Threading.Thread.Sleep(500);
                    Console.WriteLine("Done");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void InsertRetailDataToElkDateWise(RetailHeatMapElk heatmap)
        {
            try
            {
                var uri = new Uri("https://localhost:7200/");
                var settings = new ConnectionSettings(uri);
                var client = new ElasticClient(settings);
                var day = heatmap.timestamp.Day;
                var month = heatmap.timestamp.Month;
                var year = heatmap.timestamp.Year;

                string indexType = "_elk_prod";
                var DateInString = String.Concat(year, month, day);
                var indexName = String.Concat(indexType, "_", DateInString);

                settings.DefaultIndex(indexName);
                var clientResponse = client.IndexDocument(heatmap);

                //string GroupFootFallData = "{\"Membercount\":\"4\",  \"Direction\":\"entry\", \"Type\":\"Customer\"}";
                ////var clientResponse = client.Index(GroupFootFallData, i => i.Index(indexName));
                //var clientResponse = client.IndexDocument(GroupFootFallData);


                if (clientResponse.IsValid)
                {
                    System.Threading.Thread.Sleep(500);
                    Console.WriteLine("Done");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        /// <summary>
        /// Update a Document Using id For An Index.
        /// </summary>
        /// <param name="gff"></param>
        public void UpdateDataToElk(RetailGroupFoolFallElk gff)
        {
            try
            {
                var uri = new Uri("http://127.0.0.1:9200/");
                var settings = new ConnectionSettings(uri);
                var client = new ElasticClient(settings);
                var day = DateTimeOffset.Now.Day;
                var month = DateTimeOffset.Now.Month;
                var year = DateTimeOffset.Now.Year;

                string indexType = "elktest1_20201216";
                var DateInString = String.Concat(year, month, day);
                var indexName = String.Concat(indexType, "_", DateInString);

                //dynamic updateDoc = new System.Dynamic.ExpandoObject();
                //updateDoc.Title = "My new title";

                var response = client.Update(DocumentPath<RetailGroupFoolFallElk>
                .Id("nKt5anYBivAaQj_VvueF"),
                u => u
                    .Index(indexType)
                    .DocAsUpsert(true)
                    .Doc(gff)
                    
                    );

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Delete a document using Id
        /// </summary>
        public void DeleteDataFromElk()
        {

            try
            {

                var uri = new Uri("http://localhost:9200/");
                var settings = new ConnectionSettings(uri);
                var client = new ElasticClient(settings);
                var day = DateTimeOffset.Now.Day;
                var month = DateTimeOffset.Now.Month;
                var year = DateTimeOffset.Now.Year;

                var searchID = "nKt5anYBivAaQj_VvueF";

                string indexType = "elktest1_20201216";
                var DateInString = String.Concat(year, month, day);
                var indexName = String.Concat(indexType, "_", DateInString);
                var response = client.Delete<RetailGroupFoolFallElk>(searchID, d => d
                .Index(indexType));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

    }
}
