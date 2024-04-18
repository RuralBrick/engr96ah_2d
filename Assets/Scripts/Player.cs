using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpPower;

    SpriteRenderer spriteRenderer;
    new Rigidbody2D rigidbody;

    Vector2 move;
    uint floorsTouched = 0;
    bool Grounded
    {
        get => floorsTouched > 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move != Vector2.zero)
            spriteRenderer.flipX = move.x < 0;
        rigidbody.velocity = new Vector2(speed * move.x, rigidbody.velocity.y);
    }

    void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (Grounded)
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpPower);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
            floorsTouched++;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
            floorsTouched--;
    }
}
