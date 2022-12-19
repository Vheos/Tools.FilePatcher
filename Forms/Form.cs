using System.Diagnostics;
using System.Text.Json;
using Vheos.Tools.FilePatcher.Code;

namespace Vheos.Tools.FilePatcher
{
    public partial class Form : System.Windows.Forms.Form
    {
        JsonSerializerOptions jsonOptions = new() { WriteIndented = true, AllowTrailingCommas = true };

        public Form()
        {
            InitializeComponent();

            try
            {
                PatchInfo.Json patchInfo = JsonSerializer.Deserialize<PatchInfo.Json>(File.ReadAllText("PatchInfo.txt"), jsonOptions);
                label1.Text = patchInfo.Parsed.ToString();
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.Dispose();
        }
    }
}