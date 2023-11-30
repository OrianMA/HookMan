using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform checkpointPos;
    public bool isConserveState;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            PlayerController.Instance.checkPointPosition = checkpointPos.transform.position;
            PlayerController.Instance.isFirstCheckpoint = false;
            PlayerController.Instance.isConcerveStateAtDeath = isConserveState;
        }
    }

}
