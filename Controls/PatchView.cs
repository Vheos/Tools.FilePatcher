using Vheos.Tools.FilePatcher.Code.Helpers;

namespace Vheos.Tools.FilePatcher.Controls;

public partial class PatchView : UserControl
{
    public event Action<bool> OnActivityChanged = delegate { };

    public ControlState CheckboxState { get => _checkbox.GetState(); set => _checkbox.SetState(value); }
    public ControlState NameState { get => _nameLabel.GetState(); set => _nameLabel.SetState(value); }
    public ControlState PresetState { get => _presetDropdown.GetState(); set => _presetDropdown.SetState(value); }
    public ControlState EditorState { get => _valueInput.GetState(); set => _valueInput.SetState(value); }
    public ControlState ProgressBarState { get => _progressBar.GetState(); set => _progressBar.SetState(value); }

    public IEnumerable<Control> VisualControls
    {
        get
        {
            yield return _checkbox;
            yield return _nameLabel;
            yield return _presetDropdown;
            yield return _valueInput;
            yield return _progressBar;
        }
    }

    public void SetLoadProgress(float progress)
        => LoadProgress = progress;

    public float LoadProgress
    {
        get => _progressBar.Value / 100f;
        set => _progressBar.Value = value.Clamp01().Mul(100).Round();
    }

    public bool IsEnabled
    {
        get => _checkbox.Checked;
        set => _checkbox.Checked = value;
    }

    public new string Name
    {
        get => _nameLabel.Text;
        set => _nameLabel.Text = value;
    }
    public Color NameColor
    {
        get => _nameLabel.ForeColor;
        set => _nameLabel.ForeColor = value;
    }

    public string EditorPlaceholderText
    {
        get => _valueInput.PlaceholderText;
        set => _valueInput.PlaceholderText = value;
    }

    public string TooltipText
    {
        get => _tooltip.GetToolTip(_nameLabel);
        set => _tooltip.SetToolTip(_nameLabel, value);
    }
    public ToolTipIcon TooltipIcon
    {
        get => _tooltip.ToolTipIcon;
        set => _tooltip.ToolTipIcon = value;
    }

    public IEnumerable<string> Presets
    {
        get
        {
            foreach (object? item in _presetDropdown.Items)
                yield return item?.ToString() ?? string.Empty;
        }
        set
        {
            _presetDropdown.Items.Clear();
            foreach (var item in value)
                _presetDropdown.Items.Add(item);
        }
    }

    public string CurrentPreset
    {
        get => _presetDropdown.SelectedItem?.ToString() ?? string.Empty;
        set => _presetDropdown.SelectedItem = value;
    }

    public PatchView(Control parent)
    {
        InitializeComponent();

        Parent = parent;
        Width = parent.Width;
        Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        _progressBar.BringToFront();

        foreach (var control in VisualControls)
            control.SetState(ControlState.Hidden);

        _checkbox.CheckedChanged += InvokeOnActivityChanged;
    }

    private void InvokeOnActivityChanged(object? sender, EventArgs e)
        => OnActivityChanged(_checkbox.Checked);

}
