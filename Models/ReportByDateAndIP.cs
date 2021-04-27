using System;

namespace DataParser
{
    public class ReportByDateAndIP
    {
        public string ExternalIp { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Entries { get; set; }
        public decimal MinUp { get; set; }
        public double AvgUp { get; set; }
        public decimal MaxUp { get; set; }
        public double AvgDown { get; set; }
        public decimal MaxDown { get; set; }
        public decimal MinDown { get; set; }
        public decimal LastUp { get; set; }
        public decimal LastDown { get; set; }
        public DateTime Date { get; set; }

        public decimal GBDown { get;set;}
        public decimal GbUp { get;set;}
        

    }

}
