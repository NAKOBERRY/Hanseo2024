using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public Transform player;
    public LayerMask groundLayer;

    public float speed;
    public float jumpPower;

    private bool isGround;
    private bool canJump;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //땅 감지
        isGround = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        //방향
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        //플레이어가 적 위에 있나
        bool isPlayerAbove=Physics2D.Raycast(transform.position,Vector2.up,3f,1<<player.gameObject.layer);
        if (isGround)
        {
            //이동
            rigid.velocity = new Vector2(direction * speed ,rigid.velocity.y);

            //앞 벽 감지
            RaycastHit2D groundInFront = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 2f, groundLayer);
            //앞 땅 감지
            RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(direction, 0, 0), Vector2.down, 2f, groundLayer);
            //위에 천장 있는가
            RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, 3f, groundLayer);

            //앞에 벽이 없고 구멍이 있으면 점프
            if (!groundInFront.collider && !gapAhead.collider)
            {
                canJump = true;
            }
            else if (isPlayerAbove && platformAbove.collider)
            {
                canJump = true;
            }
            else if (groundInFront.collider)
            {
                canJump = true;
            }
        }
        //좌우반전
        if (direction >= 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
        //점프 애니메이션
        if (isGround == true)
        {
            animator.SetBool("IsWalk", false);
        }
        else
        {
            animator.SetBool("IsWalk", true);
        }
    }
    private void FixedUpdate()
    {
        if (isGround && canJump)
        {
            canJump = false;
            Vector2 direction=(player.position-transform.position).normalized;

            Vector2 jumpDirection = direction * jumpPower;
            rigid.AddForce(new Vector2(jumpDirection.x, jumpPower), ForceMode2D.Impulse);
        }
    }
}
