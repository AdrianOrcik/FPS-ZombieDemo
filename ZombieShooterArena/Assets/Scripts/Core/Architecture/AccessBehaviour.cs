using UnityEngine;

namespace Core.Architecture
{
    public class AccessBehaviour : MonoBehaviour
    {
        public InputManager InputManager => GetAssignedClass<InputManager>();
        public GameManager GameManager => GetAssignedClass<GameManager>();
        public EventManager EventManager => GetAssignedClass<EventManager>();

        private T GetAssignedClass<T>() where T : AccessBehaviour
        {
            return MainModel.GetAssignedClass<T>();
        }
        
        public virtual void Awake()
        {
            if (EventManager != null)
            {
                SubscribeEvents();
            }
        }

        public virtual void OnDestroy()
        {
            if (EventManager != null)
            {
                UnsubscribeEvent();
            }
        }

        private void OnDisable()
        {
            if (EventManager != null)
            {
                UnsubscribeEvent();
            }
        }

        protected virtual void SubscribeEvents()
        {
        }

        protected virtual void UnsubscribeEvent()
        {
        }
    }
}