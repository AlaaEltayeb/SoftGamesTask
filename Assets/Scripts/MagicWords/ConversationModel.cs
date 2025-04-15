using Assets.Scripts.MVVM;
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

        private readonly IViewFactory _viewFactory;

        public Conversation Conversation { get; set; } = new();
        public List<Sprite> Emojis { get; set; } = new();
        public List<Sprite> Avatars { get; set; } = new();
        public TMP_SpriteAsset EmojisSpriteAsset { get; set; }

        public ConversationModel(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;

            LoadConversation();
        }

        public void Initialize()
        {
        }

        private void LoadConversation()
        {
            Addressables.LoadAssetAsync<TextAsset>(ConversationFileName)
                .Completed += OnConversationLoaded;

            Addressables.LoadAssetAsync<Material>(SpriteAssetMaterialName)
                .Completed += OnMaterialLoaded;
        }

        private void OnMaterialLoaded(AsyncOperationHandle<Material> handle)
        {
            _spriteAssetMaterial = handle.Result;

            Addressables.Release(handle);
        }

        private void OnConversationLoaded(AsyncOperationHandle<TextAsset> handle)
        {
            Conversation = JsonUtility.FromJson<Conversation>(handle.Result.text);

            DownloadAllSpritesAsync();

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

            CreateSpriteAsset();

            var view = _viewFactory.Create<ConversationView>(
                "ConversationView",
                null);

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

        private void CreateSpriteAsset()
        {
            var spriteCount = Emojis.Count;
            var spriteSize = Emojis[0].texture.width;

            var gridSize = Mathf.CeilToInt(Mathf.Sqrt(spriteCount));
            var atlasSize = gridSize * spriteSize;

            Texture2D atlas = new(atlasSize, atlasSize, TextureFormat.ARGB32, false);
            var rects = atlas.PackTextures(Emojis.ConvertAll(sprite => sprite.texture).ToArray(), 4, spriteSize);

            var asset = ScriptableObject.CreateInstance<TMP_SpriteAsset>();
            asset.name = "RuntimeEmojiAsset";
            asset.spriteSheet = atlas;
            asset.spriteInfoList = new List<TMP_Sprite>();
            asset.material = _spriteAssetMaterial;

            for (var i = 0; i < Emojis.Count; i++)
            {
                var rect = rects[i];
                var sprite = new TMP_Sprite
                {
                    id = i,
                    name = Emojis[i].name,
                    unicode = 0xE000 + i,
                    x = rect.x * atlas.width,
                    y = rect.y * atlas.height,
                    width = rect.width * atlas.width,
                    height = rect.height * atlas.height,
                    xOffset = 0,
                    yOffset = 30,
                    xAdvance = rect.width * atlas.width,
                    scale = 1f,
                    sprite = Emojis[i],
                };

                asset.spriteInfoList.Add(sprite);
            }

            var index = 0;
            foreach (var spriteCharacter in asset.spriteCharacterTable)
            {
                spriteCharacter.glyphIndex = (uint)index;
                index++;
            }

            _spriteAssetMaterial.mainTexture = asset.spriteSheet;

            asset.UpdateLookupTables();

            EmojisSpriteAsset = asset;
        }
    }
}