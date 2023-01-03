using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vheos.Tools.FilePatcher.Code;

namespace Vheos.Tools.FilePatcher.Controls
{
    public partial class PatchView : UserControl
    {
        public event Action<bool> OnActivityChanged = delegate { };

        public new string Name
        {
            get => _nameLabel.Text;
            set => _nameLabel.Text = value;
        }

        public bool IsEnabled
        {
            get => _checkbox.Checked;
            set => _checkbox.Checked = value;
        }

        public bool CanBeDisabled
        {
            get => _checkbox.Enabled;
            set => _checkbox.Enabled = value;
        }

        public float LoadProgress
        {
            get => _progressBar.Value / 100f;
            set
            {
                _progressBar.Value = value.Clamp01().Mul(100).Round();
                UpdateControlsVisibilityOnLoading();
            }
        }

        private void UpdateControlsVisibilityOnLoading()
        {
            bool isLoaded = LoadProgress >= 1f;
            _progressBar.Visible = !isLoaded;
            _presetDropdown.Visible = isLoaded;
            _inputField.Visible = isLoaded;
        }

        public IEnumerable<string> Presets
        {
            get
            {
                foreach (var item in _presetDropdown.Items)
                    yield return item.ToString();
            }
            set
            {
                _presetDropdown.Items.Clear();
                foreach (var item in value)
                    _presetDropdown.Items.Add(item);

                _presetDropdown.Enabled = _presetDropdown.Items.Count > 0;
            }
        }

        public PatchView(Control parent)
        {
            InitializeComponent();

            Width = parent.Width;
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            Parent = parent;

            _checkbox.CheckedChanged += InvokeOnActivityChanged;
        }

        private void InvokeOnActivityChanged(object? sender, EventArgs e)
            => OnActivityChanged(_checkbox.Checked);
    }
}
