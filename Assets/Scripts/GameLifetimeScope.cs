using Assets.Scripts.AceOfShadows;
using Assets.Scripts.Command;
using Assets.Scripts.Common;
using Assets.Scripts.MagicWords;
using Assets.Scripts.MVVM;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts
{
    public sealed class GameLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private ViewContainer _viewContainer;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance<IViewContainer>(_viewContainer);

            builder.Register<ICommandFactory, CommandFactory>(Lifetime.Singleton);
            builder.Register<ICommandDispatcher, CommandDispatcher>(Lifetime.Singleton);

            builder.Register<IViewFactory, ViewFactory>(Lifetime.Singleton);

            builder.Register<ConversationModel>(Lifetime.Singleton).AsImplementedInterfaces();

            AddViewsAndViewModels(builder);
        }

        private static void AddViewsAndViewModels(IContainerBuilder builder)
        {
            RegisterComponentsInHierarchy<FPSView, FPSViewModel>(builder, Lifetime.Scoped);

            RegisterViewWithViewModelOnNewGameObject<ConversationView, ConversationViewModel>(builder,
                Lifetime.Transient);
            RegisterViewWithViewModelOnNewGameObject<DialogueLeftView, DialogueViewModel>(builder, Lifetime.Transient);
            RegisterViewWithViewModelOnNewGameObject<DialogueRightView, DialogueViewModel>(builder, Lifetime.Transient);

            RegisterViewWithViewModelOnNewGameObject<AceOfShadowsView, AceOfShadowsViewModel>(builder,
                Lifetime.Transient);
            RegisterViewWithViewModelOnNewGameObject<CardView, CardViewModel>(builder, Lifetime.Transient);
        }

        private static void RegisterViewWithViewModelOnNewGameObject<TView, TViewModel>(
            IContainerBuilder builder,
            Lifetime lifetime,
            string newGameObjectName = null)
            where TViewModel : class, IViewModel
            where TView : ViewBase<TViewModel>, IView
        {
            builder.RegisterComponentOnNewGameObject<TView>(lifetime, newGameObjectName);
            builder.Register<TViewModel>(lifetime);
        }

        private static void RegisterComponentsInHierarchy<TView, TViewModel>(
            IContainerBuilder builder,
            Lifetime lifetime)
            where TViewModel : class, IViewModel
            where TView : ViewBase<TViewModel>, IView
        {
            builder.RegisterComponentInHierarchy<TView>();
            builder.Register<TViewModel>(lifetime);
        }
    }
}