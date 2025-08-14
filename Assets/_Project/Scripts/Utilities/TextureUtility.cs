using System;
using System.IO;
using UnityEngine;

namespace AdventurerVillage.Utilities
{
    public static class TextureUtility
    {
        public enum SaveTextureFileFormat
        {
            JPG,
            PNG
        };

        /// <summary>
        /// Saves a Texture2D to disk with the specified filename and image format
        /// </summary>
        /// <param name="tex"></param>
        /// <param name="filePath"></param>
        /// <param name="fileFormat"></param>
        /// <param name="jpgQuality"></param>
        public static void SaveTextureToFile(Texture2D tex, string filePath,
            SaveTextureFileFormat fileFormat = SaveTextureFileFormat.PNG,
            int jpgQuality = 95)
        {
            Debug.Log("<color=yellow>SaveTextureToFile 0</color>");
            switch (fileFormat)
            {
                case SaveTextureFileFormat.JPG:
                    File.WriteAllBytes(filePath + ".jpg", tex.EncodeToJPG(jpgQuality));
                    break;
                case SaveTextureFileFormat.PNG:
                    File.WriteAllBytes(filePath + ".png", tex.EncodeToPNG());
                    break;
            }

            Debug.Log("<color=yellow>SaveTextureToFile 1</color>");
        }


        /// <summary>
        /// Saves a RenderTexture to disk with the specified filename and image format
        /// </summary>
        /// <param name="renderTexture"></param>
        /// <param name="filePath"></param>
        /// <param name="fileFormat"></param>
        /// <param name="jpgQuality"></param>
        public static void SaveTextureToFile(RenderTexture renderTexture, string filePath,
            SaveTextureFileFormat fileFormat = SaveTextureFileFormat.PNG, int jpgQuality = 95)
        {
            var texture2D = ConvertToTexture2D(renderTexture);
            SaveTextureToFile(texture2D, filePath, fileFormat, jpgQuality);
        }

        public static Texture2D ConvertToTexture2D(this RenderTexture renderTexture)
        {
            var texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false,false);
            var oldRt = RenderTexture.active;
            RenderTexture.active = renderTexture;
            texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture2D.Apply();
            RenderTexture.active = oldRt;
            return texture2D;
        }

        public static Texture2D LoadTextureFromFile(string filePath, int width, int height,
            SaveTextureFileFormat fileFormat = SaveTextureFileFormat.PNG)
        {
            var newTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);
            newTexture.LoadImage(ReadFile());
            return newTexture;

            byte[] ReadFile()
            {
                return fileFormat switch
                {
                    SaveTextureFileFormat.JPG => File.ReadAllBytes(filePath + ".jpg"),
                    SaveTextureFileFormat.PNG => File.ReadAllBytes(filePath + ".png"),
                    _ => throw new ArgumentOutOfRangeException(nameof(fileFormat), fileFormat, null)
                };
            }
        }

        public static byte[] GetBytes(this Texture2D texture,
            SaveTextureFileFormat fileFormat = SaveTextureFileFormat.PNG, int jpgQuality = 95)
        {
            return fileFormat switch
            {
                SaveTextureFileFormat.JPG => texture.EncodeToJPG(jpgQuality),
                SaveTextureFileFormat.PNG => texture.EncodeToPNG(),
                _ => throw new ArgumentOutOfRangeException(nameof(fileFormat), fileFormat, null)
            };
        }

        public static Texture2D ConvertToTexture2D(byte[] bytes, int width, int height)
        {
            var newTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);
            newTexture.LoadImage(bytes);
            return newTexture;
        }

        public static Sprite ToSprite(this Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
    }
}