using System.Text.Json;
using Vheos.Tools.FilePatcher.Code;
using Vheos.Tools.FilePatcher.Code.Helpers;
using Vheos.Tools.FilePatcher.Controls;

namespace Vheos.Tools.FilePatcher;

public partial class MainForm : Form
{
    private static readonly string ExecutableFileName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
    private const string PatcherSettingsExtension = "settings";

    public MainForm()
    {
        InitializeComponent();
        InitializeJsonOptions();
        PatcherSettings patcherSettings = InitializePatcherSettings();

        int counter = 0;
        foreach (var (name, model) in LoadPatchFiles(patcherSettings))
        {
            PatchView view = new(_panel);
            view.Location = new Point(0, view.Height * counter++);

            PatchController controller = new(model, view);
            controller.InitializeAsync(name);
        }
    }

    private static void InitializeJsonOptions()
    {
        JsonUtil.Options.WriteIndented = true;
        JsonUtil.Options.AllowTrailingCommas = true;
        JsonUtil.Options.ReadCommentHandling = JsonCommentHandling.Skip;
    }

    private static PatcherSettings InitializePatcherSettings()
    {
        FileInfo file = new($"{ExecutableFileName}.{PatcherSettingsExtension}");
        return (file.TryReadText(out var text) && text.TryDeserialize(out PatcherSettings.Json settingsJson))
            ? new PatcherSettings(settingsJson)
            : PatcherSettings.Default;
    }

    private static IEnumerable<KeyValuePair<string, PatchModel>> LoadPatchFiles(PatcherSettings settings)
    {
        foreach (var file in settings.GetAllPatchFiles())
            if (file.TryReadText(out var text) && text.TryDeserialize(out Dictionary<string, PatchModel.Json> jsonPatches))
                foreach (var (name, patch) in jsonPatches)
                    yield return new(name, new PatchModel(patch, settings.VanillaPresetAliases));
    }
}