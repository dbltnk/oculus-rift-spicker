using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public Paper _paper;
	public Hand _hand;
	public MousePlane _mousePlane;
	public GameObject _markerCross;
	public float snapDistance;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		_hand.transform.position = _mousePlane.MousePos;
		
		var nearestCross = _paper.FindNearestCross(_hand.transform.position, snapDistance);
		_markerCross.SetActive(nearestCross != null);
		if (nearestCross != null)
		{
			_markerCross.transform.position = nearestCross.transform.position;
			_markerCross.transform.rotation = nearestCross.transform.rotation;
			_markerCross.transform.localScale = nearestCross.transform.localScale;
			
			// clicked? -> toggle
			if (Input.GetMouseButtonDown(0))
			{
				_paper.Toggle(nearestCross);
			}
		}
	}
}
