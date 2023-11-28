using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : MonoBehaviour
{
    public float duration;
    public float newSpeedSet;
    public float newHookSpeed;
    public float minFOV;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            PowerupManager.Instance.ActiveSpeed(duration);
            PlayerController.Instance.forceRightOnHoldHook = newSpeedSet;
            PlayerController.Instance.minFOV = minFOV;
            PlayerController.Instance.hookSpeed = newHookSpeed;
            gameObject.SetActive(false);
        }
    }
}
