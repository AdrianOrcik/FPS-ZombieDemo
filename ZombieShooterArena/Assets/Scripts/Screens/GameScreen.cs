using System.Collections;
using System.Net.Http.Headers;
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

        [Header("GameStats")] [SerializeField] private TMP_Text HealthAmount;
        [SerializeField] private TMP_Text KillsAmount;

        protected override void SubscribeEvents()
        {
            EventManager.OnHitPlayer += OnHitPlayer;
            EventManager.OnRefreshGameUI += OnRefreshUI;
            EventManager.OnReloadingWeapon += OnReloading;
        }

        protected override void UnsubscribeEvent()
        {
            EventManager.OnHitPlayer -= OnHitPlayer;
            EventManager.OnRefreshGameUI -= OnRefreshUI;
            EventManager.OnReloadingWeapon -= OnReloading;
        }

        private void Awake()
        {
            base.Awake();

            ReloadingSlider.value = 1f;
            ReloadingText.gameObject.SetActive(false);

            AmmoAmount.text = PlayerController.weaponData.AmmoAmount.ToString();
            StackAmount.text = PlayerController.weaponData.StackAmount.ToString();
        }

        private void OnEnable()
        {
            OnRefreshUI();
        }

        public void OnHitPlayer()
        {
            GameManager.HitByZombie();
            if (!DamagePanelScreen.gameObject.activeSelf)
            {
                DamagePanelScreen.gameObject.SetActive(true);
            }

            OnRefreshUI();
        }

        public void OnRefreshUI()
        {
            HealthAmount.text = "Health: " + GameManager.Health.ToString();
            KillsAmount.text = "Kills: " + GameManager.Kills.ToString();

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