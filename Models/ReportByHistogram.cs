using System;

namespace DataParser
{
    public class DataHistogramStatstics
    {

        public decimal Down { get; set; }
        public decimal Up { get; set; }
        public decimal Jitter { get; set; }
        public decimal Latency { get; set; }
    }
    public class ReportByHistogram:DataHistogramStatstics
    {
        public DateTime Timestamp { get; set; }
        public string ISP { get; set; }
    }

    public class ReportByDateHistogram:DataHistogramStatstics
    {
        public DateTime Timestamp { get; set; }
        public string ISP { get; set; }
    }

}
