using UnityEngine;
using System.Collections;

public class RedCameraOverlay : MonoBehaviour {
	public static float redAlpha;
	public static float saveAlpha;
	
	private GameObject _redPlane;
	private GameObject _savePlane;
	
	// Use this for initialization
	void Awake () {
		_redPlane = gameObject.transform.Find("RedPlane").gameObject;
		_savePlane = gameObject.transform.Find("SavePlane").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		var c = _redPlane.renderer.material.color;
		c.a = redAlpha;
		_redPlane.renderer.material.color = c;

		c = _savePlane.renderer.material.color;
		c.a = saveAlpha;
		_savePlane.renderer.material.color = c;
	}
}
