using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    SpriteRenderer spriteRend;
    //public Transform firePoint;

    void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        AimArmAtMouse();
    }

    void AimArmAtMouse()
    {
        Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 armToMouse = mousePosition - (Vector2)transform.position;
        float rotationZ = Vector2.SignedAngle(transform.right, armToMouse);
        transform.Rotate(0f, 0f, rotationZ);
        FlipArm(Vector2.SignedAngle(transform.right, Vector2.right));
    }

    void FlipArm(float rotation)
    {
        if (rotation < -90f || rotation > 90f)
        {
            spriteRend.flipY = true;
            //FlipFirePoint(true);
        }
        else
        {
            spriteRend.flipY = false;
            //FlipFirePoint(false);
        }
    }

    // void FlipFirePoint(bool flip)
    // {
    //     var pos = firePoint.localPosition;
    //     pos.y = Mathf.Abs(pos.y) * (flip ? -1 : 1);
    //     firePoint.localPosition = pos;
    // }
}