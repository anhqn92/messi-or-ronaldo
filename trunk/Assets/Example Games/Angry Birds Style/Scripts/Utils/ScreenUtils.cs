using UnityEngine;
using System.Collections;

public class ScreenUtils {


	public static Vector3 getSceenSizeInUnit () {
		//Screen.width
		return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
	}

}
