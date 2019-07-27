using System.Dynamic;
using Core;
using UnityEngine;

namespace NPC
{
    public class NPC_AnimController
    {
        private NPC_Controller npcController;
        private Animator animator;

        private bool changeState = true;
        private float animationChangeDelta = 1f;
        private float animationCurrentDelta = 0f;


        public NPC_AnimController(NPC_Controller _npcController, Animator _animator)
        {
            npcController = _npcController;
            animator = _animator;
        }

        public void SetAnimation(NPC_BehaviourType npcBehaviourType)
        {
            animator.SetTrigger(npcBehaviourType.ToString());
        }

        public void DeltaUpdate()
        {
            animationCurrentDelta += Time.deltaTime;
            if (animationCurrentDelta > animationChangeDelta)
            {
                changeState = true;
                animationCurrentDelta = 0;
            }
        }

        public void AnimStateUpdate()
        {
            SwitchMoveState();
        }

        private void SwitchMoveState()
        {
            if (npcController.IsTargetFar())
            {
                if (npcController.NPC_Behaviour != NPC_BehaviourType.ANIM_TO_RUN)
                {
                    npcController.NPC_Behaviour = NPC_BehaviourType.ANIM_TO_RUN; //Handle multiple triggered run state
                    npcController.SetNpcState(NPC_BehaviourType.ANIM_TO_RUN);
                }
            }
            else
            {
                if (npcController.NPC_Behaviour != NPC_BehaviourType.ANIM_TO_ATT)
                {
                    npcController.SetNpcState(NPC_BehaviourType.ANIM_TO_ATT);
                }
            }
        }
    }
}