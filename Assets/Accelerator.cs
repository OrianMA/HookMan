using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour
{
    public float accelerateValue;

    bool isCollide;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            isCollide = true;
            //StartCoroutine(AcceleratePlayer());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            float currentSpeed = PlayerController.Instance.rb.velocity.magnitude;
            float newSpeed = currentSpeed * accelerateValue;
            PlayerController.Instance.rb.velocity = PlayerController.Instance.rb.velocity.normalized * newSpeed;
            print("Voooolll");
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            isCollide = false;
        }
    }
}
