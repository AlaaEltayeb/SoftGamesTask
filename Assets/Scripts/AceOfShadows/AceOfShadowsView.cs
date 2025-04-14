using Assets.Scripts.MVVM;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AceOfShadows
{
    public sealed class AceOfShadowsView : ViewBase<AceOfShadowsViewModel>
    {
        [SerializeField]
        private int _cardsCount;

        [SerializeField]
        private Transform _cardsParent;

        [SerializeField]
        private Transform _cardsTargetParent;

        private CancellationTokenSource _cancellationTokenSource;

        private bool _keepRunning = true;

        protected override void Bind()
        {
            ViewModel.GenerateCards(_cardsCount, _cardsParent, _cardsTargetParent);
            _cancellationTokenSource = new CancellationTokenSource();

            PopCardAsync(_cancellationTokenSource.Token);
        }

        private async Task PopCardAsync(CancellationToken cancellationToken)
        {
            while (_keepRunning)
            {
                _keepRunning = ViewModel.PopCard();
                await Task.Delay(1000, cancellationToken);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = null;
            _keepRunning = false;
        }
    }
}