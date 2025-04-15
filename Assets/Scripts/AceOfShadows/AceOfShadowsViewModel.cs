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

        public void GenerateCards(int cardsCount, Transform cardsParent, Transform cardsTargetParent)
        {
            for (var i = 0; i < cardsCount; i++)
            {
                var view = _viewFactory.Create<CardView>(
                    $"{CardViewName}{i}",
                    cardsParent);

                var viewModel = view.GetViewModel() as CardViewModel;
                viewModel.CardTargetParent = cardsTargetParent;
                _cardsStack.Push(viewModel);
            }
        }

        public bool PopCard()
        {
            if (_cardsStack.Count == 0)
                return false;

            var viewModel = _cardsStack.Pop();
            viewModel.PopCard();

            return true;
        }
    }
}