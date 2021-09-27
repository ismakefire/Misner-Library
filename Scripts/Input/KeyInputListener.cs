using UnityEngine;

namespace Misner.Lib.Control
{
    /// <summary>
    /// Key input listener.
    /// 
    /// Behavior for providing key into to any class which implements the IInputTarget interface.
    /// </summary>
    public class KeyInputListener : MonoBehaviour
    {
        #region Types
        public interface IInputTarget
        {
            void OnKeyDown(KeyCode keycode);
            void OnKeyUp(KeyCode keycode);
        }
        #endregion
        
        #region Properties
        public IInputTarget InputTarget { get; set; }
        #endregion
        
        #region MonoBehaviour
        protected void OnGUI()
        {
            if (InputTarget != null)
            {
                Event keyEvent = Event.current;
                
                if (keyEvent.type == EventType.KeyDown && Input.GetKeyDown(keyEvent.keyCode))
                {
                    InputTarget.OnKeyDown(keyEvent.keyCode);
                }
                else if (keyEvent.type == EventType.KeyUp)
                {
                    InputTarget.OnKeyUp(keyEvent.keyCode);
                }
            }
        }
        #endregion
	}
}
