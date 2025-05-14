using Game.Common.Runtime.MVVM;
using System;
using UnityEngine;

namespace Assets.Scripts.AceOfShadows
{
    public sealed class CardViewModel : ViewModelBase
    {
        public Action OnMoveCard { get; set; }
        public Action<float> OnCardSetup { get; set; }
        public Transform CardTargetParent { get; set; }

        public void MoveCard()
        {
            OnMoveCard?.Invoke();
        }

        public void SetOffset(float offset)
        {
            OnCardSetup?.Invoke(offset);
        }

        public override void Dispose()
        {
            OnMoveCard = null;
            base.Dispose();
        }
    }
}