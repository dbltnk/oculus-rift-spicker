using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Paper : MonoBehaviour {
	
	public static int[] correctAnswers = {
		0, 1, 1, 2, 0, 2,
	};
	
	public static float percentageCorrect = 0.75f;
	
	public static Vector2[] textureOffsets = {
		new Vector2(0.5f, 0.5f),	// red
		new Vector2(0f, 0f),		// green
		new Vector2(0.5f, 0f),		// blue
		new Vector2(0f, 0.5f),	
	};
	
	public List<GameObject> _answers0;
	public List<GameObject> _answers1;
	public List<GameObject> _answers2;
	public List<GameObject> _answers3;
	public List<GameObject> _answers4;
	public List<GameObject> _answers5;
	
	public bool _isPrefilled;
	
	private void EnableOneRandom(List<GameObject> l, int correctAnswer)
	{
		int answer = correctAnswer;
		
		// wrong?
		if (RandomHelper.next() > percentageCorrect)
		{
			// pick wrong random
			while (answer == correctAnswer)
			{
				answer = RandomHelper.next(0, l.Count-1);
			}
		}
		
		// set
		for(int i = 0; i < l.Count; ++i)
		{
			l[i].SetActive(i == answer);
		}
	}
	
	private void PrepareAnswers(List<GameObject> l)
	{
		// set
		for(int i = 0; i < l.Count; ++i)
		{
			l[i].SetActive(false);
			l[i].renderer.material.mainTextureOffset = textureOffsets[i];
		}
	}
	
	// Use this for initialization
	void Start () {
		PrepareAnswers(_answers0);
		PrepareAnswers(_answers1);
		PrepareAnswers(_answers2);
		PrepareAnswers(_answers3);
		PrepareAnswers(_answers4);
		PrepareAnswers(_answers5);
			
		if (_isPrefilled)
		{
			EnableOneRandom(_answers0, correctAnswers[0]);
			EnableOneRandom(_answers1, correctAnswers[1]);
			EnableOneRandom(_answers2, correctAnswers[2]);
			EnableOneRandom(_answers3, correctAnswers[3]);
			EnableOneRandom(_answers4, correctAnswers[4]);
			EnableOneRandom(_answers5, correctAnswers[5]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
