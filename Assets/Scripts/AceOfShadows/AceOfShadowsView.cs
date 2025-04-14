using Assets.Scripts.MVVM;
using UnityEngine;

namespace Assets.Scripts.AceOfShadows
{
    public sealed class AceOfShadowsView : ViewBase<AceOfShadowsViewModel>
    {
        [SerializeField]
        private int _cardsCount;

        [SerializeField]
        private Transform _cardsParent;

        protected override void Bind()
        {
            ViewModel.GenerateCards(_cardsCount, _cardsParent);
        }
    }
}