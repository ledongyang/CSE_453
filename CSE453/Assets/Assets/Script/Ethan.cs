using UnityEngine;
using System.Collections;

public class Ethan : MonoBehaviour {

	public Animator anim;
	private float inputHori;
	private float inputVerti;
	private bool run;
	private bool jump;

	void Start () {
		anim = GetComponent <Animator> ();
		run = false;
	}
	
	// Update is called once per frame
	void Update () {
		inputHori = Input.GetAxis ("Horizontal");
		inputVerti = Input.GetAxis ("Vertical");
		anim.SetFloat ("inputH", inputHori);
		anim.SetFloat ("inputV", inputVerti);

		if (Input.GetKey (KeyCode.LeftShift)) {
			run = true;
		} else {
			run = false;
		}
		anim.SetBool ("run", run);

		if (Input.GetKey ("space")) {
			jump = true;
		} else {
			jump = false;
		}
		anim.SetBool ("jump", jump);
	}
}
