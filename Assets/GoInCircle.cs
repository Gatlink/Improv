using UnityEngine;

[RequireComponent(typeof(Mobile))]
public class GoInCircle : MonoBehaviour
{
	public float angularSpeed;

	private Mobile mob;

	public void Start()
	{
		mob = GetComponent<Mobile>();
		mob.StartMoving();
	}

	public void Update()
	{
		transform.RotateAround(transform.position, transform.up, angularSpeed * Time.deltaTime);
	}
}