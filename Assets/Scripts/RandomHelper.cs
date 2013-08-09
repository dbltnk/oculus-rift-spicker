using System;
using System.Collections;
using System.Collections.Generic;

public static class RandomHelper {
	private static Random r = new Random();
	
	// min/max is included
	public static int next(int min, int max)
	{
		return min + r.Next() % (Math.Max(max, min) - min + 1);
	}
	
	// [0-1[
	public static float next()
	{
		return (float)r.NextDouble();	
	}
	
	/// <summary>
	/// Pick randomly from weighted elements
	/// </summary>
	/// <returns>
	/// A element
	/// </returns>
	/// <param name='itemWeightMap'>
	/// Item weight map, key = items, value = weight (must be int > 0)
	/// </param>
	/// <typeparam name='T'>
	/// Item type
	/// </typeparam>
	public static T pickWeightedRandom<T>(Dictionary<T,int> itemWeightMap)
	{
		int weightSum = 0;
		
		foreach(var pair in itemWeightMap)
		{
			weightSum += pair.Value;
		}
		
		int random = RandomHelper.next(0, weightSum - 1);
		
		foreach(var pair in itemWeightMap)
		{
			int weight = pair.Value;
			if (random < weight)
			{
				return pair.Key;
			}
			
			random -= weight;
		}
		
		throw new Exception("this will not happen");
	}
	
	public static T pickRandom<T>(List<T> items)
	{
		if (items.Count == 0)throw new Exception("you cant pick from an empty list");
		
		int index = next(0, items.Count - 1);
		return items[index];
	}
	
	public static T pickWeightedRandom<T>(IEnumerable<T> items, Func<T, int> weightFun)
	{
		var weightMap = new Dictionary<T, int>();
		
		foreach(var item in items)
		{
			weightMap[item] = weightFun(item);
		}
		
		return pickWeightedRandom(weightMap);
	}	
}
