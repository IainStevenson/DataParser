using System;

namespace DataParser
{
    public class ReportByHistogram : DataHistogramStatstics
    {
        public DateTime Timestamp { get; set; }
        public string ISP { get; set; }
    }

}
