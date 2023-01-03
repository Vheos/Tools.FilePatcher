using Vheos.Tools.FilePatcher.Controls;

namespace Vheos.Tools.FilePatcher.Code
{
    internal class PatchController : AController<PatchModel, PatchView>
    {
        public PatchController(PatchModel model, PatchView view) : base(model, view)
        { }

        async public Task Initialize(string name)
        {
            View.Name = name;

            View.LoadProgress = 0.0f;
            while(View.LoadProgress < 1f)
            {
                View.LoadProgress += 0.1f;
                await Task.Delay(Random.Shared.Next(100, 500));
            }

            View.CanBeDisabled = Model.PresetsByName.ContainsKey(string.Empty);
            View.Presets = Model.PresetsByName.Keys.Where(name => name.IsNotEmpty());
        }
    }
}
