using System.Collections.Generic;

namespace DataParser
{
    public class Analysis
    {
        
        /// <summary>
        /// Analysis dictionary indexed via string combination of ISO date YYY-MM-DD + space + IP Addresss
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<Summary>> Summaries { get; set; } = new Dictionary<string, List<Summary>>();
        public Totals Totals { get;set;} = new Totals ();
    }
}
