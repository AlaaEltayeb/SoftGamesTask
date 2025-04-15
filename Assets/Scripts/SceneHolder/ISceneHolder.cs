using Assets.Scripts.MVVM;

namespace Assets.Scripts.SceneHolder
{
    public interface ISceneHolder
    {
        void SetActiveScene(IView currentScene);
    }
}