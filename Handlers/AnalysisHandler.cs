using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DataParser
{
    public  class AnalysisHandler
    {
        public  Analysis Analyse(string sourceDirectory, string filePattern, string analysisFile)
        {
            var files = Directory.EnumerateFiles(sourceDirectory, filePattern, SearchOption.AllDirectories);
            Console.WriteLine($"Discovered {files.Count()} files in total.");
            Analysis analysis = new Analysis();
            Console.WriteLine($"Analysing data ... ");
            if (File.Exists(analysisFile))
            {
                Console.WriteLine($"Loading previous analysis... ");
                var analysisData = File.ReadAllText(analysisFile);
                analysis = JsonConvert.DeserializeObject<Analysis>(analysisData);
            }
            if (files.Except(analysis.Files).Count() > 0)
            {
                var newFiles = files.Except(analysis.Files);
                Console.WriteLine($"Processing {newFiles.Count()} new files... ");
                
                foreach (var file in newFiles)
                {
                    Console.Write(".");
                    var textData = File.ReadAllText(file);

                    if (!string.IsNullOrWhiteSpace(textData))
                    {
                        try
                        {
                            var data = JsonConvert.DeserializeObject<Data>($"{textData}");
                            if (data.Interface != null)
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
                                var itemDate  = DateTime.Parse(summary.Timestamp.ToLongDateString());
                                var index = $"{itemDate}\t{summary.ExternalIp}";
                                if (analysis.Summaries.ContainsKey(index))
                                {
                                    // same IP
                                    analysis.Summaries[index].Add(summary);
                                }
                                else
                                {
                                    // new IP
                                    analysis.Summaries.Add(index, new List<Summary>() { { summary } });
                                }                                
                            }
                            else
                            {
                                //Console.WriteLine($"{file} {data.Timestamp} was an error!");
                            }
                            analysis.Files.Add(file);                    
                        }
                        catch //(System.Exception ex)
                        {
                            //Console.WriteLine($"Could not read {file} {ex.Message}");
                        }
                    }
                }
                File.WriteAllText("analysis.json", JsonConvert.SerializeObject(analysis));
            }
            else
            {
                Console.WriteLine($"No new files to analyse... ");
            }
            Console.WriteLine();
                    
            return analysis;
        }
    }
}
