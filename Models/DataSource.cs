using DataParser.Models.Capture;
using System.Collections.Generic;
namespace DataParser
{
    public class DataSource
    {
        /// <summary>
        /// List of discovered files, may be more thatn capturedas some may be errors
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <returns></returns>
        public List<string> Files { get; set; } = new List<string>();

        /// <summary>
        /// Collection of data items
        /// </summary>
        /// <returns></returns>
        public List<Data> Items { get; set; } = new List< Data>();

    }
}