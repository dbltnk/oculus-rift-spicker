using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public Student _student;
	public Paper _paper;
	public Hand _hand;
	public MousePlane _mousePlane;
	public GameObject _markerCross;
	public float snapDistance;
	
	public float suspicionFactor;
	public float suspicionSpeed;
	public float suspicionDecreaseSpeed;
	
	
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
		
		Teacher teacher = GameObject.FindObjectOfType(typeof(Teacher)) as Teacher;
		var f = teacher.CalculateInViewFactor(transform.position);
		
		suspicionFactor += f * suspicionSpeed * Time.deltaTime;
		if (f == 0f) suspicionFactor -= suspicionDecreaseSpeed * Time.deltaTime;
		
		suspicionFactor = Mathf.Clamp01(suspicionFactor);
		Debug.Log(f);
		
		RedCameraOverlay.alpha = MathHelper.mapIntoRange(suspicionFactor, 0f, 1f, 0f, 0.75f);
	}
}
