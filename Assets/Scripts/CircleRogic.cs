using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CircleRogic : MonoBehaviour
{

    Rigidbody2D rigid;
    [SerializeField] private int speed = 3;
    [SerializeField] private GameObject circle;

    private void OnEnable()
    {
        rigid = circle.GetComponent<Rigidbody2D>();
    }



    private void SpawnCircle(Vector2 spawnPos, Vector2 spawnDir)
    {
        Instantiate(circle, spawnPos, Quaternion.identity);
        rigid.velocity = spawnDir * speed;
    }
}




