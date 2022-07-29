using System;
using UnityEngine;
using Photon.Pun;
using RaceLogic;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
	
	[SerializeField] private WheelCollider flw;
	[SerializeField] private WheelCollider frw;
	[SerializeField] private WheelCollider blw;
	[SerializeField] private WheelCollider brw;
	
    [SerializeField] private float acceleration = 20;
	[SerializeField] private float brakeForce = 30;
	[SerializeField] private float downForce = 10000;
    [SerializeField] private float maximumVelocity = 100;
	[SerializeField] private float turnSpeed = 0.05f;
	[SerializeField] private float maxTurn = 2.0f;
	
    private bool gasPressed;
	private bool brakePressed;
	private bool leftPressed;
	private bool rightPressed;
	private float steerFactor = 0;
     
	private void Start()	{
		
		if (!GetComponent<PhotonView>().IsMine)
		{
			Destroy(this);
		}
		
		//rb.centerOfMass = new Vector3(0.0f, -0.5f, -0.0f);
		GameObject spawnPosition = GameObject.FindWithTag("SpawnPosition");
		if (spawnPosition != null)	{
			rb.transform.position = spawnPosition.transform.position;
		}
	}
	
    private void OnGas() {gasPressed = true;}

    private void OnGasReleased() {gasPressed = false;}
	
    private void OnBrake() {brakePressed = true;}

    private void OnBrakeReleased() {brakePressed = false;}
	
    private void OnLeft() {leftPressed = true;}
	
    private void OnLeftReleased() {leftPressed = false;}
	
    private void OnRight() {rightPressed = true;}
	
    private void OnRightReleased() {rightPressed = false;}
	
	private void OnReset() {rb.transform.rotation = Quaternion.Euler(0, rb.transform.rotation.eulerAngles.y, 0);}
	
	private void OnRestart()
	{
		GameObject spawnPosition = GameObject.FindWithTag("SpawnPosition");
		if (spawnPosition != null)	{
			rb.transform.position = spawnPosition.transform.position;
		}
	}

    private void FixedUpdate()
    {
		WheelHit hit;
        if (gasPressed)
        {
			if (flw.GetGroundHit(out hit) || frw.GetGroundHit(out hit) || blw.GetGroundHit(out hit) || brw.GetGroundHit(out hit))
            {
				Debug.Log("Accelerating!");
            	rb.AddForce(rb.transform.forward * acceleration);
			}
            
        }
		
        if (brakePressed)
        {
			if (-5 > rb.velocity.magnitude || rb.velocity.magnitude > 5)
			{
				if (flw.GetGroundHit(out hit) || frw.GetGroundHit(out hit) || blw.GetGroundHit(out hit) || brw.GetGroundHit(out hit))
	            {
					Debug.Log("Braking!");
		            rb.AddForce(rb.transform.forward * -brakeForce);
	            }
			}
            
        }
		
		if (flw.GetGroundHit(out hit) || frw.GetGroundHit(out hit) || blw.GetGroundHit(out hit) || brw.GetGroundHit(out hit))
		{
			rb.AddForce(rb.transform.up * -downForce * (rb.velocity.magnitude/50));
		}
		
		
		if (rightPressed && steerFactor < maxTurn) {steerFactor += turnSpeed;}
		if (leftPressed && steerFactor > -maxTurn) {steerFactor -= turnSpeed;}
		if (!leftPressed && !rightPressed) {steerFactor -= steerFactor/5;}
		
		if (!flw.GetGroundHit(out hit) && !frw.GetGroundHit(out hit))
		{
			steerFactor -= steerFactor/5;
			
		}
		rb.transform.Rotate(0.0f, steerFactor, 0.0f, Space.Self);
		
		
		
		
    }

    private void OnTriggerEnter(Collider other)
    {
	    if (other.CompareTag("FinishLine"))
	    {
		    TrackNetworkManager.Instance.IncrementLap();
	    }
    }
}
