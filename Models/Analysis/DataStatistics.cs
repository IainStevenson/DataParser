using System;

namespace DataParser.Models.Analysis
{
    public class DataStatistics
    {

        public int Entries { get; set; }
        public decimal MinUp { get; set; }
        public double AvgUp { get; set; }
        [Obsolete]
        public double N95Up { get; set; }
        public double P25Up { get; set; }
        public double P50Up { get; set; }
        public double P75Up { get; set; }
        public decimal MaxUp { get; set; }
        public double AvgDown { get; set; }
        [Obsolete]
        public double N95Down { get; set; }
        public double P25Down { get; set; }
        public double P50Down { get; set; }
        public double P75Down { get; set; }
        public decimal MaxDown { get; set; }
        public decimal MinDown { get; set; }
        public decimal LastUp { get; set; }
        public decimal LastDown { get; set; }
    }

}
