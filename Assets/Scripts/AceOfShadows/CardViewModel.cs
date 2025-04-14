using Assets.Scripts.MVVM;
using System;
using UnityEngine;

namespace Assets.Scripts.AceOfShadows
{
    public sealed class CardViewModel : ViewModelBase
    {
        public Action OnCardPopped { get; set; }
        public Transform CardTargetParent { get; set; }

        public void PopCard()
        {
            OnCardPopped?.Invoke();
        }

        public override void Dispose()
        {
            OnCardPopped = null;
            base.Dispose();
        }
    }
}