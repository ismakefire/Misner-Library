using System;
using UnityEngine;

namespace Misner.Lib.Control
{
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
