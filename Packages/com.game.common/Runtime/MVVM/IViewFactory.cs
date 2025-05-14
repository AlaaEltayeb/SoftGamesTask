using UnityEngine;

namespace Game.Common.Runtime.MVVM
{
    public interface IViewFactory
    {
        IView Create<TView>(
            string name,
            Transform parent = null)
            where TView : IView;
    }
}