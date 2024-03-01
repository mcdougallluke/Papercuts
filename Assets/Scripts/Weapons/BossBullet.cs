using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Bulet
{
	protected Rigidbody2D rigidBody2D;

	public override BulletDataSO BulletData
	{
		get => base.BulletData;
		set
		{
			base.BulletData = value;
			rigidBody2D = GetComponent<Rigidbody2D>();
			rigidBody2D.drag = BulletData.Friction;
		}

	}

	private void FixedUpdate()
	{
		if (rigidBody2D != null && BulletData != null)
		{
			rigidBody2D.MovePosition(transform.position + BulletData.BulletSpeed * transform.right * Time.fixedDeltaTime);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Health health;
		if (health = collision.GetComponent<Health>())
		{
			health.GetHit(2, gameObject);
		}


		if (collision.gameObject.layer == LayerMask.NameToLayer("SolidObjects"))
		{
			HitObstacle();
		}
		else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			HitEnemy();
		}
		Destroy(gameObject);
	}

	private void HitEnemy()
	{
		Debug.Log("Hitting Player");
	}

	private void HitObstacle()
	{
		Debug.Log("Hitting Obstacle");
	}
}
