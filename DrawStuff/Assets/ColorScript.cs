using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorScript : MonoBehaviour {
	private ColorBlock cb;
	public Button ColorSwitch;
	public Slider colorSlider;
	
	// Use this for initialization
	void Start () {
		cb=ColorSwitch.colors;
	}
	
	// Update is called once per frame
	void Update () {
		cb=colorSlider.colors;
		ColorSwitch.colors=cb;
	}
}
