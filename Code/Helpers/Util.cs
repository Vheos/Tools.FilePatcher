using System.Diagnostics;
using System.Text;
using Vheos.Tools.FilePatcher.Controls;

namespace Vheos.Tools.FilePatcher.Code.Helpers;

public static class Util
{
    public static string WithoutHexPrefix(this string @this) => @this.HasHexPrefix() ? @this[2..] : @this;
    public static bool HasHexPrefix(this string @this) => @this.Length >= 2 && @this[0] is '0' && @this[1] is 'x' or 'X';
    public static bool HasHexLetter(this string @this) => @this.Any(c => c is >= 'a' and <= 'f' or >= 'A' and <= 'F');
    public static bool IsDecimalNumber(this string @this) => @this.All(c => c is >= '0' and <= '9');
    public static bool IsHexNumber(this string @this) => @this.All(c => c is >= '0' and <= '9' or >= 'a' and <= 'f' or >= 'A' and <= 'F');

    public static FileStream OpenForReading(this FileInfo @this) => @this.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
    public static FileStream OpenForWriting(this FileInfo @this) => @this.Open(FileMode.Open, FileAccess.Write, FileShare.None);
    public static bool CanBeRead(this FileInfo @this)
    {
        if (!@this.Exists)
            return false;

        try
        {
            @this.OpenForReading().Close();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static bool CanBeWritten(this FileInfo @this)
    {
        if (!@this.Exists || @this.IsReadOnly)
            return false;

        try
        {
            @this.OpenForWriting().Close();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static bool TryReadBytes(this FileInfo @this, out byte[] bytes, int offset, int length)
    {
        try
        {
            using var stream = @this.OpenForReading();
            stream.Position = offset;
            bytes = new byte[length];
            stream.ReadExactly(bytes);
            return true;
        }
        catch
        {
            bytes = Array.Empty<byte>();
            return false;
        }
    }
    public static byte[] ReadBytes(this FileInfo @this, int offset, int length)
    {
        if (!@this.TryReadBytes(out var text, offset, length))
            Debug.WriteLine($"Cannot read {length} bytes starting at {offset} from \"{@this.FullName}\"!");
        return text;
    }
    public static bool TryReadText(this FileInfo @this, out string text)
    {
        try
        {
            text = File.ReadAllText(@this.FullName);
            return true;
        }
        catch
        {
            text = string.Empty;
            return false;
        }
    }
    public static string ReadText(this FileInfo @this)
    {
        if (!@this.TryReadText(out var text))
            Debug.WriteLine($"Cannot read text from \"{@this.FullName}\"!");
        return text;
    }

    public static string FormattedAOB(this IEnumerable<byte> @this)
    {
        StringBuilder builder = new();
        foreach (var @byte in @this)
            builder.Append($"{@byte:X2} ");
        return builder.ToString();
    }

    public static void SetState(this Control @this, ControlState state)
    {
        @this.Visible = state != ControlState.Hidden;
        @this.Enabled = state == ControlState.Enabled;
    }

    public static ControlState GetState(this Control @this)
        => @this.Visible && @this.Enabled ? ControlState.Enabled
        : @this.Visible ? ControlState.Disabled
        : ControlState.Hidden;

    public static IEnumerable<string> SplitBySize(this string @this, int chunkSize, bool allowIncompleteChunk = false)
    {
        int length = @this.Length / chunkSize * chunkSize;

        int i = 0;
        for (; i < length; i += chunkSize)
            yield return @this.Substring(i, chunkSize);

        if (allowIncompleteChunk && i < @this.Length)
            yield return @this[i..];
    }

    public static Dictionary<K2, V2> Retype<K1, V1, K2, V2>(this Dictionary<K1, V1> @this, Func<K1, K2> keyFunc, Func<V1, V2> valueFunc, IEqualityComparer<K2>? comparer = null)
        where K2 : notnull
        where K1 : notnull
    {
        Dictionary<K2, V2> newDictionary = new(comparer);
        foreach (var (key, value) in @this)
            newDictionary[keyFunc(key)] = valueFunc(value);
        return newDictionary;
    }
    public static Dictionary<K2, V> RetypeKeys<K1, V, K2>(this Dictionary<K1, V> @this, Func<K1, K2> keyFunc, IEqualityComparer<K2>? comparer = null)
        where K2 : notnull
        where K1 : notnull
        => @this.Retype(keyFunc, value => value, comparer);
    public static Dictionary<K, V2> RetypeValues<K, V1, V2>(this Dictionary<K, V1> @this, Func<V1, V2> valueFunc, IEqualityComparer<K>? comparer = null)
        where K : notnull
        => @this.Retype(key => key, valueFunc, comparer);

    public static bool TryGetKVP<K, V>(this IDictionary<K, V> @this, out KeyValuePair<K, V> @out, Func<K, bool>? keyTest = null, Func<V, bool>? valueTest = null)
        where K : notnull
    {
        keyTest ??= _ => true;
        valueTest ??= _ => true;

        foreach (var kvp in @this)
            if (keyTest(kvp.Key) && valueTest(kvp.Value))
            {
                @out = kvp;
                return true;
            }

        @out = default;
        return false;
    }
    public static bool TryGetKey<K, V>(this IDictionary<K, V> @this, out K @out, Func<K, bool>? keyTest = null, Func<V, bool>? valueTest = null)
        where K : notnull
    {
        bool success = @this.TryGetKVP(out var kvp, keyTest, valueTest);
        @out = kvp.Key;
        return success;
    }
    public static bool TryGetValue<K, V>(this IDictionary<K, V> @this, out V @out, Func<K, bool>? keyTest = null, Func<V, bool>? valueTest = null)
    where K : notnull
    {
        bool success = @this.TryGetKVP(out var kvp, keyTest, valueTest);
        @out = kvp.Value;
        return success;
    }

    public static Dictionary<K, V> Without<K, V>(this IDictionary<K, V> @this, IEnumerable<K> keys, IEnumerable<V> values, IEqualityComparer<K>? comparer = null)
        where K : notnull
    {
        Dictionary<K, V> newDictionary = new(comparer);
        foreach (var (key, value) in @this)
            if (!keys.Contains(key) && !values.Contains(value))
                newDictionary[key] = value;

        return newDictionary;
    }
    public static Dictionary<K, V> WithoutKeys<K, V>(this IDictionary<K, V> @this, IEnumerable<K> keys, IEqualityComparer<K>? comparer = null)
        where K : notnull
        => @this.Without(keys, Array.Empty<V>());
    public static Dictionary<K, V> WithoutValues<K, V>(this IDictionary<K, V> @this, IEnumerable<V> values, IEqualityComparer<K>? comparer = null)
        where K : notnull
        => @this.Without(Array.Empty<K>(), values);
}