namespace Vheos.Tools.FilePatcher.Code
{
    public abstract class AController<TModel, TView>
    {
        public readonly TModel Model;
        public readonly TView View;

        public AController(TModel model, TView view)
        {
            Model = model;
            View = view;
        }
    }
}
