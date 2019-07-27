using Core;
using UnityEngine;

namespace NPC
{
    public enum NPC
    {
        None,
        Idle,
        Run,
        Hit,
        Die
    }

    public class NPC_Controller : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Transform Target;
        private float Speed = 4f;
        private float RotSpeed = 5f;

        public float accurancy = 1f;
        public NPC NPC_Behaviour = NPC.Idle;

        private float animationDelay = 1f;
        private float animationCounter = 0f;
        private bool animSetting = false;

        public int Life = 3;

        private bool GameOver = false;

        public void SetAnimation(string animTrigger, int speed, NPC npcBehaviour)
        {
            if (animSetting)
            {
                animSetting = false;
                Speed = speed;

                NPC_Behaviour = npcBehaviour;
                animator.SetTrigger(animTrigger);

                Debug.Log("TO=> " + npcBehaviour);
                if (npcBehaviour == NPC.Hit)
                {
                    Hit();
                }
            }
        }

        public void Hit()
        {
            Life -= 1;
            if (Life <= 0 && NPC_Behaviour != NPC.Die)
            {
                GameOver = true;
                NPC_Behaviour = NPC.Die;
                Speed = 0;
                animator.SetTrigger(Constants.ANIM_TO_DIE);
            }
        }

        private void Start()
        {
            NPC_Behaviour = NPC.Idle;
            Invoke(nameof(ToAlive), 1f);
        }

        private void ToAlive()
        {
            SetAnimation(Constants.ANIM_TO_RUN, 4, NPC.Run);
        }

        private void Update()
        {
            Debug.Log("Speed: " + Speed);

            if (animationCounter <= animationDelay)
            {
                animationCounter += Time.deltaTime;
                if (animationCounter > animationDelay)
                {
                    animSetting = true;
                    animationCounter = 0;
                }
            }
        }

        private void LateUpdate()
        {
            if (!GameOver)
            {
                if (Life <= 0 && NPC_Behaviour != NPC.Die)
                {
                    GameOver = true;
                    NPC_Behaviour = NPC.Die;
                    Speed = 0;
                    animator.SetTrigger(Constants.ANIM_TO_DIE);
                }
                else
                {
                    Walk();
                }
            }
        }

        private void Walk()
        {
            Vector3 lookAtGoal = new Vector3(Target.transform.position.x, transform.position.y,
                Target.transform.position.z);

            if (NPC_Behaviour == NPC.Run)
            {
                Vector3 direction = lookAtGoal - transform.position;


                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                    Time.deltaTime * RotSpeed);
            }

            if (Vector3.Distance(transform.position, lookAtGoal) > accurancy)
            {
                if (NPC_Behaviour == NPC.Idle || NPC_Behaviour == NPC.Hit)
                {
                    SetAnimation(Constants.ANIM_TO_RUN, 4, NPC.Run);
                    return;
                }

                if (NPC_Behaviour == NPC.Run)
                {
                    transform.Translate(0, 0, Speed * Time.deltaTime);
                    return;
                }
            }
            else
            {
                if (NPC_Behaviour != NPC.Idle)
                {
                    SetAnimation(Constants.ANIM_TO_IDLE, 0, NPC.Idle);
                }
            }
        }
    }
}