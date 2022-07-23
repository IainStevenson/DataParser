using System;

namespace DataParser
{
    public class DataStatistics
    {

        public int Entries { get; set; }
        public decimal MinUp { get; set; }
        public double AvgUp { get; set; }
        public double N95Up { get; set; }
        public decimal MaxUp { get; set; }
        public double AvgDown { get; set; }
        public double N95Down { get; set; }
        public decimal MaxDown { get; set; }
        public decimal MinDown { get; set; }
        public decimal LastUp { get; set; }
        public decimal LastDown { get; set; }
    }
    public class ReportByDate: DataStatistics
    {
        public DateTime Date { get; set; }
    }

}
