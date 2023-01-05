using Vheos.Tools.FilePatcher.Code.Enums;

namespace Vheos.Tools.FilePatcher.Code;

public readonly struct PresetInspection
{
    public readonly PresetType Type;
    public readonly byte[] AOB;
    public readonly string Name;

    public PresetInspection(PresetType type, byte[] aob, string name)
    {
        Type = type;
        Name = name;
        AOB = aob;
    }
}
