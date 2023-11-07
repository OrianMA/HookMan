using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour
{
    public float accelerateValue;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            float currentSpeed = PlayerController.Instance.rb.velocity.magnitude;
            float newSpeed = currentSpeed * accelerateValue;
            PlayerController.Instance.rb.velocity = PlayerController.Instance.rb.velocity.normalized * newSpeed;
        }
    }
}