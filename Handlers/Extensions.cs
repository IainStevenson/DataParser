namespace DataParser
{
    public static class Extensions
    {
        public static void ReplaceWithIfLessThan(this ref long source, long newValue)
        {
            if (newValue < source)
                source = newValue;
        }
        public static void ReplaceWithIfMoreThan(this ref long source, long newValue)
        {
            if (newValue > source)
                source = newValue;
        }

    }
}
