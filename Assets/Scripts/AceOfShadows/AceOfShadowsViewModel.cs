using Assets.Scripts.MVVM;
using UnityEngine;

namespace Assets.Scripts.AceOfShadows
{
    public sealed class AceOfShadowsViewModel : ViewModelBase
    {
        private readonly IViewFactory _viewFactory;

        public AceOfShadowsViewModel(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public void GenerateCards(int cardsCount, Transform cardsParent)
        {
            for (var i = 0; i < cardsCount; i++)
            {
                _viewFactory.Create<CardView>(
                    "CardView",
                    cardsParent);
            }
        }
    }
}