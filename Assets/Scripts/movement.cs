using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditor.U2D;
using UnityEngine;

public class movement : MonoBehaviour
{
    Rigidbody rb;
    TrailRenderer trail;

    [Header("Speeds Types")]
    public float speed;
    public float maxSpeed = 180;
    public float turnSpeed = 200;
    public float maxReverseSpeed = 30;

    [Header("Forces")]
    public float brakeForce;
    public float handbrakeForce;
    public float handbrakeTurnBoost;

    [Header("Boolean")]
    public bool inputAccelerate;
    public bool inputTurnLeft;
    public bool inputTurnRight;
    public bool inputBrake;
    public bool inputHandbrake;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        trail.emitting = false;
    }

    void Update()
    {
        inputAccelerate = Input.GetKey(KeyCode.W);// R2
        inputTurnLeft = Input.GetKey(KeyCode.A); 
        inputTurnRight = Input.GetKey(KeyCode.D);
        inputBrake = Input.GetKey(KeyCode.S); // L2
        inputHandbrake = Input.GetKey(KeyCode.Space); // X

        if (!inputHandbrake)
        {
            trail.emitting = false;
        }
    }

    private void FixedUpdate()
    {
        rb.useGravity = true;
        Move();
    }
    void Move()
    {
        //ACELERAR
        if (inputAccelerate)
        {
            Vector3 forward_movement = transform.forward * speed * Time.deltaTime;
            rb.velocity += forward_movement;

            if (rb.velocity.magnitude >= maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }

        //GIRAR
        if (inputTurnLeft)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, -turnSpeed * Time.deltaTime, 0));
        }

        if (inputTurnRight)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turnSpeed * Time.deltaTime, 0));
        }

        //FRENO Y MARCHA ATRAS
        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);

        if (inputBrake)
        {
            if (localVelocity.z > 0.1f)  //SI ESTA YENDO HACIA ADELANTE
            {
                // SE APLICA UNA FUERZA DE FRENADO PROPORCIONAL (NO DIVISION DIRECTA).
                Vector3 brakeForceVector = -rb.velocity.normalized * brakeForce;
                rb.AddForce(brakeForceVector, ForceMode.Acceleration);
            }
            else  // HACIA ATRAS
            { 
                Vector3 reverseMovement = -transform.forward * speed * Time.deltaTime;
                rb.velocity += reverseMovement;

                float currentReverseSpeed = -transform.InverseTransformDirection(rb.velocity).z;
                if (currentReverseSpeed > maxReverseSpeed)
                {
                    rb.velocity = transform.TransformDirection(new Vector3(localVelocity.x, localVelocity.y, -maxReverseSpeed));
                }
            }
        }

        // FRENO DE MANO
        if (inputHandbrake)
        {
            trail.emitting = true;

            Vector3 brakeForceVector = -rb.velocity.normalized * handbrakeForce;  
            rb.AddForce(brakeForceVector, ForceMode.Acceleration);

            if (rb.velocity.magnitude < 0.5f) 
            {
                rb.velocity = Vector3.zero;
            }

            // DERRAPE AL GIRAR
            if (inputTurnLeft)
            {
                rb.angularVelocity += new Vector3(0, -handbrakeTurnBoost * Time.deltaTime, 0); 
            }
            else if (inputTurnRight)
            {
                rb.angularVelocity += new Vector3(0, handbrakeTurnBoost * Time.deltaTime, 0);  
            }
        }
    }
}



