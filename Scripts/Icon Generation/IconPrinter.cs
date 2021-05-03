using UnityEngine;

namespace Misner.Lib.Icon
{
    /// <summary>
    /// Icon printer
    /// 
    /// Scene object for printing out an array of icons for use by mobile platforms.
    /// </summary>
    public class IconPrinter : MonoBehaviour
    {
        #region SerializeField
        [SerializeField]
        public RenderTexture _renderTexture;

        [SerializeField]
        public int[] _iconWidths = { 36, 48, 72, 96, 144, 192 };
        #endregion
    }
}
