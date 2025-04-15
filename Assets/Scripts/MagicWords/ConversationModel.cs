using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.MagicWords
{
    public sealed class ConversationModel : IConversationModel
    {
        private const string ConversationFileName = "MagicWords";

        public Conversation Conversation { get; set; } = new();

        public ConversationModel()
        {
            LoadConversation();
        }

        private void LoadConversation()
        {
            Addressables.LoadAssetAsync<TextAsset>(ConversationFileName)
                .Completed += OnConversationLoaded;
        }

        private void OnConversationLoaded(AsyncOperationHandle<TextAsset> handle)
        {
            Conversation = JsonUtility.FromJson<Conversation>(handle.Result.text);

            Addressables.Release(handle);
        }
    }
}