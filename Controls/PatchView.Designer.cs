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
            this.components = new System.ComponentModel.Container();
            this._editorInput = new System.Windows.Forms.TextBox();
            this._presetDropdown = new System.Windows.Forms.ComboBox();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._checkbox = new System.Windows.Forms.CheckBox();
            this._nameLabel = new System.Windows.Forms.Label();
            this._tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this._tooltip = new System.Windows.Forms.ToolTip(this.components);
            this._tableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // _editorInput
            // 
            this._editorInput.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this._editorInput.Location = new System.Drawing.Point(424, 4);
            this._editorInput.Margin = new System.Windows.Forms.Padding(0);
            this._editorInput.Name = "_editorInput";
            this._editorInput.Size = new System.Drawing.Size(94, 26);
            this._editorInput.TabIndex = 2;
            // 
            // _presetDropdown
            // 
            this._presetDropdown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._presetDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._presetDropdown.FormattingEnabled = true;
            this._presetDropdown.Location = new System.Drawing.Point(324, 3);
            this._presetDropdown.Margin = new System.Windows.Forms.Padding(0);
            this._presetDropdown.Name = "_presetDropdown";
            this._presetDropdown.Size = new System.Drawing.Size(94, 27);
            this._presetDropdown.TabIndex = 1;
            // 
            // _progressBar
            // 
            this._progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._progressBar.Location = new System.Drawing.Point(324, 10);
            this._progressBar.Margin = new System.Windows.Forms.Padding(0);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(194, 10);
            this._progressBar.TabIndex = 3;
            this._progressBar.Value = 50;
            // 
            // _checkbox
            // 
            this._checkbox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._checkbox.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._checkbox.Location = new System.Drawing.Point(0, 5);
            this._checkbox.Margin = new System.Windows.Forms.Padding(0);
            this._checkbox.Name = "_checkbox";
            this._checkbox.Size = new System.Drawing.Size(20, 20);
            this._checkbox.TabIndex = 0;
            this._checkbox.UseVisualStyleBackColor = true;
            // 
            // _nameLabel
            // 
            this._nameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._nameLabel.AutoEllipsis = true;
            this._nameLabel.AutoSize = true;
            this._nameLabel.Location = new System.Drawing.Point(20, 5);
            this._nameLabel.Margin = new System.Windows.Forms.Padding(0);
            this._nameLabel.MaximumSize = new System.Drawing.Size(0, 20);
            this._nameLabel.Name = "_nameLabel";
            this._nameLabel.Size = new System.Drawing.Size(45, 19);
            this._nameLabel.TabIndex = 1;
            this._nameLabel.Text = "Name";
            this._nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tableLayout
            // 
            this._tableLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._tableLayout.ColumnCount = 4;
            this._tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this._tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this._tableLayout.Controls.Add(this._checkbox, 0, 0);
            this._tableLayout.Controls.Add(this._nameLabel, 1, 0);
            this._tableLayout.Controls.Add(this._editorInput, 3, 0);
            this._tableLayout.Controls.Add(this._presetDropdown, 2, 0);
            this._tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableLayout.Location = new System.Drawing.Point(0, 0);
            this._tableLayout.Name = "_tableLayout";
            this._tableLayout.RowCount = 1;
            this._tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._tableLayout.Size = new System.Drawing.Size(521, 30);
            this._tableLayout.TabIndex = 4;
            // 
            // _tooltip
            // 
            this._tooltip.AutomaticDelay = 0;
            this._tooltip.AutoPopDelay = 0;
            this._tooltip.InitialDelay = 200;
            this._tooltip.ReshowDelay = 0;
            // 
            // PatchView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this._tableLayout);
            this.Controls.Add(this._progressBar);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PatchView";
            this.Size = new System.Drawing.Size(521, 30);
            this._tableLayout.ResumeLayout(false);
            this._tableLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private TextBox _editorInput;
        private ComboBox _presetDropdown;
        private CheckBox _checkbox;
        private ProgressBar _progressBar;
        private Label _nameLabel;
        private TableLayoutPanel _tableLayout;
        private ToolTip _tooltip;
    }
}
