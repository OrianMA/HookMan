using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoSingleton<PowerupManager>
{
    public List<GameObject> powerUpsInScene;

    public void reAddPowerUp()
    {
        if (powerUpsInScene.Count == 0)
            return;

        foreach(GameObject powerup in powerUpsInScene)
        {
            powerup.SetActive(true);
        }
    }
    public void ActiveSpeed(float duration)
    {
        StartCoroutine(WaitAndDisableSpeedPowerUp(duration));
    }
    IEnumerator WaitAndDisableSpeedPowerUp(float duration)
    {
        yield return new WaitForSeconds(duration);
        PlayerController.Instance.ResetHookStats();
    }

    public void StopAll()
    {
        StopAllCoroutines();
    }

}
