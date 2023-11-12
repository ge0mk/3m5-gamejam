using System.Collections;
using System.Collections.Generic;
using Gianni.Helper;
using UnityEngine;
using UnityEngine.Serialization;

public class CheeseSpown : MonoBehaviour
{
    [FormerlySerializedAs("SpownObj")]
    public GameObject SpawnObj;
    public float Range;
    public float force;

    public float Rate;
    private float left;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        left += Time.deltaTime;
        if (left > Rate)
        {
            left -= Rate;
            SpownObject();
        }
        
    }
    public void SpownObject()
    {

        var up = new Vector3(Random.Range(-Range, Range), 1f, Random.Range(-Range, Range));
        var rotation = Quaternion.LookRotation(up);
        var obj = Instantiate(SpawnObj, transform.position, rotation, transform);
        obj.layer = 20;
        var rb = obj.AddComponent<Rigidbody>();
        rb.velocity = up * force;
        rb.mass = 0.2f;
        this.InvokeWait(13f, () => Destroy(obj.gameObject));
    }
}
