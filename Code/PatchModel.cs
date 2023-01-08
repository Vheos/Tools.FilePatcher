using Vheos.Helpers.Collections;
using Vheos.Tools.FilePatcher.Code.Enums;
using Vheos.Tools.FilePatcher.Code.Helpers;
using Vheos.Tools.FilePatcher.Code.Message;

namespace Vheos.Tools.FilePatcher.Code;

public class PatchModel
{
    public readonly struct Json
    {
        public string File { get; init; }
        public string Offset { get; init; }
        public Dictionary<string, string> Presets { get; init; }
        public string Tooltip { get; init; }
    }

    public FileInfo? File { get; private set; }
    public int Offset { get; private set; }
    public byte[] VanillaAOB { get; private set; }
    public Dictionary<string, byte[]> CustomPresets { get; private set; }
    public string Tooltip { get; private set; }

    public readonly MessageSet Messages = new();

    public int MaxPresetNameLength => CustomPresets.Keys.Append(string.Empty).Max(name => name.Length);
    public bool HasErrors => Messages.Errors.Any();
    public bool HasWarnings => Messages.Warnings.Any();

    public PatchModel(Json json, IEnumerable<string> vanillaAliases)
    {
        InitializeFile(json.File);
        InitializeOffset(json.Offset, File);

        VanillaAOB = Array.Empty<byte>();
        CustomPresets = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);
        InitializePresets(json.Presets, vanillaAliases);

        Tooltip = json.Tooltip ?? string.Empty;
    }
    public async Task InitializeAsync(Action<float>? onProgressUpdate = null)
    {
        onProgressUpdate?.Invoke(0f);

        int fileLength = 10;
        for (int i = 0; i < fileLength; i++)
        {
            onProgressUpdate?.Invoke((float)i / fileLength);
            await Task.Delay(Random.Shared.Next(0, 100));
        }

        onProgressUpdate?.Invoke(1f);
    }

    public PatchFileOp ReadPreset()
        => !File!.TryReadBytes(out var bytes, Offset, VanillaAOB.Length) ? PatchFileOp.Failed(PresetType.Unknown)
        : bytes.Compare(VanillaAOB) ? PatchFileOp.Vanilla(VanillaAOB)
        : CustomPresets.TryGetKVP(out var kvp, null, aob => bytes.Compare(aob)) ? PatchFileOp.Custom(kvp.Key, kvp.Value)
        : PatchFileOp.Unknown(bytes);
    public PatchFileOp WriteVanilla()
        => File!.TryWriteBytes(VanillaAOB, Offset)
        ? PatchFileOp.Vanilla(VanillaAOB) : PatchFileOp.Failed(PresetType.Vanilla);
    public PatchFileOp WritePreset(string name)
        => CustomPresets.TryGetValue(name, out byte[]? aob) && File!.TryWriteBytes(aob, Offset)
        ? PatchFileOp.Custom(name, aob) : PatchFileOp.Failed(PresetType.Custom);

    private void InitializeFile(string path)
    {
        if (path.IsNullOrEmpty())
        {
            Messages.Err("FileNotDefined");
            return;
        }

        File = new(path);
        if (!File.Exists)
        {
            Messages.Err("FileNotFound");
            return;
        }

        if (!File.IsReadable())
            Messages.Err("FileNotReadable");

        if (!File.IsWriteable())
            Messages.Err("FileNotWriteable");
    }
    private void InitializeOffset(string stringOffset, FileInfo? file)
    {
        if (stringOffset.IsNullOrEmpty())
        {
            Messages.Err("OffsetNotDefined");
            return;
        }

        if (!stringOffset.TryToInt(out var offset, ParseBase.Auto))
            Messages.Err("OffsetNotParseable");
        else if (file != null && file.Exists && offset > file.Length)
            Messages.Err("OffsetOutOfBounds");

        Offset = offset;
    }
    private void InitializePresets(Dictionary<string, string> stringPresets, IEnumerable<string> vanillaAliases)
    {
        if (stringPresets.IsNullOrEmpty())
        {
            Messages.Err("PresetsNotDefined");
            return;
        }

        List<string> vanillaStringAobs = new();
        foreach (var (name, stringAob) in stringPresets)
        {
            if (vanillaAliases.Contains(name))
                vanillaStringAobs.Add(stringAob);
            else if (!AobUtil.TryParseToArray(stringAob, out var aob))
                Messages.Warn("FileNotFound");
            else
                CustomPresets[name] = aob;
        }

        if (vanillaStringAobs.Count == 0)
        {
            Messages.Err("VanillaPresetNotDefined");
            return;
        }

        if (vanillaStringAobs.Count >= 2)
            Messages.Warn("MultipleVanillaPresetsDefined");

        if (vanillaStringAobs.Select(AobUtil.ParseToArray).TryGetFirst(out var vanillaAob, aob => aob.Length > 0))
        {
            VanillaAOB = vanillaAob;

            if (CustomPresets.Values.Any(aob => aob.Length > VanillaAOB.Length))
                Messages.Err("PresetLongerThanVanilla");

            if (CustomPresets.Values.Any(aob => aob.Length < VanillaAOB.Length))
                Messages.Warn("PresetShorterThanVanilla");
        }
        else
        {
            Messages.Err("VanillaPresetNotParseable");
        }
    }
}