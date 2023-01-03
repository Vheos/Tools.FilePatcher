using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using Vheos.Tools.FilePatcher.Code.Enums;

namespace Vheos.Tools.FilePatcher.Code.Helpers;

public static partial class ParseExtensions
{
    public static bool TryToByte(this string @this, out byte value, ParseBase parseBase = ParseBase.Decimal)
    {
        InitializeArgs(parseBase, ref @this, out NumberStyles format);
        return byte.TryParse(@this, format, CultureInfo.InvariantCulture, out value);
    }

    public static bool TryToSByte(this string @this, out sbyte value, ParseBase parseBase = ParseBase.Decimal)
    {
        InitializeArgs(parseBase, ref @this, out NumberStyles format);
        return sbyte.TryParse(@this, format, CultureInfo.InvariantCulture, out value);
    }

    public static bool TryToShort(this string @this, out short value, ParseBase parseBase = ParseBase.Decimal)
    {
        InitializeArgs(parseBase, ref @this, out NumberStyles format);
        return short.TryParse(@this, format, CultureInfo.InvariantCulture, out value);
    }

    public static bool TryToUShort(this string @this, out ushort value, ParseBase parseBase = ParseBase.Decimal)
    {
        InitializeArgs(parseBase, ref @this, out NumberStyles format);
        return ushort.TryParse(@this, format, CultureInfo.InvariantCulture, out value);
    }

    public static bool TryToInt(this string @this, out int value, ParseBase parseBase = ParseBase.Decimal)
    {
        InitializeArgs(parseBase, ref @this, out NumberStyles format);
        return int.TryParse(@this, format, CultureInfo.InvariantCulture, out value);
    }

    public static bool TryToUInt(this string @this, out uint value, ParseBase parseBase = ParseBase.Decimal)
    {
        InitializeArgs(parseBase, ref @this, out NumberStyles format);
        return uint.TryParse(@this, format, CultureInfo.InvariantCulture, out value);
    }

    public static bool TryToLong(this string @this, out long value, ParseBase parseBase = ParseBase.Decimal)
    {
        InitializeArgs(parseBase, ref @this, out NumberStyles format);
        return long.TryParse(@this, format, CultureInfo.InvariantCulture, out value);
    }

    public static bool TryToULong(this string @this, out ulong value, ParseBase parseBase = ParseBase.Decimal)
    {
        InitializeArgs(parseBase, ref @this, out NumberStyles format);
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
        if (@this.TryToByte(out byte value, parseBase))
            return value;

        Debug.WriteLine($"Cannot parse {nameof(Byte)}: \"{@this}\"!");
        return default;
    }
    public static sbyte ToSByte(this string @this, ParseBase parseBase = ParseBase.Decimal)
    {
        if (@this.TryToSByte(out sbyte value, parseBase))
            return value;

        Debug.WriteLine($"Cannot parse {nameof(SByte)}: \"{@this}\"!");
        return default;
    }

    public static int ToInt(this string @this, ParseBase parseBase = ParseBase.Decimal)
    {
        if (@this.TryToInt(out int value, parseBase))
            return value;

        Debug.WriteLine($"Cannot parse {nameof(Int32)}: \"{@this}\"!");
        return default;
    }
    public static uint ToUInt(this string @this, ParseBase parseBase = ParseBase.Decimal)
    {
        if (@this.TryToUInt(out uint value, parseBase))
            return value;

        Debug.WriteLine($"Cannot parse {nameof(UInt32)}: \"{@this}\"!");
        return default;
    }

    public static long ToLong(this string @this, ParseBase parseBase = ParseBase.Decimal)
    {
        if (@this.TryToLong(out long value, parseBase))
            return value;

        Debug.WriteLine($"Cannot parse {nameof(Int64)}: \"{@this}\"!");
        return default;
    }
    public static ulong ToULong(this string @this, ParseBase parseBase = ParseBase.Decimal)
    {
        if (@this.TryToULong(out ulong value, parseBase))
            return value;

        Debug.WriteLine($"Cannot parse {nameof(UInt64)}: \"{@this}\"!");
        return default;
    }

    public static float ToFloat(this string @this)
    {
        if (@this.TryToFloat(out float value))
            return value;

        Debug.WriteLine($"Cannot parse {nameof(Single)}: \"{@this}\"!");
        return default;
    }
    public static double ToDouble(this string @this)
    {
        if (@this.TryToDouble(out double value))
            return value;

        Debug.WriteLine($"Cannot parse {nameof(Double)}: \"{@this}\"!");
        return default;
    }

    public static bool ToBool(this string @this)
    {
        if (@this.TryToBool(out bool value))
            return value;

        Debug.WriteLine($"Cannot parse {nameof(Boolean)}: \"{@this}\"!");
        return default;
    }
    public static BigInteger ToBigInteger(this string @this)
    {
        if (@this.TryToBigInteger(out BigInteger value))
            return value;

        Debug.WriteLine($"Cannot parse {nameof(BigInteger)}: \"{@this}\"");
        return default;
    }
    public static T ToEnum<T>(this string @this, bool ignoreCase = false) where T : struct, Enum
    {
        if (@this.TryToEnum(out T value, ignoreCase))
            return value;

        Debug.WriteLine($"Cannot parse {nameof(T)}: \"{@this}\"!");
        return default;
    }

    private static void InitializeArgs(ParseBase parseBase, ref string @this, out NumberStyles format)
    {
        switch (parseBase)
        {
            case ParseBase.Hexadecimal:
            case ParseBase.Auto when @this.HasHexPrefix() || @this.HasHexLetter():
                format = NumberStyles.HexNumber;
                @this = @this.WithoutHexPrefix();
                break;
            default:
                format = NumberStyles.Integer;
                break;
        }
    }
}