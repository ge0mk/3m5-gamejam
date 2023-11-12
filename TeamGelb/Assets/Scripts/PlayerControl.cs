using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerControl : MonoBehaviour
{
    public WheelCollider wheelLeft, wheelRight;
    public Transform visualWheel;
    public CinemachineVirtualCamera cam;
    public InputSystem input;
    public float Speed;
    
    public float StepAngel;
    public float StepSpeed;
    
    public Vector3 forceDirection;
    public float Angel;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<InputSystem>();
        //Angel = transform.eulerAngles.y;
        forceDirection = transform.forward;
        
        var follow = new GameObject("CamFollower").AddComponent<FollowPlayer>();
        follow.player = this;
        cam.Follow = follow.transform;
        cam.LookAt = follow.transform;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //transform.rotation = Quaternion.Slerp(transform.rotation, rot, 0.02f);
        switch (input.GetHorizontalDelta())
        {
            case 1f:
                Angel += StepAngel;
                forceDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * Angel), 0f, Mathf.Cos(Mathf.Deg2Rad * Angel));
                //rb.rotation = Quaternion.LookRotation(forceDirection);
                break;
            case -1f:
                Angel -= StepAngel;
                forceDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * Angel), 0f, Mathf.Cos(Mathf.Deg2Rad * Angel));
                rb.rotation = Quaternion.LookRotation(forceDirection);
                break;
        
        }
        switch (input.GetForwardDelta())
        {
            case 1f:
                Speed += StepSpeed;
                break;
            case -1f:
                Speed -= StepSpeed;
                break;
        
        }
        //rb.AddForce(forceDirection * Speed, ForceMode.Force);
        wheelLeft.steerAngle = Angel;
        wheelLeft.motorTorque = Speed;
        
        wheelRight.steerAngle = Angel;
        wheelRight.motorTorque = Speed;
        ApplyLocalPositionToVisuals(wheelLeft);
    }
    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
     
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
}
