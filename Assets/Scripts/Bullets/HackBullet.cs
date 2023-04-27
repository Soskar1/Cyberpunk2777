using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackBullet : Bullet
{
    [SerializeField] private LayerMask whatToHack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((whatToHack & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            //Начинается хак
            collision.GetComponent<Hack>().Hacking();
        }
    }
}
