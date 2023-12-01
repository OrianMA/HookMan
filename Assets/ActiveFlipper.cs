using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFlipper : MonoBehaviour
{
    public FlipperLeDaufin flipperLeDaufin;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            flipperLeDaufin.isActiveFlipper = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            flipperLeDaufin.isActiveFlipper = false;
            flipperLeDaufin.stopCounting = true;
        }
    }
}
