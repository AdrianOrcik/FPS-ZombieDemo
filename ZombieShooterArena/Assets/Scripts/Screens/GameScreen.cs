using System.Collections;
using Core.Architecture;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    public class GameScreen : AccessBehaviour
    {
        [SerializeField] private DamagePanelScreen DamagePanelScreen;
        [Header("Reloading")] [SerializeField] private Slider ReloadingSlider;
        [SerializeField] private TMP_Text ReloadingText;

        [Header("WeaponStatus")] [SerializeField]
        private TMP_Text AmmoAmount;

        [SerializeField] private TMP_Text StackAmount;

        protected override void SubscribeEvents()
        {
            EventManager.OnHitPlayer += OnHitPlayer;
            EventManager.OnPlayerShot += OnShotRefreshUI;
        }

        protected override void UnsubscribeEvent()
        {
            EventManager.OnHitPlayer -= OnHitPlayer;
            EventManager.OnPlayerShot -= OnShotRefreshUI;
        }

        private void Awake()
        {
            base.Awake();

            ReloadingSlider.value = 1f;
            ReloadingText.gameObject.SetActive(false);

            AmmoAmount.text = PlayerController.weaponData.AmmoAmount.ToString();
            StackAmount.text = PlayerController.weaponData.StackAmount.ToString();
        }

        public void OnHitPlayer()
        {
            if (!DamagePanelScreen.gameObject.activeSelf)
            {
                DamagePanelScreen.gameObject.SetActive(true);
            }
        }

        public void OnShotRefreshUI()
        {
            if (!PlayerController.weaponData.IsReloading)
            {
                AmmoAmount.text = PlayerController.weaponData.AmmoAmount.ToString();
                if (PlayerController.weaponData.ToReload())
                {
                    OnReloading();
                }
            }
        }

        public void OnReloading()
        {
            StartCoroutine(Reloading());
        }

        private IEnumerator Reloading()
        {
            PlayerController.weaponData.IsReloading = true;
            ReloadingText.gameObject.SetActive(true);
            ReloadingSlider.value = 0f;
            float value = ReloadingSlider.value;

            while (value <= 1f)
            {
                value += Time.deltaTime * 0.5f;
                ReloadingSlider.value = value;
                yield return new WaitForEndOfFrame();
            }

            ReloadingText.gameObject.SetActive(false);
            PlayerController.weaponData.IsReloading = false;
            PlayerController.weaponData.Reload();
            AmmoAmount.text = PlayerController.weaponData.AmmoAmount.ToString();
        }
    }
}