namespace Vheos.Tools.FilePatcher.Code.Enums;

[Flags]
public enum PatchErrors
{
    None = 0,

    FileNotFound = 1 << 0,
    FileCannotBeRead = 1 << 1,
    FileCannotBeWritten = 1 << 2,

    OffsetAndNeedleNotDefined = 1 << 3,
    OffsetOutOfBounds = 1 << 4,
    NeedleNotFound = 1 << 5,

    VanillaPresetNotDefined = 1 << 6,
    PresetNotDetected = 1 << 7,
}
