using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
	public ArrowController arrowController;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	void OnCollisionEnter2D(Collision2D coll) {
//		if (coll.gameObject.tag == "ball") {
//			StartCoroutine (arrowController.kickBall ());
//			//Debug.Log ("collison");
//		}
//		
//	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "ball") {
			StartCoroutine (arrowController.kickBall ());
			//Debug.Log ("collison");
		}
	}

}
