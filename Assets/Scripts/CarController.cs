using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float acceleration = 5f;
    public float steering = 2f;
    public float maxSpeed = 20f;
    public float brakeForce = 10f;
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform rearLeftWheel;
    public Transform rearRightWheel;
    public float wheelRotationSpeed = 100f;
    private float currentSpeed = 0f;
    private float inputSteer;
    private float inputAcceleration;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputSteer = Input.GetAxis("Horizontal");
        inputAcceleration = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        if (inputAcceleration != 0)
        {
            currentSpeed += inputAcceleration * acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
        }
        else
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= brakeForce * Time.deltaTime;
                if (currentSpeed < 0)
                {
                    currentSpeed = 0;
                }
            }
            else if (currentSpeed < 0)
            {
                currentSpeed += brakeForce * Time.deltaTime;
                if (currentSpeed > 0)
                {
                    currentSpeed = 0;
                }
            }
        }

        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            float steer = inputSteer * steering * Time.deltaTime * Mathf.Sign(currentSpeed);
            transform.Rotate(0f, steer, 0f);
        }

        rb.velocity = transform.forward * currentSpeed;

        RotateWheels();

        if (currentSpeed > 0.1f)
        {
            SteerFrontWheels(inputSteer * steering);
        }
        else if (currentSpeed < -0.1f)
        {
            SteerRearWheels(inputSteer * steering);
        }
    }

    void RotateWheels()
    {
        float rotationAmount = wheelRotationSpeed * Time.deltaTime * currentSpeed;
        frontLeftWheel.Rotate(rotationAmount, 0, 0);
        frontRightWheel.Rotate(rotationAmount, 0, 0);
        rearLeftWheel.Rotate(rotationAmount, 0, 0);
        rearRightWheel.Rotate(rotationAmount, 0, 0);
    }

    void SteerFrontWheels(float steer)
    {
        frontLeftWheel.localEulerAngles = new Vector3(frontLeftWheel.localEulerAngles.x, steer, frontLeftWheel.localEulerAngles.z);
        frontRightWheel.localEulerAngles = new Vector3(frontRightWheel.localEulerAngles.x, steer, frontRightWheel.localEulerAngles.z);
    }

    void SteerRearWheels(float steer)
    {
        rearLeftWheel.localEulerAngles = new Vector3(rearLeftWheel.localEulerAngles.x, -steer, rearLeftWheel.localEulerAngles.z);
        rearRightWheel.localEulerAngles = new Vector3(rearRightWheel.localEulerAngles.x, -steer, rearRightWheel.localEulerAngles.z);
    }
}