using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;

    private new Rigidbody2D rigidbody2D;
    private Vector2 movementInput = Vector2.zero;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.y = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        if (!GameManager.IsGameOver)
            rigidbody2D.velocity = movementInput * speed;
        else
            rigidbody2D.velocity = Vector2.zero;
    }
}
