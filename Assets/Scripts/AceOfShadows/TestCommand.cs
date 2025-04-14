using Assets.Scripts.Command;
using UnityEngine;

namespace Assets.Scripts.AceOfShadows
{
    public sealed class TestCommand : ICommand
    {
        public void Execute()
        {
            Debug.Log("Command Executed");
        }
    }
}