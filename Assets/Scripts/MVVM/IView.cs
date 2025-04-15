using System;

namespace Assets.Scripts.MVVM
{
    public interface IView : IDisposable
    {
        IViewModel GetViewModel();
    }
}