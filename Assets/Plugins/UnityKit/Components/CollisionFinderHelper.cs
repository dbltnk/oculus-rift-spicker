using UnityEngine;
using System.Collections;

public class CollisionFinderHelper : MonoBehaviour {
	void OnCollisionEnter(Collision collision) {
		Debug.Log("OnCollisionEnter " + collision, gameObject);
	}
	
	void OnCollisionExit(Collision collision) {
		Debug.Log("OnCollisionExit " + collision, gameObject);
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log("OnTriggerEnter " + other, gameObject);
	}
	
	void OnTriggerExit(Collider other) {
		Debug.Log("OnTriggerExit " + other, gameObject);
	}
}
