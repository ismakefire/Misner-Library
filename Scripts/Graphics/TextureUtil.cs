using UnityEngine;
using System.IO;

namespace Misner.Lib.Graphical
{
    public static class TextureUtil
    {
        public static void WriteRenderTextureToImageFile(RenderTexture textureSource, string pngOutPath)
        {
            Texture2D encodableTexture = ConvertRenderTextureToTexture2D(textureSource);

            File.WriteAllBytes(pngOutPath, encodableTexture.EncodeToPNG());
        }

        public static Texture2D ConvertRenderTextureToTexture2D(RenderTexture textureSource)
        {
            Texture2D result;

            RenderTexture oldRT = RenderTexture.active;
            RenderTexture.active = textureSource;
            {
                result = new Texture2D(textureSource.width, textureSource.height);
                result.ReadPixels(new Rect(0, 0, textureSource.width, textureSource.height), 0, 0);
                result.Apply();
            }
            RenderTexture.active = oldRT;

            return result;
        }
    }
}
