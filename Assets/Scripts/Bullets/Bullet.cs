using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;

    void Awake()
    {
        Invoke("LifeTime", lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void LifeTime()
    {
        Destroy(gameObject);
    }

    //void OnTriggerEnter2D(Collider2D coll)
    //{
    //    if (coll.GetComponent<ExplosiveBarrel>() != null) //Пуля касается взрывчатой бочки
    //    {
    //        coll.GetComponent<ExplosiveBarrel>().Explode();
    //        Destroy(gameObject);
    //    }
    //}
}
