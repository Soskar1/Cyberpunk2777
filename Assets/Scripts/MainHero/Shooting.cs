using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Weapon's ID")]
    public int weaponID;

    [Header("Bullet's options")]
    [SerializeField] private Movement movement;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float spread;

    [Header("Delay")]
    public float startTimeBtwShots;
    [HideInInspector] public float timeBtwShots;

    void Update()
    {
        if (!movement.isHacking)
        {
            if (timeBtwShots <= 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Fire();
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    private void Fire()
    {
        GameObject bulletInstance = Instantiate(bullet, shootPoint.position, transform.rotation);
        bulletInstance.transform.Rotate(0, 0, Random.Range(-spread, spread));

        timeBtwShots = startTimeBtwShots;
    }
}
