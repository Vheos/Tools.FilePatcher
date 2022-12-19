using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vheos.Helpers.Common;
using static System.Net.Mime.MediaTypeNames;
using static Vheos.Tools.FilePatcher.Code.PatchInfo;

namespace Vheos.Tools.FilePatcher.Code
{
    public readonly struct PatchInfo
    {
        public readonly string Name;
        public readonly FileInfo File;
        public readonly IReadOnlyList<byte> Needle;
        public readonly IReadOnlyList<byte> Vanilla;
        public readonly IReadOnlyList<byte> Custom;

        private PatchInfo(Json json)
        {
            Name = json.Name;
            File = new FileInfo(json.File);
            Needle = Parse(json.Needle);
            Vanilla = Parse(json.Vanilla);
            Custom = Parse(json.Custom);
        }

        static IReadOnlyList<byte> Parse(string text)
        {
            List<byte> bytes = new();
            ValueType defaultType = ValueType.Hex;
            foreach (var element in Regex.Replace(text, @"\s+", " ").Trim().Split(" "))
            {
                if (element.TryToEnum(out ValueType needleType, true))
                {
                    defaultType = needleType;
                    continue;
                }

                if (TryParseValueAsAOB(element, defaultType, out var aob))
                    bytes.AddRange(aob);
            }

            return bytes;
        }

        static bool TryParseValueAsAOB(string text, ValueType type, out byte[] aob)
        {
            aob = type switch
            {
                ValueType.Hex => text.HexAOB(),
                ValueType.Byte => text.TryToByte(out var @byte) ? @byte.AOB() : text.TryToSByte(out var @sbyte) ? @sbyte.AOB() : Array.Empty<byte>(),
                ValueType.Short => text.TryToShort(out var @short) ? @short.AOB() : text.TryToUShort(out var @ushort) ? @ushort.AOB() : Array.Empty<byte>(),
                ValueType.Int => text.TryToInt(out var @int) ? @int.AOB() : text.TryToUInt(out var @uint) ? @uint.AOB() : Array.Empty<byte>(),
                ValueType.Long => text.TryToLong(out var @long) ? @long.AOB() : text.TryToULong(out var @ulong) ? @ulong.AOB() : Array.Empty<byte>(),
                ValueType.Float => text.TryToFloat(out var @float) ? @float.AOB() : Array.Empty<byte>(),
                ValueType.Double => text.TryToDouble(out var @double) ? @double.AOB() : Array.Empty<byte>(),
                _ => Array.Empty<byte>(),
            };

            return aob.Length > 0;
        }

        override public string ToString() =>
$@"{nameof(Name)}: {Name}
{nameof(File)}: ({File.Exists}) {File.FullName}
{nameof(Needle)}: {Needle.FormattedAOB()}
{nameof(Vanilla)}: {Vanilla.FormattedAOB()}
{nameof(Custom)}: {Custom.FormattedAOB()}";

        internal readonly struct Json 
        {
            public string Name { get; init; }
            public string File { get; init; }
            public string Needle { get; init; }
            public string Position { get; init; }
            public string Vanilla { get; init; }
            public string Custom { get; init; }

            public Json()
            {
                Name = File = Needle = Position = Vanilla = Custom = String.Empty;
            }

            public PatchInfo Parsed => new(this);

            override public string ToString() =>
$@"{nameof(Name)}: {Name ?? "null"}
{nameof(Needle)}: {Needle ?? "null"}
{nameof(Vanilla)}: {Vanilla ?? "null"}
{nameof(Custom)}: {Custom ?? "null"}";
        }
    }
}
