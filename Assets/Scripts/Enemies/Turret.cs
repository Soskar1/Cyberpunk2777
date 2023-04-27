using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : AIShooting
{
    [Header("Turret's settings")]
    public LayerMask whoIsEnemy;
    [SerializeField] private float viewDistance;
    [SerializeField] private bool facingRight;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private Vector2 direction;

    private void Awake()
    {
        if (facingRight)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
    }

    private void Update()
    {
        if (CheckForTarget())
        {
            Fire();
        }
    }

    bool CheckForTarget()
    {
        RaycastHit2D[] hitInfo = Physics2D.RaycastAll(shotPos[0].position, direction, viewDistance, whoIsEnemy);

        if (hitInfo != null)
        {
            for (int index = 0; index < hitInfo.Length; index++)
            {
                if (hitInfo[index].collider.gameObject.GetComponent<Health>() != null)
                {
                    animator.SetBool("Fire", true);
                    return true;
                }
            }
        }

        animator.SetBool("Fire", false);
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        if (facingRight)
        {
            Gizmos.DrawLine(shotPos[0].position, new Vector2(shotPos[0].position.x + viewDistance, shotPos[0].position.y));
        }
        else
        {
            Gizmos.DrawLine(shotPos[0].position, new Vector2(shotPos[0].position.x - viewDistance, shotPos[0].position.y));
        }
    }
}
