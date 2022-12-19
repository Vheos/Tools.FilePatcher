using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Vheos.Tools.FilePatcher.Code
{
    public static class ParseExtensions
    {
        #region Public Methods
        public static bool TryToByte(this string @this, out byte value, bool isHex = false)
            => byte.TryParse(@this, isHex ? NumberStyles.HexNumber : NumberStyles.Integer, CultureInfo.InvariantCulture, out value);
        public static bool TryToSByte(this string @this, out sbyte value, bool isHex = false)
            => sbyte.TryParse(@this, isHex ? NumberStyles.HexNumber : NumberStyles.Integer, CultureInfo.InvariantCulture, out value);

        public static bool TryToShort(this string @this, out short value, bool isHex = false)
            => short.TryParse(@this, isHex ? NumberStyles.HexNumber : NumberStyles.Integer, CultureInfo.InvariantCulture, out value);
        public static bool TryToUShort(this string @this, out ushort value, bool isHex = false)
            => ushort.TryParse(@this, isHex ? NumberStyles.HexNumber : NumberStyles.Integer, CultureInfo.InvariantCulture, out value);

        public static bool TryToInt(this string @this, out int value, bool isHex = false)
            => int.TryParse(@this, isHex ? NumberStyles.HexNumber : NumberStyles.Integer, CultureInfo.InvariantCulture, out value);
        public static bool TryToUInt(this string @this, out uint value, bool isHex = false)
            => uint.TryParse(@this, isHex ? NumberStyles.HexNumber : NumberStyles.Integer, CultureInfo.InvariantCulture, out value);

        public static bool TryToLong(this string @this, out long value, bool isHex = false)
            => long.TryParse(@this, isHex ? NumberStyles.HexNumber : NumberStyles.Integer, CultureInfo.InvariantCulture, out value);
        public static bool TryToULong(this string @this, out ulong value, bool isHex = false)
            => ulong.TryParse(@this, isHex ? NumberStyles.HexNumber : NumberStyles.Integer, CultureInfo.InvariantCulture, out value);

        public static bool TryToFloat(this string @this, out float value) => float.TryParse(@this, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        public static bool TryToDouble(this string @this, out double value) => double.TryParse(@this, NumberStyles.Float, CultureInfo.InvariantCulture, out value);


        public static bool TryToBool(this string @this, out bool value) => bool.TryParse(@this, out value);
        public static bool TryToBigInteger(this string @this, out BigInteger value) => BigInteger.TryParse(@this, NumberStyles.Integer, CultureInfo.InvariantCulture, out value);
        public static bool TryToEnum<T>(this string @this, out T value, bool ignoreCase = false) where T : struct, Enum => Enum.TryParse(@this, ignoreCase, out value) && Enum.IsDefined(value);


        public static byte ToByte(this string @this, bool isHex = false)
        {
            if (@this.TryToByte(out byte value, isHex))
                return value;

            Debug.WriteLine($"Cannot parse {nameof(Byte)}: \"{@this}\"!");
            return default;
        }
        public static sbyte ToSByte(this string @this, bool isHex = false)
        {
            if (@this.TryToSByte(out sbyte value, isHex))
                return value;

            Debug.WriteLine($"Cannot parse {nameof(SByte)}: \"{@this}\"!");
            return default;
        }

        public static int ToInt(this string @this, bool isHex = false)
        {
            if (@this.TryToInt(out int value, isHex))
                return value;

            Debug.WriteLine($"Cannot parse {nameof(Int32)}: \"{@this}\"!");
            return default;
        }
        public static uint ToUInt(this string @this, bool isHex = false)
        {
            if (@this.TryToUInt(out uint value, isHex))
                return value;

            Debug.WriteLine($"Cannot parse {nameof(UInt32)}: \"{@this}\"!");
            return default;
        }

        public static long ToLong(this string @this, bool isHex = false)
        {
            if (@this.TryToLong(out long value, isHex))
                return value;

            Debug.WriteLine($"Cannot parse {nameof(Int64)}: \"{@this}\"!");
            return default;
        }
        public static ulong ToULong(this string @this, bool isHex = false)
        {
            if (@this.TryToULong(out ulong value, isHex))
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

        #endregion
    }
}