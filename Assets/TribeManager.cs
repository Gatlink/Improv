using UnityEngine;
using System.Collections;

public class TribeManager : MonoBehaviour
{
	public GameObject tribeMemberPrefab;

	public void AddTribeMember(int tribeIndex)
	{
		var instance = Instantiate(tribeMemberPrefab);
		var behaviour = instance.GetComponent<TribeBehaviour>();
		behaviour.Tribe = GameData.tribes[tribeIndex];
	}
}
