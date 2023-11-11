using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerControl : MonoBehaviour
{
    public InputSystem input;
    public float Speed;
    
    public Vector3 forceDirection;
    public float StepAngel;
    public float StepSpeed;
    private float angel;
    
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<InputSystem>();
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
        angel += StepAngel * input.GetHorizontalDelta();
        forceDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angel), 0f, Mathf.Cos(Mathf.Deg2Rad * angel));
        Speed += StepSpeed * input.GetForwardDelta();
    }
}
