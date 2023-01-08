namespace Vheos.Tools.FilePatcher.Code.Enums;

public enum ValueType
{
    Hex,    // AOB
    Byte,   // byte, sbyte
    Short,  // short, ushort
    Int,    // int, uint
    Long,   // long, ulong
    Float,  // float
    Double, // double

    h = Hex,
    b = Byte,
    s = Short,
    i = Int,
    l = Long,
    f = Float,
    d = Double,
}
