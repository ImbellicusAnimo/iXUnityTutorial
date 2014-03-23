using UnityEngine;
using System.Collections;

public class ThirdPersonCameraController : MonoBehaviour {

	public Transform target;

	// mouse sensitivity
	public float sensitivity = 30.0f;

	public float minXAngle = -90.0f;
	public float maxXAngle = 90.0f;
	float xAngle, yAngle;

	public float minDistance = 3.0f;
	public float maxDistance = 15.0f;

	float currentDistance = 10.0f;
	float targetDistance;
	public float correctionSpeed = 10.0f;
	public float zoomSpeed = 10.0f;

	bool isHoming = true;
	float homingSpeed = 5.0f;

	
	
	// Use this for initialization
	void Start () {
		if(target == null) {
			this.enabled = false;
		} else {
			xAngle = target.eulerAngles.x;
			yAngle = target.eulerAngles.y;
		}

		targetDistance = currentDistance;
	}

	// Update is called once per frame
	void LateUpdate () {
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");
		float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

		float forward = Input.GetAxis("Vertical");
		float strafe = Input.GetAxis("Strafe");

		if(forward != 0 || strafe != 0) { // check for movements
			isHoming = true;
		} else if(isHoming) { // stop moving
			isHoming = false;
		}

		float lastXAngle = xAngle;

		if(Input.GetMouseButton(0)) { // rotation around character
			isHoming = false;
			xAngle += mouseY * Time.deltaTime * sensitivity;
			yAngle += mouseX * Time.deltaTime * sensitivity;
		}

		if(Input.GetMouseButton(1)){
			xAngle += mouseY * Time.deltaTime * sensitivity;
		}

		if(isHoming) { // smooth reset of y-rotation
			yAngle = Mathf.LerpAngle(yAngle, target.eulerAngles.y, homingSpeed * Time.deltaTime);

			if(Mathf.Abs(yAngle - target.eulerAngles.y) < 0.1f) { // reset complete, stop homing
				isHoming = false;
				yAngle = target.eulerAngles.y;
			}
		}

		float distanceCorrection = 0;
		float cameraRadius = 0.5f; // camera sphere

		// check for terrian collisions with camera sphere
		int layerMask = 1 << LayerMask.NameToLayer("Terrain");
		RaycastHit hit = new RaycastHit();

		Vector3 rayDirection = this.transform.position - target.position;
		if(Physics.SphereCast(target.position, cameraRadius, rayDirection, out hit, targetDistance, layerMask)) {
			distanceCorrection = -targetDistance + hit.distance;
			//Debug.Log("Correction Distance: " + distanceCorrection);
		}

		//targetDistance = Mathf.Clamp(Mathf.SmoothStep(targetDistance, targetDistance - scrollWheel, Time.deltaTime * zoomSpeed), minDistance, maxDistance);
		targetDistance = Mathf.Clamp(targetDistance - (scrollWheel * zoomSpeed* Time.deltaTime ), minDistance,maxDistance);
		currentDistance = Mathf.Clamp(targetDistance + distanceCorrection, minDistance, maxDistance);

		xAngle = ClampAngle(xAngle, minXAngle, maxXAngle);

		if(currentDistance == minDistance && distanceCorrection != 0 && (mouseY) < 0) { // stop rotating when at minDistance with correction
			xAngle = lastXAngle;
		}

		this.transform.rotation = Quaternion.Euler(xAngle, yAngle, 0);
		this.transform.position = target.position + this.transform.TransformDirection(new Vector3(0, 0, -currentDistance));
	}

	static float ClampAngle(float angle, float min, float max) {
		if(angle < -360) {
			angle += 360;
		}
		if(angle > 360) {
			angle -= 360;
		}

		return Mathf.Clamp(angle, min, max);
	}
	
}