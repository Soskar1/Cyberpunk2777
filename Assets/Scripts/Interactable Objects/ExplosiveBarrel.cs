using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionDamage;
    [SerializeField] private float explosionForce;
    [SerializeField] private LayerMask whatToExplode;

    public void Explode()
    {
        //Вырубаем коллайдер, чтобы не создавался бесконечный взрыв
        GetComponent<Collider2D>().enabled = false;

        //Спавним эффект взрыва
        Instantiate(explosion, transform.position, Quaternion.identity);

        //Создаём "опасную зону"
        Collider2D[] coll = Physics2D.OverlapCircleAll(transform.position, explosionRadius, whatToExplode);
        for (int index = 0; index < coll.Length; index++)
        {
            if (coll[index].GetComponent<ExplosiveBarrel>() != null)
            {
                //Взрываем рядом стоящую бочку
                coll[index].GetComponent<ExplosiveBarrel>().Explode();
            }

            if (coll[index].GetComponent<Health>() != null)
            {
                //Наносим урон существу
                coll[index].GetComponent<Health>().GetDamage(explosionDamage);
                
                //Высчитываем направление, в котором будем отталкивать существо
                Vector2 direction = coll[index].gameObject.GetComponent<Transform>().position - transform.position;

                //Отталкиваем существо
                coll[index].gameObject.GetComponent<Rigidbody2D>().AddForce(direction * explosionForce);
            }
        }

        //Уничтожаем бочку
        Destroy(gameObject);
    }

    //Рисуем радиус поражения бочки
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        float theta = 0;
        float x = explosionRadius * Mathf.Cos(theta);
        float y = explosionRadius * Mathf.Sin(theta);
        Vector3 pos = transform.position + new Vector3(x, y, 0);
        Vector3 newPos = pos;
        Vector3 lastPos = pos;
        for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
        {
            x = explosionRadius * Mathf.Cos(theta);
            y = explosionRadius * Mathf.Sin(theta);
            newPos = transform.position + new Vector3(x, y, 0);
            Gizmos.DrawLine(pos, newPos);
            pos = newPos;
        }
        Gizmos.DrawLine(pos, lastPos);
    }
}
