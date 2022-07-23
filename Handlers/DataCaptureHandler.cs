using System;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using DataParser.Models.Capture;
using DataParser.Models.Analysis;

namespace DataParser
{
    /// <summary>
    /// Reads the raw json data and turns it into an in memory array for faster saving and loading for analysis and reporting
    /// </summary>
    public class DataCaptureHandler
    {

        public DataSource Capture(DataSource datasource, DirectoryInfo sourceDirectory, List<string> files)
        {
            var response = datasource;


            if (files.Except(response.Files).Count() > 0)
            {
                var newFiles = files.Except(response.Files);
                Console.WriteLine($"Processing {newFiles.Count()} new files... ");

                foreach (var file in newFiles)
                {
                    try
                    {
                        Console.Write(".");
                        var textData = File.ReadAllText($@"{sourceDirectory.FullName}\{file}");

                        if (!string.IsNullOrWhiteSpace(textData))
                        {

                            var data = JsonConvert.DeserializeObject<Data>($"{textData}");
                            if (data.Interface != null &&
                                data.Ping != null &&
                                data.Download != null &&
                                data.Upload != null)
                            {
                                // usefull record
                                response.Items.Add(data);
                            }
                            else
                            {
                                //Console.WriteLine($"{file} {data.Timestamp} was an error!");
                            }
                        }
                    }
                    catch //(System.Exception ex)
                    {
                        //Console.WriteLine($"Could not read {file} {ex.Message}");
                    }
                    response.Files.Add(file);
                }
                Console.WriteLine();
            }
            return response;
        }
    }
}
