using System;
using System.IO;
using System.Linq;

namespace DataParser
{
    public class ArgumentsHandler
    {
        /// <summary>
        /// Validates and returns the --filePattern argument
        /// </summary>
        /// <param name="args">The arguments provided</param>
        /// <param name="defaultResponse">The default value to return</param>
        /// <returns>Either the validated provided argument or the default value.</returns>
        public string GetFilePattern(string[] args, string defaultResponse)
        {
            var response = defaultResponse;

            if (args.Length > 0)
            {
                var arg = args.FirstOrDefault(x => x.StartsWith("--filePattern=", StringComparison.InvariantCultureIgnoreCase)
                                               || x.StartsWith("--p=", StringComparison.InvariantCultureIgnoreCase)
                                                );
                if (arg != null)
                {
                    var values = arg.Split('=', StringSplitOptions.RemoveEmptyEntries);
                    response = values[1];
                }
            }
            return response;
        }
        /// <summary>
        /// Validates the --sourceDirectory argument
        /// </summary>
        /// <param name="args">The arguments provided</param>
        /// <param name="defaultResponse">The default value to return</param>
        /// <returns>Either the validated provided argument or the default value.</returns>
        public string GetSourceDirectory(string[] args, string defaultResponse)
        {
            var response = defaultResponse;

            if (args.Length > 0)
            {
                var arg = args.FirstOrDefault(x => x.StartsWith("--sourceDirectory=", StringComparison.InvariantCultureIgnoreCase)
                                               || x.StartsWith("--s=", StringComparison.InvariantCultureIgnoreCase)
                                                );
                if (arg != null)
                {
                    var values = arg.Split('=', StringSplitOptions.RemoveEmptyEntries);
                    if (Directory.Exists(values[1]))
                    {
                        response = values[1];
                    }
                    else
                    {
                        throw new Exception($"Specified folder {arg} does not exist.");
                    }
                }
            }
            return response;
        }
        /// <summary>
        /// Validates the --analysisFile argument
        /// </summary>
        /// <param name="args">The arguments provided</param>
        /// <param name="defaultResponse">The default value to return</param>
        /// <returns>Either the validated provided argument or the default value.</returns>
        public string GetAnalysisFile(string[] args, string defaultResponse)
        {
            var response = defaultResponse;

            if (args.Length > 0)
            {
                var arg = args.FirstOrDefault(x => x.StartsWith("--analysisFile=", StringComparison.InvariantCultureIgnoreCase)
                                               || x.StartsWith("--a=", StringComparison.InvariantCultureIgnoreCase)
                                                );
                if (arg != null)
                {
                    var values = arg.Split('=', StringSplitOptions.RemoveEmptyEntries);
                    if (Directory.Exists(values[1]))
                    {
                        response = values[1];
                    }
                    else
                    {
                        throw new Exception($"Specified analysis file folder {arg} does not exist.");
                    }
                }
            }
            return response;
        }
        /// <summary>
        /// Validates the --reportFile argument
        /// </summary>
        /// <param name="args">The arguments provided</param>
        /// <param name="defaultResponse">The default value to return</param>
        /// <returns>Either the validated provided argument or the default value.</returns>
        public string GetReportFile(string[] args, string defaultResponse)
        {
            var response = defaultResponse;

            if (args.Length > 0)
            {
                var arg = args.FirstOrDefault(x => x.StartsWith("--reportFile=", StringComparison.InvariantCultureIgnoreCase)
                                               || x.StartsWith("--r=", StringComparison.InvariantCultureIgnoreCase)
                                                );
                if (arg != null)
                {
                    var values = arg.Split('=', StringSplitOptions.RemoveEmptyEntries);
                    if (Directory.Exists(values[1]))
                    {
                        response = values[1];
                    }
                    else
                    {
                        throw new Exception($"Specified report file folder {arg} does not exist.");
                    }
                }
            }
            return response;
        }

        /// <summary>
        /// Validates the --reportType argument
        /// </summary>
        /// <param name="args">The arguments provided</param>
        /// <param name="defaultResponse">The default value to return</param>
        /// <returns>Either the validated provided argument or the default value.</returns>
        public ReportTypes GetReportType(string[] args, ReportTypes defaultValue)
        {
            var response = defaultValue;
            Console.WriteLine($"{args.Length} arguments specified.");
            
            if (args.Length > 0)
            {
                var arg = args.FirstOrDefault(x => x.StartsWith("--reportBy=", StringComparison.InvariantCultureIgnoreCase)
                                               || x.StartsWith("--b=", StringComparison.InvariantCultureIgnoreCase)
                                                );
                if (arg != null)
                {
                    var values = arg.Split('=', StringSplitOptions.RemoveEmptyEntries);
                    switch (values[1].ToLower())
                    {
                        case "date":
                            response = ReportTypes.DateAndIP;
                            break;
                        case "hist":
                            response = ReportTypes.Histogram;
                            break;
                        default:
                        response = defaultValue;
                            
                        break;
                    }
                    Console.WriteLine($"A {response} report was requested.");
                    return response;            
                }
            }
            return response;
        }
    }

}
