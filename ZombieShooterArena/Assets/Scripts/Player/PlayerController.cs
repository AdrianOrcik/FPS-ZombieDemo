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

        [Header("Require Components")] [SerializeField]
        private Rigidbody rigidbody;

        public Camera camera;

        [Header("Weapons Settings")] public LayerMask mask;
        public Weapon weaponData;
        public Animator animator;
        public Transform muzzleTransform;

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

        public void InstantiateMuzzleFlash(GameObject obj, Transform parent)
        {
            GameObject _obj = Instantiate(obj, parent);
            Destroy(_obj, 1f);
        }

        public void InstantiateImpact(GameObject obj, RaycastHit hit)
        {
            GameObject _obj = Instantiate(obj, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(_obj, 2f);
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