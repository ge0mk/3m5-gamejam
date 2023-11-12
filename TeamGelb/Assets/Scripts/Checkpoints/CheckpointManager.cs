using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CheckpointManager : MonoBehaviour
{
    public List<PlayerControl> players;
    private Dictionary<PlayerControl, List<Checkpoint>> visitedCheckpointsPerPlayer;

    void Awake()
    {
        visitedCheckpointsPerPlayer = new Dictionary<PlayerControl, List<Checkpoint>>();
        players.ForEach(player =>
        {
            List<Checkpoint> checkpointsList = new List<Checkpoint>();
            visitedCheckpointsPerPlayer.Add(player, checkpointsList);
            gameObject.transform.GetChild(0).GetComponent<Checkpoint>().InformCheckpointManager(player);
        });
    }

    private void Update()
    {
        players.ForEach(player =>
        {
            if (player.transform.position.y < -4)
            {
                ResetPlayerToLastCheckpoint(player);
            }
        });
    }

    public void PlayerThroughCheckpoint(Checkpoint checkpoint, PlayerControl player)
    {
        List<Checkpoint> checkpointsList = visitedCheckpointsPerPlayer[player];
        if (!checkpointsList.Contains(checkpoint))
        {
            checkpointsList.Add(checkpoint);
        }
    }

    public void ResetPlayerToLastCheckpoint(PlayerControl player)
    {
        List<Checkpoint> checkpointsList = visitedCheckpointsPerPlayer[player];
        Transform lastCheckpointPosition = checkpointsList.Last().GetComponent<Transform>();
        var rb = player.GetComponent<Rigidbody>();
        player.ResetAngle(lastCheckpointPosition.transform.eulerAngles.y);
        rb.velocity = lastCheckpointPosition.transform.forward * player.GetSpeed();
        rb.position = lastCheckpointPosition.transform.position;
        rb.rotation = lastCheckpointPosition.transform.rotation;
    }
}
