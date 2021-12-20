using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentBreakForce;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    public Rigidbody rb;
    public float backForce;
    
    [SerializeField] private WheelCollider frontleftWheelCollider;
    [SerializeField] private WheelCollider frontrightWheelCollider;
    [SerializeField] private WheelCollider rearleftWheelCollider;
    [SerializeField] private WheelCollider rearrightWheelCollider;

    [SerializeField] private Transform frontleftWheelTransform;
    [SerializeField] private Transform frontrightWheelTransform;
    [SerializeField] private Transform rearleftWheelTransform;
    [SerializeField] private Transform rearrightWheelTransform;

    private void FixedUpdate()
    {
        if(rb.position.y < -1f)
        {
          FindObjectOfType<GameManager>().EndGame();
        }
        if (Input.GetKey("f"))
        {
            rb.AddForce(0, 0, -backForce * Time.deltaTime);
        }
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontleftWheelCollider.motorTorque = verticalInput * motorForce;
        frontrightWheelCollider.motorTorque = verticalInput * motorForce;
        currentBreakForce = isBreaking ? breakForce : 0f;

        if (isBreaking)
        {
            ApplyBreaking();
        }
    }

    private void ApplyBreaking()
    {
        frontleftWheelCollider.brakeTorque = currentBreakForce;
        frontrightWheelCollider.brakeTorque = currentBreakForce;
        rearleftWheelCollider.brakeTorque = currentBreakForce;
        rearrightWheelCollider.brakeTorque = currentBreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontleftWheelCollider.steerAngle = currentSteerAngle;
        frontrightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontrightWheelCollider, frontrightWheelTransform);
        UpdateSingleWheel(frontleftWheelCollider, frontleftWheelTransform);
        UpdateSingleWheel(rearleftWheelCollider, rearleftWheelTransform);
        UpdateSingleWheel(rearrightWheelCollider, rearrightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }


}
