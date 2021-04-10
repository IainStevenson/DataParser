using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataParser
{
    public class FileHandler
    {
        public FileHandler()
        {
        }

        public List<string> FindFileNames(DirectoryInfo directory, string filePattern)
        {
            var response = new List<string>();
            if (directory.Exists)
            {
                foreach(var file in directory.GetFiles(filePattern, SearchOption.TopDirectoryOnly))
                {
                    response.Add(file.Name);
                }
            }
            return response;
        }

        public string SaveReport(string reportFile, ReportTypes reportType, StringBuilder report)
        {
            var file = new FileInfo(reportFile);
            var reportFilename = new StringBuilder(file.Name);
            reportFilename.Replace(file.Extension, $"-{reportType}{file.Extension}");
            reportFile = reportFilename.ToString();
            System.IO.File.WriteAllText(reportFile, report.ToString());
            return reportFile;
        }
    }
}