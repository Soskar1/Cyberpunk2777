using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saver : MonoBehaviour
{
    [SerializeField] private Transform spawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Movement>() != null)
        {
            collision.gameObject.transform.position = spawn.position;
        }
    }
}
