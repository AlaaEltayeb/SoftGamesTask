using UnityEngine;

namespace Assets.Scripts.MVVM
{
    public interface IViewFactory
    {
        void Create<TView>(
            string name,
            Transform parent)
            where TView : IView;
    }
}