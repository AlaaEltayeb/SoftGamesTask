using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer.Unity;
using Vector2 = UnityEngine.Vector2;

namespace Assets.Scripts.MagicWords
{
    public sealed class ConversationModel : IInitializable
    {
        private const string ConversationFileName = "MagicWords";

        public Conversation Conversation { get; set; } = new();
        public List<Sprite> Emojis { get; set; } = new();
        public List<Sprite> Avatars { get; set; } = new();

        public ConversationModel()
        {
            LoadConversation();
        }

        public void Initialize()
        {
        }

        private void LoadConversation()
        {
            Addressables.LoadAssetAsync<TextAsset>(ConversationFileName)
                .Completed += OnConversationLoaded;
        }

        private void OnConversationLoaded(AsyncOperationHandle<TextAsset> handle)
        {
            Conversation = JsonUtility.FromJson<Conversation>(handle.Result.text);

            DownloadAllSpritesAsync();

            Addressables.Release(handle);
        }

        private async void DownloadAllSpritesAsync()
        {
            var downloadTasks = new List<Task>();

            foreach (var avatar in Conversation.avatars)
            {
                downloadTasks.Add(DownloadAvatarAsync(avatar.url, avatar.name));
            }

            foreach (var emoji in Conversation.emojies)
            {
                downloadTasks.Add(DownloadEmojiAsync(emoji.url, emoji.name));
            }

            await Task.WhenAll(downloadTasks);

            Debug.Log("Finished Downloading All Avatars");
        }

        private async Task DownloadAvatarAsync(string url, string name)
        {
            using var request = UnityWebRequestTexture.GetTexture(url);

            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to download sprite: " + request.error);
            }

            var texture = DownloadHandlerTexture.GetContent(request);

            var sprite = Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));

            sprite.name = name;
            Avatars.Add(sprite);
        }

        private async Task DownloadEmojiAsync(string url, string name)
        {
            using var request = UnityWebRequestTexture.GetTexture(url);

            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to download sprite: " + request.error);
                return;
            }

            var texture = DownloadHandlerTexture.GetContent(request);

            var sprite = Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));

            sprite.name = name;
            Emojis.Add(sprite);
        }

        private Material material;

        //private async Task<TMP_SpriteAsset> CreateSpriteAssetAsync()
        //{

        //}
    }
}