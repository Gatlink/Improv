using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider), typeof(Mobile))]
public class TribeBehaviour : MonoBehaviour
{
	private const float wallDirectionChangeFactor = 500f;

	private readonly HashSet<TribeBehaviour> inRange = new HashSet<TribeBehaviour>();

	private Mobile mobile;

	private Tribe tribe;
	public Tribe Tribe
	{
		set
		{
			tribe = value;
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

		var direction = transform.forward;
		// Wall Distance
		for (var i = 0; i < Environment.Instance.walls.Length; ++i)
		{
			var wall = Environment.Instance.walls[i];
			var toWall = wall.position - transform.position;
			var distance = wall.position.x > 0 ? toWall.x : toWall.y;
			var factor = Mathf.Max(0f, 1f / distance - 1f);
			toWall.y = 0f;
			direction += wallDirectionChangeFactor * factor * -toWall.normalized;
		}

		// TODO: Add a factor to wall direction change (the lower the distance, more it should influence the direction)
		transform.forward = direction;
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
		transform.forward = new Vector3(Random.value * 2f - 1f, 0f, Random.value * 2f - 1f);
		transform.forward.Normalize();
	}
}
