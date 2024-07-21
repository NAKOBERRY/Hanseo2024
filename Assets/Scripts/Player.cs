using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer spri;


    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private int playerHp = 6;

    [SerializeField] private Vector2 inputVec;
    [SerializeField] private Vector3 bottomOffset;
    [SerializeField] private Vector2 overlabBoxSize;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;
    private bool canReverseGravity = true;
    private bool gravityReversed = false;
    private float groundTimeCounter = 2f;
    [SerializeField] private float groundTimeThreshold = 2f;
    private float groggyTime;
    private float invincibleTime;



    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spri = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Q) && canReverseGravity && isGrounded && groundTimeCounter >= groundTimeThreshold)
        {
            ReverseGravity();
        }

        FlipSprite();
        invicibleTIme();
    }

    private void FixedUpdate()
    {
        PlayerMove();
        isGrounded = Physics2D.OverlapBox(transform.position + bottomOffset, overlabBoxSize, 0, groundLayer);

        if (isGrounded)
        {
            groundTimeCounter += Time.fixedDeltaTime;
        }
        else
        {
            groundTimeCounter = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && invincibleTime <= 0)
        {
            Ondamage(collision.transform);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + bottomOffset, overlabBoxSize);
    }

    private void Jump()
    {
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private void PlayerMove()
    {
        if (groggyTime <= 0)
            transform.Translate(Vector2.right * inputVec.x * speed * Time.fixedDeltaTime);
        else
            groggyTime -= Time.fixedDeltaTime;
    }


    private void ReverseGravity()
    {
        gravityReversed = !gravityReversed;
        rigid.gravityScale *= -1;
        groundTimeCounter = 0f;
        canReverseGravity = false;
        StartCoroutine(GravityCooldown());
    }

    private IEnumerator GravityCooldown()
    {
        yield return new WaitForSeconds(2f);
        canReverseGravity = true;
    }

    private void Ondamage(Transform target)
    {
        rigid.velocity = new Vector2(-4 * (target.position.x > transform.position.x ? 1 : -1), 10);
        playerHp--;
        groggyTime = 0.5f;
        invincibleTime = 1f;
    }

    private void FlipSprite()
    {
        if(inputVec.x < 0)
        {
            spri.flipX = true;
        }
        else if(inputVec.x > 0) 
        {
            spri.flipX= false;
        }
    }

    private void invicibleTIme()
    {
        if (invincibleTime <= 0)
        {
            spri.color = new Color(1, 1, 1, 1);
        }
        else
        {
            spri.color = new Color(1, 1, 1, 0.5f);
            invincibleTime -= Time.deltaTime;
        }
    }
}
