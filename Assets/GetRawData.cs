using UnityEngine;
using System.Collections;

[System.Serializable]
public class RawData
{
	public int[] alpha;
	public int[] beta;
	public int[] gamma;
	public int[] delta;
	public int[] epsilon;

	public override string ToString()
	{
		var text = stringifyTab("alpha", alpha);
		text += stringifyTab("beta", beta);
		text += stringifyTab("gamma", gamma);
		text += stringifyTab("delta", delta);
		text += stringifyTab("epsilon", epsilon);

		return text;
	}

	private string stringifyTab(string name, int[] tab)
	{
		var text = "\t" + name + ": [" + tab[0];
		for (var i = 1; i < tab.Length; ++i)
			text += "," + tab[i];

		text += "]\n";
		return text;
	}
}

[ExecuteInEditMode]
public class GetRawData : MonoBehaviour
{
	public const string url = "http://198.211.121.98:8000/api/data?from={0}&count={1}";

	public static GetRawData Instance;

	public int from = 0;
	public int count = 10;

	private RawData data;

	public static event System.Action DataLoadedEvent;

	public void Awake()
	{
		if (Instance != null)
		{
			DestroyImmediate(this);
			return;
		}

		Instance = this;
	}

	public IEnumerator Start ()
	{
		from = Random.Range(0, 100);

		var www = new WWW(url);

		yield return www;

		data = JsonUtility.FromJson<RawData>(www.text);

		var names = System.Enum.GetNames(typeof(TribeName));
		GameData.tribes = new Tribe[] {	
			new Tribe(names[0], data.alpha, Color.red),
			new Tribe(names[1], data.beta, Color.green),
			new Tribe(names[2], data.gamma, Color.blue),
			new Tribe(names[3], data.delta, Color.magenta),
			new Tribe(names[4], data.epsilon, Color.yellow)
		};

		if (DataLoadedEvent != null)
			DataLoadedEvent();
	}
}
