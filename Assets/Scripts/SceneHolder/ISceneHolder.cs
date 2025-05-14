using Game.Common.Runtime.MVVM;

namespace Assets.Scripts.SceneHolder
{
    public interface ISceneHolder
    {
        void SetActiveScene(IView currentScene);
    }
}