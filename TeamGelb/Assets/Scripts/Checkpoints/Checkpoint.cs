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
            InformCheckpointManager();
        }
    }

    public void InformCheckpointManager()
    {
        checkpointManager.PlayerThroughCheckpoint(this);
        GetComponent<Collider>().enabled = false; // do not trigger again
    }
}
