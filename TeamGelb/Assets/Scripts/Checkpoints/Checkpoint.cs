using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public CheckpointManager checkpointManager;

    private void OnTriggerEnter(Collider collider)
    {
        // only do something when player collides
        if (collider.TryGetComponent<PlayerControl>(out PlayerControl player))
        {
            InformCheckpointManager(player);
        }
    }

    public void InformCheckpointManager(PlayerControl player)
    {
        checkpointManager.PlayerThroughCheckpoint(this, player);
    }
}
