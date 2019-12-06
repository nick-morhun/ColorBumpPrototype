using System;
using UnityEngine;

namespace NickMorhun.ColorBump.Tools
{
    [DisallowMultipleComponent]
    public sealed class TriggerEnterReporter : MonoBehaviour
    {
        public event Action<GameObject> Collided = delegate { };

        private void Collide(Collider other)
        {
            Collided.Invoke(other.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            Collide(other);
        }
    }
}