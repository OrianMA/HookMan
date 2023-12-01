using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : MonoBehaviour
{
    public float duration;
    public float newSpeedSet;
    public float newHookSpeed;
    public float fov;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            PowerupManager.Instance.ActiveSpeed(duration);
            PlayerController.Instance.forceRightOnHoldHook = newSpeedSet;
            PlayerController.Instance.virtualCamera.m_Lens.FieldOfView = fov;
            PlayerController.Instance.isNoMoveCam = true;
            PlayerController.Instance.hookSpeed = newHookSpeed;
            PlayerController.Instance.isActivePowerUp = true;
            gameObject.SetActive(false);
        }
    }
}
