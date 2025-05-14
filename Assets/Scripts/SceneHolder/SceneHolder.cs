using Game.Common.Runtime.MVVM;

namespace Assets.Scripts.SceneHolder
{
    public sealed class SceneHolder : ISceneHolder
    {
        private IView _activeScene;

        public void SetActiveScene(IView currentScene)
        {
            _activeScene?.Dispose();
            _activeScene = currentScene;
        }
    }
}