
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
    public float rotationSpeed = 1;

    [Header("Wheels")]
    public float rotationMultiplier = 10f;

    public GameObject FrontleftWheel;
    public GameObject FrontmiddleWheel;
    public GameObject FrontrightWheel;
    public GameObject BackleftWheel;
    public GameObject BackrightWheel;

    [Header("Ground Check")]
    public float checkRadius = 0.2f;

    public Transform FrontleftgroundCheck;
    public Transform FrontmiddlegroundCheck;
    public Transform FrontrightgroundCheck;
    public Transform BackleftgroundCheck;
    public Transform BackrightgroundCheck;

    public bool FrontleftisGrounded;
    public bool FrontmiddleisGrounded;
    public bool FrontrightisGrounded;
    public bool BackleftisGrounded;
    public bool BackrightisGrounded;

    public bool isGrounded;

    public ParticleSystem FrontleftWheelParticleSystem;
    public ParticleSystem FrontmiddleWheelParticleSystem;
    public ParticleSystem FrontrightWheelParticleSystem;
    public ParticleSystem BackleftWheelParticleSystem;
    public ParticleSystem BackrightWheelParticleSystem;

    public TrailRenderer FrontleftWheelTyremarks;
    public TrailRenderer FrontmiddleWheelTyremarks;
    public TrailRenderer FrontrightWheelTyremarks;
    public TrailRenderer BackleftWheelTyremarks;
    public TrailRenderer BackrightWheelTyremarks;

    private bool speedIncreased = false;
    private bool gravityIncreased = false;
    private bool WheelSpeedIncreased = false;
    private bool TurningSpeedIncreased = false;
    // Define minimum and maximum speed values
    public float minSpeed = 0f;
    public float maxSpeed = 10f;

    public int LowGearSpeed;
    public int HighGearSpeed;

    public Rigidbody Rb;

    public void Start()
    {
        Rigidbody Rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        GroundCheck();
        Move();
        Turn();
        ChangeGears();
        rb.AddForce(Vector3.down * Gravity * 100);
        Speed = Mathf.Clamp(Speed, minSpeed, maxSpeed);
        if(Rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = Vector3.ClampMagnitude(Rb.angularVelocity, maxSpeed);
        }

    }

    public void ChangeGears()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            maxSpeed = LowGearSpeed;
            Speed = LowGearSpeed;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            maxSpeed = HighGearSpeed;
            Speed = HighGearSpeed;
        }
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
            Speed -= 0.25f;
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

        if(TurningSpeedIncreased && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) 
        {
            TurningSpeedIncreased = false;
           // rotationSpeed -= 0.25f;
        }
        else if (!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.D))
        {
            TurningSpeedIncreased = true; 
            //rotationSpeed += 1f;
        }
    }
    public void GroundCheck()
    {
        FrontleftisGrounded = Physics.CheckSphere(FrontleftgroundCheck.position, checkRadius, groundLayer);
        FrontmiddleisGrounded = Physics.CheckSphere(FrontmiddlegroundCheck.position, checkRadius, groundLayer);
        FrontrightisGrounded = Physics.CheckSphere(FrontrightgroundCheck.position, checkRadius, groundLayer);
        BackleftisGrounded = Physics.CheckSphere(BackleftgroundCheck.position, checkRadius, groundLayer);
        BackrightisGrounded = Physics.CheckSphere(BackrightgroundCheck.position, checkRadius, groundLayer);

        if (!speedIncreased && (FrontleftisGrounded || FrontmiddleisGrounded || FrontrightisGrounded || BackleftisGrounded || BackrightisGrounded))
        {
            Speed += 5f;
            speedIncreased = true;
        }
        else if (!FrontleftisGrounded || !FrontmiddleisGrounded || !FrontrightisGrounded || !BackleftisGrounded || !BackrightisGrounded)
        {
            speedIncreased = false;
            Speed -= 0.75f;
        }

        if (!gravityIncreased && (FrontleftisGrounded || FrontmiddleisGrounded || FrontrightisGrounded || BackleftisGrounded || BackrightisGrounded))
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

        if (!WheelSpeedIncreased && (FrontleftisGrounded || FrontmiddleisGrounded || FrontrightisGrounded || BackleftisGrounded || BackrightisGrounded))
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

        if (Input.GetKey(KeyCode.W) && FrontleftisGrounded) { FrontleftWheelParticleSystem.Play(); FrontleftWheelTyremarks.emitting = true; }
        else { FrontleftWheelTyremarks.emitting = false; }

        if (Input.GetKey(KeyCode.W) && FrontmiddleisGrounded) { FrontmiddleWheelParticleSystem.Play(); FrontmiddleWheelTyremarks.emitting = true; }
        else { FrontmiddleWheelTyremarks.emitting = false; }

        if (Input.GetKey(KeyCode.W) && FrontrightisGrounded) { FrontrightWheelParticleSystem.Play(); FrontrightWheelTyremarks.emitting = true; }
        else { FrontrightWheelTyremarks.emitting = false; }

        if (Input.GetKey(KeyCode.W) && BackleftisGrounded) { BackleftWheelParticleSystem.Play(); BackleftWheelTyremarks.emitting = true; }
        else { BackleftWheelTyremarks.emitting = false; }

        if (Input.GetKey(KeyCode.W) && BackrightisGrounded) { BackrightWheelParticleSystem.Play(); BackrightWheelTyremarks.emitting = true; }
        else { BackrightWheelTyremarks.emitting = false; }

    }
    public void Turn()
    {
        if (isGrounded)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float steerAngle = horizontalInput * maxSteerAngle * rotationSpeed;
            Quaternion rotation = Quaternion.Euler(0f, steerAngle, 0f);
            rb.MoveRotation(rb.rotation * rotation);
        }
    }

    public void ForwardRotateWheels(float rotationAmount)
    {
        FrontleftWheel.transform.Rotate(Vector3.forward, rotationAmount);
        FrontmiddleWheel.transform.Rotate(Vector3.forward, rotationAmount);
        FrontrightWheel.transform.Rotate(Vector3.forward, rotationAmount);
        BackleftWheel.transform.Rotate(Vector3.forward, rotationAmount);
        BackrightWheel.transform.Rotate(Vector3.forward, rotationAmount);
    }

    public void BackRotateWheels(float rotationAmount)
    {
        FrontleftWheel.transform.Rotate(Vector3.back, rotationAmount);
        FrontmiddleWheel.transform.Rotate(Vector3.back, rotationAmount);
        FrontrightWheel.transform.Rotate(Vector3.back, rotationAmount);

        BackleftWheel.transform.Rotate(Vector3.forward, rotationAmount);
        BackrightWheel.transform.Rotate(Vector3.forward, rotationAmount);
    }
}