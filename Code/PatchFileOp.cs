using Vheos.Tools.FilePatcher.Code.Enums;

namespace Vheos.Tools.FilePatcher.Code;

public readonly struct PatchFileOp
{
    private static readonly byte[] DefaultBytes = Array.Empty<byte>();
    private static readonly string DefaultName = string.Empty;

    public readonly bool Success;
    public readonly PresetType Type;
    public readonly byte[] AOB;
    public readonly string Name;

    private PatchFileOp(bool success, PresetType type, byte[] bytes, string name)
    {
        Success = success;
        Type = type;
        AOB = bytes;
        Name = name;
    }

    public static PatchFileOp Vanilla(byte[] aob) => new(true, PresetType.Vanilla, aob, DefaultName);
    public static PatchFileOp Custom(string name, byte[] aob) => new(true, PresetType.Custom, aob, name);
    public static PatchFileOp Unknown(byte[] aob) => new(true, PresetType.Unknown, aob, DefaultName);
    public static PatchFileOp Failed(PresetType type) => new(false, type, DefaultBytes, DefaultName);
}
