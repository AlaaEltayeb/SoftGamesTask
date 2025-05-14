using Assets.Scripts.Loading;
using Game.Common.Runtime.MVVM;
using Game.Common.Runtime.Command;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer.Unity;
using Vector2 = UnityEngine.Vector2;

namespace Assets.Scripts.MagicWords
{
    public sealed class ConversationModel : IConversationModel, IInitializable
    {
        private const string SpriteAssetMaterialName = "SpriteAssetMaterial";
        private const string ConversationFileName = "MagicWords";

        private Material _spriteAssetMaterial;

        private readonly ICommandDispatcher _commandDispatcher;

        private readonly List<Sprite> _emojis = new();
        private readonly IView _loadingView;

        public Conversation Conversation { get; set; } = new();
        public List<Sprite> Avatars { get; set; } = new();
        public TMP_SpriteAsset EmojisSpriteAsset { get; set; }

        public ConversationModel(IViewFactory viewFactory, ICommandDispatcher commandDispatcher)
        {
            _loadingView = viewFactory.Create<LoadingView>($"{typeof(LoadingView)}");
            _commandDispatcher = commandDispatcher;

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

        private void OnMaterialLoaded(AsyncOperationHandle<Material> handle)
        {
            _spriteAssetMaterial = new Material(handle.Result);

            DownloadAllSpritesAsync();

            Addressables.Release(handle);
        }

        private void OnConversationLoaded(AsyncOperationHandle<TextAsset> handle)
        {
            Conversation = JsonUtility.FromJson<Conversation>(handle.Result.text);

            Addressables.LoadAssetAsync<Material>(SpriteAssetMaterialName)
                .Completed += OnMaterialLoaded;

            Addressables.Release(handle);
        }

        private async void DownloadAllSpritesAsync()
        {
            var downloadTasks = Conversation.avatars
                .Select(avatar => DownloadAvatarAsync(avatar.url, avatar.name))
                .ToList();

            downloadTasks.AddRange(Conversation.emojies
                .Select(emoji => DownloadEmojiAsync(emoji.url, emoji.name)));

            await Task.WhenAll(downloadTasks);

            EmojisSpriteAsset = SpriteAssetGenerator.CreateSpriteAsset(_emojis, _spriteAssetMaterial);

            _loadingView.Dispose();
            _commandDispatcher.Execute(new ShowMainMenuCommand());
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
            _emojis.Add(sprite);
        }
    }
}