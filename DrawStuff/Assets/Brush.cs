using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour {
	public GameObject trailPrefab;
	private GameObject thisTrail;
	private Vector3 startPos;
	private Plane objPlane;
	private bool hasMoved;
	private TrailRenderer tr;
	
	// Use this for initialization
	void Start () {
		objPlane = new Plane(Camera.main.transform.forward * -1, transform.position);
		
	}
	
	public void changeColor()	{
		thisTrail.GetComponent<TrailRenderer>().material.SetColor("_Color", Color.green);
	}
	
	public void deleteTrail()	{
		Destroy(thisTrail);
	}
	
	// Update is called once per frame
	void Update () {
		if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
             if(objPlane.Raycast(mRay, out rayDistance))
				{startPos = mRay.GetPoint(rayDistance);}
			thisTrail = (GameObject)Instantiate(trailPrefab, startPos, Quaternion.identity);
			thisTrail.GetComponent<TrailRenderer>().material.SetColor("_Color", Color.blue);
			hasMoved=false;
        } else if(((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0)))	{
				
			Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			float rayDistance;
			if(objPlane.Raycast(mRay, out rayDistance))
			{
				thisTrail.transform.position = mRay.GetPoint(rayDistance);
			}
			hasMoved=false;
		}  else if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))	{
				
			if(Vector3.Distance(thisTrail.transform.position, startPos) == 0)
				{Destroy(thisTrail);}
		}
	}
}
//You DID drink your Ovaltine, didn't you?
