using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletProjectile : MonoBehaviour
{
	public event Action onProjecitleStun;
	
	void OnTriggerEnter(Collider other)
	{
		IStunnable stunnable = other.gameObject.GetComponent<IStunnable>();

		if (other.gameObject.tag != "CatPlayer" &&
			other.gameObject.tag != "CatPlayer2" &&
			other.gameObject.tag != "CleanerPlayer" &&
			other.gameObject.tag != "CleanerPlayer2")
		{
			Destroy(gameObject);
		}
		// If hit object has IStunnable interface
		else if (stunnable != null)
		{
			stunnable.Stun();
			onProjecitleStun?.Invoke();

			Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

			rb.AddForce(UnityEngine.Random.Range(-1000f, 1000f), 1500f, UnityEngine.Random.Range(-1000f, 1000f));
			Destroy(gameObject);
		}
	}
} 