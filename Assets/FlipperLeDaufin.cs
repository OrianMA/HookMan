using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class FlipperLeDaufin : MonoBehaviour
{
    public bool isActiveFlipper;
    public TMPro.TextMeshProUGUI ScoreText;
    public int ScoreTextMinToDisplay;

    public float scaleIncrease, durationScale,beginScale,maxScale;

    public float colorIncrease, maxScoreToRed;
    public Ease ease;

    public bool stopCounting;
    int score;
    PlayerController playerController;
    bool isFirsTime;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerController = collision.collider.GetComponent<PlayerController>();
        if (playerController && isActiveFlipper && !playerController.isObstacleDetect)
        {
            if (score >= ScoreTextMinToDisplay)
            {
                ScoreText.gameObject.SetActive(true);
            }
            score++;
            ScoreText.text = score.ToString();

            if (!isFirsTime)
            {
                isFirsTime = true;
                StartCoroutine(DetectPlayerHooking());
                ScoreText.transform.DOScale(beginScale, durationScale).SetEase(ease);
                ScoreText.color = Color.white;
            } else
            {
                if (ScoreText.transform.localScale.x < maxScale)
                {
                    ScoreText.transform.DOScale(ScoreText.transform.localScale.x + scaleIncrease, durationScale).SetEase(ease);
                    if (score > maxScoreToRed)
                    {
                        ScoreText.color = Color.red;
                    } else
                    {
                        colorIncrease = score / maxScoreToRed;
                        ScoreText.color = Color.Lerp(Color.white, Color.red, colorIncrease);
                    }
                    //ScoreText.color =  new Color(ScoreText.color.r + 0.001f,0,0);
                }
            }
        } else
        {
            score = 0;
        }
    }

    IEnumerator DetectPlayerHooking()
    {
        while (!PlayerController.Instance.isObstacleDetect && !stopCounting)
        {
            yield return new WaitForEndOfFrame();
        }
        ScoreText.transform.DOScale(0, durationScale*3)
            .OnComplete( () => {
            ScoreText.gameObject.SetActive(false);
        });
        ScoreText.DOColor(Color.white, durationScale * 3);
        isFirsTime = false;
        score = 0;
        stopCounting = false;
    }
}
