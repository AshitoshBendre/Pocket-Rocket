using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSphere : MonoBehaviour
{
    Rigidbody rb;
    //[SerializeField, Range(0f, 20f)]
    //float bounce = 0.5f;

    [SerializeField,Range(0f,10f)]
    float speed = 5f;

    [SerializeField,Range(0f,10f)]
    float maxAcceleration =10f;

    //[SerializeField]
    //Rect allowedArea = new Rect(-5f, -5f, 10f, 10f);

    public Vector3 velocity,desiredVelocity;
    public float yAxis = 0.5f;

    bool desiredJump;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInput();
    }

    public void playerInput()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * speed;

        //JUMP
        desiredJump |= Input.GetButtonDown("Jump");

        //OLD CODE WHICH WAS USED FOR TRANSFORM BASED MOVEMENT

        //Vector3 displacement = velocity * Time.deltaTime;
        //Vector3 newPosition = transform.localPosition + displacement;

        /*
         if(!allowedArea.Contains(new Vector2(newPosition.x , newPosition.z)))
        {
            if (newPosition.x < allowedArea.xMin)
            {
                newPosition.x = allowedArea.xMin;
                velocity.x = -velocity.x * bounce;
            }
            else if (newPosition.x > allowedArea.xMax)
            {
                newPosition.x = allowedArea.xMax;
                velocity.x = -velocity.x * bounce;
            }
            if (newPosition.z < allowedArea.yMin)
            {
                newPosition.z = allowedArea.yMin;
                velocity.z = -velocity.z * bounce;
            }
            else if (newPosition.z > allowedArea.yMax)
            {
                newPosition.z = allowedArea.yMax;
                velocity.z = -velocity.z * bounce;
            }
        }
        */
        //transform.localPosition = newPosition;
    }

    void FixedUpdate()
    {
        
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        //JUMP
        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }

        rb.velocity = velocity;
    }

    void Jump()
    {
        velocity.y += 5f;
    }
}
