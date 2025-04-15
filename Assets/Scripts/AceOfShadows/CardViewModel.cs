using Assets.Scripts.MVVM;
using System;
using UnityEngine;

namespace Assets.Scripts.AceOfShadows
{
    public sealed class CardViewModel : ViewModelBase
    {
        public Action OnMoveCard { get; set; }
        public Transform CardTargetParent { get; set; }

        public void MoveCard()
        {
            OnMoveCard?.Invoke();
        }

        public override void Dispose()
        {
            OnMoveCard = null;
            base.Dispose();
        }
    }
}