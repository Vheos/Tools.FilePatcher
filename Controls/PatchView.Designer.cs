namespace Vheos.Tools.FilePatcher.Controls
{
    partial class PatchView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._inputField = new System.Windows.Forms.TextBox();
            this._presetDropdown = new System.Windows.Forms.ComboBox();
            this._layoutRight = new System.Windows.Forms.FlowLayoutPanel();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._checkbox = new System.Windows.Forms.CheckBox();
            this._layoutLeft = new System.Windows.Forms.FlowLayoutPanel();
            this._nameLabel = new System.Windows.Forms.Label();
            this._layoutRight.SuspendLayout();
            this._layoutLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // _inputField
            // 
            this._inputField.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this._inputField.Location = new System.Drawing.Point(215, 10);
            this._inputField.Name = "_inputField";
            this._inputField.PlaceholderText = "Value";
            this._inputField.Size = new System.Drawing.Size(100, 26);
            this._inputField.TabIndex = 2;
            // 
            // _presetDropdown
            // 
            this._presetDropdown.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._presetDropdown.DisplayMember = "Preset";
            this._presetDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._presetDropdown.FormattingEnabled = true;
            this._presetDropdown.Location = new System.Drawing.Point(109, 3);
            this._presetDropdown.MinimumSize = new System.Drawing.Size(5, 0);
            this._presetDropdown.Name = "_presetDropdown";
            this._presetDropdown.Size = new System.Drawing.Size(100, 27);
            this._presetDropdown.TabIndex = 1;
            // 
            // _layoutRight
            // 
            this._layoutRight.AutoSize = true;
            this._layoutRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._layoutRight.Controls.Add(this._progressBar);
            this._layoutRight.Controls.Add(this._presetDropdown);
            this._layoutRight.Controls.Add(this._inputField);
            this._layoutRight.Dock = System.Windows.Forms.DockStyle.Right;
            this._layoutRight.Location = new System.Drawing.Point(155, 0);
            this._layoutRight.Margin = new System.Windows.Forms.Padding(0);
            this._layoutRight.Name = "_layoutRight";
            this._layoutRight.Size = new System.Drawing.Size(318, 33);
            this._layoutRight.TabIndex = 4;
            this._layoutRight.WrapContents = false;
            // 
            // _progressBar
            // 
            this._progressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this._progressBar.Location = new System.Drawing.Point(3, 3);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(100, 33);
            this._progressBar.TabIndex = 3;
            this._progressBar.Value = 50;
            this._progressBar.Visible = false;
            // 
            // _checkbox
            // 
            this._checkbox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._checkbox.AutoSize = true;
            this._checkbox.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._checkbox.Location = new System.Drawing.Point(3, 9);
            this._checkbox.Name = "_checkbox";
            this._checkbox.Size = new System.Drawing.Size(15, 14);
            this._checkbox.TabIndex = 0;
            this._checkbox.UseVisualStyleBackColor = true;
            // 
            // _layoutLeft
            // 
            this._layoutLeft.AutoSize = true;
            this._layoutLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._layoutLeft.Controls.Add(this._checkbox);
            this._layoutLeft.Controls.Add(this._nameLabel);
            this._layoutLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this._layoutLeft.Location = new System.Drawing.Point(0, 0);
            this._layoutLeft.Margin = new System.Windows.Forms.Padding(0);
            this._layoutLeft.Name = "_layoutLeft";
            this._layoutLeft.Size = new System.Drawing.Size(72, 33);
            this._layoutLeft.TabIndex = 5;
            this._layoutLeft.WrapContents = false;
            // 
            // _nameLabel
            // 
            this._nameLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._nameLabel.AutoSize = true;
            this._nameLabel.Location = new System.Drawing.Point(24, 3);
            this._nameLabel.Margin = new System.Windows.Forms.Padding(3);
            this._nameLabel.MinimumSize = new System.Drawing.Size(0, 27);
            this._nameLabel.Name = "_nameLabel";
            this._nameLabel.Size = new System.Drawing.Size(45, 27);
            this._nameLabel.TabIndex = 1;
            this._nameLabel.Text = "Name";
            this._nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PatchView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._layoutLeft);
            this.Controls.Add(this._layoutRight);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PatchView";
            this.Size = new System.Drawing.Size(473, 33);
            this._layoutRight.ResumeLayout(false);
            this._layoutRight.PerformLayout();
            this._layoutLeft.ResumeLayout(false);
            this._layoutLeft.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox _inputField;
        private ComboBox _presetDropdown;
        private FlowLayoutPanel _layoutRight;
        private CheckBox _checkbox;
        private ProgressBar _progressBar;
        private FlowLayoutPanel _layoutLeft;
        private Label _nameLabel;
    }
}
