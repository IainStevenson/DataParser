using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataParser
{
    class Program
    {

        static void Main(string[] args)
        {
            // create the available report handlers
            var reportHandlers = new Dictionary<ReportTypes, IReportHandler>() {
                { ReportTypes.DateAndIP, new ReportHandlerByDateAndIP()},
                { ReportTypes.Histogram, new ReportHandlerByHistogram()}
            };

            try
            {
                var argumentsHandler = new ArgumentsHandler();

                var sourceDirectory = argumentsHandler.GetSourceDirectory(args, $@"..\Data");
                var filePattern = argumentsHandler.GetFilePattern(args, "*.json");
                var analysisFile = argumentsHandler.GetAnalysisFile(args, "analysis.json");
                var reportFile = argumentsHandler.GetReportFile(args, "analysis.txt");
                var reportType = argumentsHandler.GetReportType(args, ReportTypes.DateAndIP);

                // perform the required analysis of the data files
                var analysis = new AnalysisHandler().Analyse(sourceDirectory, filePattern, analysisFile);

                // genreate the required report type
                var report = new ReportHandler(reportHandlers).Report(analysis, reportType);

                // save the output (overwrite previous as its cumulative (at the moment))
                var file = new FileInfo(reportFile);
                var reportFilename = new StringBuilder(file.Name);
                reportFilename.Replace(file.Extension, $"-{reportType}{file.Extension}");                
                System.IO.File.WriteAllText(reportFilename.ToString(), report.ToString());

                // send to output
                Console.WriteLine(report);

                // advise of files
                Console.WriteLine($"Analysis has been written to {new System.IO.FileInfo(analysisFile).FullName}");
                Console.WriteLine($"Report   has been written to {new System.IO.FileInfo(reportFilename.ToString()).FullName}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}
