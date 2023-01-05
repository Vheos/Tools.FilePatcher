namespace Vheos.Tools.FilePatcher.Code.Enums;

[Flags]
public enum PatchErrors
{
    None = 0,

    FileNotDefined = 1 << 0,
    FileNotFound = 1 << 1,
    FileNotReadable = 1 << 2,
    FileNotWriteable = 1 << 3,

    OffsetNotDefined = 1 << 4,
    OffsetNotParseable = 1 << 5,
    OffsetOutOfBounds = 1 << 6,

    PresetsNotDefined = 1 << 7,
    VanillaPresetNotDefined = 1 << 8,
    VanillaPresetNotParseable = 1 << 9,

    PresetLongerThanVanilla = 1 << 10,
    PresetNotDetected = 1 << 11,
}
