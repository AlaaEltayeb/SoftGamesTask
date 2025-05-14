using System;

namespace Game.Common.Runtime.MVVM
{
    public interface IView : IDisposable
    {
        IViewModel GetViewModel();
    }
}