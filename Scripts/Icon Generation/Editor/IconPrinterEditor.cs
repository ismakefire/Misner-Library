using UnityEngine;
using UnityEditor;
using Misner.Lib.Graphical;

namespace Misner.Lib.Icon
{
    /// <summary>
    /// Icon printer editor.
    /// 
    /// Builds the editor UI for printing icons.
    /// </summary>
    [CustomEditor(typeof(IconPrinter))]
    public class IconPrinterEditor : Editor
    {
        /// <summary>
        /// Gets my printer.
        /// 
        /// Our reference to the scene object.
        /// </summary>
        /// <value>My printer.</value>
        public IconPrinter MyPrinter
        {
            get
            {
                return (IconPrinter)target;
            }
        }

        /// <summary>
        /// OnInspector GUI
        /// 
        /// Builds our "Print" button.
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Print"))
            {
                foreach (int iconWidth in MyPrinter._iconWidths)
                {
                    string iconDirectory = MyPrinter._destinationPath;
                    string filePath = string.Format("{0}/{1}/icon({2}x{3}).png", Application.dataPath, iconDirectory, iconWidth, iconWidth);

                    RenderTexture iconRenderTexture = new RenderTexture(iconWidth, iconWidth, 0);
                    Graphics.Blit(MyPrinter._renderTexture, iconRenderTexture, Vector2.one, Vector2.zero);

                    TextureUtil.WriteRenderTextureToImageFile(iconRenderTexture, filePath);
                }

                AssetDatabase.Refresh();

                Debug.Log("Print to Icons");
            }

            GUILayout.EndHorizontal();
        }
    }
}
