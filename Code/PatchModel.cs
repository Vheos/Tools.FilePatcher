using Vheos.Tools.FilePatcher.Code.Enums;
using Vheos.Tools.FilePatcher.Code.Helpers;
using ValueType = Vheos.Tools.FilePatcher.Code.Enums.ValueType;

namespace Vheos.Tools.FilePatcher.Code;

public class PatchModel
{
    public readonly FileInfo File;
    public readonly int? Offset;
    public readonly byte[] Needle;
    public readonly byte[] VanillaAOB;
    public readonly IReadOnlyDictionary<string, byte[]> CustomPresets;
    public readonly ValueType? Editor;

    public readonly PresetInspection PresetInspection;
    public readonly PatchErrors Errors;

    public int MaxPresetNameLength => CustomPresets.Count > 0 ? CustomPresets.Keys.Max(name => name.Length) : 0;
    public int MaxPresetAobLength => CustomPresets.Count > 0 ? CustomPresets.Values.Max(aob => aob.Length) : 0;
    public bool HasErrors => Errors != PatchErrors.None;

    public PatchModel(Json json, IEnumerable<string> vanillaPresetAliases)
    {
        File = new FileInfo(json.File);

        Offset = json.Offset.TryToInt(out var offset, ParseBase.Auto)
            ? offset : null;

        Needle = AobUtil.ParseToArray(json.Needle);

        VanillaAOB = json.Presets.TryGetValue(out var stringAob, name => vanillaPresetAliases.Contains(name))
            ? AobUtil.ParseToArray(stringAob) : Array.Empty<byte>();

        CustomPresets = json.Presets.WithoutKeys(vanillaPresetAliases.Append(string.Empty))
            .RetypeValues(AobUtil.ParseToArray, StringComparer.OrdinalIgnoreCase);

        Editor = json.Editor.TryToEnumCached(out ValueType valueType)
            ? valueType : null;

        Errors = CheckForErrors();
    }

    public static readonly PatchModel Default = new(Json.Default, Array.Empty<string>());

    private PresetInspection InspectCurrentPreset()
    {
        var bytes = File.ReadBytes(Offset.Value, MaxPresetAobLength);

        if (bytes.Compare(VanillaAOB))
            return new(PresetType.Vanilla, VanillaAOB, string.Empty);

        foreach (var (name, aob) in CustomPresets)
            if (bytes.Compare(aob))
                return new(PresetType.Custom, aob, name);

        return new(PresetType.Unknown, bytes, string.Empty);

    }

    private PatchErrors CheckForErrors()
    {
        PatchErrors errors = PatchErrors.None;
        if (!File.Exists)
        {
            errors |= PatchErrors.FileNotFound;
        }
        else
        {
            if (!File.CanBeRead())
                errors |= PatchErrors.FileCannotBeRead;
            if (!File.CanBeWritten())
                errors |= PatchErrors.FileCannotBeWritten;
        }

        if (!Offset.HasValue && Needle.Length == 0)
            errors |= PatchErrors.OffsetAndNeedleNotDefined;

        if (File.Exists && Offset.HasValue && File.Length <= Offset)
            errors |= PatchErrors.OffsetOutOfBounds;

        if (VanillaAOB == null)
            errors |= PatchErrors.VanillaPresetNotDefined;

        return errors;
    }

    public async Task<uint?> MOCK_ScanForNeedle(Action<float>? onUpdate = null)
    {
        onUpdate?.Invoke(0f);

        int fileLength = 1;
        for (int i = 0; i < fileLength; i++)
        {
            onUpdate?.Invoke((float)i / fileLength);
            await Task.Delay(Random.Shared.Next(0, 10));
        }

        onUpdate?.Invoke(1f);
        return null;
    }

    public readonly struct Json
    {
        public string File { get; init; }
        public string Offset { get; init; }
        public string Needle { get; init; }
        public string Anchor { get; init; }
        public string Editor { get; init; }
        public Dictionary<string, string> Presets { get; init; }

        public static readonly Json Default = new()
        {
            File = string.Empty,
            Offset = string.Empty,
            Needle = string.Empty,
            Anchor = string.Empty,
            Editor = string.Empty,
            Presets = new(),
        };

        public Json()
        {
            File = Default.File;
            Offset = Default.Offset;
            Needle = Default.Needle;
            Anchor = Default.Anchor;
            Editor = Default.Editor;
            Presets = Default.Presets;
        }
    }
}