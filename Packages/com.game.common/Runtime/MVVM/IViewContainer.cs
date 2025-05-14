using UnityEngine;

namespace Game.Common.Runtime.MVVM
{
    public interface IViewContainer
    {
        GameObject GetView<TView>();
    }
}