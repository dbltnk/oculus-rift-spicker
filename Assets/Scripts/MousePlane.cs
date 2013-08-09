using UnityEngine;
using System.Collections;

public class MousePlane : MonoBehaviour {
	
	public float _width;
	public float _height;
	
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
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
