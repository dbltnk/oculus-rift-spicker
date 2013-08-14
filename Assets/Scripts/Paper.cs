using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Paper : MonoBehaviour {
	
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
	
	public IEnumerable<GameObject> EnumAllAnswers()
	{
		foreach (var a in _answers0) yield return a;
		foreach (var a in _answers1) yield return a;
		foreach (var a in _answers2) yield return a;
		foreach (var a in _answers3) yield return a;
		foreach (var a in _answers4) yield return a;
		foreach (var a in _answers5) yield return a;
	}
	
	
	private void EnableOne(List<GameObject> l, int index)
	{
		// set
		for(int i = 0; i < l.Count; ++i)
		{
			l[i].SetActive(i == index);
		}
	}
	
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
		
		EnableOne(l, answer);
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
			if (this.tag == "Important") {
				EnableOne(_answers0, Answers.PopFromAnswers(Answers._answers0));
				EnableOne(_answers1, Answers.PopFromAnswers(Answers._answers1));
				EnableOne(_answers2, Answers.PopFromAnswers(Answers._answers2));
				EnableOne(_answers3, Answers.PopFromAnswers(Answers._answers3));
				EnableOne(_answers4, Answers.PopFromAnswers(Answers._answers4));
				EnableOne(_answers5, Answers.PopFromAnswers(Answers._answers5));
			}
			else {
				EnableOneRandom(_answers0, Answers.correctAnswers[0]);
				EnableOneRandom(_answers1, Answers.correctAnswers[1]);
				EnableOneRandom(_answers2, Answers.correctAnswers[2]);
				EnableOneRandom(_answers3, Answers.correctAnswers[3]);
				EnableOneRandom(_answers4, Answers.correctAnswers[4]);
				EnableOneRandom(_answers5, Answers.correctAnswers[5]);
			}
		}
		
		
	}
	
	// can return null
	public GameObject FindNearestCross(Vector3 pos, float distLimit)
	{
		GameObject minO = null;
		float minD = float.MaxValue;
		
		foreach (GameObject cross in EnumAllAnswers())
		{
			float d = Vector3.Distance(cross.transform.position, pos);
			if ((minO == null || d < minD) && d < distLimit)
			{
				minO = cross;
				minD = d;
			}
		}
		
		return minO;
	}
	
	public void Toggle(GameObject cross)
	{
		cross.SetActive(!cross.activeSelf);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
