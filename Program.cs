using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            var argumentsHandler = new ArgumentsHandler();
            var fileHandler = new FileHandler();
            var datasourceHandler = new DataSourceHandler();

            var sourceDirectory = new DirectoryInfo(argumentsHandler.GetSourceDirectory(args, $@"..\Data"));
            var filePattern = argumentsHandler.GetFilePattern(args, "*.json");
            var datasourceFile = argumentsHandler.GetAnalysisFile(args, "datasource.json");
            var reportFile = argumentsHandler.GetReportFile(args, "analysis.txt");
            var reportType = argumentsHandler.GetReportType(args, ReportTypes.DateAndIP);

            try
            {


                var files = fileHandler.FindFileNames(sourceDirectory, filePattern);
                Console.WriteLine($"Discovered {files.Count} files in total.");

                Console.WriteLine($"Loading previous data... ");
                var datasource = datasourceHandler.Load(datasourceFile);
                Console.WriteLine($"{datasource.Files.Count} files arlready loaded. ");
                
                var newFilesCount = files.Except(datasource.Files).Count();
                Console.WriteLine($"Discovered {newFilesCount} new files found.");


                if (newFilesCount > 0)
                {
                    Console.WriteLine($"Capturing new data ... ");
                    datasource = new DataCaptureHandler().Capture(datasource, sourceDirectory, files);
                    Console.WriteLine($"Saving new data ... ");
                    datasourceHandler.Save(datasource, datasourceFile);
                    Console.WriteLine($"Data     has been written to {new System.IO.FileInfo(datasourceFile).FullName}");
                }

                Console.WriteLine($"Analysing data ... ");                    
                var analysis = new AnalysisHandler().Analyse(datasource);

                Console.WriteLine($"Generating requested Report {reportType}... ");                    
                var report = new ReportHandler(reportHandlers).Report(analysis, reportType);

                var reportFilename = fileHandler.SaveReport(reportFile, reportType, report);
                Console.WriteLine($"Report   has been written to {new System.IO.FileInfo(reportFilename.ToString()).FullName}");

                // send to output
                Console.WriteLine(report);

                // advise of files
                

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception encountered: {ex.Message}");
            }
        }
    }
}
