using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletProjectile : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag != "CatPlayer" &&
			other.gameObject.tag != "CatPlayer2" &&
			other.gameObject.tag != "CleanerPlayer" &&
			other.gameObject.tag != "CleanerPlayer2")
		{
			Debug.Log(other.gameObject.tag);
			Destroy(gameObject);
		} 
	}
} 