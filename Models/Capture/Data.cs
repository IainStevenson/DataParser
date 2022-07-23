using System;

namespace DataParser.Models.Capture
{
    public class Data
    {
        public string Type { get; set; }
        public DateTime Timestamp { get; set; }
        public Ping Ping { get; set; }
        public Download Download { get; set; }
        public Upload Upload { get; set; }
        public decimal PacketLoss { get; set; }
        public string Isp { get; set; }
        public IPInterface Interface { get; set; }
        public Server Server { get; set; }
        public Result Result { get; set; }
    }

}
