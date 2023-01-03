using System.Text.Json;
using Vheos.Tools.FilePatcher.Code;
using Vheos.Tools.FilePatcher.Code.Helpers;
using Vheos.Tools.FilePatcher.Controls;

namespace Vheos.Tools.FilePatcher;

public partial class Form : System.Windows.Forms.Form
{
    private readonly Settings Settings = new()
    {
        PatchFileExtension = "patch",
        PatchFileDirectory = @"./Patches",
        VanillaPresetAliases = new[] { "Vanilla", "Default", "Off" },
    };

    public Form()
    {
        InitializeComponent();
        InitializeJsonOptions();

        int counter = 0;
        foreach ((string name, PatchModel model) in DeserializePatchFiles(Settings))
        {
            PatchView view = new(_panel);
            view.Location = new Point(0, view.Height * counter++);

            PatchController controller = new(model, view);
            _ = controller.Initialize(name);
        }
    }

    private static void InitializeJsonOptions()
    {
        JsonUtil.Options.WriteIndented = true;
        JsonUtil.Options.AllowTrailingCommas = true;
        JsonUtil.Options.ReadCommentHandling = JsonCommentHandling.Skip;
    }

    private static IEnumerable<KeyValuePair<string, PatchModel>> DeserializePatchFiles(Settings settings)
    {
        foreach (string file in Directory.GetFiles(settings.PatchFileDirectory, $"*.{settings.PatchFileExtension}", SearchOption.AllDirectories))
        {
            if (File.ReadAllText(file).TryDeserialize(out Dictionary<string, PatchModel.Json> jsonPatches))
            {
                foreach ((string name, PatchModel.Json patch) in jsonPatches)
                    yield return new(name, new PatchModel(patch, settings.VanillaPresetAliases));
            }
        }
    }
}