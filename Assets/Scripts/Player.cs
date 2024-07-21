using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;

    [SerializeField] private Vector2 inputVec;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private Vector3 bottomOffset;
    [SerializeField] private Vector2 overlabBoxSize;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;
    private bool canReverseGravity = true;
    private bool gravityReversed = false;
    private float groundTimeCounter = 0f;
    [SerializeField] private float groundTimeThreshold = 2f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            JumpS();
        }

        if (Input.GetKeyDown(KeyCode.Q) && canReverseGravity && isGrounded && groundTimeCounter >= groundTimeThreshold)
        {
            ReverseGravity();
        }
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
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + bottomOffset, overlabBoxSize);
    }

    private void JumpS()
    {
        Vector3 jumpDirection = gravityReversed ? Vector3.down : Vector3.up;  
        transform.Translate(jumpDirection * jumpPower * Time.fixedDeltaTime); 
    }
    /*private void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(Vector2.up * (gravityReversed ? -jumpPower : jumpPower), ForceMode2D.Impulse);
    }*/

    private void PlayerMove()
    {
        var newInputVec = inputVec.normalized * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + newInputVec);
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
}
