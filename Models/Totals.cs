namespace DataParser
{
    public class Totals
    {
        public long MinDown { get; set; } = long.MaxValue;
        public long MaxDown { get; set; } = long.MinValue;
        public long MinUp { get; set; } = long.MaxValue;
        public long MaxUp { get; set; } = long.MinValue;

    }
}