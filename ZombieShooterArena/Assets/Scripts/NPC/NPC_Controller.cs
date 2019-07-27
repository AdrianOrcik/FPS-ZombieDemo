using Core;
using UnityEngine;

namespace NPC
{
    public class NPC_Controller : MonoBehaviour
    {
        private NPC_AnimController npcAnimController;

        [SerializeField] private Animator animator;
        [SerializeField] private Transform Target;

        public NPC_BehaviourType NPC_Behaviour { get; set; }
        public NPC_BehaviourType NPC_LastBehaviour { get; set; }

        private float Speed { get; set; }
        private float RotSpeed { get; set; }

        private int currentLife = 3;
        private float accurancy = 2f;
        private Vector3 lookAtGoal = Vector3.zero;

        private void Awake()
        {
            npcAnimController = new NPC_AnimController(this, animator);
        }

        private void Start()
        {
            DefaultConfig();

            //TODO: Refactor
            Invoke(nameof(ToAlive), 2f);
        }

        private void DefaultConfig()
        {
            NPC_Behaviour = NPC_BehaviourType.ANIM_TO_IDLE;
            Speed = Constants.IDLE_SPEED;
            RotSpeed = Constants.IDLE_ROT;
        }

        private void ToAlive()
        {
            SetNpcState(NPC_BehaviourType.ANIM_TO_RUN);
        }

        public void SetNpcState(NPC_BehaviourType npcBehaviour)
        {
            switch (npcBehaviour)
            {
                case NPC_BehaviourType.ANIM_TO_IDLE:
                    OnIdle();
                    break;
                case NPC_BehaviourType.ANIM_TO_RUN:
                    OnRun();
                    break;
                case NPC_BehaviourType.ANIM_TO_HIT:
                    OnHit();
                    break;
            }
        }

        public void SetAnimAttribute(int speed, int rotSpeed)
        {
            Speed = speed;
            RotSpeed = rotSpeed;
        }

        private void OnIdle()
        {
            npcAnimController.SetAnimation(NPC_BehaviourType.ANIM_TO_IDLE);
        }

        public void OnIdleConfig(NPC_BehaviourType npcBehaviourType)
        {
            NPC_Behaviour = npcBehaviourType;
            Speed = Constants.IDLE_SPEED;
            RotSpeed = Constants.IDLE_ROT;
        }

        public void OnRunConfig(NPC_BehaviourType npcBehaviourType)
        {
            NPC_Behaviour = npcBehaviourType;
            Speed = Constants.RUN_SPEED;
            RotSpeed = Constants.RUN_ROT;
        }

        private void OnRun()
        {
            npcAnimController.SetAnimation(NPC_BehaviourType.ANIM_TO_RUN);
        }

        private void OnDie()
        {
            npcAnimController.SetAnimation(NPC_BehaviourType.ANIM_TO_DIE);
        }

        private void OnHit()
        {
            currentLife -= 1;
            npcAnimController.SetAnimation(NPC_BehaviourType.ANIM_TO_HIT);
        }

        private void Update()
        {
            npcAnimController.DeltaUpdate();
            npcAnimController.AnimStateUpdate();
        }

        private void LateUpdate()
        {
            lookAtGoal = new Vector3(Target.transform.position.x, transform.position.y,
                Target.transform.position.z);

            if (IsTargetFar())
            {
                WalkCalculation();
                RotationCalculation();
            }
        }

        public bool IsTargetFar()
        {
            return Vector3.Distance(transform.position, lookAtGoal) > accurancy;
        }

        private void WalkCalculation()
        {
            transform.Translate(0, 0, Speed * Time.deltaTime);
        }

        private void RotationCalculation()
        {
            Vector3 direction = lookAtGoal - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                Time.deltaTime * RotSpeed);
        }
    }
}