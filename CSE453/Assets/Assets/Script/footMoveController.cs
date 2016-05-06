using UnityEngine;
using System.Collections;

public class footMoveController : MonoBehaviour {
	GameObject left_foot;
	GameObject right_foot;

	void Start () {
		left_foot = GameObject.FindGameObjectWithTag ("LeftFoot");
		right_foot = GameObject.FindGameObjectWithTag ("RightFoot");
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = (left_foot.transform.position + right_foot.transform.position) / 2.0f;
	}
}
