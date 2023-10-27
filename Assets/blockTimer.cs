using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockTimer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            timer.Instance.isBlockTimer = true;
        }
    }
}
