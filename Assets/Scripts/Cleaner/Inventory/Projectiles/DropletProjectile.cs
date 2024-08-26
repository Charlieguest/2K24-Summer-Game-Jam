using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletProjectile : MonoBehaviour
{
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
		else if (stunnable != null)
		{
			Debug.Log("We're onto a winner");
		}
	}
} 