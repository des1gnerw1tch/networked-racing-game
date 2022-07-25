using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float acceleration = 20;
    [SerializeField] private float maximumVelocity = 100;

    private bool gasPressed;
	private bool leftPressed;
	private bool rightPressed;
    
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
	
    private void OnLeft()
    {
        leftPressed = true;
    }
	
    private void OnLeftReleased()
    {
        leftPressed = false;
    }
	
    private void OnRight()
    {
        rightPressed = true;
    }
	
    private void OnRightReleased()
    {
        rightPressed = false;
    }

    private void FixedUpdate()
    {
        if (gasPressed)
        {
            if (rb.velocity.magnitude < maximumVelocity)
            {
                Debug.Log("Accelerating!");
                rb.AddForce(rb.transform.forward * acceleration, ForceMode.Acceleration);
            }
            else
            {
                Debug.Log("Reached max speed");
            }
            
        }
		
		if (rightPressed)
		{
			rb.transform.Rotate(0.0f, 1.0f, 0.0f, Space.Self);
		}
		
		if (leftPressed)
		{
			rb.transform.Rotate(0.0f, -1.0f, 0.0f, Space.Self);
		}
		
    }
}
