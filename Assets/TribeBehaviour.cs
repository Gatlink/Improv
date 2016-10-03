using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider), typeof(Mobile))]
public class TribeBehaviour : MonoBehaviour
{
	private readonly HashSet<TribeBehaviour> inRange = new HashSet<TribeBehaviour>();

	private Mobile mobile;
	private float timeWaiting;
	private float nextWaitingTime;

	private Tribe tribe;
	public Tribe Tribe
	{
		set
		{
			tribe = value;
			nextWaitingTime = Time.time + tribe.maxWaitingTime * 2f;
			SetRandomForward();

			mobile = GetComponent<Mobile>();
			mobile.color = value.color;
			mobile.StartMoving();
		}
	}

	public void Update()
	{
		if (tribe == null)
			return;

		if (Time.time >= nextWaitingTime)
		{
			mobile.StopMoving();
			timeWaiting = Random.Range(1f, tribe.maxWaitingTime);
			nextWaitingTime = Time.time + timeWaiting + tribe.maxWaitingTime * 5f;

			return;
		}

		if (timeWaiting > 0)
		{
			timeWaiting -= Time.deltaTime;

			if (timeWaiting <= 0)
			{
				SetRandomForward();
				mobile.StartMoving();
			}

			return;
		}

		var rand = Random.Range(0, 2) - 1; // -1, 0, 1 
		var direction = rand * transform.right;

		transform.forward += direction * tribe.naturalDeviation;

		inRange.RemoveWhere(other => other == null);
	}

	public void OnTriggerEnter(Collider col)
	{
		var other = col.gameObject.GetComponent<TribeBehaviour>();
		if (other != null)
			inRange.Add(other);
	}

	public void OnTriggerExit(Collider col)
	{
		var other = col.gameObject.GetComponent<TribeBehaviour>();
		if (other != null)
			inRange.Remove(other);
	}

	private void SetRandomForward()
	{
		transform.forward = new Vector3(Random.value, 0f, Random.value);
		transform.forward.Normalize();
	}
}
