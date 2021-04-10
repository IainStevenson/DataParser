using System;

namespace DataParser
{
    public class ReportByHistogram
    {
        public DateTime Timestamp { get; set; }
        public string ISP { get; set; }
        public decimal Down { get; set; }
        public decimal Up { get; set; }
        public decimal Jitter { get; set; }
        public decimal Latency { get; set; }
    }

}
