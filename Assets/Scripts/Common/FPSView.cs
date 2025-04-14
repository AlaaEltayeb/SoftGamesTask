using Assets.Scripts.MVVM;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public sealed class FPSView : ViewBase<FPSViewModel>
    {
        [SerializeField]
        private TextMeshProUGUI _fpsCounter;

        private float _deltaTime;

        private void Update()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
            var fps = 1.0f / _deltaTime;
            _fpsCounter.text = $"{fps:0.} FPS";
        }
    }
}