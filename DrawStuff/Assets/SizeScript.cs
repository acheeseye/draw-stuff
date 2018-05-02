using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeScript : MonoBehaviour {
	public Button SizeSwitch;
	public Slider sizeSlider;
	private float currentWidth;
	private float lastWidth;

	// Use this for initialization
	void Start () {
		SizeSwitch.GetComponentInChildren<Text>().text = ""+.05f;
	}
	
	// Update is called once per frame
	public void changeText()	{
			currentWidth+=sizeSlider.value/20-lastWidth;
			if(currentWidth == 0)
				currentWidth=.05f;
			SizeSwitch.GetComponentInChildren<Text>().text = ""+currentWidth;
			lastWidth=currentWidth;
	}
}
