using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Transform guardTransform;

	[SerializeField] float health, maxHealth = 100f;
	[SerializeField] HealthBar healthBar;

	public enum State
	{
		STATIC,
		PATROLLING,
		RANDOM
	}	

	public float patrolSpeed = 2f;
	public List<GameObject> patrolPoints = new List<GameObject>();
	private int currentPatrolIndex = 0;
	public State currentState;

	private void Awake()
	{
		healthBar = GetComponentInChildren<HealthBar>();
		Debug.Log(healthBar.ToString());
	}
	public void TakeDamage(float damage)
	{
		health -= damage;

		healthBar.UpdateHealthBar(health, maxHealth);
	}

	void Update()
	{
		/*
		if(currentState == State.PATROLLING)
		{
			Patrol();
		}
		*/
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