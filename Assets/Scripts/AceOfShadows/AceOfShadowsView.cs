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
            await DelaySafe(5f, cancellationToken);

            while (_keepRunning)
            {
                Debug.Log("Here 2");
                _keepRunning = ViewModel.PopCard();
                await DelaySafe(1f, cancellationToken);
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

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = null;
            _keepRunning = false;
        }
    }
}