using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class movement : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public float maxSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        Vector3 playerVelocity = transform.forward * speed * Time.deltaTime;
        Vector3 currentVelocity = rb.velocity + playerVelocity;
        
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {

            if (currentVelocity.magnitude > maxSpeed)
            {
                currentVelocity = currentVelocity.normalized * maxSpeed; 
            }

            rb.velocity = new Vector3(currentVelocity.x, rb.velocity.y, currentVelocity.z);
        }
    }
}
