using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Paper : MonoBehaviour {
	
	public static Vector2[] textureOffsets = {
		new Vector2(0.5f, 0.5f),	// red
		new Vector2(0f, 0f),		// green
		new Vector2(0.5f, 0f),		// blue
		new Vector2(0f, 0.5f),	
	};
	
	public List<GameObject> _answers0;
	public List<GameObject> _answers1;
	public List<GameObject> _answers2;
	
	private void EnableOneRandom(List<GameObject> l)
	{
		int r = RandomHelper.next(0, l.Count-1);
		for(int i = 0; i < l.Count; ++i)
		{
			l[i].SetActive(i == r);
			l[i].renderer.material.mainTextureOffset = textureOffsets[i];
		}
	}
	
	// Use this for initialization
	void Start () {
		EnableOneRandom(_answers0);
		EnableOneRandom(_answers1);
		EnableOneRandom(_answers2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
