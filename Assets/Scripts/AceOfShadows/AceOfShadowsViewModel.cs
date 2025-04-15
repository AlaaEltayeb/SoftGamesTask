using Assets.Scripts.MVVM;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AceOfShadows
{
    public sealed class AceOfShadowsViewModel : ViewModelBase
    {
        private const string CardViewName = "CardView";
        private readonly IViewFactory _viewFactory;

        private readonly Stack<CardViewModel> _cardsStack = new();

        private float _currentOffset;
        private const float OffsetIncrease = 5;

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
                viewModel!.CardTargetParent = cardsTargetParent;
                _cardsStack.Push(viewModel);
            }

            OffsetCardsAsync(CancellationToken.None);
        }

        public bool MoveTheCardOnTop()
        {
            if (_cardsStack.Count == 0)
                return false;

            var viewModel = _cardsStack.Pop();
            viewModel.MoveCard();

            return true;
        }

        private async Task OffsetCardsAsync(CancellationToken cancellationToken)
        {
            await DelaySafe(0.5f, cancellationToken);

            var cardsList = _cardsStack.ToList();

            for (var i = cardsList.Count - 1; i >= 0; i--)
            {
                cardsList[i].SetOffset(_currentOffset);
                _currentOffset += OffsetIncrease;
            }
        }

        private async Task DelaySafe(float seconds, CancellationToken token)
        {
            var elapsed = 0f;
            while (elapsed < seconds)
            {
                if (token.IsCancellationRequested)
                    return;

                await Task.Yield();
                elapsed += Time.unscaledDeltaTime;
            }
        }
    }
}