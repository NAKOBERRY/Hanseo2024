using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRogic : MonoBehaviour
{

    Rigidbody2D rigid;

    private void OnEnable()
    {
        rigid = GetComponent<Rigidbody2D>();
    }



    private void SpawnCircle(Vector2 spawnPos)
    {

    }
}
