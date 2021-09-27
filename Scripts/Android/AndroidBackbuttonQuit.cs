using UnityEngine;

namespace Misner.Lib.Android
{
    /// <summary>
    /// Android backbutton quit.
    /// 
    /// Stand alone behavior for detecting the Android back button and dispatching a quit message.
    /// </summary>
    public class AndroidBackbuttonQuit : MonoBehaviour
    {
        #region MonoBehaviour
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Application.Quit();
#else
                Debug.LogFormat("<color=#7f7f7f>{0}.Update(), back button for android suppressed on this platform.</color>", this.ToString());
#endif
            }
        }
        #endregion
    }
}
