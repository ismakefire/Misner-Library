using UnityEngine;

namespace Misner.Lib.Mobile
{
    /// <summary>
    /// Never sleep.
    /// 
    /// Stand alone behavior for disabling a mobile build from falling asleep.
    /// </summary>
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
