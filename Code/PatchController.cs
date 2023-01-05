using Vheos.Helpers.Collections;
using Vheos.Tools.FilePatcher.Controls;

namespace Vheos.Tools.FilePatcher.Code;

internal class PatchController : AController<PatchModel, PatchView>
{
    public PatchController(PatchModel model, PatchView view) : base(model, view)
    { }

    public async Task Initialize(string name)
    {
        View.Name = name;

        View.NameState = ControlState.Disabled;
        View.ProgressBarState = ControlState.Disabled;
        await Model.MOCK_ScanForNeedle(View.SetLoadProgress);
        View.ProgressBarState = ControlState.Hidden;
        View.NameState = ControlState.Enabled;

        View.CheckboxState = Model.VanillaAOB != null ? ControlState.Enabled : ControlState.Hidden;
        View.Presets = Model.CustomPresets.Keys;
        View.PresetState = Model.CustomPresets.Count >= 2 ? ControlState.Enabled : ControlState.Hidden;
        if (Model.CustomPresets.Keys.TryGetAny(out var preset))
            View.CurrentPreset = preset;

        if (Model.HasErrors)
        {
            if (View.CheckboxState == ControlState.Enabled)
                View.CheckboxState = ControlState.Disabled;

            View.NameColor = Color.Red;
            View.TooltipText = Model.Errors.ToString().Replace(", ", "\n");
            View.TooltipIcon = ToolTipIcon.Error;
        }
        else if (Model.HasWarnings)
        {
            View.NameColor = Color.Orange;
            View.TooltipText = Model.Warnings.ToString().Replace(", ", "\n");
            View.TooltipIcon = ToolTipIcon.Warning;
        }
    }
}
