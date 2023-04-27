using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Transform visual;
    [SerializeField] private bool facingRight;
    private float horizontalMove;

    [Header("Jump")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float jumpForce;

    [Header("Wall Running")]
    [HideInInspector] public bool wallRun = false;
    [SerializeField] private float wallRunSpeed;
    [SerializeField] private int originalGravity;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [HideInInspector] public bool isHacking = false;

    private void Awake()
    {
        //Отключаем коллизию между врагами и игроком, механизмами и игроком.
        Physics2D.IgnoreLayerCollision(8, 13);
        Physics2D.IgnoreLayerCollision(8, 12);
    }

    private void Update()
    {
        if (!isHacking)
        {
            if (!wallRun)
            {
                //Высчитываем горизонтальное передвижение персонажа, если тот находится на земле
                horizontalMove = Input.GetAxisRaw("Horizontal");

                //Проверка на прыжок
                if (Input.GetKeyDown(KeyCode.Space) && GroundCheck())
                {
                    Jump();
                }
            }
            else
            {
                ///Игрок может нажать всего лишь на 2 кнопки: прыжок и "вниз"
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    animator.SetBool("Wall Run", false);
                    wallRun = false;
                    Jump();
                }

                //Если нажимается кнопка вниз, то игрок просто выпадает со стены
                if (Input.GetKeyDown(KeyCode.S))
                {
                    animator.SetBool("Wall Run", false);
                    wallRun = false;
                }
            }

            //Обновляем параметр "Скорость" в аниматоре
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            //Поворот персонажа
            if (horizontalMove > 0 && !facingRight)
            {
                Flip();
            }
            else if (horizontalMove < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isHacking)
        {
            if (!wallRun)
            {
                if (rb2d.gravityScale == 0)
                {
                    rb2d.gravityScale = originalGravity;
                }

                //Передвижение персонажа по земле
                transform.Translate(new Vector2(horizontalMove * speed * Time.fixedDeltaTime, 0));
            }
            else
            {
                WallRun();
            }
        }
    }

    private void WallRun()
    {
        //Вырубаем гравитацию у перса
        if (rb2d.gravityScale == originalGravity)
        {
            rb2d.gravityScale = 0;
        }

        //Передвижение персонажа по стене
        rb2d.velocity = Vector2.right * horizontalMove * wallRunSpeed * Time.fixedDeltaTime;
    }

    private void Jump()
    {
        //Прыжок
        rb2d.AddForce(Vector2.up * jumpForce);
    }

    private bool GroundCheck()
    {
        //Проверка на нахождении персонажа на земле
        Collider2D hitInfo = Physics2D.OverlapCircle(groundCheck.position, 0.1f, whatIsGround);

        if (hitInfo != null)
        {
            return true;
        }

        return false;
    }

    private void Flip()
    {
        //Отзеркаливаем спрайт перса
        facingRight = !facingRight;
        
        Vector3 theScale = visual.localScale;
        theScale.x *= -1;
        visual.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Boost Wall"))
        {
            if (!GroundCheck() && horizontalMove != 0)
            {
                animator.SetBool("Wall Run", true);
                wallRun = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Boost Wall"))
        {
            animator.SetBool("Wall Run", false);
            wallRun = false;
        }
    }
}
