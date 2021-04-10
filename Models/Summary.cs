using System;

namespace DataParser
{

    public class Summary
    {       

        public DateTime Timestamp { get; set; }
        public string ExternalIp { get; set; }
        public long BandwidthUp { get; set; }
        public long BandwidthDown { get; set; }

        public decimal Jitter { get; set;}
        public decimal Latency { get; set;}
        public string ISP { get; set;}        
    }
}