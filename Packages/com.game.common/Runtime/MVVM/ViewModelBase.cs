using JetBrains.Annotations;

namespace Game.Common.Runtime.MVVM
{
    [PublicAPI]
    public abstract class ViewModelBase : IViewModel
    {
        public virtual void Dispose()
        {
        }
    }
}