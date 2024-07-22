using System.Collections;
using UnityEngine;

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
    private bool isReversingGravity = false; 
    private float groundContactTime = 0f;
    [SerializeField] private float groundTimeThreshold = 2f;
    private float groggyTime;
    private float invincibleTime;

    [SerializeField] private float rotationDuration = 1f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spri = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");

        if (!isReversingGravity)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Q) && canReverseGravity && Time.time - groundContactTime >= groundTimeThreshold)
        {
            ReverseGravity();
        }

        FlipSprite();
        invicibleTIme();
    }

    private void FixedUpdate()
    {
        if (!isReversingGravity)
        {
            PlayerMove();
        }

        CheckGrounded();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && invincibleTime <= 0)
        {
            Ondamage(collision.transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + bottomOffset, overlabBoxSize);
    }

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            Vector2 jumpDirection = gravityReversed ? Vector2.down : Vector2.up;
            rigid.AddForce(jumpDirection * jumpPower, ForceMode2D.Impulse);
        }
    }

    private void PlayerMove()
    {
        if (groggyTime <= 0)
        {
            transform.Translate(Vector2.right * inputVec.x * speed * Time.fixedDeltaTime);
        }
        else
        {
            groggyTime -= Time.fixedDeltaTime;
        }
    }

    private void ReverseGravity()
    {
        gravityReversed = !gravityReversed;
        rigid.gravityScale *= -1;
        canReverseGravity = false;
        isReversingGravity = true;

        StartCoroutine(RotatePlayerOverTime(180f, rotationDuration));
        StartCoroutine(GravityCooldown());

  
        bottomOffset *= -1;
    }

    private IEnumerator RotatePlayerOverTime(float angle, float duration)
    {
        float startRotation = transform.eulerAngles.z;
        float endRotation = startRotation + angle;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float zRotation = Mathf.Lerp(startRotation, endRotation, elapsed / duration);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, endRotation);
        isReversingGravity = false; 
    }

    private IEnumerator GravityCooldown()
    {
        yield return new WaitForSeconds(2f);
        canReverseGravity = true;
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapBox(transform.position + bottomOffset, overlabBoxSize, 0, groundLayer);
        if (isGrounded && groundContactTime == 0f)
        {
            groundContactTime = Time.time;
        }
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
        if (inputVec.x < 0)
        {
            spri.flipX = true;
        }
        else if (inputVec.x > 0)
        {
            spri.flipX = false;
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
