using JetBrains.Annotations;
using System;
using System.Reflection;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.MVVM
{
    public abstract class ViewBase<TViewModel> : MonoBehaviour, IView where TViewModel : IViewModel
    {
        private const string ViewModelPropertyName = "ViewModel";

        protected TViewModel ViewModel { get; private set; }

        [Inject]
        [UsedImplicitly]
        private void InjectViewModelBase(TViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        protected virtual void Awake()
        {
        }

        private void Start()
        {
            Bind();
        }

        protected virtual void Bind()
        {
        }

        public IViewModel GetViewModel()
        {
            var viewType = GetType();
            var propertyInfo =
                viewType.GetProperty(ViewModelPropertyName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (propertyInfo == null)
                throw new Exception("This view doesn't have a ViewModel, Make sure that view extends ViewBase<T>");

            return propertyInfo.GetValue(this) as IViewModel;
        }
    }
}