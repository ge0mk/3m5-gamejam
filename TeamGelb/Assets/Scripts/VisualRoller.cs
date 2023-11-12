using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualRoller : MonoBehaviour
{
    public PlayerControl control;
    public Vector3 Euler;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(control.Speed * Euler);
    }
}
