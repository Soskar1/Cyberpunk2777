using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    [SerializeField] private float maxHealth;

    private void Awake()
    {
        health = maxHealth;   
    }

    public void GetDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        health += healAmount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
