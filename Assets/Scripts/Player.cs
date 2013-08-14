using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {
	
	public Camera _viewCamera;
	public Student _student;
	public Paper _paper;
	public Hand _hand;
	public MousePlane _mousePlane;
	public GameObject _markerCross;
	public float snapDistance;
	
	public List<GameObject> _hearts;
	
	private Teacher _teacher;
	
	public float suspicionFactor;
	public float suspicionSpeed;
	public float suspicionDecreaseSpeed;
	
	public int Hearts {
		get {
			return _hearts.Where((o) => o.activeSelf).Count();
		}
	}
	
	public void LoseHeart()
	{
		if (Hearts == 0) return;
		_hearts[Hearts - 1].SetActive(false);
	}
	
	void Awake() {
		_teacher = GameObject.FindObjectOfType(typeof(Teacher)) as Teacher;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawRay(_viewCamera.ScreenPointToRay(new Vector3(_viewCamera.pixelWidth / 2f, _viewCamera.pixelHeight / 2f, 0f)));
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
		
		// answer quality
		Debug.Log(string.Format("correct={0} wrong={1}", _paper.CountCorrectAnswers(), _paper.CountWrongAnswers()));
		
		// calculate visibility stuff
		var r = _viewCamera.ScreenPointToRay(new Vector3(_viewCamera.pixelWidth / 2f, _viewCamera.pixelHeight / 2f, 0f));
		var isSave = Physics.Raycast(r, float.MaxValue, 1 << LayerMask.NameToLayer("SaveSpot"));
		
		var f = _teacher.CalculateInViewFactor(transform.position);
		
		if (isSave) f = 0f;
		suspicionFactor += f * suspicionSpeed * Time.deltaTime;
		
		if (f == 0f) suspicionFactor -= suspicionDecreaseSpeed * Time.deltaTime;
		
		suspicionFactor = Mathf.Clamp01(suspicionFactor);
		//Debug.Log(string.Format("f={0} save={1}", f, isSave));
		
		if (suspicionFactor >= 1f) {
			LoseHeart();
			suspicionFactor = 0f;
		}
		
		RedCameraOverlay.redAlpha = MathHelper.mapIntoRange(suspicionFactor, 0f, 1f, 0f, 0.75f);
		RedCameraOverlay.saveAlpha = isSave ? 0.5f : 0f;
		
		if (Hearts == 0) {
			RedCameraOverlay.redAlpha = 1f;
			RedCameraOverlay.saveAlpha = 0f;			
		}
	}
}
