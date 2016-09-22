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

public class GetRawData : MonoBehaviour
{
	public const string url = "http://198.211.121.98:8000/api/data?from={0}&count={1}";

	public int from = 0;
	public int count = 10;

	private RawData data;

	public IEnumerator Start ()
	{
		var www = new WWW(url);

		yield return www;

		data = JsonUtility.FromJson<RawData>(www.text);
	}
}
