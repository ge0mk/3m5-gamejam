using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    public Vector3 Offset;
    public Transform TrailRenderPrefap;
    private Transform TrailRender;
    void Start()
    {
        TrailRender = Instantiate(TrailRenderPrefap, transform.position, TrailRenderPrefap.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down,  out hit))
        {
            TrailRender.position = hit.point + Offset;
        }
    }
}
