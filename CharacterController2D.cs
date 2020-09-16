using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField]
    float speed = 10.0f;
    [SerializeField]
    float walkAcceleration = 75f;
    [SerializeField]
    float groundDeceleration = 30.0f;
    Vector2 velocity;
    BoxCollider2D boxCollider;
    bool grounded;
    [SerializeField]
    float jumpHeight = 3.0f;
    [SerializeField]
    float airAcceleration = 30.0f;
    float acceleration;
    float deceleration;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(grounded)
        {
            velocity.y = 0;

            if(Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            }
        }

        velocity.y += Physics2D.gravity.y * Time.deltaTime;

        float move = Input.GetAxis("Horizontal");

        acceleration = grounded ? walkAcceleration : airAcceleration;
        deceleration = grounded ? groundDeceleration : 0;

        if(move != 0)
            velocity.x = Mathf.MoveTowards(velocity.x, move * speed, acceleration * Time.deltaTime);
        else
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);

        transform.Translate(velocity * Time.deltaTime);

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        grounded = false;

        foreach(var hit in hits)
        {
            if (hit == boxCollider)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if (colliderDistance.isOverlapped)
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

            if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
                grounded = true;
        }
    }
}
