using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerControl : MonoBehaviour
{
    public float startSpeed;
    public float minSpeed;
    public float acceleration;

    public float stepAngle;

    private Rigidbody rb;
    private InputSystem input;

    private float speed;
    private float angle;
    private Vector3 forceDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<InputSystem>();

        speed = startSpeed;
        angle = transform.eulerAngles.y;
        forceDirection = transform.forward;
    }

    public void ResetAngle(float newAngle)
    {
        angle = newAngle;
        forceDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0f, Mathf.Cos(Mathf.Deg2Rad * angle));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(forceDirection * speed, ForceMode.Force);
        transform.rotation = Quaternion.LookRotation(forceDirection);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rot, 0.02f);
    }

    private void Update()
    {
        switch (input.GetHorizontalDelta())
        {
            case 1f:
                angle += stepAngle;
                forceDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0f, Mathf.Cos(Mathf.Deg2Rad * angle));
                break;
            case -1f:
                angle -= stepAngle;
                forceDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0f, Mathf.Cos(Mathf.Deg2Rad * angle));
                break;
        }

        float forwardDelta = input.GetForwardDelta();
        if (forwardDelta != 0f) {
            speed += acceleration * forwardDelta;
        }

        if (speed < minSpeed) {
            speed = minSpeed;
        }
    }

    public float GetSpeed()
    {
        return speed;
    }
}
