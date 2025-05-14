using Game.Common.Runtime.MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Particle
{
    public sealed class FireControllerView : ViewBase<FireControllerViewModel>
    {
        [SerializeField]
        private Animator _animator;

        private Button _controlButton;

        private bool _isFireOn = true;

        protected override void Bind()
        {
            base.Bind();

            _controlButton = GetComponentInChildren<Button>();

            _controlButton.onClick.AddListener(() =>
            {
                _isFireOn = !_isFireOn;
                _animator.SetBool("IsFireOn", _isFireOn);
            });
        }
    }
}