using Assets.Scripts.MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.AceOfShadows
{
    public class CardView : ViewBase<CardViewModel>
    {
        [SerializeField]
        private Button _cardButton;

        protected override void Bind()
        {
            base.Bind();

            _cardButton.onClick.AddListener(ViewModel.OnButtonClicked);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _cardButton.onClick.RemoveAllListeners();
        }
    }
}