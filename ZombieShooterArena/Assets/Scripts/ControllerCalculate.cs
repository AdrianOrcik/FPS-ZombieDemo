using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCalculate
{
    public Vector3 Velocity { get; set; }
    public Vector3 yRotation { get; set; }
    public Vector3 xRotation { get; set; }

    private PlayerController player;

    public ControllerCalculate(PlayerController player)
    {
        this.player = player;
        Velocity = Vector3.zero;
        yRotation = Vector3.zero;
        xRotation = Vector3.zero;
    }

    public void Update()
    {
        MovementCalculate();
        RotationCalculate();
    }

    private void MovementCalculate()
    {
        //Refactor into movementCalculation class
        //TODO: constants
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");

        Vector3 movHorizontal = player.transform.right * xMov;
        Vector3 movVertical = player.transform.forward * zMov;

        //Final movement vector
        Velocity = (movHorizontal + movVertical).normalized * player.Speed;
    }

    private void RotationCalculate()
    {
        //Calculate Rotation
        //TODO:constants
        float xRot = Input.GetAxis("Mouse X");
        xRotation = new Vector3(0f, xRot, 0f) * player.RotationSensitivity;

        float yRot = Input.GetAxis("Mouse Y");
        yRotation = new Vector3(yRot, 0f, 0f) * player.RotationSensitivity;
    }
}