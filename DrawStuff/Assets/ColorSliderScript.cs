using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSliderScript : MonoBehaviour {
	bool isDown = false;
	// Use this for initialization
	void Start () {
		transform.Translate(0f, 600f, 0f);
	}
	
	// Update is called once per frame
	public void moveColorSlider()	{
		if(isDown) 	{
			transform.Translate(0f, 600f, 0f);
			isDown=false;
		} else {
			transform.Translate(0f, -600f, 0f);
			isDown=true;
		}
		
	}
}
