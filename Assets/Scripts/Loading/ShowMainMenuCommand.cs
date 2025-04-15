using Assets.Scripts.Command;
using Assets.Scripts.InGameMenu;
using Assets.Scripts.MVVM;
using VContainer;

namespace Assets.Scripts.Loading
{
    public sealed class ShowMainMenuCommand : ICommand
    {
        private IViewFactory _viewFactory;

        [Inject]
        private void Construct(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public void Execute()
        {
            _viewFactory.Create<UIView>($"{typeof(UIView)}");
        }
    }
}