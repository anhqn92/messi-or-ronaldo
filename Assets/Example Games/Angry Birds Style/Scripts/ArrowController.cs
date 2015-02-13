using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour
{
	private bool clickedOn;
	private Vector3 moveDirection;
	private Vector3 pointStartDrag;
	public GameObject ball;
	public GameObject kick;
	public GameObject destination;
	public GameObject perfectBall;
	
	public float forceRatio = 120f;
	private Vector2 forceKich;
	private Vector3 startBallPosition;
	private bool isBallStartPossition = true;
	private float screenWidth;
	private float screenHeight;

	public GameObject character;
	private Vector3 characterPosStarted;
	private Vector3 newPosCharacter;
	private Vector3 arrowLocalScale;
	void Start ()
	{
		arrowLocalScale = transform.localScale;
		moveDirection = Vector3.right;
		startBallPosition = ball.transform.localPosition;
		screenWidth = ScreenUtils.getSceenSizeInUnit ().x;
		screenHeight = ScreenUtils.getSceenSizeInUnit ().y;
		characterPosStarted = character.transform.position;
		//Debug.Log ("X1:" + characterPosStarted.x +"Y1:" +characterPosStarted.y);
		genNewDestinationPossition ();
	}
	// Update is called once per frame
	void Update ()
	{

		if ((!isBallTranslating() && !isBallStartPossition) || Mathf.Abs (ball.transform.localPosition.x) > screenWidth +ball.renderer.bounds.size.x) {
			resetBallPositon ();
			genNewDestinationPossition ();
			//Debug.Log ("reset");
		}

		if (clickedOn && !isBallTranslating()) {
			Dragging ();
		}
	}

	//check ball is translating or not
	private bool isBallTranslating () {
		if(Mathf.Abs( ball.rigidbody2D.velocity.x) != 0 || Mathf.Abs(ball.rigidbody2D.velocity.y) != 0) {// ball stop
			return true;
		} else {
			return false;
		}
	}

	//reset ball possition
	private void resetBallPositon ()
	{
		ball.transform.localPosition = startBallPosition;
		ball.rigidbody2D.angularVelocity = 0;
		ball.rigidbody2D.velocity = new Vector2 (0,0);
		isBallStartPossition = true;
	}

	//gen random destination possiton
	private void genNewDestinationPossition ()
	{
		Vector3 newPostiton = destination.transform.localPosition;
		Debug.Log("1.LocalPos X:  "+newPostiton.x);
		newPostiton.x = Random.Range (-0.224f*screenWidth, 0.85f*screenWidth);
		Debug.Log("screenWidth: "+screenWidth);
		LeanTween.moveLocalX(destination,newPostiton.x,0.5f).setEase(LeanTweenType.easeOutExpo).setOnComplete(onGenDestinationComplete);//.setOnComplete(
		//destination.transform.position = newPostiton;

	}

	private void onGenDestinationComplete () {
		genNewPerfectBallPossition ();
	}

	private void genNewPerfectBallPossition () {
		float x = Random.Range (ball.transform.localPosition.x, destination.transform.localPosition.x);
		float y = Random.Range (-0.5f, 0.9f*screenHeight);
		Vector3 newPos = perfectBall.transform.localPosition;
		newPos.x = x;
		newPos.y = y;
		LeanTween.moveLocal(perfectBall.gameObject,newPos,0.5f).setEase(LeanTweenType.easeOutExpo);
	}

	void OnMouseDown ()
	{
		GetComponent<SpriteRenderer>().enabled = true;
		clickedOn = true;
		pointStartDrag = Camera.main.ScreenToWorldPoint (Input.mousePosition);
	}
	
	void OnMouseUp () {
		GetComponent<SpriteRenderer>().enabled = false;
		clickedOn = false;
		if(forceKich.x > 0 || forceKich.y > 0 ) { // if force >0
			character.rigidbody2D.drag = 0;
			character.rigidbody2D.velocity = new Vector2 (20,0);//move character
			character.GetComponent <Animator>().SetBool("isRun",true);
		}

	}

	public IEnumerator kickBall () {//call at character controller

		StartCoroutine (playKickAnim ());
		character.GetComponent <Animator>().SetBool("isRun",false);
		character.rigidbody2D.velocity = new Vector2 (0,0);
		character.rigidbody2D.drag = 9999999;//stop side character
		character.transform.position = characterPosStarted;
		//Debug.Log ("X2:" + characterPosStarted.x+"Y2:" +characterPosStarted.y);

		//character.GetComponent<Animator>().enabled = true;
		//character.GetComponent<Animator> ().SetTrigger("kick");
		ball.rigidbody2D.AddForce (forceKich);
		Debug.Log ("FORCE X: "+forceKich.x+ " FORCE Y: "+forceKich.y);
		yield return new WaitForSeconds (0.1f);//wait for velocity >0
		isBallStartPossition = false;
		resetArrow ();
		//Debug.Log ("X3:" + character.transform.position.x+"Y3:" +character.transform.position.y);

	}

	private IEnumerator playKickAnim () {
		kick.GetComponent <SpriteRenderer> ().enabled  = true;
		yield return new WaitForSeconds(0.2f);
		kick.GetComponent <SpriteRenderer> ().enabled  = false;
	}

	private void resetArrow () {

		transform.localScale = arrowLocalScale;
		transform.rotation = Quaternion.Euler (0, 0, 0);
		forceKich = new Vector2 (0, 0);
	}


	void Dragging ()
	{

		Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		
		moveDirection = mouseWorldPoint - pointStartDrag;
		moveDirection.z = 0; 
		moveDirection.Normalize ();

		float targetAngle = 180 + Mathf.Atan2 (moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = 
			 Quaternion.Slerp (transform.rotation, 
			                 Quaternion.Euler (0, 0, targetAngle), 
			                 10 * Time.deltaTime);
		float scale = 1f + Vector3.Distance (mouseWorldPoint, pointStartDrag);

		if (scale > 1) {
			transform.localScale = new Vector3 (0.7f * scale * arrowLocalScale.x, arrowLocalScale.y, 1);
			Vector3 vDrag = pointStartDrag - mouseWorldPoint;
			forceKich = forceRatio * new Vector2 (vDrag.x, vDrag.y);
			newPosCharacter = characterPosStarted;

			newPosCharacter.x -= (scale - 1);
			character.transform.position = newPosCharacter;
		}
	}
}
