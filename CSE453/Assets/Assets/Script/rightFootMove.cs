using UnityEngine;
using System.Collections;

public class rightFootMove : MonoBehaviour {
	Vector3 right_accelerate = new Vector3 ();
	Rigidbody right_foot_rb;
	Vector3 initial_right_foot_velocity = new Vector3();
	Vector3 final_right_foot_velocity = new Vector3(0,0,0);
	GameObject left_foot;
	Vector3 new_pos = new Vector3();

	// Use this for initialization
	void Start () {
		left_foot = GameObject.FindGameObjectWithTag ("LeftFoot");
		right_foot_rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		new_pos = transform.position;
		right_accelerate.x = Random.Range(-2f,2f);
		right_accelerate.y = Random.Range(-2f,2f);
		right_accelerate.z = Random.Range(-2f,2f);
		//move right foot
		initial_right_foot_velocity = final_right_foot_velocity;
		final_right_foot_velocity = initial_right_foot_velocity + right_accelerate * Time.deltaTime;
		right_foot_rb.velocity = final_right_foot_velocity;
		Debug.Log ("right FOOT SPEED:" + right_foot_rb.velocity);
		// correct disdance if two feet too far
		if (Mathf.Abs (transform.position.x - left_foot.transform.position.x) > 2f) {
			if (transform.position.x > left_foot.transform.position.x) {
				new_pos.x = left_foot.transform.position.x + 2f;
			} else {
				new_pos.x = left_foot.transform.position.x - 2f;
			}
		}
		if (Mathf.Abs (transform.position.y - left_foot.transform.position.y) > 1f) {
			if (transform.position.y > left_foot.transform.position.y) {
				new_pos.y = left_foot.transform.position.y + 1f;
			} else {
				new_pos.y = left_foot.transform.position.y - 1f;
			}
		}
		if (Mathf.Abs (transform.position.z - left_foot.transform.position.z) > 2f) {
			if (transform.position.z > left_foot.transform.position.z) {
				new_pos.z = left_foot.transform.position.z + 2f;
			} else {
				new_pos.z = left_foot.transform.position.z - 2f;
			}
		}
		transform.position = new_pos;
	}
}
