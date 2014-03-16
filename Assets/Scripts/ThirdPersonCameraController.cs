using UnityEngine;
using System.Collections;

public class ThirdPersonCameraController : MonoBehaviour {

	public Transform target;
	public float distance;
	public float height;
	
	public float rotationSpeed  = 1.0f;
	Quaternion rotationOffset = Quaternion.identity;
	Vector3 lastMousePosition;

	// Update is called once per frame
	void LateUpdate () {
		if (Input.GetMouseButtonDown(1))
		{
			lastMousePosition = Input.mousePosition;
		}
		else if (Input.GetMouseButton(1))
		{
			Vector3 delta = Input.mousePosition - lastMousePosition; 
			rotationOffset *= Quaternion.AngleAxis(delta.x * rotationSpeed * Time.deltaTime, Vector3.up);       
			lastMousePosition = Input.mousePosition;
		}
		if(Input.GetMouseButtonDown(2))
		{
			rotationOffset = Quaternion.identity;
		}
		this.transform.position = target.position + 
			target.transform.TransformDirection(rotationOffset *  new Vector3(0,height,-distance));
		this.transform.LookAt(target);				
	}

	
	// Use this for initialization
	void Start () {
		if(target == null)
		{
			this.enabled = false;
		}
	}
	
	
}