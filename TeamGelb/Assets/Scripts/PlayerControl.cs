using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerControl : MonoBehaviour
{
    public InputSystem input;
    public float Speed;
    
    public float StepAngel;
    public float StepSpeed;
    
    private Vector3 forceDirection;
    public float Angel;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<InputSystem>();
        Angel = transform.eulerAngles.y;
        forceDirection = transform.forward;
    }
    public void ResetAngel(float YAxe)
    {
        Angel = YAxe;
        forceDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * Angel), 0f, Mathf.Cos(Mathf.Deg2Rad * Angel));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(forceDirection * Speed, ForceMode.Force);
        transform.rotation = Quaternion.LookRotation(forceDirection);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rot, 0.02f);
    }
    private void Update()
    {
        switch (input.GetHorizontalDelta())
        {
            case 1f:
                Angel += StepAngel;
                forceDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * Angel), 0f, Mathf.Cos(Mathf.Deg2Rad * Angel));
                break;
            case -1f:
                Angel -= StepAngel;
                forceDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * Angel), 0f, Mathf.Cos(Mathf.Deg2Rad * Angel));
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
    }
}
