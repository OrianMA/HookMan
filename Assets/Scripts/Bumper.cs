using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float bumperForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            print("Playerrrr");
            PlayerController.Instance.rb.velocity = Vector2.up * bumperForce + Vector2.right * PlayerController.Instance.rb.velocity.x;
        }
    }
}
