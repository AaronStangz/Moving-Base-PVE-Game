
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShuttleDriving : MonoBehaviour
{
    public Rigidbody rb;
    public LayerMask groundLayer;
    public float Speed = 1;
    public float Gravity = 10f;

    [Header("Turning")]
    public float maxSteerAngle = 0.1f;
    public int rotationSpeed = 1;

    [Header("Wheels")]
    public float rotationMultiplier = 10f;

    public GameObject FrontleftWheel;
    public GameObject FrontmiddelWheel;
    public GameObject FrontrightWheel;
    public GameObject BackleftWheel;
    public GameObject BackrightWheel;

    [Header("Ground Check")]
    public float checkRadius = 0.2f;

    public Transform FrontleftgroundCheck;
    public Transform FrontmiddelgroundCheck;
    public Transform FrontrightgroundCheck;
    public Transform BackleftgroundCheck;
    public Transform BackrightgroundCheck;

    public bool FrontleftisGrounded;
    public bool FrontmiddelisGrounded;
    public bool FrontrightisGrounded;
    public bool BackleftisGrounded;
    public bool BackrightisGrounded;

    public bool isGrounded;

    private bool speedIncreased = false;
    private bool gravityIncreased = false;
    private bool WheelSpeedIncreased = false;
    // Define minimum and maximum speed values
    public float minSpeed = 0f;
    public float maxSpeed = 10f;

    void Update()
    {
        GroundCheck();
        Move();
        Turn();
        rb.AddForce(Vector3.down * Gravity * 100);
        Speed = Mathf.Clamp(Speed, minSpeed, maxSpeed);
    }

    public void Move()
    {
        float rotationAmount = Speed * rotationMultiplier * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(Vector3.forward * Speed * 100);
            ForwardRotateWheels(rotationAmount);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(Vector3.back * Speed * 0.5f * 100);
            BackRotateWheels(rotationAmount);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            //Speed = 0; // Can set to 0 or reset speed here
        }

        if (Input.GetKey(KeyCode.A))
        {
            BackrightWheel.transform.Rotate(Vector3.back, rotationAmount * -2f);
            BackleftWheel.transform.Rotate(Vector3.back, rotationAmount * 2);
        }
        if (Input.GetKey(KeyCode.D))
        {
            BackrightWheel.transform.Rotate(Vector3.back, rotationAmount * 2f);
            BackleftWheel.transform.Rotate(Vector3.back, rotationAmount * -2f);
        }
    }
    public void GroundCheck()
    {
        FrontleftisGrounded = Physics.CheckSphere(FrontleftgroundCheck.position, checkRadius, groundLayer);
        FrontmiddelisGrounded = Physics.CheckSphere(FrontmiddelgroundCheck.position, checkRadius, groundLayer);
        FrontrightisGrounded = Physics.CheckSphere(FrontrightgroundCheck.position, checkRadius, groundLayer);
        BackleftisGrounded = Physics.CheckSphere(BackleftgroundCheck.position, checkRadius, groundLayer);
        BackrightisGrounded = Physics.CheckSphere(BackrightgroundCheck.position, checkRadius, groundLayer);

        if (!speedIncreased && (FrontleftisGrounded || FrontmiddelisGrounded || FrontrightisGrounded || BackleftisGrounded || BackrightisGrounded))
        {
            Speed += 5f;
            speedIncreased = true;
        }
        else if (!FrontleftisGrounded || !FrontmiddelisGrounded || !FrontrightisGrounded || !BackleftisGrounded || !BackrightisGrounded)
        {
            speedIncreased = false;
            Speed -= 0.75f;
        }

        if (!gravityIncreased && (FrontleftisGrounded || FrontmiddelisGrounded || FrontrightisGrounded || BackleftisGrounded || BackrightisGrounded))
        {
            isGrounded = true;
            Gravity = 2f;
            gravityIncreased = true;
        }
        else
        {
            isGrounded = false;
            Gravity += 0.1f;
            gravityIncreased = false;
        }

        if (!WheelSpeedIncreased && (FrontleftisGrounded || FrontmiddelisGrounded || FrontrightisGrounded || BackleftisGrounded || BackrightisGrounded))
        {
            isGrounded = true;
            rotationMultiplier = 20f;
            WheelSpeedIncreased = true;
        }
        else
        {
            isGrounded = false;
            rotationMultiplier += 10f;
            WheelSpeedIncreased = false;
        }
    }
    public void Turn()
    {
        if (isGrounded)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float steerAngle = horizontalInput * maxSteerAngle;
            Quaternion rotation = Quaternion.Euler(0f, steerAngle, 0f);
            rb.MoveRotation(rb.rotation * rotation);
        }
    }

    public void ForwardRotateWheels(float rotationAmount)
    {
        FrontleftWheel.transform.Rotate(Vector3.forward, rotationAmount);
        FrontmiddelWheel.transform.Rotate(Vector3.forward, rotationAmount);
        FrontrightWheel.transform.Rotate(Vector3.forward, rotationAmount);
        BackleftWheel.transform.Rotate(Vector3.forward, rotationAmount);
        BackrightWheel.transform.Rotate(Vector3.forward, rotationAmount);
    }

    public void BackRotateWheels(float rotationAmount)
    {
        FrontleftWheel.transform.Rotate(Vector3.back, rotationAmount);
        FrontmiddelWheel.transform.Rotate(Vector3.back, rotationAmount);
        FrontrightWheel.transform.Rotate(Vector3.back, rotationAmount);

        BackleftWheel.transform.Rotate(Vector3.forward, rotationAmount);
        BackrightWheel.transform.Rotate(Vector3.forward, rotationAmount);
    }
}