using Core.Architecture;
using UnityEngine;

namespace Core
{
    public class GameManager : MainBehaviour
    {
        //TODO: all property fill by default data
        public bool IsPaused { get; set; }

        //TODO: Into GameData PlayerData
        public int Health { get; set; }
        public int Kills { get; set; }

        public override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

            Health = 100;
            Kills = 0;

            IsPaused = false;
            Cursor.visible = false;
        }

        protected override void SubscribeEvents()
        {
            EventManager.OnClickEscape += OnPauseGame;
        }

        protected override void UnsubscribeEvent()
        {
            EventManager.OnClickEscape -= OnPauseGame;
        }

        public void KillZombie()
        {
            Kills += 1;
        }

        public void HitByZombie()
        {
            if (Health > 0)
            {
                Health -= 20;
            }
        }

        private void OnPauseGame()
        {
            if (!IsPaused)
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                IsPaused = true;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.None;
                IsPaused = false;
                Time.timeScale = 1;
            }
        }
    }
}