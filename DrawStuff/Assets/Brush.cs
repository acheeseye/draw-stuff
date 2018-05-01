using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Brush : MonoBehaviour {
	public GameObject trailPrefab;
	public Slider colorSlider;
	private List<GameObject> trails = new List<GameObject>();
	private GameObject thisTrail;
	private int count=0;
	private Vector3 startPos;
	private Plane objPlane;
	private bool hasMoved;
	private TrailRenderer tr;
    private GameObject eraseToggle;
    private float dz;
    private bool eraseMode;
	private Color color = Color.white;
	
	// Use this for initialization
	void Start () {
        eraseToggle = GameObject.Find("Canvas/Erase");             // looks for Erase toggle
        eraseToggle.GetComponent<Toggle>().isOn = false;    // default erase toggle is off
		objPlane = new Plane(Camera.main.transform.forward * -1, transform.position);
        eraseMode = false;
        dz = 0;
	}
	
	public void changeColor()	{
		if(colorSlider.value == 0)	{
			color=Color.white;
		} else if(colorSlider.value == 1)	{
			color=Color.red;
		} else if(colorSlider.value == 2)	{
			color=Color.magenta;
		} else if(colorSlider.value == 3)	{
			color=Color.blue;
		} else if(colorSlider.value == 4) {
			color=Color.cyan;
		} else if(colorSlider.value == 5)	{
			color=Color.green;
		} else if(colorSlider.value == 6)	{
			color=Color.yellow;
		} else if(colorSlider.value == 7)	{
			color=Color.grey;
		}
	}
	
	public void deleteTrail()	{
		if(count>1)	{
			Destroy(trails[count-2]);
			trails.RemoveAt(count-2);
			count--;
		}
	}
    
	// Update is called once per frame
	void Update () {

        // checks for toggle value and alters brush color accordingly
        if(eraseToggle.GetComponent<Toggle>().isOn)
        {
            eraseMode = true;
        }
        else
        {
            eraseMode = false;
        }

        // when first touched
		if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            int BrushTagCount = GameObject.FindGameObjectsWithTag("BrushTag").Length;
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            dz += (float)0.0001;
            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance))
            { startPos = mRay.GetPoint(rayDistance); }
            startPos.z = -dz;

            Vector3 v3;
            v3.x = startPos.x;
            v3.y = startPos.y;
            v3.z = -dz;

            trails.Add((GameObject)Instantiate(trailPrefab, startPos, Quaternion.identity));
			count++;
            trails[count-1].transform.position = v3;
            if (eraseMode)
            {
                trails[count-1].GetComponent<TrailRenderer>().material.SetColor("_Color", Color.clear);
            }
            else
            {
                trails[count-1].GetComponent<TrailRenderer>().material.SetColor("_Color", color);
            }
            hasMoved = false;

        // when moved after touched
        } else if(((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0)))
        {
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 v3;
            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance))
            {
                v3.x = mRay.GetPoint(rayDistance).x;
                v3.y = mRay.GetPoint(rayDistance).y;
                v3.z = -dz;
                trails[count-1].transform.position = v3;
            }
            hasMoved = false;
		}  else if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))	{
				
			if(Vector3.Distance(trails[count-1].transform.position, startPos) == 0)
				{
					Destroy(trails[count-1]);
					trails.RemoveAt(count-1);
					count--;
				}
		}
	}
}

