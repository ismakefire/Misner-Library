using UnityEngine;

namespace Misner.Lib.Graphical
{
    public static class ColorUtil
    {
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
