using UnityEngine;

public class Environment : MonoBehaviour
{
	public static Environment Instance;

	public Transform[] walls;

	public void Awake()
	{
		if (Instance != null)
		{
			DestroyImmediate(this);
			return;
		}

		Instance = this;
	}
}
