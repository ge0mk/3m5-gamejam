using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    public GameObject rat;
    public float spawnAreaSize;
    public int numRats;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numRats; i++) {
            var position = Random.insideUnitCircle * spawnAreaSize;
            var rotation = new Vector3(0f, Random.Range(0f, 360f), 0f);
            var instance = Instantiate(rat, new Vector3(position.x, 0, position.y), Quaternion.Euler(rotation));
        }
    }

    // Update is called once per frame
    void Update() {}
}
