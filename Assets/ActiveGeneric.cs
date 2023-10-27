using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGeneric : MonoBehaviour
{

    public GameObject Generic;
    public GameObject RestartPanel;
    public RectTransform Credits;
    public float genericSpeed;
    public GameObject ScoreText;
    public TMPro.TextMeshProUGUI scoreTextMesh;
    public float timeBeforeCrash;
    public BoxCollider2D boxCollider2D;


    float currentTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            Generic.SetActive(true);
            scoreTextMesh.text = timer.Instance.m_TextMeshPro.text;
            ScoreText.SetActive(false);
            StartCoroutine(DefileGeneric());
            boxCollider2D.enabled = false;
        }
    }

    IEnumerator DefileGeneric()
    {
        while (currentTime < timeBeforeCrash)
        {
            yield return new WaitForEndOfFrame();
            Credits.position += Vector3.up * (genericSpeed * Time.deltaTime);
            currentTime += Time.deltaTime;
        }
        RestartPanel.SetActive(true);
        Generic.SetActive(false);
    }
}
