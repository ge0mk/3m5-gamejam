using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerControl : MonoBehaviour
{
    public float Speed;
    
    public Vector3 forceDirection;
    public float StepAngel;
    public KeyCode left, Right, Up, Down;

    public float angel;
    public float StepTime;
    
    private Rigidbody rb;
    private float timeLeft;
    private bool hasClicked;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        timeLeft += Time.deltaTime;
        if (timeLeft > StepTime)
        {
            if (hasClicked)
            {
                timeLeft = 0f;
                hasClicked = false;
                return;
            }
            if (Input.GetKey(left))
            {
                angel -= StepAngel;
                forceDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angel), 0f, Mathf.Cos(Mathf.Deg2Rad * angel));
                hasClicked = true;
            }
            if (Input.GetKey(Right))
            { 
                angel += StepAngel;
                forceDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angel), 0f, Mathf.Cos(Mathf.Deg2Rad * angel));
                hasClicked = true;
            }
        }
 
    }
}
