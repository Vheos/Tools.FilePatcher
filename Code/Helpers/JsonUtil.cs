using System.Diagnostics;
using System.Text.Json;

namespace Vheos.Tools.FilePatcher.Code.Helpers
{
    public static class JsonUtil
    {
        public static readonly JsonSerializerOptions Options = new();


        public static bool TryDeserialize<T>(this string @this, out T result)
        {
            try
            {
                result = JsonSerializer.Deserialize<T>(@this, Options);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        public static T Deserialize<T>(this string @this)
        {
            if (!@this.TryDeserialize(out T result))
                Debug.WriteLine($"Cannot parse {nameof(T)}: \"{@this}\"!");

            return result;
        }
    }
}
