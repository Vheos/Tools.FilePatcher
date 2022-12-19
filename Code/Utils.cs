using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Vheos.Helpers.Math;

namespace Vheos.Tools.FilePatcher.Code
{
    public static class Utils
    {
        static public string FormattedAOB(this IEnumerable<byte> @this)
        {
            StringBuilder builder = new();
            foreach (var @byte in @this)
                builder.Append($"{@byte:X2} ");
            return builder.ToString();
        }

        static public string HexOnly(this string @this) => Regex.Replace(@this, @"[^0-9^A-F]", "", RegexOptions.IgnoreCase);

        static IEnumerable<string> SplitBySize(this string @this, int chunkSize, bool allowIncompleteChunk = false)
        {
            int length = @this.Length / chunkSize * chunkSize;

            int i;
            for (i = 0; i < length; i += chunkSize)
                yield return @this.Substring(i, chunkSize);

            if (allowIncompleteChunk && i < @this.Length)
                yield return @this[i..];
        }

        static public byte[] HexAOB(this string @this)
        {
            string[] hexes = @this.HexOnly().SplitBySize(2, true).ToArray();
            byte[] bytes = new byte[hexes.Length];

            for (int i = 0; i < hexes.Length; i++)
                bytes[i] = hexes[i].ToByte(true);

            return bytes;
        }

        static public byte[] AOB(this byte @this) => new byte[] { @this };
        static public byte[] AOB(this sbyte @this) => new byte[] { (byte)@this };
        static public byte[] AOB(this ushort @this) => BitConverter.GetBytes(@this);
        static public byte[] AOB(this short @this) => BitConverter.GetBytes(@this);
        static public byte[] AOB(this uint @this) => BitConverter.GetBytes(@this);
        static public byte[] AOB(this int @this) => BitConverter.GetBytes(@this);
        static public byte[] AOB(this ulong @this) => BitConverter.GetBytes(@this);
        static public byte[] AOB(this long @this) => BitConverter.GetBytes(@this);
        static public byte[] AOB(this float @this) => BitConverter.GetBytes(@this);
        static public byte[] AOB(this double @this) => BitConverter.GetBytes(@this);
        static public byte[] AOB<T>(this T[] @this)
        {
            byte[] bytes = new byte[@this.Length * Unsafe.SizeOf<T>()];
            Buffer.BlockCopy(@this, 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
