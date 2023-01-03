using System.Text;

namespace Vheos.Tools.FilePatcher.Code.Helpers;

public static class Util
{
    public static string WithoutHexPrefix(this string @this) => @this.HasHexPrefix() ? @this[2..] : @this;
    public static bool HasHexPrefix(this string @this) => @this.Length >= 2 && @this[0] is '0' && @this[1] is 'x' or 'X';
    public static bool HasHexLetter(this string @this) => @this.Any(c => c is >= 'a' and <= 'f' or >= 'A' and <= 'F');
    public static bool IsDecimalNumber(this string @this) => @this.All(c => c is >= '0' and <= '9');
    public static bool IsHexNumber(this string @this) => @this.All(c => c is >= '0' and <= '9' or >= 'a' and <= 'f' or >= 'A' and <= 'F');

    public static string FormattedAOB(this IEnumerable<byte> @this)
    {
        StringBuilder builder = new();
        foreach (byte @byte in @this)
            _ = builder.Append($"{@byte:X2} ");
        return builder.ToString();
    }

    public static IEnumerable<string> SplitBySize(this string @this, int chunkSize, bool allowIncompleteChunk = false)
    {
        int length = @this.Length / chunkSize * chunkSize;

        int i;
        for (i = 0; i < length; i += chunkSize)
            yield return @this.Substring(i, chunkSize);

        if (allowIncompleteChunk && i < @this.Length)
            yield return @this[i..];
    }

    public static Dictionary<K2, V2> Retype<K1, V1, K2, V2>(this Dictionary<K1, V1> @this, Func<K1, K2> keyFunc, Func<V1, V2> valueFunc, IEqualityComparer<K2>? comparer = null)
        where K2 : notnull
        where K1 : notnull
    {
        Dictionary<K2, V2> toDictionary = new(comparer);
        foreach (KeyValuePair<K1, V1> kvp in @this)
            toDictionary[keyFunc(kvp.Key)] = valueFunc(kvp.Value);
        return toDictionary;
    }

    public static Dictionary<K2, V> RetypeKey<K1, V, K2>(this Dictionary<K1, V> @this, Func<K1, K2> keyFunc, IEqualityComparer<K2>? comparer = null)
        where K2 : notnull
        where K1 : notnull
        => @this.Retype(keyFunc, value => value, comparer);

    public static Dictionary<K, V2> RetypeValue<K, V1, V2>(this Dictionary<K, V1> @this, Func<V1, V2> valueFunc, IEqualityComparer<K>? comparer = null)
        where K : notnull
        => @this.Retype(key => key, valueFunc, comparer);
}
