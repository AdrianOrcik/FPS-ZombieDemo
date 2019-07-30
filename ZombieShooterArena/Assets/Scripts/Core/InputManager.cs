using Core.Architecture;
using UnityEngine;

namespace Core
{
    public class InputManager : MainBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyUp(Constants.INPUT_ESCAPE_BTN))
            {
                EventManager.OnTriggerClickEscape();
            }

            if (Input.GetKeyUp(KeyCode.R))
            {
                EventManager.OnTriggerReloadingWeapon();
            }
        }
    }
}