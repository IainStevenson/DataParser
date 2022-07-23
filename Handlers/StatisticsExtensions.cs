namespace DataParser
{
    public static class StatisticsExtensions
    {
        /// <summary>
        /// From a sequence of numbers finds the nth percentile value using the nearest rank method.
        /// <example>
        /// var value = Percentile( [1,2,3,4,5,6,7,8,9,10], 90) 
        /// returns value of 9
        /// <\example>
        /// </summary>
        public static double Percentile(this long[] sequence, int  percentile)
        {
            if (percentile > 100) percentile = 100;
            if (percentile < 1 ) percentile = 1;

            System.Array.Sort(sequence);
            
            int N = sequence.Length;
           
            decimal realIndex = (percentile /100m) * (sequence.Length-1);
            int index=(int)realIndex;
            
            return (double)sequence[index];
        }
    }
}