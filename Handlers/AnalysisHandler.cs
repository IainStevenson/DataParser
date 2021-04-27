using System;
using System.Collections.Generic;

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
                    BytesDown = data.Download.Bytes,
                    BandwidthUp = data.Upload.Bandwidth,
                    BytesUp = data.Upload.Bytes,
                    Jitter = data.Ping.Jitter,
                    Latency = data.Ping.Latency,
                    ISP = data.Isp,
                    
                };

                var itemDate = DateTime.Parse(summary.Timestamp.ToLongDateString());

                var index = $"{itemDate}\t{summary.ExternalIp}";

                if (response.Summaries.ContainsKey(index))
                {
                    response.Summaries[index].Add(summary);
                }
                else
                {
                    response.Summaries.Add(index, new List<Summary>() { summary });
                }
                response.Totals.MinDown = Math.Min(response.Totals.MinDown, summary.BandwidthDown);
                response.Totals.MaxDown = Math.Max(response.Totals.MaxDown, summary.BandwidthDown);
                response.Totals.MinUp = Math.Min(response.Totals.MinUp, summary.BandwidthUp);
                response.Totals.MaxUp = Math.Max(response.Totals.MaxUp, summary.BandwidthUp);

            }
            return response;
        }
    }
}
