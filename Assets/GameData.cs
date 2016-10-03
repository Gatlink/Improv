using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TribeName
{
	Alpha,
	Beta,
	Gamma,
	Delta,
	Epsilon
}

[System.Serializable]
public class Tribe
{
	public string name;
	public int[] behaviour;
	public float naturalDeviation;
	public float maxWaitingTime;
	public Color color;

	public Tribe(string name, int[] behaviour, Color color)
	{
		this.name = name;
		this.behaviour = behaviour;
		naturalDeviation = Random.value * 0.1f;
		maxWaitingTime = Random.value * 10f;
		this.color = color;
	}

}

public static class GameData
{
	public static Tribe[] tribes;
}
