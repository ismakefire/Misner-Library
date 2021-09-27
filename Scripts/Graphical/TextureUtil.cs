using UnityEngine;
using System.IO;

namespace Misner.Lib.Graphical
{
    /// <summary>
    /// Texture Utility
    /// 
    /// Simple static utility for generating textures and dumping them to files.
    /// </summary>
    public static class TextureUtil
    {
        /// <summary>
        /// Writes the render texture provided to an image file.
        /// </summary>
        /// <param name="textureSource">Texture source.</param>
        /// <param name="pngOutPath">Png out path.</param>
        public static void WriteRenderTextureToImageFile(RenderTexture textureSource, string pngOutPath)
        {
            Texture2D encodableTexture = ConvertRenderTextureToTexture2D(textureSource);

            File.WriteAllBytes(pngOutPath, encodableTexture.EncodeToPNG());
        }

        /// <summary>
        /// Rasterizes a gpu based texture to a ram based texture. Making is useable for file io.
        /// </summary>
        /// <returns>The render texture to texture2d.</returns>
        /// <param name="textureSource">Texture source.</param>
        private static Texture2D ConvertRenderTextureToTexture2D(RenderTexture textureSource)
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
