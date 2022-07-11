using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataParser
{
    public class ReportHandlerByDateAndIP : IReportHandler
    {
        public ReportTypes ReportType { get; } = ReportTypes.DateAndIP;


        /// <summary>
        /// From a sequence of numbers finds the nth percentile value using the nearest rank method.
        /// <example>
        /// var value = Percentile( [1,2,3,4,5,6,7,8,9,10], 90) 
        /// returns value of 9
        /// <\example>
        /// </summary>
        public double Percentile(long[] sequence, int  percentile)
        {
            if (percentile > 100) percentile = 100;
            if (percentile < 1 ) percentile = 1;

            Array.Sort(sequence);
            
            int N = sequence.Length;
           
            decimal realIndex = (percentile /100m) * (sequence.Length-1);
            int index=(int)realIndex;
            
            return (double)sequence[index];

            // if(index+1<elements.Length)
            //     return (double)(elements[index]*(1-frac)+elements[index+1]*frac);
            // else
            //     
        }


        public StringBuilder Report(Analysis analysis)
        {
            StringBuilder response = new StringBuilder();
            var reportItems = new List<ReportByDateAndIP>();

            foreach (var index in analysis.Summaries.Keys.OrderBy(x => x))
            {
                var reportItem = new ReportByDateAndIP();

                var summary = analysis.Summaries[index];

                var keyArgs = index.Split('\t', System.StringSplitOptions.RemoveEmptyEntries);
                var date = DateTime.Parse(keyArgs[0]);
                var address = keyArgs[1];

                reportItem.Date = date;
                reportItem.ExternalIp = address;

                reportItem.Entries = summary.Count;

                reportItem.From = summary.Min(x => x.Timestamp);
                reportItem.To = summary.Max(x => x.Timestamp);


                reportItem.MinDown = summary.Min(x => x.BandwidthDown) / 125000m;
                reportItem.AvgDown = summary.Average(x => x.BandwidthDown) / 125000;

                var thisDaysSummaries = analysis.Summaries
                            .Where( x=> x.Key.StartsWith( $"{reportItem.Date}" )).Select(y=>y.Value).ToList();

                var thisDaysItems = thisDaysSummaries.SelectMany(x => x ).ToArray();
                // long[] thisdaysdown = thisDaysSummaries.Select(x => x.BandwidthDown).ToArray();

                var n95Up = Math.Floor(this.Percentile(thisDaysItems.Select(x=>x.BandwidthUp).ToArray(), 95)) ;
                var n95Down = Math.Floor(this.Percentile(thisDaysItems.Select(x=>x.BandwidthDown).ToArray(), 95)) ;

                reportItem.N95Up = (double) (n95Up / 125000);
                reportItem.MaxDown = summary.Max(x => x.BandwidthDown) / 125000m;
                reportItem.MinUp = summary.Min(x => x.BandwidthUp) / 125000m;
                reportItem.AvgUp = summary.Average(x => x.BandwidthUp) / 125000;
                reportItem.MaxUp = summary.Max(x => x.BandwidthUp) / 125000m;
                reportItem.N95Down = (double) (n95Down / 125000);

                reportItem.LastUp = (summary.LastOrDefault()?.BandwidthUp ?? 1) / 125000m;
                reportItem.LastDown = (summary.LastOrDefault()?.BandwidthDown ?? 1) / 125000m;

                reportItems.Add(reportItem);

            }

            response.Append($"\nAnalysis Report: ");
            response.Append($"\nFound {analysis.Files.Count()} distinct files");
            response.Append($"\nFound {analysis.Summaries.Keys.Count()} distinct ip addresses");

            StringBuilder report = new StringBuilder();
            report.Append($"\n                                                                                Mbit/s");
            response.Append(report);
            report.Clear();
            report.Append($"\n                                                                                Down                                    Up                                ");
            response.Append(report);
            report.Clear();
            report.Append($"\n                                                                                ------------------------------------    --------------------------------------");
            response.Append(report);
            report.Clear();
            report.Append($"\n{"Date",-12}\t{"Address",-15}\t{"From",-22}\t{"To",-22}\t");
            report.Append($"Min   \tAvg   \t95th  \tMax   \tLast \t");
            report.Append($"Min   \tAvg   \t95th  \tMax   \tLast\t");
            report.Append($"Entries");
            response.Append(report);
            report.Clear();
            report.Append($"\n--------------------------------------------------------------------------------------------------------------------------------------------------------------");
            response.Append(report);

            foreach (var reportItem in reportItems.OrderBy(x => x.To))
            {
                report.Clear();

                report.Append($"\n{ reportItem.Date,-12:yyyy-MM-dd}\t");
                report.Append($"{ reportItem.ExternalIp,-15}\t");
                report.Append($"{ reportItem.From,-22:yyyy-MM-dd HH:mm:ss}\t");
                report.Append($"{ reportItem.To,-22:yyyy-MM-dd HH:mm:ss}\t");
                report.Append($"{ reportItem.MinDown,-6:00.00}\t");
                report.Append($"{ reportItem.AvgDown,-6:00.00}\t");
                report.Append($"{ reportItem.N95Down,-6:00.00}\t");
                report.Append($"{ reportItem.MaxDown,-6:00.00}\t");
                report.Append($"{ reportItem.LastDown,-6:00.00}\t");
                report.Append($"{ reportItem.MinUp,-6:00.00}\t");
                report.Append($"{ reportItem.AvgUp,-6:00.00}\t");
                report.Append($"{ reportItem.N95Up,-6:00.00}\t");
                report.Append($"{ reportItem.MaxUp,-6:00.00}\t");
                report.Append($"{ reportItem.LastUp,-6:00.00}\t");
                report.Append($"{ reportItem.Entries,6:000}");

                response.Append(report);

            }

            return response;
        }

    }
}
