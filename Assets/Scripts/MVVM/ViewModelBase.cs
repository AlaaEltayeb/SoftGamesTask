using JetBrains.Annotations;

namespace Assets.Scripts.MVVM
{
    [PublicAPI]
    public abstract class ViewModelBase : IViewModel
    {
        public void Dispose()
        {
        }
    }
}