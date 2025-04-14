using UnityEngine;

namespace Assets.Scripts.MVVM
{
    public interface IViewFactory
    {
        IView Create<TView>(
            string name,
            Transform parent)
            where TView : IView;
    }
}