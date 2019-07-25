using Core.Architecture;
using UnityEngine;

namespace Core
{
    public class InputManager : MainBehaviour
    {
        public void IsEscape()
        {
            if (Input.GetKeyUp(Constants.INPUT_ESCAPE_BTN))
            {
                EventManager.OnTriggerClickEscape();
            }
        }
    }
}