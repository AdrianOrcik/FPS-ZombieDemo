using UnityEngine;

namespace NPC
{
    public class NPC_BodyHit : MonoBehaviour
    {
        public NPC_Controller NpcController;
        public NPC_BodyType NpcBodyType;

        public void SetNpcState()
        {
            Debug.Log(NpcBodyType);
            NpcController.SetNpcState(NPC_BehaviourType.ANIM_TO_HIT, NpcBodyType);
        }
    }
}