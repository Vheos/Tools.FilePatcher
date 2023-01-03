namespace Vheos.Tools.FilePatcher.Code;

public readonly struct Settings
{
    public string PatchFileExtension { get; init; }
    public string PatchFileDirectory { get; init; }
    public string[] VanillaPresetAliases { get; init; }
}