using UnityEngine;
using System.Collections;

public class RedCameraOverlay : MonoBehaviour {
	public static float alpha;
	
	private GameObject _plane;
	
	// Use this for initialization
	void Start () {
		_plane = gameObject.transform.Find("Plane").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		var c = _plane.renderer.material.color;
		c.a = alpha;
		_plane.renderer.material.color = c;
	}
}
