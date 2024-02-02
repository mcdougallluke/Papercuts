using System.Collections.Generic;
using UnityEngine;

public class GuardPatrol : MonoBehaviour
{
	public Transform guardTransform;
	public float patrolSpeed = 2f;

	public List<GameObject> patrolPoints = new List<GameObject>();

	private int currentPatrolIndex = 0;

	void Update()
	{
		Patrol();
	}

	void Patrol()
	{
		if (patrolPoints.Count == 0)
		{
			Debug.LogWarning("No patrol points found.");
			return;
		}

		// Move towards the current patrol point
		Vector3 targetPosition = patrolPoints[currentPatrolIndex].transform.position;
		guardTransform.position = Vector3.MoveTowards(guardTransform.position, targetPosition, patrolSpeed * Time.deltaTime);

		// Check if the guard has reached the current patrol point
		if (Vector3.Distance(guardTransform.position, targetPosition) < 0.1f)
		{
			// Move to the next patrol point
			currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
		}
	}
}