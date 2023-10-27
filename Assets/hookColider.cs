using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookColider : MonoBehaviour
{
    public LayerMask layerMask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FrontEnvironment")
        {
            print(collision.transform.position);
            PlayerController.Instance.HookTouch(collision.transform.position);
        }
    }
}
