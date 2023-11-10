using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoSingleton<PowerupManager>
{
    public List<GameObject> powerUpsInScene;
    int _coins;

    public void reAddPowerUp()
    {
        if (powerUpsInScene.Count == 0)
            return;

        foreach(GameObject powerup in powerUpsInScene)
        {
            powerup.SetActive(true);
        }

        _coins = 0;
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

    public void AddCoin()
    {
        _coins++;
        UiManager.Instance._gameView.ActiveCoin();
    }

    public void StopAll()
    {
        StopAllCoroutines();
    }

}
