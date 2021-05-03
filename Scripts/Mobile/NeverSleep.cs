using UnityEngine;

namespace Misner.Lib.Mobile
{
    public class NeverSleep : MonoBehaviour
    {
        #region MonoBehaviour
        void Awake()
        {
            Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
        }
        #endregion
    }
}
