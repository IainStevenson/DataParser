using System;

namespace DataParser
{
    public class ReportByDateAndIP : DataStatistics
    {
        public string ExternalIp { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public DateTime Date { get; set; }
    }

}
