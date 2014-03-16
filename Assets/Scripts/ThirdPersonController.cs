using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]  							
public class ThirdPersonController : MonoBehaviour {
	
	public float rotationSpeed = 60.0f;											
	public float walkingSpeed = 8.0f;			
	CharacterController characterController;
	
	// Use this for initialization
	void Start () {
		characterController = this.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		float forward = Input.GetAxis("Vertical");					
		float strafe = Input.GetAxis("Strafe");							
		float rotate = Input.GetAxis("Horizontal");		
		
		transform.Rotate(Vector3.up, rotate   * rotationSpeed * Time.deltaTime);
		Vector3 direction = this.transform.right * strafe + this.transform.forward * forward;
		direction.Normalize();												
				
		characterController.SimpleMove(direction * walkingSpeed);		
	
	}
}
