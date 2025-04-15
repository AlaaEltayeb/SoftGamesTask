using Assets.Scripts.MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.InGameMenu
{
    public sealed class InGameMenuView : ViewBase<InGameMenuViewModel>
    {
        [SerializeField]
        private Button _aceOfShadowsButton;

        [SerializeField]
        private Button _magicWordsButton;

        [SerializeField]
        private Button _phoenixFlameButton;

        protected override void Bind()
        {
            base.Bind();

            _aceOfShadowsButton.onClick.AddListener(() =>
            {
                ViewModel.OnAceOfShadowsSelected();
                gameObject.SetActive(false);
            });

            _magicWordsButton.onClick.AddListener(() =>
            {
                ViewModel.OnMagicWordsSelected();
                gameObject.SetActive(false);
            });

            _phoenixFlameButton.onClick.AddListener(() =>
            {
                ViewModel.OnPhoenixFlameSelected();
                gameObject.SetActive(false);
            });
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _aceOfShadowsButton.onClick.RemoveAllListeners();
            _magicWordsButton.onClick.RemoveAllListeners();
            _phoenixFlameButton.onClick.RemoveAllListeners();
        }
    }
}