using System;

namespace DataParser.Models.Analysis
{
    public class DataStatistics
    {
        public int Entries { get; set; }
        public Statistics Download { get; set;} = new Statistics();
        public Statistics Upload { get; set; } = new Statistics();
    }

}
