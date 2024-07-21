using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Vector2 inputVec;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 10f;
    private bool isGrounded;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private void PlayerMove()
    {
        var newInputVec = inputVec * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + newInputVec);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}