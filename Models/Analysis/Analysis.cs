using System;
using System.Collections.Generic;

namespace DataParser.Models.Analysis
{
    public class Analysis
    {
        /// <summary>
        /// List of files succssfully analysed
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <returns></returns>
        public List<string> Files { get; set; } = new List<string>();

        /// <summary>
        /// Analysis dictionary indexed via string combination of ISO date YYYY-MM-DD + space + IP Addresss
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<Summary>> Summaries { get; set; } = new Dictionary<string, List<Summary>>();

    }

}
