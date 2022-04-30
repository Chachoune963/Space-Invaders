using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Finish")
        {
            Destroy(hit.gameObject);
            Destroy(gameObject);
        }
    }
}
