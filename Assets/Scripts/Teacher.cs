using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class Teacher : MonoBehaviour {

	public List<AudioClip> footsteps;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		PlayRandomSFX();
	}
	
	void PlayRandomSFX(){
		if (audio.isPlaying) return; 
		else {
			audio.clip = RandomHelper.pickRandom(footsteps);
			audio.Play();
			}
		}
}