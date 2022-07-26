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
	
	
     
	private void Start()	{
		rb.centerOfMass = new Vector3(0.0f, -0.3f, -6.0f);
		GameObject spawnPosition = GameObject.FindWithTag("SpawnPosition");
		if (spawnPosition != null)	{
			rb.transform.position = spawnPosition.transform.position;
		}
	}
	
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
	
	private void OnReset()
	{
		rb.transform.rotation = Quaternion.Euler(0, rb.transform.rotation.eulerAngles.y, 0);
	}
	
	private void OnRestart()
	{
		GameObject spawnPosition = GameObject.FindWithTag("SpawnPosition");
		if (spawnPosition != null)	{
			rb.transform.position = spawnPosition.transform.position;
		}
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
		
		if (rightPressed && rb.velocity.magnitude > 2)
		{
			rb.transform.Rotate(0.0f, 6.0f/rb.velocity.magnitude, 0.0f, Space.Self);
		}
		
		if (rightPressed && rb.velocity.magnitude <= 2 && rb.velocity.magnitude > 0)
		{
			rb.transform.Rotate(0.0f, 3.0f, 0.0f, Space.Self);
		}
		
		if (leftPressed && rb.velocity.magnitude > 2)
		{
			rb.transform.Rotate(0.0f, -6.0f/rb.velocity.magnitude, 0.0f, Space.Self);
		}
		
		if (leftPressed && rb.velocity.magnitude <= 2 && rb.velocity.magnitude > 0)
		{
			rb.transform.Rotate(0.0f, -3.0f, 0.0f, Space.Self);
		}
		
    }
}
