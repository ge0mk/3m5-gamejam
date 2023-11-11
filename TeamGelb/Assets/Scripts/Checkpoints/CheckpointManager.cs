using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CheckpointManager : MonoBehaviour
{
    private List<Checkpoint> visitedCheckpoints;
    void Awake()
    {
        visitedCheckpoints = new List<Checkpoint>();
        gameObject.transform.GetChild(0).GetComponent<Checkpoint>().InformCheckpointManager();
    }

    public void PlayerThroughCheckpoint(Checkpoint checkpoint)
    {
        visitedCheckpoints.Add(checkpoint);
    }

    public void ResetPlayerToLastCheckpoint(PlayerControl player)
    {
        Transform lastCheckpointPosition = visitedCheckpoints.Last().GetComponent<Transform>();
        player.GetComponent<Transform>().position = lastCheckpointPosition.transform.position;
    }
}
