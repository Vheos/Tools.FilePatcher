using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Vheos.Tools.FilePatcher.Code.Enums;
using ValueType = Vheos.Tools.FilePatcher.Code.Enums.ValueType;

namespace Vheos.Tools.FilePatcher.Code.Helpers;

public static class AobUtil
{
    public static byte[] ParseToArray(string text) => Parse(text).ToArray();

    public static IEnumerable<byte> Parse(string text)
    {
        ValueType defaultType = ValueType.Hex;
        foreach (var element in text.SplitTextElements())
        {
            if (element.TryToEnumCached(out ValueType valueType))
            {
                defaultType = valueType;
            }
            else
            {
                foreach (var parsedElement in ParseTextElement(element, defaultType))
                    yield return parsedElement;
            }
        }
    }

    public static byte[] AOB(this byte @this) => new byte[] { @this };
    public static byte[] AOB(this sbyte @this) => new byte[] { (byte)@this };
    public static byte[] AOB(this ushort @this) => BitConverter.GetBytes(@this);
    public static byte[] AOB(this short @this) => BitConverter.GetBytes(@this);
    public static byte[] AOB(this uint @this) => BitConverter.GetBytes(@this);
    public static byte[] AOB(this int @this) => BitConverter.GetBytes(@this);
    public static byte[] AOB(this ulong @this) => BitConverter.GetBytes(@this);
    public static byte[] AOB(this long @this) => BitConverter.GetBytes(@this);
    public static byte[] AOB(this float @this) => BitConverter.GetBytes(@this);
    public static byte[] AOB(this double @this) => BitConverter.GetBytes(@this);

    public static bool Compare(this byte[] @this, byte[] other, bool requreExactLength = false)
    {
        if (@this == other)
            return true;
        if (@this == null || other == null)
            return false;
        if (requreExactLength && @this.Length != other.Length)
            return false;

        var length = @this.Length.Min(other.Length);
        for (int i = 0; i < length; i++)
            if (@this[i] != other[i])
                return false;

        return true;
    }

    private static IEnumerable<byte> ParseTextElement(string text, ValueType type) => type switch
    {
        ValueType.Hex => text.HexAOB(),
        ValueType.Byte when text.TryToByte(out var @byte) => @byte.AOB(),
        ValueType.Byte when text.TryToSByte(out var @sbyte) => @sbyte.AOB(),
        ValueType.Short when text.TryToShort(out var @short) => @short.AOB(),
        ValueType.Short when text.TryToUShort(out var @ushort) => @ushort.AOB(),
        ValueType.Int when text.TryToInt(out var @int) => @int.AOB(),
        ValueType.Int when text.TryToUInt(out var @uint) => @uint.AOB(),
        ValueType.Long when text.TryToLong(out var @long) => @long.AOB(),
        ValueType.Long when text.TryToULong(out var @ulong) => @ulong.AOB(),
        ValueType.Float when text.TryToFloat(out var @float) => @float.AOB(),
        ValueType.Double when text.TryToDouble(out var @double) => @double.AOB(),
        _ => Array.Empty<byte>()
    };
    private static string HexDigits(this string @this) => Regex.Replace(@this, @"[^0-9a-f]+", "", RegexOptions.IgnoreCase);
    private static string[] SplitTextElements(this string @this) => Regex.Replace(@this, @"[^a-z0-9+\-.]+", "|", RegexOptions.IgnoreCase).Split("|");

    public static byte[] HexAOB(this string @this)
    {
        string[] hexes = @this.HexDigits().SplitBySize(2, true).ToArray();
        byte[] bytes = new byte[hexes.Length];

        for (int i = 0; i < hexes.Length; i++)
            bytes[i] = hexes[i].ToByte(ParseBase.Hexadecimal);

        return bytes;
    }
}
