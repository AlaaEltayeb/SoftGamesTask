using UnityEngine;

namespace Assets.Scripts.MVVM
{
    public interface IViewFactory
    {
        IView Create<TView>(
            string name,
            Transform parent = null)
            where TView : IView;
    }
}