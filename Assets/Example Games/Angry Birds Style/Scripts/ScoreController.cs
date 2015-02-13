using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {
	public GUIText scoreText;
	private int score =0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "ball") {
			score +=1;
			scoreText.text = "Score: "+ score.ToString();
		}
	}
}
