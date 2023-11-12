using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CheckpointManager : MonoBehaviour
{
    public PlayerControl player;
    private List<Checkpoint> visitedCheckpoints;
    void Awake()
    {
        visitedCheckpoints = new List<Checkpoint>();
        gameObject.transform.GetChild(0).GetComponent<Checkpoint>().InformCheckpointManager();
    }

    private void Update() {
        if (player.transform.position.y < -4) {
            ResetPlayerToLastCheckpoint(player);
        }
    }

    public void PlayerThroughCheckpoint(Checkpoint checkpoint)
    {
        visitedCheckpoints.Add(checkpoint);
    }

    public void ResetPlayerToLastCheckpoint(PlayerControl player)
    {
        Transform lastCheckpointPosition = visitedCheckpoints.Last().GetComponent<Transform>();
        var rb = player.GetComponent<Rigidbody>();
        rb.velocity = lastCheckpointPosition.transform.forward * player.Speed;
        rb.position= lastCheckpointPosition.transform.position;
        rb.rotation = lastCheckpointPosition.transform.rotation;
    }
}
