using Vheos.Helpers.Collections;
using Vheos.Tools.FilePatcher.Code.Enums;
using Vheos.Tools.FilePatcher.Code.Helpers;

namespace Vheos.Tools.FilePatcher.Code;

public class PatchModel
{
    public readonly struct Json
    {
        public string File { get; init; }
        public string Offset { get; init; }
        public Dictionary<string, string> Presets { get; init; }
    }

    public FileInfo? File { get; private set; }
    public int Offset { get; private set; }
    public byte[] VanillaAOB { get; private set; }
    public Dictionary<string, byte[]> CustomPresets { get; private set; }

    public PatchErrors Errors { get; private set; }
    public PatchWarnings Warnings { get; private set; }
    public PresetInspection PresetInspection { get; private set; }

    public int MaxPresetNameLength => CustomPresets.Keys.Append(string.Empty).Max(name => name.Length);
    public bool HasErrors => Errors != PatchErrors.None;
    public bool HasWarnings => Warnings != PatchWarnings.None;

    public PatchModel(Json json, IEnumerable<string> vanillaAliases)
    {
        InitializeFile(json.File);
        InitializeOffset(json.Offset, File);

        VanillaAOB = Array.Empty<byte>();
        CustomPresets = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);
        InitializePresets(json.Presets, vanillaAliases);
    }
    private void Initialize()
    {

    }

    private void InitializeFile(string path)
    {
        if (path.IsNullOrEmpty())
        {
            Errors |= PatchErrors.FileNotDefined;
            return;
        }

        File = new(path);
        if (!File.Exists)
        {
            Errors |= PatchErrors.FileNotFound;
            return;
        }

        if (!File.IsReadable())
            Errors |= PatchErrors.FileNotReadable;

        if (!File.IsWriteable())
            Errors |= PatchErrors.FileNotWriteable;
    }
    private void InitializeOffset(string stringOffset, FileInfo? file)
    {
        if (stringOffset.IsNullOrEmpty())
        {
            Errors |= PatchErrors.OffsetNotDefined;
            return;
        }

        if (!stringOffset.TryToInt(out var offset, ParseBase.Auto))
            Errors |= PatchErrors.OffsetNotDefined;
        else if (file != null && file.Exists && offset > file.Length)
            Errors |= PatchErrors.OffsetOutOfBounds;

        Offset = offset;
    }
    private void InitializePresets(Dictionary<string, string> stringPresets, IEnumerable<string> vanillaAliases)
    {
        if (stringPresets.IsNullOrEmpty())
        {
            Errors |= PatchErrors.PresetsNotDefined;
            return;
        }

        List<string> vanillaStringAobs = new();
        foreach (var (name, stringAob) in stringPresets)
        {
            if (vanillaAliases.Contains(name))
                vanillaStringAobs.Add(stringAob);
            else if (!AobUtil.TryParseToArray(stringAob, out var aob))
                Warnings |= PatchWarnings.PresetNotParseable;
            else
                CustomPresets[name] = aob;
        }

        if (vanillaStringAobs.Count == 0)
            Errors |= PatchErrors.VanillaPresetNotDefined;
        else if (vanillaStringAobs.Count >= 2)
            Warnings |= PatchWarnings.MultipleVanillaPresetsDefined;

        if (vanillaStringAobs.Select(AobUtil.ParseToArray).TryGetFirst(out var vanillaAob, aob => aob.Length > 0))
        {
            VanillaAOB = vanillaAob;

            if (CustomPresets.Values.Any(aob => aob.Length > VanillaAOB.Length))
                Errors |= PatchErrors.PresetLongerThanVanilla;

            if (CustomPresets.Values.Any(aob => aob.Length < VanillaAOB.Length))
                Warnings |= PatchWarnings.PresetShorterThanVanilla;
        }
        else
        {
            Errors |= PatchErrors.VanillaPresetNotParseable;
        }
    }

    public static PatchModel Create(Json json, IEnumerable<string> vanillaAliases)
    {
        PatchModel newPatchModel = new(json, vanillaAliases);
        newPatchModel.Initialize();
        return newPatchModel;

    }

    private PresetInspection InspectCurrentPreset()
    {
        byte[] bytes = File.ReadBytes(Offset, VanillaAOB.Length);

        if (bytes.Compare(VanillaAOB))
            return new(PresetType.Vanilla, VanillaAOB, string.Empty);

        foreach (var (name, aob) in CustomPresets)
            if (bytes.Compare(aob))
                return new(PresetType.Custom, aob, name);

        return new(PresetType.Unknown, bytes, string.Empty);

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
}