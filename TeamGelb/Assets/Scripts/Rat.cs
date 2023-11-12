using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    public GameObject c;

    public float speed = 5;
    public float directionChangeInterval = 1;
    public float maxHeadingChange = 45;

    CharacterController controller;
    float heading;
    float timer = 0.0f;
    Vector3 targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        a.SetActive(false);
        b.SetActive(false);
        c.SetActive(false);

        switch (Random.Range(0, 4)) {
            case 0: a.SetActive(true); break;
            case 1: b.SetActive(true); break;
            case 2: c.SetActive(true); break;
        }

        controller = GetComponent<CharacterController>();

        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);
    }

    void Update ()
    {
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
        var forward = transform.TransformDirection(Vector3.forward);
        controller.SimpleMove(forward * speed);

        timer += Time.deltaTime;
        if (timer > directionChangeInterval) {
            timer = 0.0f;
            NewHeadingRoutine();
        }
    }

    void NewHeadingRoutine ()
    {
        var dir = transform.eulerAngles.y;
        var floor = dir - maxHeadingChange;
        var ceil  = dir + maxHeadingChange;
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }
}
