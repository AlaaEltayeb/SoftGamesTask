namespace Assets.Scripts.Command
{
    public interface ICommandDispatcher
    {
        void Execute(ICommand command);
    }
}