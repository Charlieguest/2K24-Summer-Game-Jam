using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletProjectileDud : MonoBehaviour
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
			Debug.Log(other.gameObject.tag);
		}
	}
}
