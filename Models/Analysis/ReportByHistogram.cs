using System;

namespace DataParser.Models.Analysis
{
    public class ReportByHistogram : DataHistogramStatstics
    {
        public DateTime Timestamp { get; set; }
        public string ISP { get; set; }
    }

}
