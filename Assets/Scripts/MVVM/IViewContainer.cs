using UnityEngine;

namespace Assets.Scripts.MVVM
{
    public interface IViewContainer
    {
        GameObject GetView<TView>();
    }
}