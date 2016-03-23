using UnityEngine;
using System.Collections;

public class ScoccerShooter : MonoBehaviour {

	GameObject soccer;
	GameObject soccerHolder;
	GameObject player;
	Rigidbody rb;
	public float SoccerSpeed=30;
	public bool hasBall=true;
	// Use this for initialization
	void Start () {
		soccerHolder = transform.Find ("soccerHolder").gameObject;
		soccer = soccerHolder.transform.Find ("soccer").gameObject;
		rb = soccer.GetComponent<Rigidbody> ();
		player = this.gameObject;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && hasBall) {
			hasBall = false;
			soccer.transform.parent = null;
			rb.isKinematic = false;
			rb.velocity = Camera.main.transform.forward * SoccerSpeed;
		}
		if (Input.GetKeyDown ("r") && hasBall == false) {
			ResetSoccer ();
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "soccerChild" && hasBall == false) {
			Debug.Log ("pick up");
			ResetSoccer ();
		}
	}

	void ResetSoccer(){
		hasBall = true;
		rb.isKinematic = true;
		soccer.transform.parent = soccerHolder.transform;
		soccer.transform.localPosition = new Vector3 (0, 0, 0);
	}
}
