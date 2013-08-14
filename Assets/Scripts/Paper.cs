using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
	
	public List<int> _myAnswers = new List<int>();
	
	public IEnumerable<GameObject> EnumAllAnswers()
	{
		foreach (var a in _answers0) yield return a;
		foreach (var a in _answers1) yield return a;
		foreach (var a in _answers2) yield return a;
		foreach (var a in _answers3) yield return a;
		foreach (var a in _answers4) yield return a;
		foreach (var a in _answers5) yield return a;
	}
	
	public List<GameObject> GetRow(int row)
	{
		switch(row)
		{
			case 0: return _answers0;
			case 1: return _answers1;
			case 2: return _answers2;
			case 3: return _answers3;
			case 4: return _answers4;
			case 5: return _answers5;
			default: return null;
		}
	}
	
	public IEnumerable<GameObject> EnumRow(int row)
	{
		switch(row)
		{
			case 0: foreach (var a in _answers0) yield return a; break;
			case 1: foreach (var a in _answers1) yield return a; break;
			case 2: foreach (var a in _answers2) yield return a; break;
			case 3: foreach (var a in _answers3) yield return a; break;
			case 4: foreach (var a in _answers4) yield return a; break;
			case 5: foreach (var a in _answers5) yield return a; break;
			default: break;
		}
	}
	
	private void EnableOne(List<GameObject> l, int index)
	{
		// set
		for(int i = 0; i < l.Count; ++i)
		{
			l[i].SetActive(i == index);
		}
	}
	
	private int PickRandom(int correctAnswer)
	{
		int answer = correctAnswer;
		
		// wrong?
		if (RandomHelper.next() > percentageCorrect)
		{
			// pick wrong random
			while (answer == correctAnswer)
			{
				answer = RandomHelper.next(0, 3);
			}
		}
		
		return answer;
	}
	
	private void EnableOneRandom(List<GameObject> l, int correctAnswer)
	{
		EnableOne(l, PickRandom(correctAnswer));
	}
	
	// object, row, index
	public void VisitAnswers(System.Action<GameObject, int, int> visitor)
	{
		for (int row = 0; row < 6; ++row) 
		{
			var r = GetRow(row);
			for (int i = 0; i < r.Count; ++i) 
			{
				visitor(r[i], row, i);
			}
		}		
	}
	
	public int CountWrongAnswers()
	{
		int sum = 0;
		
		VisitAnswers((o, row, i) => {
			bool own = o.activeSelf;
			bool correct = Answers.correctAnswers[row] == i;
			if (own != correct) ++sum;
		});
		
		return sum;
	}
	
	public int CountCorrectAnswers()
	{
		int sum = 0;
		
		VisitAnswers((o, row, i) => {
			bool own = o.activeSelf;
			bool correct = Answers.correctAnswers[row] == i;
			if (own == correct) ++sum;
		});
		
		return sum;
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
			
		// collect my answers		
		if (this.tag == "Important") 
		{
			_myAnswers.Add(Answers.PopFromAnswers(Answers._answers0));
			_myAnswers.Add(Answers.PopFromAnswers(Answers._answers1));
			_myAnswers.Add(Answers.PopFromAnswers(Answers._answers2));
			_myAnswers.Add(Answers.PopFromAnswers(Answers._answers3));
			_myAnswers.Add(Answers.PopFromAnswers(Answers._answers4));
			_myAnswers.Add(Answers.PopFromAnswers(Answers._answers5));
		} else {
			_myAnswers.Add(PickRandom(Answers.correctAnswers[0]));
			_myAnswers.Add(PickRandom(Answers.correctAnswers[1]));
			_myAnswers.Add(PickRandom(Answers.correctAnswers[2]));
			_myAnswers.Add(PickRandom(Answers.correctAnswers[3]));
			_myAnswers.Add(PickRandom(Answers.correctAnswers[4]));
			_myAnswers.Add(PickRandom(Answers.correctAnswers[5]));
		}
		
		if (_isPrefilled)
		{
			StartCoroutine(coReveal());
		}
	}
	
	public IEnumerator coReveal()
	{
		List<int> rows = new List<int>();
		for (int i = 0; i < 3; ++i)
		{
			if (RandomHelper.next(0,1) == 0) {
				rows.Add(i*2 + 0);
				rows.Add(i*2 + 1);
			} else {
				rows.Add(i*2 + 1);
				rows.Add(i*2 + 0);
			}
		}
		
		foreach(var i in rows)
		{
			EnableOne(GetRow(i), _myAnswers[i]);
			yield return new WaitForSeconds(RandomHelper.next(20f, 30f));
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
