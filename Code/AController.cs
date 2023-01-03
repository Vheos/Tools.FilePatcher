using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vheos.Tools.FilePatcher.Controls;

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
