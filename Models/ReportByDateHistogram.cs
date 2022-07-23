using System;

namespace DataParser
{
    public class ReportByDateHistogram : DataHistogramStatstics
    {
        public DateTime Timestamp { get; set; }
        public string ISP { get; set; }
    }

}
