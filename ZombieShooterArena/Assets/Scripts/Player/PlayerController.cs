using Core;
using Core.Architecture;
using ScriptableScripts;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : AccessBehaviour
    {
        public float Speed { get; set; }
        public float RotationSensitivity { get; set; }

        [Header("Require Components")] 
        [SerializeField] private Rigidbody rigidbody;
        public Camera camera;

        [Header("Weapons Settings")] 
        public LayerMask mask;
        public Weapon weaponData;
        public Animator animator;

        private ControllerCalculate controllerCalculate;
        private WeaponController weaponController;
    
        private bool paused;
    
        private void Awake()
        {
            controllerCalculate = new ControllerCalculate(this);
            weaponController = new WeaponController(this);
        }

        private void Start()
        {
            paused = false;
            Cursor.visible = false;
        
            Speed = 5;
            RotationSensitivity = 3;
        }

        //Calculation in Update
        private void Update()
        {
            controllerCalculate.Update();
            weaponController.Update();
        }

        //Physic movement in FixedUpdate
        private void FixedUpdate()
        {
            Movement();
            Rotation();
        }

        private void Movement()
        {
            if (controllerCalculate.Velocity != Vector3.zero)
            {
                rigidbody.MovePosition(rigidbody.position + controllerCalculate.Velocity * Time.deltaTime);
            }
        }
        private void Rotation()
        {
            rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(controllerCalculate.xRotation));

            if (camera != null)
            {
                camera.transform.Rotate(-controllerCalculate.yRotation);
            }
        }

    }
}