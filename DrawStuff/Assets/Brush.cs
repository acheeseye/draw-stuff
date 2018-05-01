using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brush : MonoBehaviour {
	public GameObject trailPrefab;
	private GameObject thisTrail;
	private Vector3 startPos;
	private Plane objPlane;
	private bool hasMoved;
	private TrailRenderer tr;
    private GameObject eraseToggle;
    private float dz;

    private bool eraseMode;

	// Use this for initialization
	void Start () {
        eraseToggle = GameObject.Find("Erase");             // looks for Erase toggle
        eraseToggle.GetComponent<Toggle>().isOn = false;    // default erase toggle is off
		objPlane = new Plane(Camera.main.transform.forward * -1, transform.position);
        eraseMode = false;
        dz = 0;
	}
	
	public void changeColor()	{
		thisTrail.GetComponent<TrailRenderer>().material.SetColor("_Color", Color.red);
	}
	
	public void deleteTrail()	{
		Destroy(thisTrail);
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

            thisTrail = Instantiate(trailPrefab, startPos, Quaternion.identity);
            thisTrail.transform.position = v3;
            if (eraseMode)
            {
                thisTrail.GetComponent<TrailRenderer>().material.SetColor("_Color", Color.clear);
            }
            else
            {
                thisTrail.GetComponent<TrailRenderer>().material.SetColor("_Color", Color.blue);
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
                thisTrail.transform.position = v3;
            }
            hasMoved = false;

        // if touch lifted
		}  else if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))	{
				
			if(Vector3.Distance(thisTrail.transform.position, startPos) == 0)
				{Destroy(thisTrail);}
		}
	}
}
//You DID drink your Ovaltine, didn't you?
