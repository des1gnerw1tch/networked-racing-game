using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float acceleration = 10;
    [SerializeField] private float maximumVelocity = 100;

    private bool gasPressed;
    
    private void OnGas()
    {
        gasPressed = true;
        rb.drag = 0;
    }

    private void OnGasReleased()
    {
        gasPressed = false;
        rb.drag = 1;
    }

    private void FixedUpdate()
    {
        if (gasPressed)
        {
            if (rb.velocity.magnitude < maximumVelocity)
            {
                Debug.Log("Accelerating!");
                rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
            }
            else
            {
                Debug.Log("Reached max speed");
            }
            
        }
    }
}
