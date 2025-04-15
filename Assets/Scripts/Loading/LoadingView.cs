using Assets.Scripts.MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Loading
{
    public sealed class LoadingView : ViewBase<LoadingViewModel>
    {
        [SerializeField]
        private Image _loading;

        private void Update()
        {
            _loading.transform.Rotate(-Vector3.forward, 0.5f);
        }
    }
}