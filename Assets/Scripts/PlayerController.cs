using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Transform movePoint;
    private void Start()
    {
        movePoint.parent = null;
    }
    private void Update()
    {
        transform.position=Vector3.MoveTowards(transform.position, movePoint.position, speed*Time.deltaTime);

        if(Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                movePoint.position += new Vector3(0f,Input.GetAxisRaw("Vertical"), 0f);
            }
        }
    }
}
