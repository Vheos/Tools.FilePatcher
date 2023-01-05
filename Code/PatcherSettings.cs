using Vheos.Tools.FilePatcher.Code.Helpers;

namespace Vheos.Tools.FilePatcher.Code;

public class PatcherSettings
{
    public readonly string PatchExtension;
    public readonly DirectoryInfo PatchesDirectory;
    public readonly bool IncludeSubdirectories;
    public readonly IReadOnlyCollection<string> VanillaPresetAliases;

    public FileInfo[] GetAllPatchFiles()
        => PatchesDirectory.GetFiles($"*.{PatchExtension}", IncludeSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

    public static readonly PatcherSettings Default = new(Json.Default);

    public PatcherSettings(Json json)
    {
        PatchExtension = json.PatchExtension;
        PatchesDirectory = new DirectoryInfo(json.PatchesDirectory);
        IncludeSubdirectories = json.IncludeSubdirectories.ToBool();
        VanillaPresetAliases = json.VanillaPresetAliases;
    }

    public readonly struct Json
    {
        public string PatchExtension { get; init; }
        public string PatchesDirectory { get; init; }
        public string IncludeSubdirectories { get; init; }
        public string[] VanillaPresetAliases { get; init; }

        public static readonly Json Default = new()
        {
            PatchExtension = "patch",
            PatchesDirectory = @"./Patches",
            IncludeSubdirectories = "true",
            VanillaPresetAliases = new[] { "", "Vanilla", "Default", "Off" },
        };

        public Json()
        {
            PatchExtension = Default.PatchExtension;
            PatchesDirectory = Default.PatchesDirectory;
            IncludeSubdirectories = Default.IncludeSubdirectories;
            VanillaPresetAliases = Default.VanillaPresetAliases;
        }
    }
}