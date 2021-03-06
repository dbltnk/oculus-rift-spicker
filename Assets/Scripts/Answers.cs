﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Answers : MonoBehaviour {
	
	public static int numberOfQuestions = 6;
	public static List<int> correctAnswers;
	
	public static float percentageCorrect = 0.75f;
	
	public static List<int> _answers0 = new List<int>();
	public static List<int> _answers1 = new List<int>();
	public static List<int> _answers2 = new List<int>();
	public static List<int> _answers3 = new List<int>();
	public static List<int> _answers4 = new List<int>();
	public static List<int> _answers5 = new List<int>();
	
	public static int PopFromAnswers(List<int> l)
	{
		int r = l[0];
		l.RemoveAt(0);
		return r;
	}
	
	private void GenerateAnswers(List<int> l, int correctAnswer) {
		
		
		int numberOfVisiblePapers = CountImportantPapers();
		int numberOfCorrectAnswersNeeded = (int) (Mathf.Floor(numberOfVisiblePapers * percentageCorrect));
		
		for (int i = 1; i <= numberOfCorrectAnswersNeeded; i++)
        {
            l.Add(correctAnswer);
        }	
		
		int numberOfWrongAnswersNeeded = numberOfVisiblePapers - numberOfCorrectAnswersNeeded;	
			
		for (int i = 1; i <= numberOfWrongAnswersNeeded; i++)
        {
			if (correctAnswer == 0) {
				if (RandomHelper.next(-1,1) >= 0 ) {
					l.Add(1);
				}
				else {
					l.Add(2);
				}	
			}
			else if (correctAnswer == 1) {
				if (RandomHelper.next(-1,1) >= 0 ) {
					l.Add(0);
				}
				else {
					l.Add(2);
				}	
			}
			else {
				if (RandomHelper.next(-1,1) >= 0 ) {
					l.Add(0);
				}
				else {
					l.Add(1);
				}	
			}
        }
		
		RandomHelper.shuffle(l);
		
	}
	
	private void GenerateCorrectAnswers() {
		correctAnswers = new List<int>();
		for (int i = 1; i <= numberOfQuestions; i++)
	    {
	        correctAnswers.Add(RandomHelper.next(0,2));
	    }		
	}
	
	
	private int CountImportantPapers() {
		GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Important");
		return gos.Length;
	}	
	
	void Awake () {
			GenerateCorrectAnswers();
		
			GenerateAnswers(_answers0, correctAnswers[0]);
			GenerateAnswers(_answers1, correctAnswers[1]);
			GenerateAnswers(_answers2, correctAnswers[2]);
			GenerateAnswers(_answers3, correctAnswers[3]);
			GenerateAnswers(_answers4, correctAnswers[4]);
			GenerateAnswers(_answers5, correctAnswers[5]);
	}
	
	void Update () {
	
	}
}
