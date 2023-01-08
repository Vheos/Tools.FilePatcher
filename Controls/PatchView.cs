using System.Diagnostics;
using System.Reflection;
using Vheos.Tools.FilePatcher.Code.Helpers;

namespace Vheos.Tools.FilePatcher.Controls;

public partial class PatchView : UserControl
{
    private string _previousPreset = "";

    public static Color DisabledColor { get; set; } = SystemColors.ControlDark;
    public static Color EnabledColor { get; set; } = SystemColors.ControlText;
    public static Color WarningColor { get; set; } = Color.Orange;
    public static Color ErrorColor { get; set; } = Color.Red;

    public event Action<bool> OnChangeActivity = delegate { };
    public event Action<string> OnChangePreset = delegate { };

    public ControlState CheckboxState { get => _checkbox.GetState(); set => _checkbox.SetState(value); }
    public ControlState NameState { get => _nameLabel.GetState(); set => _nameLabel.SetState(value); }
    public ControlState PresetState { get => _presetDropdown.GetState(); set => _presetDropdown.SetState(value); }
    public ControlState EditorState { get => _editorInput.GetState(); set => _editorInput.SetState(value); }
    public ControlState LoadingBarState { get => _progressBar.GetState(); set => _progressBar.SetState(value); }

    public Control CheckboxControl => _checkbox;
    public Control NameControl => _nameLabel;
    public Control PresetControl => _presetDropdown;
    public Control EditorControl => _editorInput;
    public Control LoadingBarControl => _progressBar;
    public IEnumerable<Control> VisualControls
    {
        get
        {
            yield return _checkbox;
            yield return _nameLabel;
            yield return _presetDropdown;
            yield return _editorInput;
            yield return _progressBar;
        }
    }

    public bool IsActive
    {
        get => _checkbox.Checked;
        set => _checkbox.Checked = value;

    }
    public new string Name
    {
        get => _nameLabel.Text;
        set => _nameLabel.Text = value;
    }
    public Color TextColor
    {
        get => _nameLabel.ForeColor;
        set => _nameLabel.ForeColor = value;
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
            {
                if (_presetDropdown.Items.Add(item) == 0)
                    _presetDropdown.SelectedItem = item;
            }

            _presetDropdown.Visible = _presetDropdown.Items.Count >= 2;
        }
    }
    public string CurrentPreset
    {
        get => _presetDropdown.SelectedItem?.ToString() ?? string.Empty;
        set => _presetDropdown.SelectedItem = value;
    }
    public string Editor
    {
        get => _editorInput.Text;
        set => _editorInput.Text = value;
    }
    public float LoadingBarProgress
    {
        get => _progressBar.Value / 100f;
        set => _progressBar.Value = value.Clamp01().Mul(100).Round();
    }

    public PatchView(Control parent)
    {
        InitializeComponent();

        Parent = parent;
        Width = parent.Width;
        Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        _progressBar.BringToFront();

        _checkbox.CheckedChanged += OnChangeCheckboxState;
        _nameLabel.MouseUp += OnClickName;
        _presetDropdown.SelectedIndexChanged += OnChangePresetDropdownIndex;
    }



    public void HideAll()
    {
        foreach (var control in VisualControls)
            control.SetState(ControlState.Hidden);
    }
    public void StartLoading()
    {
        _progressBar.Visible = true;
        LoadingBarProgress = 0;
    }
    public void StopLoading()
    {
        _progressBar.Visible = false;
    }
    public void SetErrorState()
    {
        HideAll();
        NameState = ControlState.Enabled;
        TextColor = ErrorColor;
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


    private  Lock _silentEvents = new();
    public Lock SilentEvents
    {
        get
        {
            _silentEvents.Initialize();
            return _silentEvents;
        }
    }

    private void OnChangePresetDropdownIndex(object? sender, EventArgs e)
    {
        if (CurrentPreset == _previousPreset)
            return;

        Debug.WriteLine("Sdf");
        _previousPreset = CurrentPreset;

        if (!_silentEvents)
            OnChangePreset(CurrentPreset);        
    }
    private void OnChangeCheckboxState(object? sender, EventArgs e)
    {
        if (!_silentEvents)
            OnChangeActivity(_checkbox.Checked);
    }
    private void OnClickName(object? sender, MouseEventArgs e)
    {
        if (_checkbox.Visible && _checkbox.Enabled)
            _checkbox.Checked = !_checkbox.Checked;
    }
}
