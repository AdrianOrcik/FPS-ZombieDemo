using Core.Architecture;
using UnityEngine;

namespace Screens
{
    public class GameScreen : AccessBehaviour
    {
        [SerializeField] private DamagePanelScreen DamagePanelScreen;

        protected override void SubscribeEvents()
        {
            EventManager.OnHitPlayer += OnHitPlayer;
        }

        protected override void UnsubscribeEvent()
        {
            EventManager.OnHitPlayer -= OnHitPlayer;
        }

        public void OnHitPlayer()
        {
            if (!DamagePanelScreen.gameObject.activeSelf)
            {
                DamagePanelScreen.gameObject.SetActive(true);
            }
        }
    }
}