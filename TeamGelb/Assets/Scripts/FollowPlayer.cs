using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public PlayerControl player;
    public Vector3 Offset;
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        // for Camara
        transform.position = player.transform.position + Offset;
        transform.rotation = Quaternion.LookRotation(player.forceDirection);
    }
}
