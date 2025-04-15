using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.MagicWords
{
    public class SpriteAssetGenerator
    {
        public static TMP_SpriteAsset CreateSpriteAsset(List<Sprite> emojis, Material material)
        {
            var spriteCount = emojis.Count;
            var spriteSize = emojis[0].texture.width;

            var gridSize = Mathf.CeilToInt(Mathf.Sqrt(spriteCount));
            var atlasSize = gridSize * spriteSize;

            Texture2D atlas = new(atlasSize, atlasSize, TextureFormat.ARGB32, false);
            var rects = atlas.PackTextures(emojis.ConvertAll(sprite => sprite.texture).ToArray(), 4, spriteSize);

            var asset = ScriptableObject.CreateInstance<TMP_SpriteAsset>();
            asset.name = "RuntimeEmojiAsset";
            asset.spriteSheet = atlas;
            asset.spriteInfoList = new List<TMP_Sprite>();
            asset.material = material;

            for (var i = 0; i < emojis.Count; i++)
            {
                var rect = rects[i];
                var sprite = new TMP_Sprite
                {
                    id = i,
                    name = emojis[i].name,
                    unicode = 0xE000 + i,
                    x = rect.x * atlas.width,
                    y = rect.y * atlas.height,
                    width = rect.width * atlas.width,
                    height = rect.height * atlas.height,
                    xOffset = 0,
                    yOffset = 30,
                    xAdvance = rect.width * atlas.width,
                    scale = 1f,
                    sprite = emojis[i],
                };

                asset.spriteInfoList.Add(sprite);
            }

            var index = 0;
            foreach (var spriteCharacter in asset.spriteCharacterTable)
            {
                spriteCharacter.glyphIndex = (uint)index;
                index++;
            }

            material.mainTexture = asset.spriteSheet;

            asset.UpdateLookupTables();

            return asset;
        }
    }
}