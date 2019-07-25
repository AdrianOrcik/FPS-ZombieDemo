using Core.Architecture;
using UnityEngine;

namespace Core
{
    public class EventManager : MainBehaviour
    {
        public delegate void OnClickEscapeHandler();
        public event OnClickEscapeHandler OnClickEscape;

        public void OnTriggerClickEscape()
        {
            OnClickEscape?.Invoke();
        }
    }
}