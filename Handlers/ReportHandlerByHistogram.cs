using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataParser
{

    public class ReportHandlerByHistogram : IReportHandler
    {
        public static decimal BytesPerSecondToBitsPerSecond = 125000m;
        public ReportTypes ReportType { get; } = ReportTypes.Histogram;
        public StringBuilder Report(Analysis analysis)
        {
            StringBuilder response = new StringBuilder();



            var reportItems = new List<ReportByHistogram>();
            Console.WriteLine("Reporting Histogram");

            StringBuilder report = new StringBuilder();

            foreach (var index in analysis.Summaries.Keys.OrderBy(x => x)) // order by date/IP
            {
                var summaries = analysis.Summaries[index];

                foreach (var data in summaries.OrderBy(x => x.Timestamp))
                {
                    var reportItem = new ReportByHistogram();
                    reportItem.Timestamp = data.Timestamp;
                    reportItem.Down = data.BandwidthDown / BytesPerSecondToBitsPerSecond;
                    reportItem.Up = data.BandwidthUp / BytesPerSecondToBitsPerSecond;
                    reportItem.ISP = data.ISP;
                    reportItem.Jitter = data.Jitter;
                    reportItem.Latency = data.Latency;

                    reportItems.Add(reportItem);

                }
            }

            Console.WriteLine($"Report Items {reportItems.Count}");


            response.Append($"\nAnalysis Report: ");
            response.Append($"\n\nTotals: Min Down {analysis.Totals.MinDown / BytesPerSecondToBitsPerSecond,-6:00.00} Max Down {analysis.Totals.MaxDown / BytesPerSecondToBitsPerSecond,-6:00.00} Min Up {analysis.Totals.MinUp / 125000m,-6:00.00} Max Up {analysis.Totals.MaxUp / 125000m,-6:00.00}\n");

            report.Append($"\n{"Time",-21}\t");
            report.Append($"{"Down",-6}\t");
            report.Append($"{"Up",-6}\t");
            report.Append($"{"Jitter",-6}\t");
            report.Append($"{"Latency",-6}\t");
            response.Append(report);


            foreach (var reportItem in reportItems.OrderBy(x => x.Timestamp))
            {
                report.Clear();

                report.Append($"\n{ reportItem.Timestamp,-21:yyyy-MM-dd hh:MM:ss}\t");
                report.Append($"{ reportItem.Down,-6:00.00}\t");
                report.Append($"{ reportItem.Up,-6:00.00}\t");
                report.Append($"{ reportItem.Jitter,-7:0.00}\t");
                report.Append($"{ reportItem.Latency,-7:0.00}\t");

                response.Append(report);

            }


            return response;
        }
    }
}
