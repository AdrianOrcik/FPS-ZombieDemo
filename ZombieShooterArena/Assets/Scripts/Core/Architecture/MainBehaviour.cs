namespace Core.Architecture
{
    public class MainBehaviour : AccessBehaviour
    {
        public void AssignClass(AccessBehaviour common)
        {
            MainModel.CommonBehaviours.Add(common);
        }

        public override void OnDestroy()
        { 
            base.OnDestroy();
            if (MainModel.CommonBehaviours.Contains(this))
            {
                MainModel.CommonBehaviours.Remove(this);
            }
        }

        public override void Awake()
        {
            base.Awake();
            AssignClass(this);
        }
        
        protected override void SubscribeEvents()
        {
        }

        protected override void UnsubscribeEvent()
        {
        }
        
    }
}