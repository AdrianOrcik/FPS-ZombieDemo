using Core.Architecture;
using UnityEngine;

namespace Core
{
    public class GameManager : MainBehaviour
    {
        //TODO: all property fill by default data
        public bool IsPaused { get; set; }

        public override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            IsPaused = false;
        }

        protected override void SubscribeEvents()
        {
            EventManager.OnClickEscape += OnPauseGame;
        }

        protected override void UnsubscribeEvent()
        {
            EventManager.OnClickEscape -= OnPauseGame;
        }

        private void OnPauseGame()
        {
            if (!IsPaused)
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                IsPaused = true;
            }
            else
            {
                Cursor.visible = false;
                IsPaused = false;
                Time.timeScale = 1;
            }
        }
    }
}