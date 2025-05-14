#nullable enable
using JetBrains.Annotations;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Common.Runtime.MVVM
{
    [UsedImplicitly]
    public sealed class ViewFactory : IViewFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly IViewContainer _viewContainer;

        public ViewFactory(
            IObjectResolver objectResolver,
            IViewContainer viewContainer)
        {
            _objectResolver = objectResolver;
            _viewContainer = viewContainer;
        }

        public IView Create<TView>(string name, Transform parent = null) where TView : IView
        {
            var prefab = _viewContainer.GetView<TView>();

            if (prefab == null)
            {
                throw new Exception(
                    $"A prefab with type '{typeof(TView)}' must be assigned at view container SO.");
            }

            return CreateView(prefab, name, parent);
        }

        private IView CreateView(GameObject prefab, string name, Transform parent)
        {
            GameObject? gameObject;
            try
            {
                gameObject = _objectResolver.Instantiate(prefab, parent);
                gameObject.SetActive(false);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to instantiate view '{name}'", e);
            }

            gameObject.name = name;
            var view = gameObject.GetComponent<IView>();

            var viewModel = view.GetViewModel();
            if (viewModel == null)
            {
                throw new Exception(
                    $"View '{name}' doesn't have a ViewModel, make sure that view extends ViewBase<T>.");
            }

            gameObject.SetActive(true);

            return view;
        }
    }
}