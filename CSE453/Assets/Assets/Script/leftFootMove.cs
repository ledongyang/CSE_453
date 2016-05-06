using UnityEngine;
using System.Collections;

public class leftFootMove : MonoBehaviour {
	Vector3 left_accelerate = new Vector3 ();
	Rigidbody left_foot_rb;
	Vector3 initial_left_foot_velocity = new Vector3();
	Vector3 final_left_foot_velocity = new Vector3(0,0,0);
	GameObject right_foot;
	Vector3 new_pos = new Vector3();

	// Use this for initialization
	void Start () {
		right_foot = GameObject.FindGameObjectWithTag ("RightFoot");
		left_foot_rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		new_pos = transform.position;
		left_accelerate.x = Random.Range(-2f,2f);
		left_accelerate.y = Random.Range(-2f,2f);
		left_accelerate.z = Random.Range(-2f,2f);
		//move left foot
		initial_left_foot_velocity = final_left_foot_velocity;
		final_left_foot_velocity = initial_left_foot_velocity + left_accelerate * Time.deltaTime;
		left_foot_rb.velocity = final_left_foot_velocity;
		Debug.Log ("LEFT FOOT SPEED:" + left_foot_rb.velocity);
		//correct distance if two feet too far
		if (Mathf.Abs (transform.position.x - right_foot.transform.position.x) > 2f) {
			if (transform.position.x > right_foot.transform.position.x) {
				new_pos.x = right_foot.transform.position.x + 2f;
			} else {
				new_pos.x = right_foot.transform.position.x - 2f;
			}
		}
		if (Mathf.Abs (transform.position.y - right_foot.transform.position.y) > 1f) {
			if (transform.position.y > right_foot.transform.position.y) {
				new_pos.y = right_foot.transform.position.y + 1f;
			} else {
				new_pos.y = right_foot.transform.position.y - 1f;
			}
		}
		if (Mathf.Abs (transform.position.z - right_foot.transform.position.z) > 2f) {
			if (transform.position.z > right_foot.transform.position.z) {
				new_pos.z = right_foot.transform.position.z + 2f;
			} else {
				new_pos.z = right_foot.transform.position.z - 2f;
			}
		}
		transform.position = new_pos;
	}
}
