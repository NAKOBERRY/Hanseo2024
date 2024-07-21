using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;

    public Vector2 inputVec;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        PlayerMove();
        Jump();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

    }

    private void PlayerMove()
    {
        var newInputVec = inputVec.normalized * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + newInputVec);
    }
}
