using System;

namespace DataParser.Models.Analysis
{
    public class ReportByDateHistogram : DataHistogramStatstics
    {
        public DateTime Timestamp { get; set; }
        public string ISP { get; set; }
    }

}
