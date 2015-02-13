using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour {
	public float xPossitionMoveTo;
	public float timeMove;
	// Use this for initialization
	void Start () {
		moveLoopCloud ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void moveLoopCloud () {

		LeanTween.moveLocalX (gameObject,xPossitionMoveTo,timeMove).setEase(LeanTweenType.linear).setLoopType(LeanTweenType.clamp).setLoopCount(9999);
	}
}
