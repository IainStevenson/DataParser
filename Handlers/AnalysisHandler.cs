using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DataParser
{

    /// <summary>
    /// Summaries the data
    /// </summary>
    public class AnalysisHandler
    {
        public Analysis Analyse(DataSource datasource)
        {

            var response = new Analysis();

            foreach (var data in datasource.Items)
            {
                var summary = new Summary()
                {
                    Timestamp = data.Timestamp,
                    ExternalIp = data.Interface.ExternalIp,
                    BandwidthDown = data.Download.Bandwidth,
                    BandwidthUp = data.Upload.Bandwidth,
                    Jitter = data.Ping.Jitter,
                    Latency = data.Ping.Latency,
                    ISP = data.Isp
                };

                var itemDate = DateTime.Parse(summary.Timestamp.ToLongDateString());

                var index = $"{itemDate}\t{summary.ExternalIp}";
                
                if (response.Summaries.ContainsKey(index))
                {
                    response.Summaries[index].Add(summary);
                }
                else
                {
                    response.Summaries.Add(index, new List<Summary>() {summary});
                }

                
            }
            

            return response;
        }
    }
}
