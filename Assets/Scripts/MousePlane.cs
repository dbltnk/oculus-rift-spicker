using UnityEngine;
using System.Collections;

public class MousePlane : MonoBehaviour {
	
	public float _width;
	public float _height;
	
	public Vector3 MousePos {
		get {
			return transform.TransformPoint(_mousePos);
		}
	}
	
	// from 0 to 1
	public Vector3 _mousePos;
	
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.magenta;
		
		var w2 = _width / 2f;
		var h2 = _height / 2f;
		
		var p0 = transform.TransformPoint(new Vector3(-w2,0f,h2));
		var p1 = transform.TransformPoint(new Vector3(w2,0f,h2));
		var p2 = transform.TransformPoint(new Vector3(w2,0f,-h2));
		var p3 = transform.TransformPoint(new Vector3(-w2,0f,-h2));
		
		Gizmos.DrawLine(p0, p1);
		Gizmos.DrawLine(p1, p2);
		Gizmos.DrawLine(p2, p3);
		Gizmos.DrawLine(p3, p0);
		
		Gizmos.DrawSphere(MousePos, 0.01f);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// screen space to local pos
		float lx = 1f - Mathf.Clamp01(Input.mousePosition.x / (float)Screen.width);
		float ly = 1f - Mathf.Clamp01(Input.mousePosition.y / (float)Screen.height);
		
		_mousePos.x = (0.5f - lx) * _width;
		_mousePos.y = 0f;
		_mousePos.z = (0.5f - ly) * _height;
	}
}
