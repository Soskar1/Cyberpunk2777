using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : MonoBehaviour
{
    [Header("Settings")]
    public List<Transform> shotPos;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float spread;

    [Header("Time")]
    [SerializeField] private float timeBtwShots;
    [SerializeField] private float startTimeBtwShots;

    private void Awake()
    {
        timeBtwShots = startTimeBtwShots;
    }

    public void Fire()
    {
        if (timeBtwShots <= 0)
        {
            if (shotPos.Count == 0)
            {
                GameObject bulletInstance = Instantiate(bullet, shotPos[0].position, transform.rotation);
                bulletInstance.transform.Rotate(0, 0, Random.Range(-spread, spread));
            }
            else
            {
                for (int index = 0; index < shotPos.Count; index++)
                {
                    GameObject bulletInstance = Instantiate(bullet, shotPos[index].position, transform.rotation);
                    bulletInstance.transform.Rotate(0, 0, Random.Range(-spread, spread));
                }
            }

            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
