using System;
using UnityEngine;

namespace Misner.Lib.Control
{
    /// <summary>
    /// Direction key.
    /// 
    /// A trivial mapping from KeyCodes to spatial directions for building control mechanisms in Unity.
    /// </summary>
    [Serializable]
    public struct DirectionKey
    {
        [SerializeField]
        public string name;

        [SerializeField]
        public KeyCode key;

        [SerializeField]
        public Vector3 direction;
    }
}
