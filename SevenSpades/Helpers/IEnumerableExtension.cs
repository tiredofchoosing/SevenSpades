namespace SevenSpades.Helpers
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<int> IndexesOf(this IEnumerable<int> list, int value)
        {
            int index = 0;
            foreach (var elem in list)
            {
                if (elem == value)
                {
                    yield return index;
                }
                index++;
            }
        }
    }
}
