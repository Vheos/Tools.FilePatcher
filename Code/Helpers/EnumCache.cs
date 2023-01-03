namespace Vheos.Tools.FilePatcher.Code.Helpers
{
    public static class EnumCache
    {
        public static bool TryToEnumCached<T>(this string text, out T value) where T : struct, Enum
            => GenericEnumCache<T>.NamesByValue.TryGetValue(text, out value);

        public static T ToEnumCached<T>(this string text) where T : struct, Enum
            => text.TryToEnumCached<T>(out var value) ? value : default;

        private static class GenericEnumCache<T> where T : struct, Enum
        {
            public static readonly Dictionary<string, T> NamesByValue;

            static GenericEnumCache()
            {
                NamesByValue = new(StringComparer.OrdinalIgnoreCase);
                foreach (var name in Enum.GetNames<T>())
                    NamesByValue.Add(name, Enum.Parse<T>(name));
            }
        }
    }
}
