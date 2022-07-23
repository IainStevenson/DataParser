using DataParser.Models.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataParser
{

    public class ReportHandlerByDateAndIP : IReportHandler
    {
        public ReportTypes ReportType { get; } = ReportTypes.DateAndIP;

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

                var thisDaysSummaries = analysis.Summaries
                                                .Where(x => x.Key.StartsWith($"{reportItem.Date}"))
                                                .Select(y => y.Value)
                                                .ToList();
                var thisDaysItems = thisDaysSummaries.SelectMany(x => x).ToArray();
                var thisDaysDownloadItems = thisDaysItems.Select(x => x.BandwidthDown).ToArray();
                var thisDaysUploadItems = thisDaysItems.Select(x => x.BandwidthUp).ToArray();


                SetStatistics(reportItem.Download,
                                        summary.Min(x => x.BandwidthDown),
                                        summary.Max(x => x.BandwidthDown),
                                        (summary.LastOrDefault()?.BandwidthDown ?? 1),
                                        thisDaysDownloadItems);


                SetStatistics(reportItem.Upload,
                                        summary.Min(x => x.BandwidthUp),
                                        summary.Max(x => x.BandwidthUp),
                                        (summary.LastOrDefault()?.BandwidthUp ?? 1),
                                        thisDaysUploadItems);


                reportItems.Add(reportItem);

            }

            var reportTitle = $"\nBy Date and IP Address Analysis Report:";

            response.Append($"\nFound {analysis.Files.Count()} distinct files");
            response.Append($"\nFound {analysis.Summaries.Keys.Count()} distinct ip addresses");
            var newLineSpacer = $"\n                                                                                ";
            var dataSectionUnderline = "---------------------------------------------";
            var fullUnderLine = $"\n{dataSectionUnderline}{dataSectionUnderline}{dataSectionUnderline}{dataSectionUnderline}--";
            var contextDescription = $"\n{"Date",-12}\t{"Address",-15}\t{"From",-22}\t{"To",-22}\t";
            var dataSectionDesrciption = $"Min   \t25th  \t50th  \t75th  \tMax   \tLast\t";
            StringBuilder report = new StringBuilder();
            report.Append(reportTitle);
            report.Append($"{newLineSpacer}Mbit/s");
            
            response.Append(report);
            
            report.Clear();
            
            report.Append($"{newLineSpacer}Down                                            Up                                         ");
            
            response.Append(report);
            report.Clear();
            
            report.Append($"{newLineSpacer}{dataSectionUnderline}{dataSectionUnderline}");
            response.Append(report);
            report.Clear();
            
            report.Append($"{contextDescription}{dataSectionDesrciption}{dataSectionDesrciption}Tests");
            response.Append(report);
            report.Clear();
            
            report.Append(fullUnderLine);
            response.Append(report);

            foreach (var reportItem in reportItems.OrderBy(x => x.To))
            {
                report.Clear();

                report.Append($"\n{reportItem.Date,-12:yyyy-MM-dd}\t");
                report.Append($"{reportItem.ExternalIp,-15}\t");
                report.Append($"{reportItem.From,-22:yyyy-MM-dd HH:mm:ss}\t");
                report.Append($"{reportItem.To,-22:yyyy-MM-dd HH:mm:ss}\t");
                report.Append($"{reportItem.Download.Min,-6:00.00}\t");
                report.Append($"{reportItem.Download.P25,-6:00.00}\t");
                report.Append($"{reportItem.Download.P50,-6:00.00}\t");
                report.Append($"{reportItem.Download.P75,-6:00.00}\t");
                report.Append($"{reportItem.Download.Max,-6:00.00}\t");
                report.Append($"{reportItem.Download.Last,-6:00.00}\t");
                report.Append($"{reportItem.Upload.Min,-6:00.00}\t");
                report.Append($"{reportItem.Upload.P25,-6:00.00}\t");
                report.Append($"{reportItem.Upload.P50,-6:00.00}\t");
                report.Append($"{reportItem.Upload.P75,-6:00.00}\t");
                report.Append($"{reportItem.Upload.Max,-6:00.00}\t");
                report.Append($"{reportItem.Upload.Last,-6:00.00}\t");
                report.Append($"{reportItem.Entries,6:000}");

                response.Append(report);

            }

            return response;
        }

        private void SetStatistics(Statistics stats, long min, long max, long last, long[] thisDaysStatItems)
        {
            stats.Min = min / 125000m;
            stats.P25 = ((decimal)Math.Floor(thisDaysStatItems.Percentile(25))) / 125000m;
            stats.P50 = ((decimal)Math.Floor(thisDaysStatItems.Percentile(50))) / 125000m;
            stats.P75 = ((decimal)Math.Floor(thisDaysStatItems.Percentile(55))) / 125000m;
            stats.Max = max / 125000m;
            stats.Last = last / 125000m;
        }
    }
}
