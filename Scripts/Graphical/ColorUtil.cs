using UnityEngine;

namespace Misner.Lib.Graphical
{
    /// <summary>
    /// Simple color utility.
    /// </summary>
    public static class ColorUtil
    {
        /// <summary>
        /// Converts an integer hex code into a float based color.
        /// </summary>
        /// <returns>The hex.</returns>
        /// <param name="hexCode">Hex code.</param>
        public static Color FromHex(uint hexCode)
        {
            float r = ((hexCode & 0xff0000) >> 16) / 255f;
            float g = ((hexCode & 0xff00) >> 8) / 255f;
            float b = (hexCode & 0xff) / 255f;

            Color result = new Color(r, g, b);

            return result;
        }
    }
}
