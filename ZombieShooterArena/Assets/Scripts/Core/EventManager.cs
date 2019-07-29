using Core.Architecture;
using UnityEngine;

namespace Core
{
    public class EventManager : MainBehaviour
    {
        public delegate void OnClickEscapeHandler();

        public event OnClickEscapeHandler OnClickEscape;

        public delegate void OnHitPlayerHandler();

        public event OnHitPlayerHandler OnHitPlayer;

        public delegate void OnPlayerShotHandler();

        public event OnPlayerShotHandler OnPlayerShot;

        public void OnTriggerClickEscape()
        {
            OnClickEscape?.Invoke();
        }

        public void OnTriggerHitPlayer()
        {
            OnHitPlayer?.Invoke();
        }

        public void OnTriggerPlayerShot()
        {
            OnPlayerShot?.Invoke();
        }
    }
}