using UnityEngine;
using System.Collections;

public class TrailController : MonoBehaviour {
	public GameObject ball;
	public float xToBall;
	public float yToBall;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		updatePossition ();
	}

	private void updatePossition () {
		Vector3 newPos = ball.transform.position;
		newPos.x -= xToBall;
		newPos.y -= yToBall;
		transform.position = newPos;
	}
}
