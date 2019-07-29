using Core;
using Core.Architecture;
using UnityEngine;

namespace NPC
{
    public class NPC_Controller : AccessBehaviour
    {
        private NPC_AnimController npcAnimController;

        [SerializeField] private Animator animator;
        [SerializeField] private Transform target;
        [SerializeField] private Collider[] colliders;

        public NPC_BehaviourType NPC_Behaviour { get; set; }
        public NPC_BehaviourType NPC_LastBehaviour { get; set; }

        private float Speed { get; set; }
        private float RotSpeed { get; set; }

        private int currentLife = 3;
        private float accurancy = 1.8f;
        private Vector3 lookAtGoal = Vector3.zero;

        private void Awake()
        {
            npcAnimController = new NPC_AnimController(this, animator);
        }

        private void Start()
        {
            DefaultConfig();
            //TODO: Refactor
        }

        private void DefaultConfig()
        {
            NPC_Behaviour = NPC_BehaviourType.ANIM_TO_RUN;
            Speed = Constants.IDLE_SPEED;
            RotSpeed = Constants.IDLE_ROT;
        }

        public void SetNpcState(NPC_BehaviourType npcBehaviour, NPC_BodyType bodyType = NPC_BodyType.Body)
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
                    OnHit(bodyType);
                    break;
                case NPC_BehaviourType.ANIM_TO_ATT:
                    OnAttack();
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

        public void OnPlayerHit()
        {
            EventManager.OnTriggerHitPlayer();
        }

        public void OnIdleConfig(NPC_BehaviourType npcBehaviourType)
        {
            Debug.Log("IDLE_CONFIG");
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

        private void OnDie(NPC_BehaviourType npcBehaviourType)
        {
            foreach (Collider col in colliders)
            {
                col.enabled = false;
            }

            npcAnimController.SetAnimation(npcBehaviourType);
        }

        private void OnAttack()
        {
            npcAnimController.SetAnimation(NPC_BehaviourType.ANIM_TO_ATT);
        }

        private void OnHit(NPC_BodyType npcBodyType)
        {
            if (npcBodyType == NPC_BodyType.Head)
            {
                OnDie(NPC_BehaviourType.ANIM_TO_HEADSHOTDIE);
            }
            else
            {
                currentLife -= 1;

                if (currentLife <= 0)
                {
                    OnDie(NPC_BehaviourType.ANIM_TO_DIE);
                }
            }
        }

        private void Update()
        {
            npcAnimController.DeltaUpdate();
            npcAnimController.AnimStateUpdate();
        }

        private void LateUpdate()
        {
            lookAtGoal = new Vector3(target.transform.position.x, transform.position.y,
                target.transform.position.z);

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