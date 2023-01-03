using System.Collections.Generic;
using Vheos.Tools.FilePatcher.Code.Helpers;

namespace Vheos.Tools.FilePatcher.Code
{
    public class PatchModel
    {
        public readonly FileInfo File;
        public readonly uint? Offset;
        public readonly byte[] Needle;
        public readonly Dictionary<string, byte[]> PresetsByName;

        public PatchModel(Json json, IEnumerable<string> vanillaPresetAliases)
        {
            File = new FileInfo(json.File);
            Offset = json.Offset.ToUInt(ParseBase.Auto);
            Needle = AobUtil.ParseToArray(json.Needle);
            PresetsByName = json.Presets.Retype(FilterAliases, AobUtil.ParseToArray, StringComparer.OrdinalIgnoreCase);

            string FilterAliases(string name) => vanillaPresetAliases.Contains(name) ? string.Empty : name;
        }

        public override string ToString() =>
$@"{nameof(File)}: {File.Name} / {File.Exists}
{nameof(Offset)}: {Offset}
{nameof(Needle)}: {Needle.FormattedAOB()}
{nameof(PresetsByName)}: {PresetsByName.Aggregate("", (acc, current ) => $"{acc}\n• {current.Key}: {current.Value.FormattedAOB()}")}";

        public readonly struct Json
        {
            public string File { get; init; }
            public string Offset { get; init; }
            public string Needle { get; init; }
            public string Anchor { get; init; }
            public Dictionary<string, string> Presets { get; init; }

            public Json()
            {
                File =
                Offset =
                Needle =
                Anchor =
                    string.Empty;

                Presets =
                    new Dictionary<string, string>();
            }
        }
    }
}