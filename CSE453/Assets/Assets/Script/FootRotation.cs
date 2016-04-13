using UnityEngine;
using System.Collections;

public class FootRotation : MonoBehaviour {

	public Vector3 current_orientation;
	public float rotateSpeed;

	void update_current_orientation() {
		current_orientation.x = Mathf.Sin (current_orientation.x + rotateSpeed * Time.deltaTime);
		current_orientation.z = Mathf.Sin (current_orientation.x + rotateSpeed * Time.deltaTime);
		//current_orientation.y += rotateSpeed * Time.deltaTime;
		//current_orientation.x += rotateSpeed * Time.deltaTime;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		update_current_orientation ();

		Vector3 localForward = transform.worldToLocalMatrix.MultiplyVector (transform.forward);

		Vector3 rotation = current_orientation - Vector3.forward;
		transform.Rotate(rotation, Space.Self);
	}
}
