using Core.Architecture;
using Enums;
using UnityEngine;

namespace Core
{
    public class EventManager : MainBehaviour
    {
        public delegate void OnClickEscapeHandler();

        public event OnClickEscapeHandler OnClickEscape;

        public delegate void OnHitPlayerHandler();

        public event OnHitPlayerHandler OnHitPlayer;

        public delegate void OnRefreshGameUIHandler();

        public event OnRefreshGameUIHandler OnRefreshGameUI;

        public delegate void OnReloadingWeaponHandler();

        public event OnReloadingWeaponHandler OnReloadingWeapon;

        public void OnTriggerReloadingWeapon()
        {
            OnReloadingWeapon?.Invoke();
        }

        public void OnTriggerRefreshGameUI()
        {
            OnRefreshGameUI?.Invoke();
        }

        public void OnTriggerClickEscape()
        {
            OnClickEscape?.Invoke();
        }

        public void OnTriggerHitPlayer()
        {
            OnHitPlayer?.Invoke();
        }
    }
}