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
        isGrounded = Physics2D.OverlapBox(transform.position + bottomOffset, overlabBoxSize, 0, groundLayer);
    }

    private void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private void PlayerMove()
    {
        var newInputVec = inputVec.normalized * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + newInputVec);
    }  
}
