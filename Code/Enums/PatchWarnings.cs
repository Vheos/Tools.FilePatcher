namespace Vheos.Tools.FilePatcher.Code.Enums;

[Flags]
public enum PatchWarnings
{
    None = 0,

    MultipleVanillaPresetsDefined = 1 << 0,
    PresetNotParseable = 1 << 1,
    PresetShorterThanVanilla = 1 << 2,
}
