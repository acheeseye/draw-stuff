using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPlacement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		float initialWidth = (Screen.width);
		float initialHeight = (Screen.height);
		transform.position = new Vector3(initialWidth, initialHeight, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
