using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]  							
public class ThirdPersonController : MonoBehaviour {
	
	public float rotationSpeed = 60.0f;											
	public float walkingSpeed = 8.0f;
	public float jumpSpeed = 8.0f;
	public float mouseSensitivity = 10;
	public float airSpeed 	= 5f;
	public float gravity = 9.81f;
	
	
	Vector3 velocity = Vector3.zero;

	CharacterController characterController;
	
	// Use this for initialization
	void Start () {
		characterController = this.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		float forward = Input.GetAxis("Vertical");					
		float strafe = Input.GetAxis("Strafe");							
		float rotate = Input.GetAxis("Horizontal") * rotationSpeed;		
		
		Vector3 airVelocity= Vector3.zero;
		
		// Drehung des Spielers per Maus
		if(Input.GetMouseButton(1))
		{
			rotate = Input.GetAxis("Mouse X") * mouseSensitivity;
		}
		
		if(characterController.isGrounded)
		{
			velocity = this.transform.right * walkingSpeed * strafe + 
				this.transform.forward * walkingSpeed * forward;				
			
			if(Input.GetButton ("Jump"))
			{
				velocity.y = jumpSpeed;
				
			}		
		}
		else // Anpassung der Bewegung in der Luft
		{
			airVelocity = forward * airSpeed * this.transform.forward + strafe * airSpeed * this.transform.right;
		}		
		
		// Gravitation 
		velocity.y-=gravity * Time.deltaTime;
		
		characterController.Move((velocity + airVelocity)  *  Time.deltaTime);			
		transform.Rotate(Vector3.up, rotate  * Time.deltaTime);	
	
	}
}
