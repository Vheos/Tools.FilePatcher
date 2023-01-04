using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using Vheos.Tools.FilePatcher.Code.Enums;

namespace Vheos.Tools.FilePatcher.Code.Helpers;

public static partial class ParseExtensions
{
    public static bool TryToByte(this string @this, out byte value, ParseBase parseBase = ParseBase.Decimal)
    {
        parseBase.InitializeArgs(ref @this, out NumberStyles format);
        return byte.TryParse(@this, format, CultureInfo.InvariantCulture, out value);
    }

    public static bool TryToSByte(this string @this, out sbyte value, ParseBase parseBase = ParseBase.Decimal)
    {
        parseBase.InitializeArgs(ref @this, out NumberStyles format);
        return sbyte.TryParse(@this, format, CultureInfo.InvariantCulture, out value);
    }

    public static bool TryToShort(this string @this, out short value, ParseBase parseBase = ParseBase.Decimal)
    {
        parseBase.InitializeArgs(ref @this, out NumberStyles format);
        return short.TryParse(@this, format, CultureInfo.InvariantCulture, out value);
    }

    public static bool TryToUShort(this string @this, out ushort value, ParseBase parseBase = ParseBase.Decimal)
    {
        parseBase.InitializeArgs(ref @this, out NumberStyles format);
        return ushort.TryParse(@this, format, CultureInfo.InvariantCulture, out value);
    }

    public static bool TryToInt(this string @this, out int value, ParseBase parseBase = ParseBase.Decimal)
    {
        parseBase.InitializeArgs(ref @this, out NumberStyles format);
        return int.TryParse(@this, format, CultureInfo.InvariantCulture, out value);
    }

    public static bool TryToUInt(this string @this, out uint value, ParseBase parseBase = ParseBase.Decimal)
    {
        parseBase.InitializeArgs(ref @this, out NumberStyles format);
        return uint.TryParse(@this, format, CultureInfo.InvariantCulture, out value);
    }

    public static bool TryToLong(this string @this, out long value, ParseBase parseBase = ParseBase.Decimal)
    {
        parseBase.InitializeArgs(ref @this, out NumberStyles format);
        return long.TryParse(@this, format, CultureInfo.InvariantCulture, out value);
    }

    public static bool TryToULong(this string @this, out ulong value, ParseBase parseBase = ParseBase.Decimal)
    {
        parseBase.InitializeArgs(ref @this, out NumberStyles format);
        return ulong.TryParse(@this, format, CultureInfo.InvariantCulture, out value);
    }

    public static bool TryToFloat(this string @this, out float value)
        => float.TryParse(@this, NumberStyles.Float, CultureInfo.InvariantCulture, out value);

    public static bool TryToDouble(this string @this, out double value)
        => double.TryParse(@this, NumberStyles.Float, CultureInfo.InvariantCulture, out value);

    public static bool TryToBool(this string @this, out bool value)
        => bool.TryParse(@this, out value);

    public static bool TryToBigInteger(this string @this, out BigInteger value)
        => BigInteger.TryParse(@this, NumberStyles.Integer, CultureInfo.InvariantCulture, out value);

    public static bool TryToEnum<T>(this string @this, out T value, bool ignoreCase = false) where T : struct, Enum
        => Enum.TryParse(@this, ignoreCase, out value) && Enum.IsDefined(value);

    public static byte ToByte(this string @this, ParseBase parseBase = ParseBase.Decimal)
    {
        if (!@this.TryToByte(out var value, parseBase))
            @this.LogWarning<byte>();
        return value;
    }
    public static sbyte ToSByte(this string @this, ParseBase parseBase = ParseBase.Decimal)
    {
        if (!@this.TryToSByte(out var value, parseBase))
            @this.LogWarning<sbyte>();
        return value;
    }

    public static int ToInt(this string @this, ParseBase parseBase = ParseBase.Decimal)
    {
        if (!@this.TryToInt(out var value, parseBase))
            @this.LogWarning<int>();
        return value;
    }
    public static uint ToUInt(this string @this, ParseBase parseBase = ParseBase.Decimal)
    {
        if (!@this.TryToUInt(out var value, parseBase))
            @this.LogWarning<uint>();
        return value;
    }

    public static long ToLong(this string @this, ParseBase parseBase = ParseBase.Decimal)
    {
        if (!@this.TryToLong(out var value, parseBase))
            @this.LogWarning<long>();
        return value;
    }
    public static ulong ToULong(this string @this, ParseBase parseBase = ParseBase.Decimal)
    {
        if (!@this.TryToULong(out var value, parseBase))
            @this.LogWarning<ulong>();
        return value;
    }

    public static float ToFloat(this string @this)
    {
        if (!@this.TryToFloat(out var value))
            @this.LogWarning<float>();
        return value;
    }
    public static double ToDouble(this string @this)
    {
        if (!@this.TryToDouble(out var value))
            @this.LogWarning<double>();
        return value;
    }

    public static bool ToBool(this string @this)
    {
        if (!@this.TryToBool(out var value))
            @this.LogWarning<bool>();
        return value;
    }
    public static BigInteger ToBigInteger(this string @this)
    {
        if (!@this.TryToBigInteger(out var value))
            @this.LogWarning<BigInteger>();
        return value;
    }
    public static T ToEnum<T>(this string @this, bool ignoreCase = false) where T : struct, Enum
    {
        if (!@this.TryToEnum(out T value, ignoreCase))
            @this.LogWarning<T>();
        return value;
    }

    private static void InitializeArgs(this ParseBase @this, ref string parseText, out NumberStyles format)
    {
        switch (@this)
        {
            case ParseBase.Hexadecimal:
            case ParseBase.Auto when parseText.HasHexPrefix() || parseText.HasHexLetter():
                format = NumberStyles.HexNumber;
                parseText = parseText.WithoutHexPrefix();
                break;
            default:
                format = NumberStyles.Integer;
                break;
        }
    }
    private static void LogWarning<T>(this string @this)
        => Debug.WriteLine($"Cannot parse \"{@this}\" as {typeof(T).Name}! Returning default value ({default(T)})");
}