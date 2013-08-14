using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public Camera _viewCamera;
	public Student _student;
	public Paper _paper;
	public Hand _hand;
	public MousePlane _mousePlane;
	public GameObject _markerCross;
	public float snapDistance;
	
	private Teacher _teacher;
	
	public float suspicionFactor;
	public float suspicionSpeed;
	public float suspicionDecreaseSpeed;
	
	
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
		
		RedCameraOverlay.redAlpha = MathHelper.mapIntoRange(suspicionFactor, 0f, 1f, 0f, 0.75f);
		RedCameraOverlay.saveAlpha = isSave ? 0.5f : 0f;
	}
}
