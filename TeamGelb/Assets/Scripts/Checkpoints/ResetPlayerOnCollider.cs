using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerOnCollider : MonoBehaviour
{
    public CheckpointManager checkpointManager;

    private void OnCollisionEnter(Collision collision)
    {
        // only do something when player collides
        if (collision.collider.TryGetComponent<PlayerControl>(out PlayerControl player))
        {
            checkpointManager.ResetPlayerToLastCheckpoint(player);
        }
    }
}
