using Vheos.Tools.FilePatcher.Code.Enums;
using Vheos.Tools.FilePatcher.Code.Helpers;

namespace Vheos.Tools.FilePatcher.Code;

public class PatchModel
{
    public readonly FileInfo File;
    public readonly uint? Offset;
    public readonly IReadOnlyCollection<byte> Needle;
    public readonly IReadOnlyDictionary<string, byte[]> PresetsByName;

    public PatchModel(Json json, IEnumerable<string> vanillaPresetAliases)
    {
        File = new FileInfo(json.File);
        Offset = json.Offset.ToUInt(ParseBase.Auto);
        Needle = AobUtil.ParseToArray(json.Needle);
        PresetsByName = json.Presets.Retype(FilterAliases, AobUtil.ParseToArray, StringComparer.OrdinalIgnoreCase);

        string FilterAliases(string name) => vanillaPresetAliases.Contains(name) ? string.Empty : name;
    }

    public static readonly PatchModel Default = new(Json.Default, Array.Empty<string>());

    public override string ToString() =>
$@"{nameof(File)}: {File.Name} / {File.Exists}
{nameof(Offset)}: {Offset}
{nameof(Needle)}: {Needle.FormattedAOB()}
{nameof(PresetsByName)}: {PresetsByName.Aggregate("", (acc, current) => $"{acc}\n• {current.Key}: {current.Value.FormattedAOB()}")}";

    public readonly struct Json
    {
        public string File { get; init; }
        public string Offset { get; init; }
        public string Needle { get; init; }
        public string Anchor { get; init; }
        public Dictionary<string, string> Presets { get; init; }

        public static readonly Json Default = new()
        {
            File = string.Empty,
            Offset = string.Empty,
            Needle = string.Empty,
            Anchor = string.Empty,
            Presets = new(),
        };

        public Json()
        {
            File = Default.File;
            Offset = Default.Offset;
            Needle = Default.Needle;
            Anchor = Default.Anchor;
            Presets = Default.Presets;
        }
    }
}