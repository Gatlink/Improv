using UnityEngine;

public class Mobile : MonoBehaviour
{
	public float speed;
	public float timeToFullSpeed;
	public Color color;

	private float desiredSpeed;
	private float currentSpeed;
	private float lastMovementChange;

	public void Start()
	{
		var renderer = GetComponent<Renderer>();
		if (renderer != null)
			renderer.material.color = color;
	}

	public void LateUpdate()
	{
		var step = timeToFullSpeed != 0 ? desiredSpeed * (Time.time - lastMovementChange) / timeToFullSpeed : desiredSpeed;
		currentSpeed = Mathf.Min(desiredSpeed, currentSpeed + step * Time.deltaTime);
		var velocity = transform.forward * currentSpeed;
		transform.position += velocity * Time.deltaTime;
	}

	public void StartMoving()
	{
		desiredSpeed = speed;
		lastMovementChange = Time.time;
	}

	public void StopMoving()
	{
		desiredSpeed = 0f;
		lastMovementChange = Time.time;
	}
}
