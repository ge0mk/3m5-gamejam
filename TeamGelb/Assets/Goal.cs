using System;
using System.Collections;
using System.Collections.Generic;
using Gianni.Helper;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Collider col;
    public CheeseSpown Spawn;
    public Menue menue;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            menue.Goal(other.transform);
            this.InvokeWaitLoop(0.001f, Spawn.SpownObject);
            this.InvokeWaitLoop(0.001f, Spawn.SpownObject);
            this.InvokeWaitLoop(0.001f, Spawn.SpownObject);
            this.InvokeWaitLoop(0.001f, Spawn.SpownObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
