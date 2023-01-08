using Vheos.Tools.FilePatcher.Code.Enums;
using Vheos.Tools.FilePatcher.Controls;

namespace Vheos.Tools.FilePatcher.Code;

internal class PatchController : AController<PatchModel, PatchView>
{
    public PatchController(PatchModel model, PatchView view) : base(model, view)
    { }

    public async Task InitializeAsync(string name)
    {
        View.Name = name;
        if (Model.HasErrors)
        {
            View.SetErrorState();
            View.TooltipText = string.Join('\n', Model.Messages.Errors.Select(message => message.Title));
            return;
        }

        View.PresetControl.Visible = false;
        View.EditorControl.Visible = false;
        View.StartLoading();
        await Model.InitializeAsync(progress => View.LoadingBarProgress = progress);
        View.StopLoading();

        View.CheckboxControl.Enabled = true;
        View.TooltipText = Model.Tooltip;
        View.Presets = Model.CustomPresets.Keys;
        View.EditorControl.Visible = true;
        View.EditorControl.Enabled = false;

        UpdateView(Model.ReadPreset());
        View.OnChangeActivity += currentState =>
        {
            PatchFileOp presetOperation = currentState ? Model.WritePreset(View.CurrentPreset) : Model.WriteVanilla();
            UpdateView(presetOperation);
        };
        View.OnChangePreset += presetName =>
        {
            PatchFileOp presetOperation = Model.WritePreset(presetName);
            UpdateView(presetOperation);
        };
    }

    private void UpdateView(PatchFileOp presetOperation)
    {
        if (!presetOperation.Success)
            return;

        using (View.SilentEvents)
        {
            View.IsActive = presetOperation.Type == PresetType.Custom;
            View.TextColor = presetOperation.Type switch
            {
                PresetType.Vanilla => PatchView.DisabledColor,
                PresetType.Custom => PatchView.EnabledColor,
                _ => PatchView.WarningColor,
            };
            View.PresetControl.Enabled = presetOperation.Type != PresetType.Vanilla;
            if (presetOperation.Type == PresetType.Custom)
                View.CurrentPreset = presetOperation.Name;
            View.Editor = presetOperation.AOB.Aggregate(string.Empty, (acc, current) => $"{acc}{current:X2}");
        }
    }
}
