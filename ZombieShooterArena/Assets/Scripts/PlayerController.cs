using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float Speed { get; set; }
    public float RotationSensitivity { get; set; }

    [Header("RequireComponents")] 
    [SerializeField]private Rigidbody rigidbody;
    [SerializeField] private Camera camera;

    private ControllerCalculate controllerCalculate;

    private void Awake()
    {
        controllerCalculate = new ControllerCalculate(this);
    }

    private void Start()
    {
        Speed = 5;
        RotationSensitivity = 3;
    }

    //Calculation in Update
    private void Update()
    {
        controllerCalculate.Update();
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