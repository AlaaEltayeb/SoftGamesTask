using Assets.Scripts.MVVM;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AceOfShadows
{
    public sealed class AceOfShadowsViewModel : ViewModelBase
    {
        private const string CardViewName = "CardView";
        private readonly IViewFactory _viewFactory;

        private readonly Stack<CardViewModel> _cardsStack = new();

        public AceOfShadowsViewModel(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public void GenerateCards(int cardsCount, Transform cardsParent)
        {
            for (var i = 0; i < cardsCount; i++)
            {
                var view = _viewFactory.Create<CardView>(
                    $"{CardViewName}{i}",
                    cardsParent);

                _cardsStack.Push(view.GetViewModel() as CardViewModel);
            }
        }

        public bool PopCard()
        {
            if (_cardsStack.Count == 0)
            {
                Debug.Log("No more cards to pop");
                return false;
            }

            var viewModel = _cardsStack.Pop();
            viewModel.PopCard();
            Debug.Log("Card Popped");

            return true;
        }
    }
}