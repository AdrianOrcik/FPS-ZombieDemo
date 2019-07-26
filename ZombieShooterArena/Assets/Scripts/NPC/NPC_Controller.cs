using UnityEngine;

namespace NPC
{
    public class NPC_Controller : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void SetAnimation(string animTrigger)
        {
            animator.SetTrigger(animTrigger);
        }
    }
}