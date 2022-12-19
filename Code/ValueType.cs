using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vheos.Tools.FilePatcher.Code
{
    public  enum ValueType
    {
        Hex,    // ? bytes  / hex, h, (default)
        Byte,   // 1 byte   / byte, b
        Short,  // 2 bytes  / short, s
        Int,    // 4 bytes  / int, i
        Long,   // 8 bytes  / long, l
        Float,  // 4 bytes  / float, f
        Double, // 8 bytes  / double, d       

        h = Hex,
        b = Byte,
        s = Short,
        i = Int,
        l = Long,
        f = Float,
        d = Double,
    }

    /* Examples:
     * byte 50 51 52  float 0.25 0.5  byte 53     =   b 50 51 52  f 0.25 0.5  b 53   =   b 50 51 52   f 0.25     0.5        b 53
     * aob  32 33 34 00 00 80 3E 00 00 00 3F 53   =   3233340000803E0000003F53       =   323334       0000803E   0000003F   53
     * 
     * b - all subsequent values with unspecified type will be treated as BYTE
     * 50b - only this value will be treated as byte
     * 
     * b 51 52   53 54   - 4 bytes
     * b 51 52f  53 54   - byte, float, 2 bytes
     * b 51 52 f 53 54   - 2 bytes, 2 floats
     * 
     */
}
