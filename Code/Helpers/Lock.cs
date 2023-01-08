namespace Vheos.Tools.FilePatcher.Code.Helpers;
public class Lock : IDisposable
{
    private byte _locks = 0;
    public bool Active => _locks > 0;
    public void Initialize() => _locks++;
    public void Dispose() => _locks--;

    public static implicit operator bool(Lock @this) => @this.Active;
}