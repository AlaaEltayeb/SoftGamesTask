using Assets.Scripts.MVVM;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts
{
    public static class LifetimeScopeExtension
    {
        public static void RegisterViewWithViewModelOnNewGameObject<TView, TViewModel>(
            this IContainerBuilder builder,
            Lifetime lifetime,
            string newGameObjectName = null)
            where TViewModel : class, IViewModel
            where TView : ViewBase<TViewModel>, IView
        {
            builder.RegisterComponentOnNewGameObject<TView>(lifetime, newGameObjectName);
            builder.Register<TViewModel>(lifetime);
        }

        public static void RegisterComponentsInHierarchy<TView, TViewModel>(
            this IContainerBuilder builder,
            Lifetime lifetime)
            where TViewModel : class, IViewModel
            where TView : ViewBase<TViewModel>, IView
        {
            builder.RegisterComponentInHierarchy<TView>();
            builder.Register<TViewModel>(lifetime);
        }
    }
}