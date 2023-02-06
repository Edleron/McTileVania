using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    // Todo deprem gerçeği
    [SerializeField] float moveSpeed = 3.0f;
    Rigidbody2D myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.name);
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    private void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }
}
